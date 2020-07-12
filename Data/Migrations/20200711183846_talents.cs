using Microsoft.EntityFrameworkCore.Migrations;

namespace stajbul.Data.Migrations
{
    public partial class talents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stajbulUserid",
                table: "talents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "jobApplications",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stajbulUserID = table.Column<int>(nullable: false),
                    jobPostingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobApplications", x => x.id);
                    table.ForeignKey(
                        name: "FK_jobApplications_jobPosting_jobPostingID",
                        column: x => x.jobPostingID,
                        principalTable: "jobPosting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jobApplications_stajbulUser_stajbulUserID",
                        column: x => x.stajbulUserID,
                        principalTable: "stajbulUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_talents_stajbulUserid",
                table: "talents",
                column: "stajbulUserid");

            migrationBuilder.CreateIndex(
                name: "IX_jobApplications_jobPostingID",
                table: "jobApplications",
                column: "jobPostingID");

            migrationBuilder.CreateIndex(
                name: "IX_jobApplications_stajbulUserID",
                table: "jobApplications",
                column: "stajbulUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_talents_stajbulUser_stajbulUserid",
                table: "talents",
                column: "stajbulUserid",
                principalTable: "stajbulUser",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_talents_stajbulUser_stajbulUserid",
                table: "talents");

            migrationBuilder.DropTable(
                name: "jobApplications");

            migrationBuilder.DropIndex(
                name: "IX_talents_stajbulUserid",
                table: "talents");

            migrationBuilder.DropColumn(
                name: "stajbulUserid",
                table: "talents");
        }
    }
}
