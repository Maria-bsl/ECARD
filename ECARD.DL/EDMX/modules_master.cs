//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECARD.DL.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class modules_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public modules_master()
        {
            this.activity_master = new HashSet<activity_master>();
        }
    
        public long sno { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string module_status { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<activity_master> activity_master { get; set; }
    }
}
