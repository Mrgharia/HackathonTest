using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonTest.Migrations
{
    /// <inheritdoc />
    public partial class AddPipelineShipperToNominationRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NominationRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pipeline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipper = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GisbStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchedQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityTypeIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollNom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecLocProp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecLocId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpIdProp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecRank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelLoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelLocId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownIdProp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DelRank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelLocProp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacityBlockId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PkgId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperSpecificId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomTrackingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomSubmittedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NomQuickResponseDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentDuns = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NominationRecords");
        }
    }
}
