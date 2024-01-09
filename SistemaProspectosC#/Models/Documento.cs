using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaProspectosC_.Models
{
    public class Documento
    {
        [Key]
        public int documentoId { get; set; }
        public string titulo { get; set; }
        public string ruta { get; set; }

        public int prospectoID { get; set; }
        public Prospecto prospecto { get; set; } = null!;
        

    }
}
