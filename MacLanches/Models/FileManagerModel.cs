namespace MacLanches.Models
{
    public class FileManagerModel
    {
        //dá acesso a métodos e propriedades para tratar os arquivos
        public FileInfo[] Files { get; set; }

        //interface que permite enviar os arquivos
        public IFormFile IFormFile { get; set; }

        //lista dos arquivos que quero enviar
        public List<IFormFile> IFormFiles { get; set; }

        //armazenar nome da pasta no servidor onde está salvando os arquivos
        public string PathImagensProduto { get; set; }
    }
}
