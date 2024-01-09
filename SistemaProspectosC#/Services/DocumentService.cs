namespace SistemaProspectosC_.Services
{
    public class DocumentService
    {
        public bool SaveDocuments(List<IFormFile> files, int id)
        {
            foreach (IFormFile file in files)
            {
                if (file == null || file.Length == 0)
                {
                    return false;
                }

            }
            var Path = GetDocumentRoute(id);

            try
            {
                foreach (IFormFile file in files)
                {
                    var Path_Filename = System.IO.Path.Join(Path, file.FileName);

                    using (var fileStream = new FileStream(Path_Filename, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;



        }

        public string GetDocumentRoute(int prospectoID)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            var fullPath = System.IO.Path.Join(path, "Documentos_Prospectos", prospectoID.ToString());
            
            Directory.CreateDirectory(fullPath);

            return fullPath;
        }
    }
}

