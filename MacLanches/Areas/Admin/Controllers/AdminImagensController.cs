using MacLanches.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MacLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
                                      IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            //soma a quantidade de bytes dos arquivos enviados
            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            //monta o caminho onde vai salvar
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            //percore cada arquivo que quero enviar e verifica se são do tipo que quero receber
            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg")
                    || formFile.FileName.Contains(".gif")
                    || formFile.FileName.Contains(".png"))
                {
                    //monta o nome do arquivo completo
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    //atribui a variável para poder informar na view
                    filePathsName.Add(fileNameWithPath);

                    //método para copiar o arquivo para o servidor
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                                    $"com tamanho total de: {size} bytes";

            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }
    }
}
