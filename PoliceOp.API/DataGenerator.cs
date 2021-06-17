using Faker;
using PoliceOp.Models;
using System;
using System.Threading.Tasks;


namespace PoliceOp.API
{
    public enum Couleur
    {
        Red, Black, Blue, Orange, Violet, Green, Cyan, Gray
    }

    public class DataGenerator
    {
        PasswordGenerator.Password pwd = new PasswordGenerator.Password(true, false, true, false, 8);

        public async Task<Personne> GeneratePersonne()
        {
            Residence R = new Residence()
            {
                Rue = Faker.Address.StreetAddress(true),
                NumeroChambre = Faker.Address.ZipCode(),
                Type = string.Concat(Faker.Lorem.Words(3))
            };

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
                Residence = R,
                Profession = string.Concat(Faker.Lorem.Words(2)),
                Sexe = Faker.Enum.Random<Sexe>().ToString(),
                SignesParticuliers = string.Concat(Faker.Lorem.Words(4)),
                SituationMatrimoniale = string.Concat(Faker.Lorem.Words(2)),
                Taille = Convert.ToDouble(Faker.RandomNumber.Next(45, 300)),
                Teint = Faker.Enum.Random<Couleur>().ToString(),
                Telephone = Phone.Number(),
                Photographie = Convert.FromBase64String("alze9ve2avez")//await File.ReadAllBytesAsync("wwwroot/images/EliteCap.png")
            };

            return P;
        }

        public async Task<Agent> GenerateAgent()
        {
            Residence R = new Residence()
            {
                Rue = Faker.Address.StreetAddress(true),
                NumeroChambre = Faker.Address.ZipCode(),
                Type = string.Concat(Faker.Lorem.Words(3))
            };


            Agent A = new Agent()
            {
                UID = Guid.NewGuid().ToString(),
                Residence = R,
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
                MereId = (new Random()).Next(1, 55),
                PereId = (new Random()).Next(9, 85),
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
