using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management_System_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanDocumentFileDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "LoanDocuments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "LoanDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "LoanDocuments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "LoanDocuments");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "LoanDocuments");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "LoanDocuments");
        }
    }
}
