using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class initiall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodeText = table.Column<string>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersCode_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersCode_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodeErrors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodeId = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeErrors_UsersCode_CodeId",
                        column: x => x.CodeId,
                        principalTable: "UsersCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodeId = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeResults_UsersCode_CodeId",
                        column: x => x.CodeId,
                        principalTable: "UsersCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeErrors_CodeId",
                table: "CodeErrors",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeResults_CodeId",
                table: "CodeResults",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCode_ExerciseId",
                table: "UsersCode",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCode_UserId",
                table: "UsersCode",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeErrors");

            migrationBuilder.DropTable(
                name: "CodeResults");

            migrationBuilder.DropTable(
                name: "UsersCode");
        }
    }
}
