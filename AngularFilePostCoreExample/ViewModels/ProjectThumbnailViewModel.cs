using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.ViewModels
{
    public class ProjectThumbnailViewModel
    {
        public int ProjectId { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
