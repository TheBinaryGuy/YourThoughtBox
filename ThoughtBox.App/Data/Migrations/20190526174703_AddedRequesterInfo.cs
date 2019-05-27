using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtBox.App.Data.Migrations
{
    public partial class AddedRequesterInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Thoughts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Thoughts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RequesterInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IP = table.Column<string>(nullable: true),
                    ThoughtId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequesterInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequesterInfo_Thoughts_ThoughtId",
                        column: x => x.ThoughtId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequesterInfo_ThoughtId",
                table: "RequesterInfo",
                column: "ThoughtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequesterInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Thoughts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Thoughts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
