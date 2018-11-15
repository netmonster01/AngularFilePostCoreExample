using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularFilePostCoreExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularFilePostCoreExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        [HttpPost]
        [Route("UploadThubmnail")]
        public IActionResult UploadThunbnail(ProjectThumbnail model)
        {

            return Ok();
        }
    }
}