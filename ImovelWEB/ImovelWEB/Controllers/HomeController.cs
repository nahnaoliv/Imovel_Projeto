using ImovelWEB.Models;
using ImovelWEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImovelWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpWebServices _httpWebServices = new HttpWebServices("https://localhost:44364/");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var listaImoveis = new List<Imovel>();

            try
            {
                listaImoveis = JsonConvert.DeserializeObject<List<Imovel>>(await _httpWebServices.GetAsync("api/Imovel/"));
            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                return View("Error", error);
            }

            //listaImoveis = listaImoveis.Where(x => x.Alugado == false).ToList();
            return View(listaImoveis);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
