using System.ComponentModel.DataAnnotations;
using System;

namespace Projekt_SBD.Models
{
    public class Klienci
    {
        [Key]
        public int Id_klienta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public ICollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}
