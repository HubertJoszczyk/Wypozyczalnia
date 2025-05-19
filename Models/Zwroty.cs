using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_SBD.Models
{
    public class Zwroty
    {
        [Key]
        public long Id_Zwrotu { get; set; }

        [ForeignKey("Wypozyczenia")]
        public long Id_Wypozyczenia { get; set; }
        public Wypozyczenia Wypozyczenie { get; set; }

        public DateTime Data_zwrotu { get; set; }
        public string Stan_Przedmiotu { get; set; }
        public string Uwagi { get; set; }
    }
}
