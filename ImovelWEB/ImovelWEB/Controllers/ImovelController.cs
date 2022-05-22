using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ImovelWEB.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ImovelWEB.Services;

namespace ImovelWEB.Controllers
{
    public class ImovelController : Controller
    {
        //definicao/atribuicao da classe
        private readonly HttpWebServices _httpWebServices = new HttpWebServices("https://localhost:44364/");
        // GET: Imovel
        public async Task<IActionResult> Index()
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

            return View(listaImoveis);
        }

        // GET: Imovel/Details/5
        public async Task<IActionResult> Details(int? id, string urlRetorno = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = new Imovel();

            try
            {
                imovel = JsonConvert.DeserializeObject<Imovel>(await _httpWebServices.GetAsync("api/Imovel/" + id));
            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                return View("Error", error);
            }

            if (imovel == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(urlRetorno))
            {
                ViewBag.urlRetorno = urlRetorno;
            }

            return View(imovel);
        }

        // GET: Imovel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Imovel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,Cep,Rua,Numero,Bairro,Complemento,Cidade,Estado,Referencia,Alugado")] Imovel imovel)
        {
            ModelState.Clear();

            decimal precoDecimal;
            var precoString = Request.Form["Preco"].ToString().Replace("R$ ", "");

            if (decimal.TryParse(precoString, out precoDecimal) == false || precoDecimal <= 0)
            {
                ModelState.AddModelError("Preco", "Preço inválido, digite novamente.");
            }

            //if (imovel.Nome == null || imovel.Nome == "")
            if (string.IsNullOrEmpty(imovel.Nome)) //Nome
            {
                ModelState.AddModelError("Nome", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Descricao)) //Descrição
            {
                ModelState.AddModelError("Descricao", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Cep)) //CEP
            {
                ModelState.AddModelError("Cep", "Preenchimento obrigatório.");
            }
            else
            {
                var cepFormatado = imovel.Cep.Replace("-", "").Replace(".", "");

                if (cepFormatado.Length < 8 || cepFormatado.Length > 8)
                {
                    ModelState.AddModelError("Cep", "CEP Inválido!");
                }

                imovel.Cep = cepFormatado;
            }

            if (string.IsNullOrEmpty(imovel.Rua)) //Rua
            {
                ModelState.AddModelError("Rua", "Preenchimento obrigatório.");
            }

            if (imovel.Numero <= 0) //Numero
            {
                ModelState.AddModelError("Numero", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Bairro)) //Bairro
            {
                ModelState.AddModelError("Bairro", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Cidade)) //Cidade
            {
                ModelState.AddModelError("Cidade", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Estado) || imovel.Estado.Length < 2 || imovel.Estado.Length > 2) //Estado
            {
                ModelState.AddModelError("Estado", "Preenchimento obrigatório.");
            }

            imovel.Preco = precoDecimal;
            imovel.Alugado = false;

            if (ModelState.IsValid) //alerta
            {
                try
                {
                    await _httpWebServices.PostAsync("api/Imovel/", JsonConvert.SerializeObject(imovel));
                }
                catch (Exception ex)
                {
                    ErrorViewModel error = new ErrorViewModel();
                    error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                    return View("Error", error);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(imovel);
        }

        // GET: Imovel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = new Imovel();

            try
            {
                imovel = JsonConvert.DeserializeObject<Imovel>(await _httpWebServices.GetAsync("api/Imovel/" + id));
            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                return View("Error", error);
            }

            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,Cep,Rua,Numero,Bairro,Complemento,Cidade,Estado,Referencia,Alugado")] Imovel imovel)
        {
            ModelState.Clear();

            decimal precoDecimal;
            var precoString = Request.Form["Preco"].ToString().Replace("R$ ", "");

            if (decimal.TryParse(precoString, out precoDecimal) == false || precoDecimal <= 0)
            {
                ModelState.AddModelError("Preco", "Preço inválido, digite novamente.");
            }
            imovel.Preco = precoDecimal;

            //if (imovel.Nome == null || imovel.Nome == "")
            if (string.IsNullOrEmpty(imovel.Nome)) //Nome
            {
                ModelState.AddModelError("Nome", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Descricao)) //Descrição
            {
                ModelState.AddModelError("Descricao", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Cep)) //CEP
            {
                ModelState.AddModelError("Cep", "Preenchimento obrigatório.");
            }
            else
            {
                var cepFormatado = imovel.Cep.Replace("-", "").Replace(".", "");

                if (cepFormatado.Length < 8 || cepFormatado.Length > 8)
                {
                    ModelState.AddModelError("Cep", "CEP Inválido!");
                }

                imovel.Cep = cepFormatado;
            }

            if (string.IsNullOrEmpty(imovel.Rua)) //Rua
            {
                ModelState.AddModelError("Rua", "Preenchimento obrigatório.");
            }

            if (imovel.Numero <= 0) //Numero
            {
                ModelState.AddModelError("Numero", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Bairro)) //Bairro
            {
                ModelState.AddModelError("Bairro", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Cidade)) //Cidade
            {
                ModelState.AddModelError("Cidade", "Preenchimento obrigatório.");
            }

            if (string.IsNullOrEmpty(imovel.Estado) || imovel.Estado.Length < 2 || imovel.Estado.Length > 2) //Estado
            {
                ModelState.AddModelError("Estado", "Preenchimento obrigatório.");
            }

            if (id != imovel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _httpWebServices.PostAsync("api/Imovel/" + id, JsonConvert.SerializeObject(imovel), "PUT");
                }
                catch (Exception ex)
                {
                    ErrorViewModel error = new ErrorViewModel();
                    error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                    return View("Error", error);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(imovel);
        }

        // GET: Imovel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = new Imovel();

            try
            {
                imovel = JsonConvert.DeserializeObject<Imovel>(await _httpWebServices.GetAsync("api/Imovel/" + id));
            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                return View("Error", error);
            }

            if (imovel == null)
            {
                return NotFound();
            }
            return View(imovel);
        }

        // POST: Imovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _httpWebServices.PostAsync("api/Imovel/" + id, methodType: "DELETE");
            }
            catch (Exception ex)
            {
                ErrorViewModel error = new ErrorViewModel();
                error.RequestId = ex.Message + "<br/>" + ex.StackTrace;

                return View("Error", error);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
