using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1B.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;

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
        [Route("GetAll")]
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
        [Route("GetById/{ElementoPorPlatoID}")]
        public IActionResult GetById(int id)
        {
            ElementosPorPlato? elementoPorPlato = _parcialContexto.ElementosPorPlato.FirstOrDefault(e => e.EmpresaId == id);

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
        [Route("Actualizar/{ElementoPorPlatoID}")]
        public IActionResult actualizarElementosPorPlato(int id, [FromBody] ElementosPorPlato elementosPorPlatoModiicar)
        {
            ElementosPorPlato? elementoPorPlatoActual = (from e in _parcialContexto.ElementosPorPlato where e.ElementoPorPlatoID == id select e).FirstOrDefault();

            if (elementoPorPlatoActual == null)
            {
                return NotFound();
            }

            elementoPorPlatoActual.EmpresaId = elementosPorPlatoModiicar.EmpresaId;
            elementoPorPlatoActual.PlatoID = elementosPorPlatoModiicar.PlatoID;
            elementoPorPlatoActual.ElementoID = elementosPorPlatoModiicar.ElementoID;
            elementoPorPlatoActual.Cantidad = elementosPorPlatoModiicar.Cantidad;
            elementoPorPlatoActual.Estado = elementosPorPlatoModiicar.Estado;

            _parcialContexto.Entry(elementoPorPlatoActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(elementoPorPlatoActual);
        }

        

        //BORRAR
        [HttpDelete]
        [Route("eliminar/{ElementoPorPlatoID}")]
        public IActionResult EliminarElemento(int id)
        {
            ElementosPorPlato? elementoPorPlato = (from e in _parcialContexto.ElementosPorPlato where e.ElementoPorPlatoID == id select e).FirstOrDefault();

            if (elementoPorPlato == null)
            {
                return NotFound();
            }
            _parcialContexto.ElementosPorPlato.Attach(elementoPorPlato);
            _parcialContexto.ElementosPorPlato.Remove(elementoPorPlato);
            _parcialContexto.SaveChanges();

            return Ok(elementoPorPlato);
        }
        // FIN DEL CRUD
    }
}
