using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Infrastructure.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PolicyAccountNumber = table.Column<Guid>(nullable: false),
                    PolicyNumber = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyAccounts_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PolicyAccountId = table.Column<Guid>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingEntry_PolicyAccounts_PolicyAccountId",
                        column: x => x.PolicyAccountId,
                        principalTable: "PolicyAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntry_PolicyAccountId",
                table: "AccountingEntry",
                column: "PolicyAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAccounts_OwnerId",
                table: "PolicyAccounts",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingEntry");

            migrationBuilder.DropTable(
                name: "PolicyAccounts");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
