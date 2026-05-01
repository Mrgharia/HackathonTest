using HackathonTest.Data;
using HackathonTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace HackathonTest.Controllers
{
    public class NominationController : Controller
    {
        private readonly AppDbContext _context;

        public NominationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(
            string? pipeline = null,
            string? shipper = null,
            bool showMineOnly = true,
            int page = 1,
            int pageSize = 10,
            bool addRow = false)
        {
            var currentUser = HttpContext.Session.GetString("UserName") ?? "Admin";

            var query = _context.NominationRecords.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pipeline))
                query = query.Where(x => x.Pipeline == pipeline);

            if (!string.IsNullOrWhiteSpace(shipper))
                query = query.Where(x => x.Shipper == shipper);

            //if (showMineOnly)
            //    query = query.Where(x => x.CreatedBy == currentUser);

            var totalRecords = query.Count();

            var records = query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new NominationViewModel
            {
                Pipeline = pipeline,
                Shipper = shipper,
                ShowMineOnly = showMineOnly,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Records = records,
                ShowAddRow = addRow
            };

            PopulateMasterData(vm);

            return View(vm);
        }

        /// <summary>
        /// This endpoint is OPTIONAL.
        /// Use it only if you want JS to fetch default values from controller.
        /// Add Row should NOT save anything to DB.
        /// </summary>
        [HttpGet]
        /* public IActionResult GetDefaultRowData(string? pipeline, string? shipper)
         {
             var today = DateTime.Today;
             var currentUser = HttpContext.Session.GetString("UserName") ?? "Admin";

             var defaultRow = new
             {
                 Pipeline = pipeline ?? "Columbia Gas Transmission",
                 Shipper = shipper ?? "Enercross LLC (078711334)",
                 NomStatus = "Unsubmitted",
                 GisbStatus = "",
                 SchedQty = "",
                 TransType = "01",
                 QuantityTypeIndicator = "Receipt",
                 StartedDate = today.ToString("yyyy-MM-dd"),
                 EndDate = today.AddDays(1).ToString("yyyy-MM-dd"),
                 CreatedDate = today.ToString("yyyy-MM-dd"),
                 Cycle = "Timely",
                 ContractNumber = "",
                 RollNom = "Yes",
                 RecLocation = "",
                 RecLocProp = "",
                 RecLocId = "",
                 UpName = "",
                 UpIdProp = "",
                 UpId = "",
                 UpContractNumber = "",
                 RecQty = 0,
                 RecRank = "",
                 DelLoc = "",
                 DelLocProp = "",
                 DelLocId = "",
                 DownName = "",
                 DownIdProp = "",
                 DownId = "",
                 DownContractNumber = "",
                 DelQuantity = 0,
                 DelRank = "",
                 DealType = "",
                 CapacityBlockId = "",
                 PkgId = "",
                 FuelPercent = 0,
                 CreatedBy = currentUser,
                 ShipperSpecificId = "",
                 NomTrackingId = "",
                 NomSubmittedDateTime = "",
                 NomQuickResponseDateTime = "",
                 ReferenceNumber = "",
                 AgentDuns = ""
             };

             return Json(defaultRow);
         }
        */


        [HttpGet]
        public IActionResult AddRow()
        {
            Console.WriteLine("AddRow controller hit");
            var vm = new NominationViewModel();

            PopulateMasterData(vm);

            vm.NewRecord = new NominationRecord
            {
                NomStatus = "Unsubmitted",
                QuantityTypeIndicator = "Receipt",
                Cycle = "Timely",
                RollNom = "Yes",
                CreatedDate = DateTime.Today,
              //  CreatedBy = HttpContext.Session.GetString("UserName") ?? "Admin"
            };

            return PartialView("_AddNominationRow", vm);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRecords(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest();
            }

            var records = _context.NominationRecords
                .Where(x => ids.Contains(x.Id))
                .ToList();

            _context.NominationRecords.RemoveRange(records);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Validate(NominationViewModel vm)
        {
            TempData["Message"] = "Nomination validated successfully.";
            return RedirectToAction("Index", new
            {
                pipeline = vm.Pipeline,
                shipper = vm.Shipper,
                showMineOnly = vm.ShowMineOnly,
                page = vm.CurrentPage,
                pageSize = vm.PageSize
            });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult SaveRecord(NominationViewModel vm)
        {
            var currentUser = HttpContext.Session.GetString("UserName") ?? "Admin";

            if (vm.NewRecord == null)
            {
                TempData["Message"] = "No record data found.";
                return RedirectToAction("Index", new
                {
                    pipeline = vm.Pipeline,
                    shipper = vm.Shipper,
                    showMineOnly = vm.ShowMineOnly,
                    page = vm.CurrentPage,
                    pageSize = vm.PageSize
                });
            }

            vm.NewRecord.Pipeline = vm.Pipeline;
            vm.NewRecord.Shipper = vm.Shipper;
         //   vm.NewRecord.CreatedBy = currentUser;

            if (!vm.NewRecord.CreatedDate.HasValue)
                vm.NewRecord.CreatedDate = DateTime.Today;

            _context.NominationRecords.Add(vm.NewRecord);
            _context.SaveChanges();

            TempData["Message"] = "Record saved successfully.";

            return RedirectToAction("Index", new
            {
                pipeline = vm.Pipeline,
                shipper = vm.Shipper,
                showMineOnly = vm.ShowMineOnly,
                page = 1,
                pageSize = vm.PageSize
            });
        }


        [HttpPost]
        public IActionResult SendRecords([FromBody] List<NominationSendDto> records)
        {
            if (records == null || records.Count == 0)
            {
                return BadRequest("Invalid data received");
            }

            string json = JsonSerializer.Serialize(records, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine("Received Nomination JSON:");
            Console.WriteLine(json);

            return Ok(new
            {
                message = $"{records.Count} records received successfully",
                data = records
            });
        }
        private void PopulateMasterData(NominationViewModel vm)
        {
            vm.Pipelines = _context.PipelineMasters
                .Select(x => x.Name)
                .OrderBy(x => x)
                .ToList();

            vm.Shippers = _context.ShipperMasters
                .Select(x => x.Name)
                .OrderBy(x => x)
                .ToList();

            vm.NomStatuses = _context.DropdownMasters
                .Where(x => x.Type == "NomStatus")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.GisbStatus = _context.NominationRecords
           .Where(x => x.GisbStatus != null && x.GisbStatus != "")
    .Select(x => x.GisbStatus)
    .Distinct()
    .OrderBy(x => x)
    .ToList();

            vm.recQty = _context.NominationRecords
    .Where(x => x.RecQty.HasValue)
    .Select(x => x.RecQty.Value.ToString())
    .Distinct()
    .OrderBy(x => x)
    .ToList();

            vm.DelQuantity = _context.NominationRecords
                .Where(x => x.DelQuantity.HasValue)
                .Select(x => x.DelQuantity.Value.ToString())
                .Distinct()
                .OrderBy(x => x)
                .ToList();



            vm.SchedQty = _context.NominationRecords
                .Where(x => x.SchedQty != null)
                .Select(x => x.SchedQty.ToString())
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            vm.TransactionTypes = _context.DropdownMasters
                .Where(x => x.Type == "TransType")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.QuantityTypeIndicators = _context.DropdownMasters
                .Where(x => x.Type == "QuantityTypeIndicator")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.Cycles = _context.DropdownMasters
                .Where(x => x.Type == "Cycle")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.RollNomOptions = _context.DropdownMasters
                .Where(x => x.Type == "RollNom")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.ContractNumbers = _context.DropdownMasters
                .Where(x => x.Type == "ContractNumber")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.RecLocations = _context.DropdownMasters
                .Where(x => x.Type == "RecLocation")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.RecLocProps = _context.DropdownMasters
                .Where(x => x.Type == "RecLocProp")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.RecLocIds = _context.DropdownMasters
                .Where(x => x.Type == "RecLocId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.RecRank = _context.DropdownMasters
                .Where(x => x.Type == "RecRank")
                .Select(x => x.Value)
                .Distinct()
                .ToList();


            vm.DelRank = _context.DropdownMasters
              .Where(x => x.Type == "DelRank")
              .Select(x => x.Value)
              .Distinct()
              .ToList();

            vm.RecLocIds = _context.DropdownMasters
                            .Where(x => x.Type == "RecLocId")
                            .Select(x => x.Value)
                            .Distinct()
                            .ToList();


            vm.UpNames = _context.DropdownMasters
                .Where(x => x.Type == "UpName")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.UpIdProps = _context.DropdownMasters
                .Where(x => x.Type == "UpIdProp")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.UpIds = _context.DropdownMasters
                .Where(x => x.Type == "UpId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.UpContractNumbers = _context.DropdownMasters
                .Where(x => x.Type == "UpContractNumber")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DelLocs = _context.DropdownMasters
                .Where(x => x.Type == "DelLoc")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DelLocProp = _context.DropdownMasters
                .Where(x => x.Type == "DelLocProp")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DelLocIds = _context.DropdownMasters
                .Where(x => x.Type == "DelLocId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DownNames = _context.DropdownMasters
                .Where(x => x.Type == "DownName")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DownIdProps = _context.DropdownMasters
                .Where(x => x.Type == "DownIdProp")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DownIds = _context.DropdownMasters
                .Where(x => x.Type == "DownId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DownContractNumbers = _context.DropdownMasters
                .Where(x => x.Type == "DownContractNumber")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.DealTypes = _context.DropdownMasters
                .Where(x => x.Type == "DealType")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.CapacityBlockIds = _context.DropdownMasters
                .Where(x => x.Type == "CapacityBlockId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.PkgIds = _context.DropdownMasters
                .Where(x => x.Type == "PkgId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.ReferenceNumbers = _context.NominationRecords
                .Where(x => x.ReferenceNumber != null && x.ReferenceNumber != "")
                .Select(x => x.ReferenceNumber)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            vm.AgentDunsList = _context.NominationRecords
                .Select(x => x.AgentDuns)
                .Where(x => x != null && x != "")
                .Distinct()
                .OrderBy(x => x)
                 .ToList();



            vm.fuelpercent = _context.NominationRecords
.Where(x => x.FuelPercent != null)
.Select(x => x.FuelPercent.Value.ToString())
.Distinct()
.OrderBy(x => x)
.ToList();
            vm.ShipperSpecificIds = _context.DropdownMasters
                .Where(x => x.Type == "ShipperSpecificId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            vm.NomTrackingIds = _context.DropdownMasters
                .Where(x => x.Type == "NomTrackingId")
                .Select(x => x.Value)
                .Distinct()
                .ToList();
        }
    }
}