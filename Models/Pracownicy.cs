using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt_SBD.Models
{
    public class Pracownicy
    {
        [Key]
        public int Id_Pracownika { get; set; }
        public string Typ { get; set; }

        public ICollection<Raporty> Raporty { get; set; }
    }
}
