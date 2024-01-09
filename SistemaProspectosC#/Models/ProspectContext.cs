using Microsoft.EntityFrameworkCore;

namespace SistemaProspectosC_.Models
{
    public class ProspectContext : DbContext
    {
        public DbSet<Prospecto> Prospectos { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        public String DbPath { get; set; }
        public ProspectContext(DbContextOptions<ProspectContext> options) : base(options) 
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "prospectos.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prospecto>()
                .HasMany(p => p.documents)
                .WithOne(d => d.prospecto)
                .HasForeignKey(d => d.prospectoID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}

