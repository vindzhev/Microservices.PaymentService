using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Infrastructure.Persistance.Migrations
{
    public partial class RemovedPolicyAccountNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyAccountNumber",
                table: "PolicyAccounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PolicyAccountNumber",
                table: "PolicyAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
