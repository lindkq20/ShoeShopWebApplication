using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ShoeShopWebApplication.Models
{
    public class ResponseArticles
    {
        public object articles { get; set; }
        public bool success { get; set; }
        public int total_elements { get; set; }
    }
}