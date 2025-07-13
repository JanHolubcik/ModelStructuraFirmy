using Microsoft.EntityFrameworkCore;

namespace KrosUlohaJH.Models
{
    public class StrukturaFirmyContext : DbContext
    {
        public StrukturaFirmyContext(DbContextOptions<StrukturaFirmyContext> options) : base(options) { }
        public DbSet<Firma> Firmy { get; set; }
        public DbSet<Divizia> Divizie { get; set; }
        public DbSet<Projekt> Projekty { get; set; }
        public DbSet<Oddelenie> Oddelenia { get; set; }
        public DbSet<Zamestnanec> Zamestnanci { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //môže byť len jeden majiteľ firmy
            modelBuilder.Entity<Firma>()
                .HasOne(f => f.Riaditel)
                .WithOne() 
                .HasForeignKey<Firma>(f => f.RiaditelRc)
                .HasPrincipalKey<Zamestnanec>(z => z.RodneCislo); 

            //divízia môže mať len jednu firmu, jedna firma môže mať viacero divízii
            modelBuilder.Entity<Divizia>()
               .HasOne(d => d.Firma)
               .WithMany(f => f.Divizie)
               .HasForeignKey(d => d.FirmaId);

            //môže byť len jeden vedúci divízie
            modelBuilder.Entity<Divizia>()
                .HasOne(d => d.Veduci)
                .WithOne()
                .HasForeignKey<Divizia>(d => d.VeduciRC)
                .HasPrincipalKey<Zamestnanec>(z => z.RodneCislo);

            //projekt môže mať len jednu divíziu, jedna divízia môže mať viacero projektov 
            modelBuilder.Entity<Projekt>()
               .HasOne(d => d.Divizia)
               .WithMany(f => f.Projekty)
               .HasForeignKey(d => d.DiviziaId);

            //môže byť len jeden vedúci projektu
            modelBuilder.Entity<Projekt>()
                .HasOne(d => d.VeduciProjektu)
                .WithOne()
                .HasForeignKey<Projekt>(d => d.VeduciProjektuRC)
                .HasPrincipalKey<Zamestnanec>(z => z.RodneCislo);

            //projekt môže mať len jednu divíziu, jedna divízia môže mať viacero projektov 
            modelBuilder.Entity<Oddelenie>()
               .HasOne(d => d.Projekt)
               .WithMany(f => f.Oddelenia)
               .HasForeignKey(d => d.ProjektId);

            //môže byť len jeden vedúci projektu
            modelBuilder.Entity<Oddelenie>()
                .HasOne(d => d.VeduciOddelenia)
                .WithOne()
                .HasForeignKey<Oddelenie>(d => d.VeduciOddeleniaRc)
                .HasPrincipalKey<Zamestnanec>(z => z.RodneCislo);

            //oddelenie môže mať viacero zamestnancov
            modelBuilder.Entity<Zamestnanec>()
                .HasOne(d => d.Oddelenie)
                .WithMany(f => f.Zamestnanci)
                .HasForeignKey(d => d.OddelenieId);



        }
    }
}
