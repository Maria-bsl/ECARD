using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
  public class Modules
    {
        #region Properties
        public long Sno { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddModule(Modules sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                modules_master ps = new modules_master()
                {
                    code = sc.Code,
                    name = sc.Description,
                    module_status = sc.Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.modules_master.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public bool ValidateModule(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.modules_master
                                  where (c.name.ToLower().Equals(name)) 
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public long GetLastInsertedId()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                return (from x in context.modules_master
                        select x).OrderByDescending(x => x.sno).First().sno;
            }
        }
        public bool ValidateDeletion(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.modules_master
                                  where (c.sno == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<Modules> GetModule(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.modules_master
                                where c.module_status == "Active" 
                                select new Modules
                                {
                                    Sno = c.sno,
                                    Description = c.name,
                                    Code = c.code,
                                    Status = c.module_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Description).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Modules> GetModule1()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.modules_master
                                where c.module_status == "Active" && c.name != "Access Rights" && c.name != "Audit Trails" && c.name != "Zblock"
                                select new Modules
                                {
                                    Sno = c.sno,
                                    Description = c.name,
                                    Code = c.code,
                                    Status = c.module_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Sno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Modules editmoduleText1(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.modules_master
                                where c.sno == chsno 
                                select new Modules
                                {
                                    Sno = c.sno,
                                    Description = c.name,
                                    Code = c.code,
                                    Status = c.module_status,
                                    Audit_Date = c.posted_date,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Modules editmoduleText(long chsno, long isno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.modules_master
                                where c.sno == chsno 
                                select new Modules
                                {
                                    Sno = c.sno,
                                    Description = c.name,
                                    Code = c.code,
                                    Status = c.module_status,
                                    Audit_Date = c.posted_date,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public void Deletemodule(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.modules_master
                                   where n.sno == no 
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.modules_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateModule(Modules dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.modules_master
                                         where u.sno == dep.Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.name = dep.Description;
                    UpdateContactInfo.module_status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
