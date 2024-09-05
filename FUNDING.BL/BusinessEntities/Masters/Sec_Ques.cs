using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Sec_Ques
    {
        #region Properties
        public long SNO { get; set; }
        public string Question_Name { get; set; }
        public string Status { get; set; }
        public string Posted_By { get; set; }
        public DateTime Posted_Date { get; set; }
        #endregion Properties
        #region Methods   
        public long AddSecQues(Sec_Ques sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //reg_question ps = new reg_question()
                //{
                //    q_name = sc.Question_Name,
                //    q_status = sc.Status,
                //    posted_by = sc.Posted_By,
                //    posted_date = DateTime.Now,
                //};
                //context.reg_question.Add(ps);
                //context.SaveChanges();
                //return ps.sno;
                return 5;
            }
        }
        public List<Sec_Ques> GetSecQ()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var adetails = (from c in context.reg_question.Where(c => c.q_status == "Active")
                //                select new Sec_Ques
                //                {
                //                    SNO = c.sno,
                //                    Question_Name = c.q_name,
                //                    Status = c.q_status
                //                }).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails;
                //else
                    return null;
            }
        }
        public bool ValidateSecQ(string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var validation = (from c in context.reg_question
                //                  where (c.q_name.ToLower().Equals(name))
                //                  select c);
                //if (validation.Count() > 0)
                //    return true;
                //else
                    return false;
            }
        }
        public bool ValidateSecQDel(string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var validation = (from c in context.reg_question
                //                  where (c.q_name.ToLower().Equals(name))
                //                  select c);
                //if (validation.Count() > 0)
                //    return true;
                //else
                    return false;
            }
        }        
        public bool Validateupdatecheckcont(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var validationUpdate = (from v in context.reg_question
                //                          join a in context.facility_registration on v.sno equals a.sno
                //                          where a.sno == no
                //                          select v);
                //var validationUpdate1 = (from v in context.reg_question
                //                           join a in context.facility_users on v.sno equals a.sno
                //                           where a.sno == no
                //                           select v);
                //var validationUpdate2 = (from v in context.reg_question
                //                         join a in context.emp_detail on v.sno equals a.sno
                //                         where a.sno == no
                //                         select v);
                //if (/*validationUpdate.Count() != 0 || validationUpdate1.Count() != 0 ||*/ validationUpdate2.Count() !=0)
                //    return true;
                //else
                    return false;
            }
        }
        //public bool CheckQues(long no)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var validation = (from c in context.facility_registration
        //                          where (c.sno == no)
        //                          select c);
        //        var validationch = (from c in context.facility_users
        //                            where (c.sno == no)
        //                            select c);
        //        if (validation.Count() > 0 || validationch.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        public List<Sec_Ques> GETQuestions()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var adetails = (from c in context.reg_question
                //                select new Sec_Ques
                //                {
                //                    SNO = c.sno,
                //                    Question_Name = c.q_name,
                //                    Status = c.q_status,
                //                    Posted_By = c.posted_by,
                //                    Posted_Date = (DateTime)c.posted_date
                //                }).OrderByDescending(z => z.Posted_Date).ToList();
                //if (adetails != null && adetails.Count > 0)
                //    return adetails;
                //else
                    return null;
            }
        }
        public Sec_Ques GetSecQText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var edetails = (from c in context.reg_question
                //                where c.sno == chsno
                //                select new Sec_Ques
                //                {
                //                    SNO = c.sno,
                //                    Question_Name = c.q_name,
                //                    Status = c.q_status
                //                }).FirstOrDefault();
                //if (edetails != null)
                //    return edetails;
                //else
                    return null;
            }
        }
        public Sec_Ques EditSecQ(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var edetails = (from c in context.reg_question
                //                where c.sno == sno
                //                select new Sec_Ques
                //                {
                //                    SNO = c.sno,
                //                    Question_Name = c.q_name,
                //                    Status = c.q_status
                //                }).FirstOrDefault();
                //if (edetails != null)
                //    return edetails;
                //else
                    return null;
            }
        }
        public void DeleteSecQ(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var noteDetails = (from n in context.reg_question
                //                   where n.sno == no
                //                   select n).First();
                //if (noteDetails != null)
                //{                  
                //    context.reg_question.Remove(noteDetails);
                //    context.SaveChanges();
                //}
            }
        }
        public void UpdateSecQ(Sec_Ques dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var UpdateContactInfo = (from u in context.reg_question
                //                         where u.sno == dep.SNO
                //                         select u).FirstOrDefault();
                //if (UpdateContactInfo != null)
                //{
                //    UpdateContactInfo.q_name = dep.Question_Name;
                //    UpdateContactInfo.q_status = dep.Status;
                //    UpdateContactInfo.posted_by = dep.Posted_By;
                //    UpdateContactInfo.posted_date = DateTime.Now;
                //    context.SaveChanges();
                //}
            }
        }     
        #endregion Methods
    }
}

     