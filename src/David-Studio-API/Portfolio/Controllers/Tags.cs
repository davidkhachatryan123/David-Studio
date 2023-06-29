using System;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class Tags : ControllerBase
    {
        public Tags()
        {

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult GetOk()
        {
            return Ok();
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetOk_v2()
        {
            return Ok();
        }
    }
}

