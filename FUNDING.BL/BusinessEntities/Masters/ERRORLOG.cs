using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class ERRORLOG
    {
        #region Properties
        public int Error_LogID { get; set; }
        public String Message { get; set; }
        public String Source { get; set; }
        public String Target_Site { get; set; }
        public String Stack_Trace { get; set; }
        public String Inner_Exception { get; set; }
        public String Request_URL { get; set; }
        public String Delivery_Code { get; set; }
        public String Browser_Type { get; set; }
        public DateTime Audit_Date { get; set; }

        #endregion Properties
        #region Methods

        public void AddERROR(ERRORLOG sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                ErrorLog ps = new ErrorLog()
                {
                    Message = sc.Message,
                    Source = sc.Source,
                    TargetSite = sc.Target_Site,
                    StackTrace = sc.Stack_Trace,
                    InnerException = sc.Inner_Exception,
                    RequestURL = sc.Request_URL,
                    deliveryCode =sc.Delivery_Code,
                    BrowserType =sc.Browser_Type,
                    AuditDate = DateTime.Now
                };
                context.ErrorLogs.Add(ps);
                context.SaveChanges();
            }
        }
        //public bool ValidateLicense(string licno, String name, long facino)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var validation = (from c in context.ErrorLogs
        //                          where (c.driver_license_no == licno || c.driver_name == name) && c.facility_reg_sno == facino
        //                          select c);
        //        if (validation.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        public List<ERRORLOG> GetERROR()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.ErrorLogs
                                    //where c.history == null || c.history != "yes"
                                select new ERRORLOG
                                {
                                    Error_LogID = c.ErrorLogID,
                                    Message = c.Message,
                                    Source = c.Source,
                                    Target_Site = c.TargetSite,
                                    Stack_Trace = c.StackTrace,
                                    Inner_Exception = c.InnerException,
                                    Request_URL = c.RequestURL,
                                    Delivery_Code = c.deliveryCode,
                                    Browser_Type = c.BrowserType,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public ERRORLOG getERRORText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.ErrorLogs
                                where c.ErrorLogID == chsno
                                select new ERRORLOG
                                {
                                    Error_LogID = c.ErrorLogID,
                                    Message = c.Message,
                                    Source = c.Source,
                                    Target_Site = c.TargetSite,
                                    Stack_Trace = c.StackTrace,
                                    Inner_Exception = c.InnerException,
                                    Request_URL = c.RequestURL,
                                    Delivery_Code = c.deliveryCode,
                                    Browser_Type = c.BrowserType,


                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public ERRORLOG EditERRORLOG(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.ErrorLogs
                                where c.ErrorLogID == sno

                                select new ERRORLOG
                                {
                                    Error_LogID = c.ErrorLogID,
                                    Message = c.Message,
                                    Source = c.Source,
                                    Target_Site = c.TargetSite,
                                    Stack_Trace = c.StackTrace,
                                    Inner_Exception = c.InnerException,
                                    Request_URL = c.RequestURL,
                                    Delivery_Code = c.deliveryCode,
                                    Browser_Type = c.BrowserType,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteERRORLOG(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.ErrorLogs
                                   where n.ErrorLogID == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.ErrorLogs.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateERRORLOG(ERRORLOG dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.ErrorLogs
                                         where u.ErrorLogID == dep.Error_LogID
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.Message = dep.Message;
                    UpdateContactInfo.Source = dep.Source;
                    UpdateContactInfo.TargetSite = dep.Target_Site;
                    UpdateContactInfo.StackTrace = dep.Stack_Trace;
                    UpdateContactInfo.InnerException = dep.Inner_Exception;
                    UpdateContactInfo.RequestURL = dep.Request_URL;
                    UpdateContactInfo.deliveryCode = dep.Delivery_Code;
                    UpdateContactInfo.BrowserType = dep.Browser_Type;
                    UpdateContactInfo.AuditDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}

