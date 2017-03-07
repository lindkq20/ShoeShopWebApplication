using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopWebApplication.Models
{
    public class ResponseStores
    {
        public object stores { get; set; }
        public bool success { get; set; }
        public int total_elements { get; set; }
    }
}