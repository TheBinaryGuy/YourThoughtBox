using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectX.App.Data.Migrations
{
    public partial class RequesterInfoAndOtherChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequesterInfo");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Thoughts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DeleteRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IP = table.Column<string>(nullable: true),
                    ThoughtId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeleteRequests_Thoughts_ThoughtId",
                        column: x => x.ThoughtId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Viewers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IP = table.Column<string>(nullable: true),
                    ThoughtId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viewers_Thoughts_ThoughtId",
                        column: x => x.ThoughtId,
                        principalTable: "Thoughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeleteRequests_ThoughtId",
                table: "DeleteRequests",
                column: "ThoughtId");

            migrationBuilder.CreateIndex(
                name: "IX_Viewers_ThoughtId",
                table: "Viewers",
                column: "ThoughtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeleteRequests");

            migrationBuilder.DropTable(
                name: "Viewers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Thoughts");

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
    }
}
