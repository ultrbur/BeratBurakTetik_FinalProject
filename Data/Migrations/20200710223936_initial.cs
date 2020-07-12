using Microsoft.EntityFrameworkCore.Migrations;

namespace stajbul.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobPosting",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: false),
                    sector = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobPosting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "talents",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_talents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "talentsofJobs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    talentID = table.Column<int>(nullable: false),
                    jobPostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_talentsofJobs", x => x.id);
                    table.ForeignKey(
                        name: "FK_talentsofJobs_jobPosting_jobPostingID",
                        column: x => x.jobPostingID,
                        principalTable: "jobPosting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_talentsofJobs_talents_talentID",
                        column: x => x.talentID,
                        principalTable: "talents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_talentsofJobs_jobPostingID",
                table: "talentsofJobs",
                column: "jobPostingID");

            migrationBuilder.CreateIndex(
                name: "IX_talentsofJobs_talentID",
                table: "talentsofJobs",
                column: "talentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "talentsofJobs");

            migrationBuilder.DropTable(
                name: "jobPosting");

            migrationBuilder.DropTable(
                name: "talents");
        }
    }
}
