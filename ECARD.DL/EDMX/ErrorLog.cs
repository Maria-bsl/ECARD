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
    
    public partial class ErrorLog
    {
        public int ErrorLogID { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public string RequestURL { get; set; }
        public string deliveryCode { get; set; }
        public string BrowserType { get; set; }
        public Nullable<System.DateTime> AuditDate { get; set; }
    }
}
