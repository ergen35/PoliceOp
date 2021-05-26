using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceOp.API.Migrations
{
    public partial class ClassesRedone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvisRecherches",
                columns: table => new
                {
                    UID = table.Column<Guid>(nullable: false),
                    DateEmission = table.Column<DateTime>(nullable: false),
                    StatutRecherche = table.Column<string>(nullable: false),
                    Informations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvisRecherches", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    PersonneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(nullable: false),
                    NPI = table.Column<string>(nullable: false),
                    IFU = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    Sexe = table.Column<int>(nullable: false),
                    LieuNaissance = table.Column<string>(nullable: false),
                    Nationalite = table.Column<string>(nullable: false),
                    Profession = table.Column<string>(nullable: false),
                    SituationMatrimoniale = table.Column<string>(nullable: false),
                    SignesParticuliers = table.Column<string>(nullable: false),
                    CouleurYeux = table.Column<string>(nullable: false),
                    CouleurCheveux = table.Column<string>(nullable: false),
                    Teint = table.Column<string>(nullable: false),
                    Taille = table.Column<double>(nullable: false),
                    Photographie = table.Column<byte[]>(nullable: false),
                    BiometrieID = table.Column<int>(nullable: false),
                    ResidenceId = table.Column<int>(nullable: false),
                    Personne_pere = table.Column<int>(nullable: true),
                    Personne_mere = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Matricule = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Corps = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_Personnes_Personnes_Personne_mere",
                        column: x => x.Personne_mere,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personnes_Personnes_Personne_pere",
                        column: x => x.Personne_pere,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requetes",
                columns: table => new
                {
                    UID = table.Column<Guid>(nullable: false),
                    TermeRequete = table.Column<string>(nullable: false),
                    DateRequete = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requetes", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_Personne_mere",
                table: "Personnes",
                column: "Personne_mere");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_Personne_pere",
                table: "Personnes",
                column: "Personne_pere");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvisRecherches");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Requetes");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
