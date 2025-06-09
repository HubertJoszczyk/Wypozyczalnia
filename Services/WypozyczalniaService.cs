using Microsoft.EntityFrameworkCore;
using Projekt_SBD.Data;
using Projekt_SBD.Models;
using System;

namespace Projekt_SBD.Services
{
    public class WypozyczalniaService
    {
        private readonly AppDbContext _context;
        public WypozyczalniaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task WypozyczAsync(long klientId, long przedmiotId, DateTime dataWypozyczenia)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC DodajWypozyczenie @p0, @p1, @p2", klientId, przedmiotId, dataWypozyczenia);
        }

        public async Task ZwrocAsync(long wypozyczenieId, DateTime Data_zwrotu, string stan, string uwagi)
        {
            await _context.Database
                .ExecuteSqlRawAsync("EXEC ZwrocPrzedmiot @p0, @p1, @p2, @p3", wypozyczenieId, Data_zwrotu, stan, uwagi);
        }
        public async Task<(List<PobierzAktywneWypozyczeniaDto>, int)> PobierzAktywneWypozyczeniaPagedAsync(int pageNumber, int pageSize)
        {
            var baseQuery = _context.Wypozyczenia
                .Where(w => w.Status == "Aktywne");

            var totalCount = await baseQuery.CountAsync();

            var dane = await baseQuery
                .Include(w => w.Klient)
                .Include(w => w.Przedmiot)
                .OrderBy(w => w.Data_Wypozyczenia)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(w => new PobierzAktywneWypozyczeniaDto
                {
                    Id_Wypozyczenia = w.Id_Wypozyczenia,
                    Imie = w.Klient.Imie,
                    Nazwisko = w.Klient.Nazwisko,
                    Nazwa = w.Przedmiot.Nazwa,
                    Data_Wypozyczenia = w.Data_Wypozyczenia
                })
                .ToListAsync();

            return (dane, totalCount);
        }

    }
}
