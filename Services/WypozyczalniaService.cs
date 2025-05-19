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

        public async Task WypozyczAsync(long klientId, long przedmiotId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC WypozyczPrzedmiot @p0, @p1", klientId, przedmiotId);
        }

        public async Task ZwrocAsync(long wypozyczenieId, string stan, string uwagi)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ZwrocPrzedmiot @p0, @p1, @p2", wypozyczenieId, stan, uwagi);
        }
    }
}
