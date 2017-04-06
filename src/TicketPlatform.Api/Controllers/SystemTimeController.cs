using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TicketPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    public class SystemTimeController : Controller
    {
        [HttpGet]
        public DateTime Get() => DateTime.Now;
    }
}
