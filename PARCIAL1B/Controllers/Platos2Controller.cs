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
        [Route("api/[controller]")]
        [ApiController]
        public class platosController : ControllerBase
        {
            private readonly parcialContexto _parcialContexto;

            public platosController(parcialContexto parcialContexto)
            {
                _parcialContexto = parcialContexto;
            }

            ////
        }
    }
}
