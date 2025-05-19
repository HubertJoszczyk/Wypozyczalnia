using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_SBD.Models
{
    public class Raporty
    {
        [Key]
        public int Id_Raportu { get; set; }
        public string Typ_Raportu { get; set; }
        public string Opis { get; set; }
        public DateTime Data_Generacji { get; set; }

        [ForeignKey("Pracownik")]
        public int Id_Pracownika { get; set; }
        public Pracownicy Pracownik { get; set; }
    }
}
