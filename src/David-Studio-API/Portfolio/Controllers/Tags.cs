using System;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Tags : ControllerBase
    {
        public Tags()
        {
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public string Get() => ".Net Core Web API Version 1";
    }
}

