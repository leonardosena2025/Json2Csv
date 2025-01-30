using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Json2Csv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PreviewDadosEmCSV(string objetoJson)
        {
            var objeto = JsonConvert.SerializeObject(objetoJson);

            var objetoJsonConvertido = JsonConvert.DeserializeObject<DataTable>(objetoJson);


            return PartialView("_PreviewJsonConvertido", objetoJsonConvertido);
        }

        public FileResult ConverterJsonCSV(string objetoJson)
        {
            List<string> linhasCsv = new List<string>();

            var objetoJsonConvertido = JsonConvert.DeserializeObject<DataTable>(objetoJson);
            var colunascsv = "";

            foreach (var colunasObjDtTable in objetoJsonConvertido.Columns)
            {
                colunascsv += colunasObjDtTable + ";";
            }

            linhasCsv.Add(string.Join(";", colunascsv));

            for (var i = 0; i < objetoJsonConvertido.Rows.Count; i++)
            {
                var linhascsv = "";

                foreach (var linhaObjDtTable in objetoJsonConvertido.Rows[i].ItemArray)
                {
                    linhascsv += linhaObjDtTable + ";";
                }

                linhasCsv.Add(string.Join(";", linhascsv));
            }

            byte[] dataAsBytes = linhasCsv.SelectMany(s => Encoding.UTF8.GetBytes(s + Environment.NewLine)).ToArray();
            var result = new FileContentResult(dataAsBytes, "application/octet-stream");
            result.FileDownloadName = "arquivo.csv";
            return result;

            //System.IO.File.WriteAllLines("C:\\Temp\\arquivo.csv", linhasCsv)
            //return File(, "text/csv", "arquivo.csv");

            //using (TextWriter tw = new StreamWriter("C:/Temp/arquivo.csv", false, Encoding.Default))
            //{
            //    tw.Write(linhasCsv);
            //    tw.Close();
            //}
        }
    }
}
