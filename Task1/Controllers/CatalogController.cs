using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using Task1.DAO;

namespace Task1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly SampleContext _context;

        public CatalogController(SampleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SaveCatalogToJson()
        {
            var catalogStructure = _context.Cataloges;

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(catalogStructure, jsonSettings);

            System.IO.File.WriteAllText("catalog.json", json);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LoadCatalogFromJson()
        {
            var json = System.IO.File.ReadAllText("catalog.json");

            var catalogStructure = JsonConvert.DeserializeObject<List<Catalog>>(json);

            return RedirectToAction("Index", "Home");
        }
    }
}
