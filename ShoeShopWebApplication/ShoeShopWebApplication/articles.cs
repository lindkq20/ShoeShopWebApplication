//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoeShopWebApplication
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class articles
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<int> total_in_shelf { get; set; }
        public Nullable<int> total_in_vault { get; set; }
        public Nullable<int> store_id { get; set; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual stores stores { get; set; }
    }
}
