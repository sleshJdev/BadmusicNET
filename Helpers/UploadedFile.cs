using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Helpers
{
    public class UploadedFile
    {
        public int FileSize { get; set; }        
        public byte[] Contents { get; set; }
    }
}