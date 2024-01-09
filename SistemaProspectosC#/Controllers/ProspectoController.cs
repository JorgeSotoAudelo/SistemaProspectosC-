using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaProspectosC_.Models;
using SistemaProspectosC_.Enums;
using SistemaProspectosC_.Services;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace SistemaProspectosC_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProspectoController : ControllerBase {

        private readonly ProspectContext _prospectoContext;
        private readonly DocumentService _documentService;

        public ProspectoController(ProspectContext prospectoContext, DocumentService documentService)
        {
            _prospectoContext = prospectoContext;
            _documentService = documentService;

        }

        [HttpGet]
        public IActionResult GetProspectos()
        {
            List<Prospecto> listaProspectos = _prospectoContext.Prospectos
                .Include(p => p.documents)
                .ToList();
            return Ok(listaProspectos);
        }
        [HttpGet("{id}")]
        public IActionResult GetProspecto(int id)
        {
            Prospecto prospecto = _prospectoContext.Prospectos
                .Include(p => p.documents)
                .FirstOrDefault(p => p.prospectoId == id);

            if(prospecto == null) return NotFound("El prospecto solicitado no existe");
            
            return Ok(prospecto);
        }

        [HttpGet("download/{Prospectoid}/{DocumentName}")]
        public IActionResult DownloadDocument(int Prospectoid, String DocumentName)
        {
            if (DocumentName == null) return BadRequest("Ruta invalida");

            DocumentName = System.Net.WebUtility.UrlDecode(DocumentName);
            String ruta = _documentService.GetDocumentRoute(Prospectoid);
            String rutaDescarga = System.IO.Path.Join(ruta, DocumentName);


            String rutaDescargaEncoded = System.Net.WebUtility.UrlEncode(rutaDescarga); 


            var fileBytes = System.IO.File.ReadAllBytes(rutaDescarga);


            return File(fileBytes, "application/pdf", DocumentName);

        }

        [HttpPost]
        public IActionResult CreateProspecto([FromForm] ProspectoViewModel prospectoViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (prospectoViewModel.prospecto == null) return BadRequest();

            Prospecto prospecto = JsonConvert.DeserializeObject<Prospecto>(prospectoViewModel.prospecto);

            _prospectoContext.Prospectos.Add(prospecto);
            _prospectoContext.SaveChanges();

            Console.WriteLine(prospectoViewModel.archivos.ToString());

            foreach(FormFile file in prospectoViewModel.archivos)
            {
                if(file == null) continue;

                Documento doc = new Documento();
                doc.titulo = file.FileName;
                doc.ruta = _documentService.GetDocumentRoute(prospecto.prospectoId);
                doc.prospecto = prospecto;
                _prospectoContext.Documentos.Add(doc);
                _prospectoContext.SaveChanges();
            }
            bool guardados = _documentService.SaveDocuments(prospectoViewModel.archivos, prospecto.prospectoId);
            
            if (!guardados) return BadRequest("Error al guardar documentos");

  
            return Ok("Prospecto Guardado Exitosamente"); 
            
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProspecto(int id)
        {
            var prospecto = _prospectoContext.Prospectos.Find(id);

            if(prospecto == null)
            {
                return NotFound(); 
            }
            _prospectoContext.Remove<Prospecto>(prospecto);
            _prospectoContext.SaveChanges();

            return Ok();
        }

        [HttpPatch("{idProspecto}/{numEstatus}")]
        public IActionResult ChangeProspectoStatus(int idProspecto, int numEstatus, [FromBody] String observacion)
        {
            var prospecto = _prospectoContext.Prospectos.Find(idProspecto);
            bool estatusIndefinido = !Enum.IsDefined(typeof(Estatus), numEstatus);

            if(prospecto == null || estatusIndefinido )
            {
                return NotFound();
            }
            prospecto.observacion = observacion;
            prospecto.estatus = (Estatus)numEstatus;
            _prospectoContext.SaveChanges();

            return Ok();
        }

    }
}
