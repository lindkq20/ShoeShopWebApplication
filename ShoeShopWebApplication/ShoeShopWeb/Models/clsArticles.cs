using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShopWeb.Models
{
    public class clsArticles
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]       
        public string name { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string description { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<int> total_in_shelf { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<int> total_in_vault { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public Nullable<int> store_id { get; set; }
    }
}