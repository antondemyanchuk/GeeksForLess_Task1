using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using Task1.DAO;

namespace Task1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly SampleContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CatalogController(SampleContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult SaveCatalogJson()
        {
            var catalogData = _context.Cataloges.ToList();

            if (catalogData.Count == 0)
            {
                return NotFound("No catalog data to save.");
            }

            var jsonCatalogData = JsonSerializer.Serialize(catalogData);

            var fileName = "catalog.json";
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, fileName);

            System.IO.File.WriteAllText(filePath, jsonCatalogData);

            return PhysicalFile(filePath, "application/json", WebUtility.UrlEncode(fileName));
        }

        [HttpGet]
        public IActionResult ImportNewCatalog()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportNewCatalog([FromForm] IFormFile jsonFile)
        {
            if (jsonFile != null && jsonFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    jsonFile.CopyTo(stream);
                    var json = Encoding.UTF8.GetString(stream.ToArray());

                    var catalogData = JsonSerializer.Deserialize<Catalog[]>(json);

                    _context.Cataloges.RemoveRange(_context.Cataloges);
                    _context.Cataloges.AddRange(catalogData);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            return BadRequest("No file selected.");
        }

        [HttpGet]
        public IActionResult Start()
        {
            return RedirectToAction("ImportNewCatalog");
        }
    }
}


