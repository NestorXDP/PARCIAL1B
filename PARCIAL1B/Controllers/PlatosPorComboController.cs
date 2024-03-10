using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1B.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.RegularExpressions;

namespace PARCIAL1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosPorComboController : ControllerBase
    {
        private readonly parcialContexto _parcialContexto;

        public PlatosPorComboController(parcialContexto parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<PlatosPorCombo> listadoPlatosPorCombo = _parcialContexto.PlatosPorCombo.ToList();

            if (listadoPlatosPorCombo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPlatosPorCombo);
        }

        [HttpGet]
        [Route("GetById/{empresaId}/{comboId}/{platoId}")]
        public IActionResult GetById(int empresaId, int comboId, int platoId)
        {
            PlatosPorCombo platoPorCombo = _parcialContexto.PlatosPorCombo.FirstOrDefault(e => e.EmpresaID == empresaId && e.ComboID == comboId && e.PlatoID == platoId);

            if (platoPorCombo == null)
            {
                return NotFound();
            }

            return Ok(platoPorCombo);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult GuardarPlatoPorCombo([FromBody] PlatosPorCombo platoPorCombo)
        {
            try
            {
                _parcialContexto.PlatosPorCombo.Add(platoPorCombo);
                _parcialContexto.SaveChanges();

                return Ok(platoPorCombo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{empresaId}/{comboId}/{platoId}")]
        public ActionResult ActualizarPlatoPorCombo(int empresaId, int comboId, int platoId, [FromBody] PlatosPorCombo platoPorComboModificar)
        {
            PlatosPorCombo platoPorComboActual = _parcialContexto.PlatosPorCombo.FirstOrDefault(e => e.EmpresaID == empresaId && e.ComboID == comboId && e.PlatoID == platoId);

            if (platoPorComboActual == null)
            {
                return NotFound();
            }

            platoPorComboActual.Estado = platoPorComboModificar.Estado;

            _parcialContexto.Entry(platoPorComboActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(platoPorComboActual);
        }

        [HttpDelete]
        [Route("eliminar/{empresaId}/{comboId}/{platoId}")]
        public IActionResult EliminarPlatoPorCombo(int empresaId, int comboId, int platoId)
        {
            PlatosPorCombo platoPorCombo = _parcialContexto.PlatosPorCombo.FirstOrDefault(e => e.EmpresaID == empresaId && e.ComboID == comboId && e.PlatoID == platoId);

            if (platoPorCombo == null)
            {
                return NotFound();
            }

            _parcialContexto.PlatosPorCombo.Remove(platoPorCombo);
            _parcialContexto.SaveChanges();

            return Ok(platoPorCombo);
        }
        // FIN DEL CRUD
    }
}
