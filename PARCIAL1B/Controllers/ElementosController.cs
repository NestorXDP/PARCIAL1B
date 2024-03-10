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
    public class ElementosController : ControllerBase
    {
            private readonly parcialContexto _parcialContexto;

            public ElementosController(parcialContexto parcialContexto)
            {
                _parcialContexto = parcialContexto;
            }

            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                List<Elementos> listadoElementos = _parcialContexto.Elementos.ToList();

                if (listadoElementos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(listadoElementos);
            }

            [HttpGet]
            [Route("GetById/{id}")]
            public IActionResult GetById(int id)
            {
                Elementos? elemento = _parcialContexto.Elementos.FirstOrDefault(e => e.EmpresaID == id);

                if (elemento == null)
                {
                    return NotFound();
                }

                return Ok(elemento);
            }

            [HttpGet]
            [Route("Find/{filtro}")]
            public IActionResult FindByElement(string filtro)
            {
                Elementos? elemento = _parcialContexto.Elementos.FirstOrDefault(e => e.Elemento.Contains(filtro));

                if (elemento == null)
                {
                    return NotFound();
                }

                return Ok(elemento);
            }

            [HttpPost]
            [Route("Agregar")]
            public IActionResult GuardarElemento([FromBody] Elementos elemento)
            {
                try
                {
                    _parcialContexto.Elementos.Add(elemento);
                    _parcialContexto.SaveChanges();

                    return Ok(elemento);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut]
            [Route("actualizar/{id}")]
            public ActionResult ActualizarElemento(int id, [FromBody] Elementos elementoModificar)
            {
                Elementos? elementoActual = _parcialContexto.Elementos.FirstOrDefault(e => e.EmpresaID == id);

                if (elementoActual == null)
                {
                    return NotFound();
                }

                elementoActual.Elemento = elementoModificar.Elemento;
                elementoActual.CantidadMinima = elementoModificar.CantidadMinima;
                elementoActual.UnidadMedida = elementoModificar.UnidadMedida;
                elementoActual.Costo = elementoModificar.Costo;
                elementoActual.Estado = elementoModificar.Estado;

                _parcialContexto.Entry(elementoActual).State = EntityState.Modified;
                _parcialContexto.SaveChanges();

                return Ok(elementoActual);
            }

            [HttpDelete]
            [Route("eliminar/{id}")]
            public IActionResult EliminarElemento(int id)
            {
                Elementos? elemento = _parcialContexto.Elementos.FirstOrDefault(e => e.ElementoID == id);

                if (elemento == null)
                {
                    return NotFound();
                }

                _parcialContexto.Elementos.Remove(elemento);
                _parcialContexto.SaveChanges();

                return Ok(elemento);
            }
        [HttpGet]
        [Route("Find2/{buscar}")]
        public IActionResult BuscarPlato(string buscar)
        {
            var listadoPlato = (from e in _parcialContexto.Elementos
                                 join t in _parcialContexto.ElementosPorPlato
                                     on e.ElementoID equals t.ElementoID
                                 join m in _parcialContexto.Platos
                                     on t.PlatoID equals m.PlatoID

                                where m.NombrePlato.Contains(buscar)

                                 select new
                                 {
                                     e.ElementoID,
                                     e.EmpresaID,
                                     e.Elemento,
                                     e.CantidadMinima,
                                     e.UnidadMedida,
                                     e.Costo,
                                     e.Estado,
                                     plato_id = m.PlatoID,
                                     nombre_plato = m.NombrePlato
                                 }).ToList();

            if (listadoPlato == null)
            {
                return NotFound();
            }

            return Ok(listadoPlato);
        }

        // FIN DEL CRUD
    }
    }


