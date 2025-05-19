using Microsoft.AspNetCore.Mvc;
using Projekt_SBD.Services;
using System;

namespace Projekt_SBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WypozyczeniaController : ControllerBase
    {
        private readonly WypozyczalniaService _service;
        public WypozyczeniaController(WypozyczalniaService service)
        {
            _service = service;
        }

        [HttpPost("wypozycz")]
        public async Task<IActionResult> Wypozycz(long klientId, long przedmiotId)
        {
            await _service.WypozyczAsync(klientId, przedmiotId);
            return Ok("Wypożyczono");
        }

        [HttpPost("zwroc")]
        public async Task<IActionResult> Zwroc(long wypozyczenieId, string stan, string uwagi)
        {
            await _service.ZwrocAsync(wypozyczenieId, stan, uwagi);
            return Ok("Zwrócono");
        }
    }
}
