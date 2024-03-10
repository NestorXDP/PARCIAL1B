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
    public class Platos2Controller : ControllerBase
        {
            private readonly parcialContexto _parcialContexto;

            public Platos2Controller(parcialContexto parcialContexto)
            {
                _parcialContexto = parcialContexto;
            }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
            {
                List<Platos> listadoPlatos = (from p in _parcialContexto.Platos
                                               select p).ToList();

                if (listadoPlatos.Count() == 0)
                {
                    return NotFound();

                }
                return Ok(listadoPlatos);
            }

            [HttpGet]
            [Route("GetById/{id}")]
            public IActionResult GetById(int id)
            {
                Platos? plato = _parcialContexto.Platos.FirstOrDefault(e => e.PlatoID == id);

                if (plato == null)
                {
                    return NotFound();
                }

                return Ok(plato);
            }

            //BUSCAR POR DESCRIPCION 
            [HttpGet]
            [Route("Find/{filtro}")]
            public IActionResult FindByDescription(string filtro)
            {
                Platos? plato = _parcialContexto.Platos.FirstOrDefault(e => e.DescripcionPlato.Contains(filtro));

                if (plato == null)
                {
                    return NotFound();
                }

                return Ok(plato);
            }

            //crear

            [HttpPost]
            [Route("add")]
            public IActionResult GuardarPlato([FromBody] Platos plato)
            {
                try
                {
                    _parcialContexto.Platos.Add(plato);
                    _parcialContexto.SaveChanges();

                    return Ok(plato);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            //modificar

            [HttpPut]
            [Route("actualizar/{id}")]
            public ActionResult ActualizarPlato(int id, [FromBody] Platos platoModificar)
            {
                Platos? platoActual = (from p in _parcialContexto.Platos where p.PlatoID == id select p).FirstOrDefault();

                if (platoActual == null)
                {
                    return NotFound();
                }
                platoActual.EmpresaID = platoModificar.EmpresaID;
                platoActual.GrupoID = platoModificar.GrupoID;
                platoActual.NombrePlato = platoModificar.NombrePlato;
                platoActual.DescripcionPlato = platoModificar.DescripcionPlato;
                platoActual.Precio = platoModificar.Precio;
                

                _parcialContexto.Entry(platoActual).State = EntityState.Modified;

                _parcialContexto.SaveChanges();

                return Ok(platoActual);
            }

            [HttpDelete]
            [Route("eliminar/{id}")]
            public IActionResult EliminarPlato(int id)
            {
                Platos? plato = _parcialContexto.Platos.FirstOrDefault(p => p.PlatoID == id);

                if (plato == null)
                {
                    return NotFound();
                }

                _parcialContexto.Platos.Remove(plato);
                _parcialContexto.SaveChanges();

                return Ok(plato);
            }
        // FIN DEL CRUD
        [HttpGet]
        [Route("Find2/{buscar}")]
        public IActionResult BuscarPlato(string buscar)
        {
            var listadoPlato = (from e in _parcialContexto.Elementos
                                join t in _parcialContexto.ElementosPorPlato
                                    on e.ElementoID equals t.ElementoID
                                join m in _parcialContexto.Platos
                                    on t.PlatoID equals m.PlatoID

                                where e.Elemento.Contains(buscar)

                                select new
                                {
                                    m.PlatoID,
                                    m.EmpresaID,
                                    m.GrupoID,
                                    m.NombrePlato,
                                    m.DescripcionPlato,
                                    m.Precio,
                                    elemento_id = e.ElementoID,
                                    elemento_nombre = e.Elemento
                             
                                    
                                }).ToList();

            if (listadoPlato == null)
            {
                return NotFound();
            }

            return Ok(listadoPlato);
        }
    }
}

