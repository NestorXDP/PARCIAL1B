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
        [Route("GetAll")]
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
        [Route("GetByIdp/{PlatosPorComboID}")]
        public IActionResult GetById(int id)
        {
            PlatosPorCombo? platoPorCombo = _parcialContexto.PlatosPorCombo.FirstOrDefault(e => e.PlatosPorComboID == id);

            if (platoPorCombo == null)
            {
                return NotFound();
            }

            return Ok(platoPorCombo);
        }

        [HttpPost]
        [Route("addp")]
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
        [Route("actualizarp/{id}")]
        public ActionResult ActualizarPlatoPorCombo(int id, [FromBody] PlatosPorCombo platoPorComboModificar)
        {
            PlatosPorCombo? platoPorComboActual = (from p in _parcialContexto.PlatosPorCombo where p.PlatosPorComboID == id select p).FirstOrDefault();
            
            if (platoPorComboActual == null)
            {
                return NotFound();
            }
            platoPorComboActual.EmpresaID = platoPorComboModificar.EmpresaID;
            platoPorComboActual.ComboID = platoPorComboModificar.ComboID;
            platoPorComboActual.PlatoID = platoPorComboModificar.PlatoID;
            platoPorComboActual.Estado = platoPorComboModificar.Estado;

            _parcialContexto.Entry(platoPorComboActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(platoPorComboActual);
        }

        [HttpDelete]
        [Route("eliminarp/{PlatosPorComboID}")]
        public IActionResult EliminarPlatoPorCombo(int id)
        {
            PlatosPorCombo? platoPorCombo = _parcialContexto.PlatosPorCombo.FirstOrDefault(p => p.PlatosPorComboID == id);
            
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
