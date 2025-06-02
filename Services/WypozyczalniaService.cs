using Microsoft.EntityFrameworkCore;
using Projekt_SBD.Data;
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
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DodajWypozyczenie @p0, @p1, @p2", klientId, przedmiotId,dataWypozyczenia);
        }

        public async Task ZwrocAsync(long wypozyczenieId, DateTime Data_zwrotu, string stan, string uwagi)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ZwrocPrzedmiot @p0, @p1, @p2, @p3", wypozyczenieId,Data_zwrotu, stan, uwagi);
        }
    }
}
