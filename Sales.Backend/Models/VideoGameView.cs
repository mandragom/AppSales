using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sales.Common.Models;

namespace Sales.Backend.Models
{
    public class VideoGameView : VideoGames
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}