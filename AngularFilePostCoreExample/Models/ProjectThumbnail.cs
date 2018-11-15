using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Models
{
    public class ProjectThumbnail
    {
        public int ProjectId { get; set; }
        public IFormFile Thumbnail { get; set; }
    }
}
