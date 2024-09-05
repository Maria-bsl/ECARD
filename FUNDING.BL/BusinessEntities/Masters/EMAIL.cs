using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class EMAIL
    {
        #region Properties
        public long SNO { get; set; }
        public string Flow_Id { get; set; }
        public string Email_Text { get; set; }
        public string Local_Text { get; set; }
        public string Subject { get; set; }
        public string Local_subject { get; set; }
        public DateTime? Effective_Date { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion Properties
        #region Methods
        public long AddEMAIL(EMAIL sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                email_text ps = new email_text()
                {
                    flow_id = sc.Flow_Id,
                    email_text1 = sc.Email_Text,
                    email_text_local=sc.Local_Text,
                    email_sub=sc.Subject,
                    email_sub_local=sc.Local_subject,
                    effective_date = DateTime.Now,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.email_text.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public bool ValidateEMAIL(String id)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.email_text
                                  where (c.flow_id ==id)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<EMAIL> GetEMAIL()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.email_text
                                select new EMAIL
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    Email_Text = c.email_text1,
                                    Local_Text=c.email_text_local,
                                    Subject=c.email_sub,
                                    Local_subject=c.email_sub_local,
                                    Effective_Date = (DateTime)c.effective_date
                                }).OrderByDescending(z=>z.Effective_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public EMAIL getEMAILText()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.email_text
                                orderby c.effective_date descending
                                select new EMAIL
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    Email_Text = c.email_text1,
                                    Local_Text = c.email_text_local,
                                    Subject = c.email_sub,
                                    Local_subject = Local_subject,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMAIL getEMAILst(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.email_text.Where(c=>c.flow_id==name)
                                orderby c.effective_date descending
                                select new EMAIL
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    Email_Text = c.email_text1,
                                    Local_Text = c.email_text_local,
                                    Subject = c.email_sub,
                                    Local_subject = Local_subject,
                                    Effective_Date = (DateTime)c.effective_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMAIL EditEMAIL(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.email_text
                                where c.sno == sno
                                select new EMAIL
                                {
                                    SNO = c.sno,
                                    Flow_Id = c.flow_id,
                                    Email_Text = c.email_text1,
                                    Local_Text = c.email_text_local,
                                    Subject = c.email_sub,
                                    Local_subject = Local_subject,
                                    Effective_Date = (DateTime)c.effective_date,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteEMAIL(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.email_text
                                   where n.sno == no
                                   select n).First();
                if (noteDetails != null)
                {
                    context.email_text.Remove(noteDetails);
                    context.SaveChanges();
                }
            }
        }
        public void UpdateEMAIL(EMAIL dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.email_text
                                         where u.sno == dep.SNO
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flow_id = dep.Flow_Id;
                    UpdateContactInfo.email_text1 = dep.Email_Text;
                    UpdateContactInfo.email_text_local = dep.Local_Text;
                    UpdateContactInfo.email_sub = dep.Subject;
                    UpdateContactInfo.email_sub_local = dep.Local_subject;
                    UpdateContactInfo.effective_date = DateTime.Now;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        //public EMAIL Getmailsch(String sno)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var edetailsch = (from c in context.email_text
        //                          join i in context.emp_detail on c.email_sub equals i.emp_detail_id.ToString()
        //                          where c.email_sub == sno && i.emp_status == "Approved" && i.emp_status == "Active"
        //                          select new EMAIL
        //                          {
        //                              use = c.insti_users_sno,
        //                              Q_Name = c.user_fullname,
        //                              User_Name = c.username,
        //                              Password = c.password,
        //                          }).FirstOrDefault();
        //        if (edetailsch != null)
        //            return edetailsch;
        //        else
        //            return null;
        //    }
        //}
        #endregion Methods
    }
}




