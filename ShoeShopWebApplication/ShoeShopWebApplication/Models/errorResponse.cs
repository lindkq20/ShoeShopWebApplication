using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ShoeShopWebApplication.Models
{
    public class errorResponse
    {
        public const string error400 = "Bad request";
        public const string error401 = "Not authorized";
        public const string error404 = "Record not found";
        public const string error500 = "Server Error";
        public string error_msg { get; set; }
        public HttpStatusCode error_code { get; set; }
        public bool success { get; set; }
    }
}