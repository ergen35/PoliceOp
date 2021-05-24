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

        public DbSet<PoliceOp.Models.AvisRecherche> AvisRecherche { get; set; }

        public DbSet<PoliceOp.Models.Requete> Requete { get; set; }

        public DbSet<PoliceOp.Models.Session> Session { get; set; }
    }
}
