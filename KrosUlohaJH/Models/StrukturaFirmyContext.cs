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
            //môže byť len jeden unikátny majiteľ firmy
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

            //môže byť len jeden vedúci divízie, jeden vedúci môže mať nastarosti viacero divízii
            modelBuilder.Entity<Divizia>()
                 .HasOne(d => d.Veduci)
                 .WithMany(z => z.VedeneDivizie)
                 .HasForeignKey(d => d.VeduciRC)
                 .HasPrincipalKey(z => z.RodneCislo);

            //projekt môže mať len jednu divíziu, jedna divízia môže mať viacero projektov 
            modelBuilder.Entity<Projekt>()
               .HasOne(d => d.Divizia)
               .WithMany(f => f.Projekty)
               .HasForeignKey(d => d.DiviziaId);

            //môže byť len jeden vedúci projektu, jeden vedúci môže mať viacero projektov
            modelBuilder.Entity<Projekt>()
                .HasOne(d => d.VeduciProjektu)
                .WithMany(z => z.Projekty)
                .HasForeignKey(d => d.VeduciProjektuRC)
                .HasPrincipalKey(z => z.RodneCislo);


            //oddelenie môže mať len jedno oddelenia, jedno oddelenie môže mať viacero projektov 
            modelBuilder.Entity<Oddelenie>()
               .HasOne(d => d.Projekt)
               .WithMany(f => f.Oddelenia)
               .HasForeignKey(d => d.ProjektId);

            //môže byť len jeden vedúci oddelenia, jeden zamestnanec môže mať na starosti viacero oddelení
            modelBuilder.Entity<Oddelenie>()
                .HasOne(d => d.VeduciOddelenia)
                .WithMany(z => z.Oddelenia)
                .HasForeignKey(d => d.VeduciOddeleniaRc)
                .HasPrincipalKey(z => z.RodneCislo);


            //oddelenie môže mať viacero zamestnancov
            modelBuilder.Entity<Zamestnanec>()
                .HasOne(d => d.Oddelenie)
                .WithMany(f => f.Zamestnanci)
                .HasForeignKey(d => d.OddelenieId);



        }
    }
}
