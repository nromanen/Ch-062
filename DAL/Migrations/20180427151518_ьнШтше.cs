using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class ьнШтше : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Exercises");

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
                name: "IX_Exercises_CourseId",
                table: "Exercises",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeErrorId",
                table: "CodeHistories",
                column: "CodeErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeHistories_CodeResultId",
                table: "CodeHistories",
                column: "CodeResultId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Courses_CourseId",
                table: "Exercises",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeHistories_CodeErrors_CodeErrorId",
                table: "CodeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeHistories_CodeResults_CodeResultId",
                table: "CodeHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Courses_CourseId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CourseId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeErrorId",
                table: "CodeHistories");

            migrationBuilder.DropIndex(
                name: "IX_CodeHistories_CodeResultId",
                table: "CodeHistories");

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

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Exercises",
                nullable: true);
        }
    }
}
