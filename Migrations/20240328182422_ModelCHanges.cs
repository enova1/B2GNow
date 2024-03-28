using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_CORE.Migrations
{
    /// <inheritdoc />
    public partial class ModelCHanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "EmployeePhones",
                newName: "PhoneType");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "EmployeeAddresses",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "EmployeeAddresses",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "EmployeePhones",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "EmployeeAddresses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "EmployeeAddresses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "EmployeePhones");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "EmployeeAddresses");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "EmployeeAddresses");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PhoneType",
                table: "EmployeePhones",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "EmployeeAddresses",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "EmployeeAddresses",
                newName: "Country");
        }
    }
}
