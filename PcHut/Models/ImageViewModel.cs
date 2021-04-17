using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PcHut.Models;

namespace PcHut.Models
{
    public class ImageViewModel : product
    {
        public HttpPostedFileBase ProductPic { get; set; }
    }
}