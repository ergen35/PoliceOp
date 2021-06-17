using Microsoft.EntityFrameworkCore;

namespace PoliceOp.API.Data
{
    public class PoliceOpAPIContext : DbContext
    {
        public PoliceOpAPIContext(DbContextOptions<PoliceOpAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PoliceOp.Models.Agent> Agents { get; set; }
        public DbSet<PoliceOp.Models.AvisRecherche> AvisRecherches { get; set; }
        public DbSet<PoliceOp.Models.Biometrie> BioData { get; set; }
        public DbSet<PoliceOp.Models.Operateur> Operateurs { get; set; }
        public DbSet<PoliceOp.Models.Personne> Personnes { get; set; }
        public DbSet<PoliceOp.Models.Requete> Requetes { get; set; }
        public DbSet<PoliceOp.Models.Residence> Residences { get; set; }
        public DbSet<PoliceOp.Models.Session> Sessions { get; set; }
        public DbSet<PoliceOp.Models.Diffusion> Diffusions { get; set; }
        public DbSet<PoliceOp.Models.PieceJointe> PieceJointes { get; set; }

    }
}
