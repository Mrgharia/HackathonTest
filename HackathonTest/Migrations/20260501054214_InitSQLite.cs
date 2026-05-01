using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonTest.Migrations
{
    /// <inheritdoc />
    public partial class InitSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DropdownMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropdownMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NominationRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pipeline = table.Column<string>(type: "TEXT", nullable: true),
                    Shipper = table.Column<string>(type: "TEXT", nullable: true),
                    NomStatus = table.Column<string>(type: "TEXT", nullable: true),
                    GisbStatus = table.Column<string>(type: "TEXT", nullable: true),
                    SchedQty = table.Column<decimal>(type: "TEXT", nullable: true),
                    TransType = table.Column<string>(type: "TEXT", nullable: true),
                    QuantityTypeIndicator = table.Column<string>(type: "TEXT", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cycle = table.Column<string>(type: "TEXT", nullable: true),
                    ContractNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RollNom = table.Column<string>(type: "TEXT", nullable: true),
                    RecLocation = table.Column<string>(type: "TEXT", nullable: true),
                    RecLocProp = table.Column<string>(type: "TEXT", nullable: true),
                    RecLocId = table.Column<string>(type: "TEXT", nullable: true),
                    UpName = table.Column<string>(type: "TEXT", nullable: true),
                    UpIdProp = table.Column<string>(type: "TEXT", nullable: true),
                    UpId = table.Column<string>(type: "TEXT", nullable: true),
                    UpContractNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RecQty = table.Column<decimal>(type: "TEXT", nullable: true),
                    RecRank = table.Column<string>(type: "TEXT", nullable: true),
                    DelLoc = table.Column<string>(type: "TEXT", nullable: true),
                    DelLocId = table.Column<string>(type: "TEXT", nullable: true),
                    DelLocProp = table.Column<string>(type: "TEXT", nullable: true),
                    DownName = table.Column<string>(type: "TEXT", nullable: true),
                    DownIdProp = table.Column<string>(type: "TEXT", nullable: true),
                    DownId = table.Column<string>(type: "TEXT", nullable: true),
                    DownContractNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DelQuantity = table.Column<decimal>(type: "TEXT", nullable: true),
                    DelRank = table.Column<string>(type: "TEXT", nullable: true),
                    DealType = table.Column<string>(type: "TEXT", nullable: true),
                    CapacityBlockId = table.Column<string>(type: "TEXT", nullable: true),
                    PkgId = table.Column<string>(type: "TEXT", nullable: true),
                    FuelPercent = table.Column<decimal>(type: "TEXT", nullable: true),
                    ShipperSpecificId = table.Column<string>(type: "TEXT", nullable: true),
                    NomTrackingId = table.Column<string>(type: "TEXT", nullable: true),
                    NomSubmittedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NomQuickResponseDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AgentDuns = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PipelineMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipperMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperMasters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DropdownMasters");

            migrationBuilder.DropTable(
                name: "NominationRecords");

            migrationBuilder.DropTable(
                name: "PipelineMasters");

            migrationBuilder.DropTable(
                name: "ShipperMasters");
        }
    }
}
