using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordSetup.Migrations
{
    public partial class running : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubRegionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRegionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubRegionRecords_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRegionRecordTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubRegionRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRegionRecordTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubRegionRecordTables_SubRegionRecords_SubRegionRecordId",
                        column: x => x.SubRegionRecordId,
                        principalTable: "SubRegionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableSchemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubRegionRecordTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableSchemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableSchemas_SubRegionRecordTables_SubRegionRecordTableId",
                        column: x => x.SubRegionRecordTableId,
                        principalTable: "SubRegionRecordTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubRegionRecords_RegionId",
                table: "SubRegionRecords",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRegionRecordTables_SubRegionRecordId",
                table: "SubRegionRecordTables",
                column: "SubRegionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TableSchemas_SubRegionRecordTableId",
                table: "TableSchemas",
                column: "SubRegionRecordTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableSchemas");

            migrationBuilder.DropTable(
                name: "SubRegionRecordTables");

            migrationBuilder.DropTable(
                name: "SubRegionRecords");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
