using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuestionsAndAnswers.DataLayer.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    RolesID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_SoftwareNameId",
                table: "QuestionAnswer",
                column: "SoftwareNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_AppealId",
                table: "Application",
                column: "AppealId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ExecutorId",
                table: "Application",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_SoftwareNameId",
                table: "Application",
                column: "SoftwareNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_StatusId",
                table: "Application",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Appeal_AppealId",
                table: "Application",
                column: "AppealId",
                principalTable: "Appeal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Executor_ExecutorId",
                table: "Application",
                column: "ExecutorId",
                principalTable: "Executor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_softwareNames_SoftwareNameId",
                table: "Application",
                column: "SoftwareNameId",
                principalTable: "softwareNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Status_StatusId",
                table: "Application",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswer_softwareNames_SoftwareNameId",
                table: "QuestionAnswer",
                column: "SoftwareNameId",
                principalTable: "softwareNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
