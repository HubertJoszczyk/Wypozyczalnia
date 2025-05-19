using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_SBD.Models
{
    public class Wypozyczenia
    {
        [Key]
        public long Id_Wypozyczenia { get; set; }

        [ForeignKey("Klient")]
        public long Id_Klienta { get; set; }
        public Klienci Klient { get; set; }

        [ForeignKey("Przedmiot")]
        public long Id_Przedmiotu { get; set; }
        public Przedmioty Przedmiot { get; set; }

        public DateTime Data_Wypozyczenia { get; set; }
        public DateTime Data_zwrotu { get; set; }
        public string Status { get; set; } // np. "Aktywne", "Zwrócone"

        public Zwroty Zwrot { get; set; }
    }
}
