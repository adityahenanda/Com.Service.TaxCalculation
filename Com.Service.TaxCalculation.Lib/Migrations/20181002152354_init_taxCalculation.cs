using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Service.TaxCalculation.Lib.Migrations
{
    public partial class init_taxCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxCalculation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<double>(nullable: true),
                    TotalTaxAmount = table.Column<double>(nullable: true),
                    GrandTotal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TaxCode = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: true),
                    Total = table.Column<double>(nullable: true),
                    TaxCalculationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCalculationDetails_TaxCalculation_TaxCalculationId",
                        column: x => x.TaxCalculationId,
                        principalTable: "TaxCalculation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxCalculationDetails_TaxCalculationId",
                table: "TaxCalculationDetails",
                column: "TaxCalculationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxCalculationDetails");

            migrationBuilder.DropTable(
                name: "TaxCalculation");
        }
    }
}
