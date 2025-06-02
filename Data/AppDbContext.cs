using Microsoft.EntityFrameworkCore;
using Projekt_SBD.Models;
namespace Projekt_SBD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pracownicy> Pracownicy { get; set; }
        public DbSet<Raporty> Raporty { get; set; }
        public DbSet<Klienci> Klienci { get; set; }
        public DbSet<Przedmioty> Przedmioty { get; set; }
        public DbSet<Wypozyczenia> Wypozyczenia { get; set; }
        public DbSet<Zwroty> Zwroty { get; set; }
        public DbSet<PobierzAktywneWypozyczeniaDto> PobierzAktywneWypozyczeniaDto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Klienci
            modelBuilder.Entity<Klienci>()
                .HasKey(k => k.Id_klienta);

            // Pracownicy
            modelBuilder.Entity<Pracownicy>()
                .HasKey(p => p.Id_Pracownika);

            // Przedmioty
            modelBuilder.Entity<Przedmioty>()
                .HasKey(p => p.Id_Przedmiotu);

            // Raporty
            modelBuilder.Entity<Raporty>()
                .HasKey(r => r.Id_Raportu);
            modelBuilder.Entity<Raporty>()
                .HasOne(r => r.Pracownik)
                .WithMany(p => p.Raporty)
                .HasForeignKey(r => r.Id_Pracownika);

            // Wypożyczenia
            modelBuilder.Entity<Wypozyczenia>()
                .HasKey(w => w.Id_Wypozyczenia);
            modelBuilder.Entity<Wypozyczenia>()
                .HasOne(w => w.Klient)
                .WithMany(k => k.Wypozyczenia)
                .HasForeignKey(w => w.Id_Klienta);
            modelBuilder.Entity<Wypozyczenia>()
                .HasOne(w => w.Przedmiot)
                .WithMany(p => p.Wypozyczenia)
                .HasForeignKey(w => w.Id_Przedmiotu);

            // Zwroty
            modelBuilder.Entity<Zwroty>()
                .HasKey(z => z.Id_Zwrotu);
            modelBuilder.Entity<Zwroty>()
                .HasOne(z => z.Wypozyczenie)
                .WithOne(w => w.Zwrot)
                .HasForeignKey<Zwroty>(z => z.Id_Wypozyczenia)
                .OnDelete(DeleteBehavior.Restrict);
            //
            modelBuilder.Entity<PobierzAktywneWypozyczeniaDto>().HasNoKey();
        }
    }

}