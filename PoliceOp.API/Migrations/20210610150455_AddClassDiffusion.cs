using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceOp.API.Migrations
{
    public partial class AddClassDiffusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diffusions",
                columns: table => new
                {
                    DiffusionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(nullable: false),
                    Sujet = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: false),
                    Cible = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diffusions", x => x.DiffusionId);
                });

            migrationBuilder.CreateTable(
                name: "PieceJointes",
                columns: table => new
                {
                    PieceJointeId = table.Column<Guid>(nullable: false),
                    NomFichier = table.Column<string>(nullable: false),
                    ExtensionFichier = table.Column<string>(nullable: false),
                    Fichier = table.Column<byte[]>(nullable: false),
                    DiffusionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceJointes", x => x.PieceJointeId);
                    table.ForeignKey(
                        name: "FK_PieceJointes_Diffusions_DiffusionId",
                        column: x => x.DiffusionId,
                        principalTable: "Diffusions",
                        principalColumn: "DiffusionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PieceJointes_DiffusionId",
                table: "PieceJointes",
                column: "DiffusionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PieceJointes");

            migrationBuilder.DropTable(
                name: "Diffusions");
        }
    }
}
