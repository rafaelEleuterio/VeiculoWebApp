using Microsoft.AspNetCore.Mvc;
using VeiculoDao;
using VeiculoModelsLibrary.Models;

namespace VeiculoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly Contexto _context;

        private readonly ILogger<VeiculosController> _logger;

        public VeiculosController(ILogger<VeiculosController> logger, Contexto context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("ListarVeiculos")]
        public ActionResult<IEnumerable<Veiculo>> Get()
        {
            var veiculos = _context.Veiculos.ToArray();
            return Ok(veiculos);
        }

        [HttpPost("CadastrarVeiculo")]
        public ActionResult Post([FromBody] Veiculo veiculo)
        {
            if (_context.Veiculos.Any(v => v.Id == veiculo.Id))
            {
                return BadRequest("Este veículo já foi inserido no banco de dados.");
            }

            _context.Veiculos.Add(new Veiculo()
            {
                Id = new Guid(),
                Placa = veiculo.Placa,
                Modelo = veiculo.Modelo,
                Montadora = veiculo.Montadora
            });

            _context.SaveChanges();

            return Created("CadastrarVeiculo", veiculo);
        }

        [HttpPut("AlterarVeiculo")]
        public ActionResult Put([FromBody] Veiculo veiculo)
        {
            if (!_context.Veiculos.Any(v => v.Id == veiculo.Id))
            {
                return BadRequest("Nenhum veículo com este Id foi encontrado no banco de dados.");
            }
            var _veiculo = _context.Veiculos.Single(v => v.Id == veiculo.Id);

            _veiculo.Placa = veiculo.Placa;
            _veiculo.Modelo = veiculo.Modelo;
            _veiculo.Montadora = veiculo.Montadora;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("ExcluirVeiculo/{id}")]
        public ActionResult Delete(Guid id)
        {
            if (!_context.Veiculos.Any(v => v.Id == id))
            {
                return BadRequest("Nenhum veículo com este Id foi encontrado no banco de dados.");
            }

            var veiculo = _context.Veiculos.Single(v => v.Id == id);
            
            _context.Veiculos.Remove(veiculo);

            _context.SaveChanges();

            return Ok();
        }

    }
}