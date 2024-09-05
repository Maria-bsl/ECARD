using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class DESIGNATION
    {
        #region Properties
        public long Desg_Id { get; set; }     
        public string Desg_Name { get; set; }
        public string Posted_By { get; set; }
        public DateTime? Posted_Date { get; set; }
        #endregion properties
        #region methods
        public long AddUser(DESIGNATION sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                designation_list ps = new designation_list()
                {
                    desg_name = sc.Desg_Name,
                    posted_by = sc.Posted_By,
                    posted_date = DateTime.Now,
                };
                context.designation_list.Add(ps);
                context.SaveChanges();
                return ps.desg_id;
            }
        }
        public bool ValidateDesignation( String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.designation_list
                                  where ( c.desg_name.ToLower().Equals(name))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateDeletion(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.desg_id == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<DESIGNATION> GetDesignation()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.designation_list
                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                    Posted_Date=c.posted_date,
                                }).OrderByDescending(z=>z.Posted_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public DESIGNATION getDesignationText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.designation_list
                                where c.desg_id == chsno
                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public DESIGNATION Editdesignation(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.designation_list
                                where c.desg_id == sno
                                select new DESIGNATION
                                {
                                    Desg_Id = c.desg_id,
                                    Desg_Name = c.desg_name,
                                    Posted_By = c.posted_by,
                                    Posted_Date = c.posted_date
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void DeleteDesignation(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.designation_list
                                   where n.desg_id == no
                                   select n).First();
                if (noteDetails != null)
                {
                    context.designation_list.Remove(noteDetails);
                    context.SaveChanges();
                }
            }
        }
        public void UpdateDesignation(DESIGNATION dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.designation_list
                                         where u.desg_id == dep.Desg_Id
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.desg_name = dep. Desg_Name;
                    UpdateContactInfo.posted_by = dep.Posted_By;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        #endregion Methods
    }
}

   
