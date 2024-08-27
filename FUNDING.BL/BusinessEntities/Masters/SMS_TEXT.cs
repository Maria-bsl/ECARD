using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
  public  class SMS_TEXT
    {
        #region Properties
        public long SNO { get; set; }
        public string Flow_Id { get; set; }
        public string SMS_Text { get; set; }
        public string SMS_Subject { get; set; }
        public string SMS_Local { get; set; }
        public string SMS_Other { get; set; }

        public DateTime? Effective_Date { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }

        #endregion Properties
        #region Methods
        public long AddSMS(SMS_TEXT sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                sms_text ps = new sms_text()
                {
                    flow_id = sc.Flow_Id,
                    sms_text1 = sc.SMS_Text,
                    sms_sub = sc.SMS_Subject,
                    sms_sub_local = sc.SMS_Local,
                    sms_text_other = sc.SMS_Other,
                    effective_date = DateTime.Now,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.sms_text.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public bool ValidateSMS(string mail)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.sms_text
                                  where (c.sms_text1 == mail)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<SMS_TEXT> GetSMS()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.sms_text
                                select new SMS_TEXT
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    SMS_Text = c.sms_text1,
                                    SMS_Subject = c.sms_sub,
                                    SMS_Local = c.sms_sub_local,
                                    SMS_Other = c.sms_text_other,
                                    Effective_Date = (DateTime)c.effective_date
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public SMS_TEXT getSMSText()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.sms_text
                                orderby c.effective_date descending
                                select new SMS_TEXT
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    SMS_Text = c.sms_text1,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public SMS_TEXT getSMSLst(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.sms_text
                                where c.flow_id == sno.ToString()
                                select new SMS_TEXT
                                {
                                    Flow_Id = c.flow_id,
                                    SMS_Text = c.sms_text1,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public SMS_TEXT EditSMS(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.sms_text
                                where c.sno == sno

                                select new SMS_TEXT
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    SMS_Text = c.sms_text1,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteSMS(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.sms_text
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.sms_text.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateSMS(SMS_TEXT dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.sms_text
                                         where u.sno == dep.SNO
                                         select u).FirstOrDefault(); 
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flow_id = dep.Flow_Id;
                    UpdateContactInfo.sms_text1 = dep.SMS_Text;
                    UpdateContactInfo.sms_sub = dep.SMS_Subject;
                    UpdateContactInfo.sms_sub_local = dep.SMS_Local;
                    UpdateContactInfo.sms_text_other = dep.SMS_Other;
                    UpdateContactInfo.effective_date = DateTime.Now;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
