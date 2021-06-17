using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PoliceOp.API.Migrations
{
    public partial class MigrationZero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BioData",
                columns: table => new
                {
                    UID = table.Column<Guid>(nullable: false),
                    DonneesFaciales = table.Column<byte[]>(nullable: true),
                    DonneesDigitales = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioData", x => x.UID);
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

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    PersonneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(nullable: false),
                    NPI = table.Column<string>(nullable: false),
                    IFU = table.Column<string>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    Sexe = table.Column<string>(nullable: false),
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
                    BiometrieUID = table.Column<Guid>(nullable: true),
                    PereId = table.Column<int>(nullable: false),
                    MereId = table.Column<int>(nullable: false),
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
                        name: "FK_Personnes_BioData_BiometrieUID",
                        column: x => x.BiometrieUID,
                        principalTable: "BioData",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvisRecherches",
                columns: table => new
                {
                    UID = table.Column<Guid>(nullable: false),
                    DateEmission = table.Column<DateTime>(nullable: false),
                    StatutRecherche = table.Column<string>(nullable: false),
                    Informations = table.Column<string>(nullable: true),
                    PersonneRechercheePersonneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvisRecherches", x => x.UID);
                    table.ForeignKey(
                        name: "FK_AvisRecherches_Personnes_PersonneRechercheePersonneId",
                        column: x => x.PersonneRechercheePersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    ResidenceId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Rue = table.Column<string>(nullable: true),
                    NumeroParcelle = table.Column<string>(nullable: true),
                    NumeroChambre = table.Column<string>(nullable: true),
                    CoordonneesGeo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PersonneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residences_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvisRecherches_PersonneRechercheePersonneId",
                table: "AvisRecherches",
                column: "PersonneRechercheePersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_BiometrieUID",
                table: "Personnes",
                column: "BiometrieUID");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_PersonneId",
                table: "Residences",
                column: "PersonneId",
                unique: true,
                filter: "[PersonneId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvisRecherches");

            migrationBuilder.DropTable(
                name: "Requetes");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "BioData");
        }
    }
}
