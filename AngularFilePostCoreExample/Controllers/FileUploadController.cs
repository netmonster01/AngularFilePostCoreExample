using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngularFilePostCoreExample.Models;
using AngularFilePostCoreExample.ViewModels;
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
        public async Task<IActionResult> UploadThunbnailAsync([FromForm] ProjectThumbnailViewModel model)
        {
            //var a = Request.Form.FormData["model"];
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var project = new ProjectThumbnail
                {
                    ProjectId = model.ProjectId
                };
                using (var memoryStream = new MemoryStream())
                {
                    await model.AvatarImage.CopyToAsync(memoryStream);
                    project.AvatarImage = memoryStream.ToArray();
                }
            }

            return Ok();
        }
    }
}