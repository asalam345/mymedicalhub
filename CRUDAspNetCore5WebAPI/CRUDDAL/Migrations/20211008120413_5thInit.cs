using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_DAL.Migrations
{
    public partial class _5thInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEnrollment",
                table: "RoleEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientTypeEnrollment",
                table: "PatientTypeEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientType",
                table: "PatientType");

            migrationBuilder.RenameTable(
                name: "RoleEnrollment",
                newName: "RoleEnrollments");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "PatientTypeEnrollment",
                newName: "PatientTypeEnrollments");

            migrationBuilder.RenameTable(
                name: "PatientType",
                newName: "PatientTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEnrollments",
                table: "RoleEnrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientTypeEnrollments",
                table: "PatientTypeEnrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientTypes",
                table: "PatientTypes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEnrollments",
                table: "RoleEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientTypes",
                table: "PatientTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientTypeEnrollments",
                table: "PatientTypeEnrollments");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RoleEnrollments",
                newName: "RoleEnrollment");

            migrationBuilder.RenameTable(
                name: "PatientTypes",
                newName: "PatientType");

            migrationBuilder.RenameTable(
                name: "PatientTypeEnrollments",
                newName: "PatientTypeEnrollment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEnrollment",
                table: "RoleEnrollment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientType",
                table: "PatientType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientTypeEnrollment",
                table: "PatientTypeEnrollment",
                column: "Id");
        }
    }
}
