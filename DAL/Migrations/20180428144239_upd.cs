using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeHistories_CodeErrors_CodeErrorId",
                table: "CodeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeHistories_CodeResults_CodeResultId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeResults_CodeId",
                table: "CodeResults");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeErrorId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeResultId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeErrors_CodeId",
                table: "CodeErrors");

            migrationBuilder.DropColumn(
                name: "CodeErrorId",
                table: "CodeHistories");

            migrationBuilder.DropColumn(
                name: "CodeResultId",
                table: "CodeHistories");

            migrationBuilder.DropColumn(
                name: "CodeResultsId",
                table: "CodeHistories");

            migrationBuilder.DropColumn(
                name: "ErrorsId",
                table: "CodeHistories");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentText = table.Column<string>(nullable: true),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeResults_CodeId",
                table: "CodeResults",
                column: "CodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeId",
                table: "CodeHistories",
                column: "CodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeErrors_CodeId",
                table: "CodeErrors",
                column: "CodeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_CodeResults_CodeId",
                table: "CodeResults");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeErrors_CodeId",
                table: "CodeErrors");

            migrationBuilder.AddColumn<int>(
                name: "CodeErrorId",
                table: "CodeHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodeResultId",
                table: "CodeHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeResultsId",
                table: "CodeHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorsId",
                table: "CodeHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeResults_CodeId",
                table: "CodeResults",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeErrorId",
                table: "CodeHistories",
                column: "CodeErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeId",
                table: "CodeHistories",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeResultId",
                table: "CodeHistories",
                column: "CodeResultId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeErrors_CodeId",
                table: "CodeErrors",
                column: "CodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeHistories_CodeErrors_CodeErrorId",
                table: "CodeHistories",
                column: "CodeErrorId",
                principalTable: "CodeErrors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeHistories_CodeResults_CodeResultId",
                table: "CodeHistories",
                column: "CodeResultId",
                principalTable: "CodeResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
