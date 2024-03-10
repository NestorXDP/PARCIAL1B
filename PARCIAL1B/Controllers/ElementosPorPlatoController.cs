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
    public class ElementosPorPlatoController : ControllerBase
    {
        private readonly parcialContexto _parcialContexto;

        public ElementosPorPlatoController(parcialContexto parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ElementosPorPlato> listadoElementosPorPlato = _parcialContexto.ElementosPorPlato.ToList();

            if (listadoElementosPorPlato.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoElementosPorPlato);
        }

        //BUSCAR POR ID
        [HttpGet]
        [Route("GetById/{empresaId}/{platoId}/{elementoId}")]
        public IActionResult GetById(int empresaId, int platoId, int elementoId)
        {
            ElementosPorPlato elementoPorPlato = _parcialContexto.ElementosPorPlato.FirstOrDefault(e => e.EmpresaId == empresaId && e.PlatoID == platoId && e.ElementoID == elementoId);

            if (elementoPorPlato == null)
            {
                return NotFound();
            }

            return Ok(elementoPorPlato);
        }

        //AGREGAR

        [HttpPost]
        [Route("add")]
        public IActionResult GuardarElementoPorPlato([FromBody] ElementosPorPlato elementoPorPlato)
        {
            try
            {
                _parcialContexto.ElementosPorPlato.Add(elementoPorPlato);
                _parcialContexto.SaveChanges();

                return Ok(elementoPorPlato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ACTUALIZAR

        [HttpPut]
        [Route("actualizar/{empresaId}/{platoId}/{elementoId}")]
        public ActionResult ActualizarElementoPorPlato(int empresaId, int platoId, int elementoId, [FromBody] ElementosPorPlato elementoPorPlatoModificar)
        {
            ElementosPorPlato elementoPorPlatoActual = _parcialContexto.ElementosPorPlato.FirstOrDefault(e => e.EmpresaId == empresaId && e.PlatoID == platoId && e.ElementoID == elementoId);

            if (elementoPorPlatoActual == null)
            {
                return NotFound();
            }

            elementoPorPlatoActual.Cantidad = elementoPorPlatoModificar.Cantidad;
            elementoPorPlatoActual.Estado = elementoPorPlatoModificar.Estado;

            _parcialContexto.Entry(elementoPorPlatoActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(elementoPorPlatoActual);
        }

        //BORRAR
        [HttpDelete]
        [Route("eliminar/{empresaId}/{platoId}/{elementoId}")]
        public IActionResult EliminarElementoPorPlato(int empresaId, int platoId, int elementoId)
        {
            ElementosPorPlato elementoPorPlato = _parcialContexto.ElementosPorPlato.FirstOrDefault(e => e.EmpresaId == empresaId && e.PlatoID == platoId && e.ElementoID == elementoId);

            if (elementoPorPlato == null)
            {
                return NotFound();
            }

            _parcialContexto.ElementosPorPlato.Remove(elementoPorPlato);
            _parcialContexto.SaveChanges();

            return Ok(elementoPorPlato);
        }
        // FIN DEL CRUD
    }
}
