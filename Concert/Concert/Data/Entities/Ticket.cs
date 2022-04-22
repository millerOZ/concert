using System.ComponentModel.DataAnnotations;

namespace Concert.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public bool WasUsed { get; set; }
        [Display(Name = "Documento del usuario")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]

        public string Document { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Display(Name = "Nombre y apellido")]

        public string Name { get; set; }
        [Display(Name = "Entrada")]
        public ICollection<Entrance> Entrances { get; set; }
        [Display(Name = "Fecha boleta usada")]
        public DateTime Date { get; set; }
    }
}
