using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;

namespace FUNDING.BL.BusinessEntities.Masters
{
  public  class SERVIC_ERRLOG
    {

        #region Properties
        public long SNO { get; set; }
        public string Error { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public void AddErr(SERVIC_ERRLOG sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                service_error_logs ps = new service_error_logs()
                {

                    error = sc.Error,
                    posted_date = DateTime.Now,
                };
                context.service_error_logs.Add(ps);
                context.SaveChanges();
            }
        }
        public bool ValidatError(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.service_error_logs
                                  where (c.error == name)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<SERVIC_ERRLOG> Geterror()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.service_error_logs
                                    //where c.history == null || c.history != "yes"
                                select new SERVIC_ERRLOG
                                {
                                    SNO = c.sno,
                                    Error = c.error,

                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public SERVIC_ERRLOG geterrorText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.service_error_logs
                                where c.sno == chsno
                                select new SERVIC_ERRLOG
                                {
                                    SNO = c.sno,
                                    Error = c.error,


                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public SERVIC_ERRLOG Editerror(long msno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.service_error_logs
                                where c.sno == msno

                                select new SERVIC_ERRLOG
                                {
                                    SNO = c.sno,
                                    Error = c.error,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void Deleteerror(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.service_error_logs
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.service_error_logs.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateError(SERVIC_ERRLOG dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.service_error_logs
                                         where u.sno == dep.SNO
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.error = dep.Error;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
