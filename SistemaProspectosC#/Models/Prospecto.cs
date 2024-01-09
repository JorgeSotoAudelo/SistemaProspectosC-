using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using SistemaProspectosC_.Enums;

namespace SistemaProspectosC_.Models
{
    public class Prospecto
    {
        [Key]
        public int prospectoId {  get; set; }
        public String nombre { get; set; }
        public String primerApellido { get; set; }
        public String segundoApellido { get; set; }
        public String calle {  get; set; }
        public String numero { get; set; }
        public String colonia { get; set; }
        public String codigoPostal { get; set; }
        public String telefono { get; set; }
        public String RFC { get; set; }

        public String observacion { get; set; } = "";
        public Estatus estatus { get; set; } = Estatus.Enviado;
        public List<Documento> documents { get; } = new();
        
           
    }

}
