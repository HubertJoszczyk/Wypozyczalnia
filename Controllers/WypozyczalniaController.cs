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
        public async Task<IActionResult> Wypozycz(long klientId, long przedmiotId, DateTime DataWypozyczenia)
        {
            await _service.WypozyczAsync(klientId, przedmiotId,DataWypozyczenia);
            return Ok("Wypożyczono");
        }

        [HttpPost("zwroc")]
        public async Task<IActionResult> Zwroc(long wypozyczenieId,DateTime Data_zwrotu, string stan, string uwagi)
        {
            await _service.ZwrocAsync(wypozyczenieId,Data_zwrotu, stan, uwagi);
            return Ok("Zwrócono");
        }
        //[HttpsPost()]
        //public async Task<IActionResult>  {

          //  return Ok();
        //}
    }
}
