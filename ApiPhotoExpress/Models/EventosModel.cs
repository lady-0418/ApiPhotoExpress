using System.ComponentModel.DataAnnotations;
namespace ApiPhotoExpress.Models
{
    public class EventosModel
    {
        [Key]
        public int IdEvento { get; set; }
        public string? NombreInstitucion { get; set; }
        public string? DireccionInstitucion { get; set; }

        public int NumeroAlumnos { get; set; }
        public TimeSpan HoraInicio { get; set; }

        public DateTime FechaEvento { get; set; }
        public decimal CostoServicio { get; set; }

        public Boolean ServicioTogaBirrete { get; set; }
    }
}
