using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiv2.Migrations
{
    /// <inheritdoc />
    public partial class Added_Contact_email_Associations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Associations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Associations");
        }
    }
}
