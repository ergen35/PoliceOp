using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PoliceOp.Models;

namespace PoliceOp.API.Data
{
    public class PoliceOpAPIContext : DbContext
    {
        public PoliceOpAPIContext (DbContextOptions<PoliceOpAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PoliceOp.Models.Agent> Agents { get; set; }
        public DbSet<PoliceOp.Models.AvisRecherche> AvisRecherches { get; set; }
        public DbSet<PoliceOp.Models.Biometrie> BioData { get; set; }
        public DbSet<PoliceOp.Models.Coordonnees> Coordonnees { get; set; }
        public DbSet<PoliceOp.Models.Operateur> Operateurs { get; set; }
        public DbSet<PoliceOp.Models.Personne> Personnes { get; set; }
        public DbSet<PoliceOp.Models.PersonneSpeciale> PersonneSpeciales { get; set; }
        public DbSet<PoliceOp.Models.Proprietaire> Proprietaires { get; set; }
        public DbSet<PoliceOp.Models.Requete> Requetes { get; set; }
        public DbSet<PoliceOp.Models.Residence> Residences { get; set; }
        public DbSet<PoliceOp.Models.Session> Sessions { get; set; }

    }
}
