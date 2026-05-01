using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackathonTest.Models
{
    public class NominationViewModel
    {
        public string? Pipeline { get; set; }
        public string? Shipper { get; set; }
        public bool ShowAddRow { get; set; } = false;

        public List<NominationRecord> Records { get; set; } = new();
        public NominationRecord NewRecord { get; set; } = new();

        public int TotalRecords { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public bool ShowMineOnly { get; set; } = true;

        public List<string> Pipelines { get; set; } = new();
        public List<string> Shippers { get; set; } = new();

        // Hardcoded only where you want fixed options
        public List<string> Cycles { get; set; } = new();

        // DB-backed dropdown option lists
        public List<string> NomStatuses { get; set; } = new();
        public List<string> GisbStatus { get; set; } = new();
        public List<string> TransactionTypes { get; set; } = new();

        public List<string> SchedQty { get; set; } = new();

        public List<string> QuantityTypeIndicators { get; set; } = new();
        public List<string> RollNomOptions { get; set; } = new();
        public List<string> ContractNumbers { get; set; } = new();
        public List<string> RecLocations { get; set; } = new();
        public List<string> RecLocProps { get; set; } = new();
        public List<string> RecLocIds { get; set; } = new();
        public List<string> UpNames { get; set; } = new();
        public List<string> UpIdProps { get; set; } = new();
        public List<string> UpIds { get; set; } = new();
        public List<string> UpContractNumbers { get; set; } = new();
        public List<string> RecRank { get; set; } = new();
        public List<string> DelLocs { get; set; } = new();
        public List<string> DelLocIds { get; set; } = new();
        public List<string> DownNames { get; set; } = new();
        public List<string> DelLocProp { get; set; } = new();
        public List<string> DownIdProps { get; set; } = new();
        public List<string> DownIds { get; set; } = new();
        public List<string> DownContractNumbers { get; set; } = new();
        public List<string> DelRank { get; set; } = new();
        public List<string> DealTypes { get; set; } = new();
        public List<string> recQty { get; set; } = new();
        public List<string> DelQuantity { get; set; } = new();
        public List<string> fuelpercent { get; set; } = new();

        public List<string> CapacityBlockIds { get; set; } = new();
        public List<string> PkgIds { get; set; } = new();
        public List<string> ShipperSpecificIds { get; set; } = new();
        public List<string> NomTrackingIds { get; set; } = new();
        public List<string> ReferenceNumbers { get; set; } = new();
        public List<string> AgentDunsList { get; set; } = new();
    }

    public class NominationRecord
    {
        public int Id { get; set; }

        public string? Pipeline { get; set; }

        public string? Shipper { get; set; }


        [Display(Name = "Nom Status")]
        public string? NomStatus { get; set; }

        [Display(Name = "GISB Status")]
        public string? GisbStatus { get; set; }

        [Display(Name = "Sched Qty")]
        public decimal? SchedQty { get; set; }

        [Display(Name = "Trans Type")]
        public string? TransType { get; set; }

        [Display(Name = "Quantity Type Indicator")]
        public string? QuantityTypeIndicator { get; set; }

        [Display(Name = "Started Date")]
        public DateTime? StartedDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Cycle")]
        public string? Cycle { get; set; }

        [Display(Name = "K#")]
        public string? ContractNumber { get; set; }

        [Display(Name = "Roll Nom")]
        public string? RollNom { get; set; }

        [Display(Name = "Rec Location")]
        public string? RecLocation { get; set; }

        [Display(Name = "Rec Loc Prop")]
        public string? RecLocProp { get; set; }

        [Display(Name = "Rec Loc ID")]
        public string? RecLocId { get; set; }

        [Display(Name = "Up Name")]
        public string? UpName { get; set; }

        [Display(Name = "Up ID Prop")]
        public string? UpIdProp { get; set; }

        [Display(Name = "Up ID")]
        public string? UpId { get; set; }

        [Display(Name = "Up K#")]
        public string? UpContractNumber { get; set; }

        [Display(Name = "Rec Qty")]
        public decimal? RecQty { get; set; }

        [Display(Name = "Rec Rank")]
        public string? RecRank { get; set; }

        [Display(Name = "Del Loc")]
        public string? DelLoc { get; set; }

        [Display(Name = "Del Loc ID")]
        public string? DelLocId { get; set; }

        [Display(Name = "Del Loc Prop")]
        public string? DelLocProp { get; set; }



        [Display(Name = "Down Name")]
        public string? DownName { get; set; }

        [Display(Name = "Down ID Prop")]
        public string? DownIdProp { get; set; }

        [Display(Name = "Down ID")]
        public string? DownId { get; set; }

        [Display(Name = "Down K#")]
        public string? DownContractNumber { get; set; }

        [Display(Name = "Del Quantity")]
        public decimal? DelQuantity { get; set; }

        [Display(Name = "Del Rank")]
        public string? DelRank { get; set; }

        [Display(Name = "Deal Type")]
        public string? DealType { get; set; }

        [Display(Name = "Capacity Block Id")]
        public string? CapacityBlockId { get; set; }

        [Display(Name = "Pkg ID")]
        public string? PkgId { get; set; }

        [Display(Name = "Fuel %")]
        public decimal? FuelPercent { get; set; }

      //  [Display(Name = "Created By")]
        //public string? CreatedBy { get; set; }

        [Display(Name = "Shipper Specific ID")]
        public string? ShipperSpecificId { get; set; }

        [Display(Name = "Nom Tracking ID")]
        public string? NomTrackingId { get; set; }

        [Display(Name = "Date/Time Nom Submitted")]
        public DateTime? NomSubmittedDateTime { get; set; }

        [Display(Name = "Date/Time Nom Quick Response Received")]
        public DateTime? NomQuickResponseDateTime { get; set; }

        [Display(Name = "Reference Number")]
        public string? ReferenceNumber { get; set; }

        [Display(Name = "Agent Duns")]
        public string? AgentDuns { get; set; }
    }
}