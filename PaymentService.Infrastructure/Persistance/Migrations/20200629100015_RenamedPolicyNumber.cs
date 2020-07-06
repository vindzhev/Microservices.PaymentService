using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Infrastructure.Persistance.Migrations
{
    public partial class RenamedPolicyNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyNumber",
                table: "PolicyAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "PolicyId",
                table: "PolicyAccounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "PolicyAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "PolicyNumber",
                table: "PolicyAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
