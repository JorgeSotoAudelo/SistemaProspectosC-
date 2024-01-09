using Microsoft.AspNetCore.Mvc;

namespace SistemaProspectosC_.Controllers
{

    [Route("Vistas")]
    public class HomeController : Controller
    {
        [HttpGet("verProspectos")]
        public IActionResult verProspectos()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ListadoProspectos.html");
            var contentType = "text/html";
   
            return PhysicalFile(filePath, contentType);
        }
        [HttpGet("CrearProspecto")]
        public IActionResult crearProspecto()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CrearProspecto.html");
            var contentType = "text/html";

            return PhysicalFile(filePath, contentType);
        }
        [HttpGet("{id}")]
        public IActionResult evaluarProspecto()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EvaluarProspecto.html");
            var contentType = "text/html";

            return PhysicalFile(filePath, contentType);
        }
    }
}
