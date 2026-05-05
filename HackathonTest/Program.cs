using HackathonTest.Data;
using HackathonTest.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// SQLite connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Auto create/update DB + seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    if (!db.PipelineMasters.Any())
    {
        db.PipelineMasters.AddRange(
            new PipelineMaster { Name = "Columbia Gas Transmission" },
            new PipelineMaster { Name = "Algonquin Gas Transmission" },
            new PipelineMaster { Name = "Adelphia Gateway" }
        );
    }

    if (!db.ShipperMasters.Any())
    {
        db.ShipperMasters.AddRange(
            new ShipperMaster { Name = "Enercross LLC" },
            new ShipperMaster { Name = "Shell Energy" },
            new ShipperMaster { Name = "Reliance Gas" }
        );
    }

    if (!db.DropdownMasters.Any())
    {
        db.DropdownMasters.AddRange(
            new DropDownMaster { Type = "NomStatus", Value = "Unsubmitted" },
            new DropDownMaster { Type = "NomStatus", Value = "Submitted" },
            new DropDownMaster { Type = "NomStatus", Value = "Draft" },

            new DropDownMaster { Type = "TransType", Value = "1" },
            new DropDownMaster { Type = "TransType", Value = "2" },
            new DropDownMaster { Type = "TransType", Value = "2" },

            new DropDownMaster { Type = "QuantityTypeIndicator", Value = "Receipt" },
            new DropDownMaster { Type = "QuantityTypeIndicator", Value = "Delivery" },
            new DropDownMaster { Type = "QuantityTypeIndicator", Value = "Both" },

            new DropDownMaster { Type = "Cycle", Value = "Timely" },
            new DropDownMaster { Type = "Cycle", Value = "Evening" },
            
            new DropDownMaster { Type = "RollNom", Value = "Yes" },
            new DropDownMaster { Type = "RollNom", Value = "No" },

            new DropDownMaster { Type = "ContractNumber", Value = "501005" },
            new DropDownMaster { Type = "ContractNumber", Value = "501006" },
            new DropDownMaster { Type = "ContractNumber", Value = "502003" },

            new DropDownMaster { Type = "RecLocation", Value = "MAHWAH" },
            new DropDownMaster { Type = "RecLocation", Value = "Lincoln" },
            new DropDownMaster { Type = "RecLocation", Value = "Germany" },

            new DropDownMaster { Type = "RecLocProp", Value = "002001" },
            new DropDownMaster { Type = "RecLocProp", Value = "002005" },
            new DropDownMaster { Type = "RecLocProp", Value = "002007" },

            new DropDownMaster { Type = "RecLocId", Value = "002001" },
            new DropDownMaster { Type = "RecLocId", Value = "002003" },
            new DropDownMaster { Type = "RecLocId", Value = "002004" },

            new DropDownMaster { Type = "RecRank", Value = "1" },
            new DropDownMaster { Type = "RecRank", Value = "2" },
            new DropDownMaster { Type = "RecRank", Value = "3" },

            new DropDownMaster { Type = "DelRank", Value = "1" },
            new DropDownMaster { Type = "DelRank", Value = "2" },
            new DropDownMaster { Type = "DelRank", Value = "3" },

            new DropDownMaster { Type = "UpName", Value = "Enercross LLC" },
            new DropDownMaster { Type = "UpName", Value = "Shell Energy" },
            new DropDownMaster { Type = "UpName", Value = "Reliance Gas" },

            new DropDownMaster { Type = "UpIdProp", Value = "8392" },
            new DropDownMaster { Type = "UpIdProp", Value = "8495" },
            new DropDownMaster { Type = "UpIdProp", Value = "8798" },

            new DropDownMaster { Type = "UpId", Value = "067622" },
            new DropDownMaster { Type = "UpId", Value = "114-03-43-22-07-1" },
            new DropDownMaster { Type = "UpId", Value = "06-47-34-55-7" },

            new DropDownMaster { Type = "UpContractNumber", Value = "100158152" },
            new DropDownMaster { Type = "UpContractNumber", Value = "100168252" },
            new DropDownMaster { Type = "UpContractNumber", Value = "1001793482" },

            new DropDownMaster { Type = "DelLoc", Value = "Italy" },
            new DropDownMaster { Type = "DelLoc", Value = "Germany" },
            new DropDownMaster { Type = "DelLoc", Value = "France" },

            new DropDownMaster { Type = "DelLocProp", Value = "00742" },
            new DropDownMaster { Type = "DelLocProp", Value = "00811" },
            new DropDownMaster { Type = "DelLocProp", Value = "00999" },

            new DropDownMaster { Type = "DelLocId", Value = "711-2411705" },
            new DropDownMaster { Type = "DelLocId", Value = "811-5555555" },
            new DropDownMaster { Type = "DelLocId", Value = "999-8888888" },

            new DropDownMaster { Type = "DownName", Value = "EMA Energy" },
            new DropDownMaster { Type = "DownName", Value = "Adani Gas" },
            new DropDownMaster { Type = "DownName", Value = "GAIL India" },

            new DropDownMaster { Type = "DownIdProp", Value = "7385" },
            new DropDownMaster { Type = "DownIdProp", Value = "7488" },
            new DropDownMaster { Type = "DownIdProp", Value = "7599" },

            new DropDownMaster { Type = "DownId", Value = "100135272" },
            new DropDownMaster { Type = "DownId", Value = "200245678" },
            new DropDownMaster { Type = "DownId", Value = "300987654" },

            new DropDownMaster { Type = "DownContractNumber", Value = "1-604-88" },
            new DropDownMaster { Type = "DownContractNumber", Value = "2-704-99" },
            new DropDownMaster { Type = "DownContractNumber", Value = "3-804-77" },

            new DropDownMaster { Type = "DealType", Value = "Base" },
            new DropDownMaster { Type = "DealType", Value = "Interruptible" },
            new DropDownMaster { Type = "DealType", Value = "Firm" },

            new DropDownMaster { Type = "CapacityBlockId", Value = "CB-001" },
            new DropDownMaster { Type = "CapacityBlockId", Value = "CB-002" },
            new DropDownMaster { Type = "CapacityBlockId", Value = "CB-003" },

            new DropDownMaster { Type = "PkgId", Value = "PKG-101" },
            new DropDownMaster { Type = "PkgId", Value = "PKG-102" },
            new DropDownMaster { Type = "PkgId", Value = "PKG-103" },

            new DropDownMaster { Type = "ShipperSpecificId", Value = "SS-001" },
            new DropDownMaster { Type = "ShipperSpecificId", Value = "SS-002" },
            new DropDownMaster { Type = "ShipperSpecificId", Value = "SS-003" },

            new DropDownMaster { Type = "NomTrackingId", Value = "NT-001" },
            new DropDownMaster { Type = "NomTrackingId", Value = "NT-002" },
            new DropDownMaster { Type = "NomTrackingId", Value = "NT-003" });
    }

    db.SaveChanges();
    if (!db.DropdownMasters.Any(x => x.Type == "AgentDuns"))
    {
        db.DropdownMasters.AddRange(
            new DropDownMaster { Type = "AgentDuns", Value = "AGD-001" },
            new DropDownMaster { Type = "AgentDuns", Value = "AGD-002" },
            new DropDownMaster { Type = "AgentDuns", Value = "AGD-003" }
        );

        db.SaveChanges();
    }
}


// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Nomination}/{action=Index}/{id?}");

app.Run();