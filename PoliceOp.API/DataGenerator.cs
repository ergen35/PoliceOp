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

        public async Task<Personne> GeneratePersonne()
        {

            Personne P = new Personne()
            {
                BiometrieID = Faker.RandomNumber.Next(0, 500000000),
                CouleurCheveux = Faker.Enum.Random<Couleur>().ToString(),
                CouleurYeux = Faker.Enum.Random<Couleur>().ToString(),
                DateNaissance = Faker.Identification.DateOfBirth(),
                IFU = Faker.Identification.UkNhsNumber(),
                LieuNaissance = Address.City(),
                Nationalite = "Béninoise",
                NPI = Identification.UsPassportNumber(),
                Prenom = $"{ Name.Last() + " " + Name.Middle() }",
                Nom = Name.Last(),
                Profession = Faker.Lorem.Sentence(2),
                Sexe = Faker.Enum.Random<Sexe>(),
                SignesParticuliers = Faker.Lorem.Words(4).ToString(),
                SituationMatrimoniale = Faker.Lorem.Words(2).ToString(),
                Taille = Convert.ToDouble(Faker.RandomNumber.Next(45, 300)),
                Teint = Faker.Enum.Random<Couleur>().ToString(),
                Telephone = Phone.Number(),
                Photographie =  new byte[] { }//await File.ReadAllBytesAsync("wwwroot/images/EliteCap.png")
            };

            return P;
        }

        public async Task<Agent> GenerateAgent()
        {
            Agent A = new Agent()
            {
                BiometrieID = Faker.RandomNumber.Next(0, 500000000),
                CouleurCheveux = Faker.Enum.Random<Couleur>().ToString(),
                CouleurYeux = Faker.Enum.Random<Couleur>().ToString(),
                DateNaissance = Faker.Identification.DateOfBirth(),
                IFU = Faker.Identification.UkNhsNumber(),
                LieuNaissance = Address.City(),
                Nationalite = "Béninoise",
                NPI = Identification.UsPassportNumber(),
                Prenom = $"{ Name.Last() + " " + Name.Middle() }",
                Nom = Name.Last(),
                Profession = Faker.Lorem.Sentence(2),
                Sexe = Faker.Enum.Random<Sexe>(),
                SignesParticuliers = Faker.Lorem.Words(4).ToString(),
                SituationMatrimoniale = Faker.Lorem.Words(2).ToString(),
                Taille = Convert.ToDouble(Faker.RandomNumber.Next(45, 300)),
                Teint = Faker.Enum.Random<Couleur>().ToString(),
                Telephone = Phone.Number(),
                Photographie = await File.ReadAllBytesAsync("wwwroot/images/EliteCap.png")
            };

            return A;
        }
    }
}
