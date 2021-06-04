using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoliceOp.API.Data;
using PoliceOp.Models;
using Faker;
using System.IO;


namespace PoliceOp.API
{
    public enum Couleur{
        Rouge, Noir, Bleu, Orange, Violet, Vert, Cyan, Gris
    }

    public class DataGenerator
    {
        PasswordGenerator.Password pwd = new PasswordGenerator.Password(true, false, true, false, 8);

        public async Task<Personne> GeneratePersonne()
        {

            Personne P = new Personne()
            {
                UID = Guid.NewGuid().ToString(),
                CouleurCheveux = Faker.Enum.Random<Couleur>().ToString(),
                CouleurYeux = Faker.Enum.Random<Couleur>().ToString(),
                DateNaissance = Faker.Identification.DateOfBirth(),
                IFU = Faker.Identification.UkNhsNumber(),
                LieuNaissance = Address.City(),
                Nationalite = "Beninoise",
                NPI = Identification.UsPassportNumber(),
                Prenom = $"{ Name.Last() + " " + Name.Middle() }",
                Nom = Name.Last(),
                Profession = string.Concat(Faker.Lorem.Words(2)),
                Sexe = Faker.Enum.Random<Sexe>().ToString(),
                SignesParticuliers = string.Concat(Faker.Lorem.Words(4)),
                SituationMatrimoniale = string.Concat(Faker.Lorem.Words(2)),
                Taille = Convert.ToDouble(Faker.RandomNumber.Next(45, 300)),
                Teint = Faker.Enum.Random<Couleur>().ToString(),
                Telephone = Phone.Number(),
                Photographie =  Convert.FromBase64String("alze9ve2avez")//await File.ReadAllBytesAsync("wwwroot/images/EliteCap.png")
            };

            return P;
        }

        public async Task<Agent> GenerateAgent()
        {
            Agent A = new Agent()
            {
                UID = Guid.NewGuid().ToString(),
                Grade = string.Concat(Faker.Lorem.Words(2)),
                Matricule = Faker.Identification.UkNhsNumber(),
                PasswordHash = pwd.Next(),
                Corps = string.Concat(Lorem.Words(3)),
                CouleurCheveux = Faker.Enum.Random<Couleur>().ToString(),
                CouleurYeux = Faker.Enum.Random<Couleur>().ToString(),
                DateNaissance = Faker.Identification.DateOfBirth(),
                IFU = Faker.Identification.UkNhsNumber(),
                LieuNaissance = Address.City(),
                Nationalite = "Beninoise",
                NPI = Identification.UsPassportNumber(),
                Prenom = $"{ Name.Last() + " " + Name.Middle() }",
                Nom = Name.Last(),
                Sexe = Faker.Enum.Random<Sexe>().ToString(),
                SignesParticuliers = string.Concat(Faker.Lorem.Words(4)),
                SituationMatrimoniale = string.Concat(Faker.Lorem.Words(2)),
                Taille = Convert.ToDouble(Faker.RandomNumber.Next(45, 300)),
                Teint = Faker.Enum.Random<Couleur>().ToString(),
                Telephone = Phone.Number(),
                Profession = string.Concat(Faker.Lorem.Words(3)),
                Photographie = Convert.FromBase64String("alze9ve2avez") //Convert.ToBase64String(await File.ReadAllBytesAsync("wwwroot/images/EliteCap.png"))
            };

            return A;
        }
    }
}
