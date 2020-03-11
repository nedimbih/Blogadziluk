using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class HomeController : ControllerBase {
        public IActionResult Index() {
            return BadRequest();
        }
    }
}