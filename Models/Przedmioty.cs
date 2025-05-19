using System.ComponentModel.DataAnnotations;

namespace Projekt_SBD.Models
{
    public class Przedmioty
    {
        public int Id_Przedmiotu { get; set; }
        public string Nazwa { get; set; }
        public string Typ { get; set; }
        public string Opis { get; set; }
        public string Dostepnosc { get; set; }
        public ICollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}
