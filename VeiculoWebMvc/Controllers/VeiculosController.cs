using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using VeiculoDao;
using VeiculoModelsLibrary.Models;
using VeiculoWebMvc.Models;
using static VeiculoWebMvc.CommonMethods.StringMethods;
using static VeiculoWebMvc.Helpers.ApiHelper;

namespace VeiculoWebMvc.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly Contexto _context;
        private static string _controllerApiName = "api/Veiculos";

        public VeiculosController(Contexto context)
        {
            _context = context;
            InitiateClient();
        }

        // GET: Veiculos
        [Route("Veiculo")]
        public async Task<IActionResult> Index()
        {
            var actionName = "ListarVeiculos";
            var uri = Flurl.Url.Combine(ApiClient.BaseAddress.ToString(), _controllerApiName, actionName);

            using (HttpResponseMessage response = await ApiClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Error), new { statusCode = response.StatusCode, errorMessage = await response.Content.ReadAsStringAsync() });
                }

                var veiculos = await response.Content.ReadFromJsonAsync<IEnumerable<Veiculo>>();

                return View(veiculos);
            }
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Placa,Modelo,Montadora")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                var actionName = "CadastrarVeiculo";
                var uri = Flurl.Url.Combine(ApiClient.BaseAddress.ToString(), _controllerApiName, actionName);

                using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(uri, veiculo))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Error), new { statusCode = response.StatusCode, errorMessage = await response.Content.ReadAsStringAsync() });
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Error));
        }

        // GET: Veiculos/Edit/Guid
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/Guid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Placa,Modelo,Montadora")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var actionName = "AlterarVeiculo";
                var uri = Flurl.Url.Combine(ApiClient.BaseAddress.ToString(), _controllerApiName, actionName);

                using (HttpResponseMessage response = await ApiClient.PutAsJsonAsync(uri, veiculo))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Error), new { statusCode = response.StatusCode, errorMessage = await response.Content.ReadAsStringAsync() });
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(veiculo);
        }

        // GET: Veiculos/Delete/Guid
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/Guid
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Veiculos == null)
            {
                return Problem("A entidade 'Contexto.Veiculos' está null.");
            }

            if (id != null)
            {
                var actionName = "ExcluirVeiculo";
                var uri = Flurl.Url.Combine(ApiClient.BaseAddress.ToString(), _controllerApiName, actionName, id.ToString());

                using (HttpResponseMessage response = await ApiClient.DeleteAsync(uri))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Error), new { statusCode = response.StatusCode, errorMessage = await response.Content.ReadAsStringAsync() });
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(Guid id)
        {
            return (_context.Veiculos?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(HttpStatusCode? statusCode = null, string? errorMessage = null)
        {
            var errorViewModel = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode,
                ErrorMessage = errorMessage,
            };

            ModelState.Values
                .SelectMany(v => v.Errors)
                .ToList()
                .ForEach(e =>
                {
                    errorViewModel.ErrorMessage += $"{e.ErrorMessage}\n";
                });

            return View(errorViewModel);
        }
    }
}
