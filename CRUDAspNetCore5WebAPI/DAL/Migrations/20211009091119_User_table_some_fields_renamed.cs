using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class User_table_some_fields_renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPassword",
                schema: "dbo",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "dbo",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                schema: "dbo",
                table: "Users",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "dbo",
                table: "Users",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "dbo",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "dbo",
                table: "Users",
                newName: "UserEmail");
        }
    }
}
