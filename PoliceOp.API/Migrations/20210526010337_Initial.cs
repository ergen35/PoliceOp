using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceOp.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvisRecherches",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(nullable: false),
                    DateEmission = table.Column<DateTime>(nullable: false),
                    StatutRecherche = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvisRecherches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BioData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(nullable: false),
                    DonneesFaciales = table.Column<string>(nullable: true),
                    DonneesDigitales = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Coordonnees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rue = table.Column<string>(nullable: true),
                    NumeroParcelle = table.Column<string>(nullable: true),
                    NumeroChambre = table.Column<string>(nullable: true),
                    CoordonneesGeo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordonnees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(nullable: false),
                    NPI = table.Column<string>(nullable: false),
                    IFU = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    fk_pere = table.Column<int>(nullable: true),
                    fk_mere = table.Column<int>(nullable: true),
                    Sexe = table.Column<int>(nullable: false),
                    LieuNaissance = table.Column<string>(nullable: false),
                    Nationalité = table.Column<string>(nullable: false),
                    Profession = table.Column<string>(nullable: false),
                    SituationMatrimoniale = table.Column<string>(nullable: false),
                    SignesParticuliers = table.Column<string>(nullable: false),
                    CouleurYeux = table.Column<string>(nullable: false),
                    CouleurCheveux = table.Column<string>(nullable: false),
                    Teint = table.Column<string>(nullable: false),
                    Taille = table.Column<double>(nullable: false),
                    Photographie = table.Column<byte[]>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Matricule = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Corps = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Personnes_Personnes_fk_mere",
                        column: x => x.fk_mere,
                        principalTable: "Personnes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnes_Personnes_fk_pere",
                        column: x => x.fk_pere,
                        principalTable: "Personnes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requetes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(nullable: false),
                    TermeRequete = table.Column<string>(nullable: false),
                    DateRequete = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requetes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_fk_mere",
                table: "Personnes",
                column: "fk_mere");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_fk_pere",
                table: "Personnes",
                column: "fk_pere");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvisRecherches");

            migrationBuilder.DropTable(
                name: "BioData");

            migrationBuilder.DropTable(
                name: "Coordonnees");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Requetes");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
