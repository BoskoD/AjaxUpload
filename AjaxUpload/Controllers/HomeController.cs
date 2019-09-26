using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AjaxUpload.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AjaxUpload.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile fajl)
        {
            if (fajl == null || fajl.Length == 0)
            {
                ViewData["Poruka"] = "Niste odabrali fajl";
                return View("Rezultat");
            }

            var root = Directory.GetCurrentDirectory();
            var putanja = Path.Combine(root, "wwwroot/Fajlovi", fajl.FileName);

            try
            {
                using(FileStream fs = new FileStream(putanja, FileMode.Create))
                {
                    await fajl.CopyToAsync(fs);
                    ViewData["Poruka"] = $@"Iskopiran fajl: {fajl.FileName} <br>
                    Tip: {fajl.ContentType} <br>
                    Velicina: {fajl.Length/1024} KB";
                    return View("Rezultat");
                }
            }
            catch (System.Exception)
            {
                
                ViewData["Poruka"] = "Greska";
                return View("Rezultat");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
