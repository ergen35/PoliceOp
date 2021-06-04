using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceOp.API.Migrations
{
    public partial class Renew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
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
                    Photographie = table.Column<string>(nullable: false),
                    BiometrieID = table.Column<int>(nullable: false),
                    ResidenceId = table.Column<int>(nullable: false),
                    PereId = table.Column<int>(nullable: false),
                    MereId = table.Column<int>(nullable: false),
                    Matricule = table.Column<string>(nullable: false),
                    Grade = table.Column<string>(nullable: false),
                    Corps = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Service = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.PersonneId);
                });

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
                    Photographie = table.Column<string>(nullable: false),
                    BiometrieID = table.Column<int>(nullable: false),
                    ResidenceId = table.Column<int>(nullable: false),
                    PereId = table.Column<int>(nullable: false),
                    MereId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.PersonneId);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");

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
