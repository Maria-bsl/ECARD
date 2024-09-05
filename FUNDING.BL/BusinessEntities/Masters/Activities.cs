using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
   public class Activities
    {
        #region Properties
        public long Sno { get; set; }
        public string Code { get; set; }
        public string MCode { get; set; }
        public long Mod_Sno { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string MStatus { get; set; }


        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddAct(Activities sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                activity_master ps = new activity_master()
                {
                    code = sc.Code,
                    descript = sc.Description,
                    activity_status = sc.Status,
                    msno = sc.Mod_Sno,
                    mname = sc.Name,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.activity_master.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public long GetLastInsertedId()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                return (from x in context.activity_master
                        select x).OrderByDescending(x => x.sno).First().sno;
            }
        }
        public bool ValidateAct(String name, long msno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.activity_master
                                  where (c.descript.ToLower().Equals(name)) && c.msno == msno 
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
                var validation = (from c in context.activity_master
                                  where (c.sno == no)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<Activities> GetAct()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.activity_master
                                join mod in context.modules_master on c.msno equals mod.sno
                                where c.activity_status == "Active" 
                                select new Activities
                                {
                                    Sno = c.sno,
                                    Name = c.mname,
                                    Mod_Sno = (long)c.msno,
                                    MStatus = mod.module_status,
                                    Description = c.descript,
                                    Code = c.code,
                                    MCode = mod.code,
                                    Status = c.activity_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Activities> GetActlist(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.activity_master
                                where c.activity_status == "Active"  && c.msno == sno && c.descript != "Role" && c.descript != "Audit Trails"
                                select new Activities
                                {
                                    Sno = c.sno,
                                    Name = c.mname,
                                    Mod_Sno = (long)c.msno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.activity_status,
                                    Audit_Date = c.posted_date,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Activities editactText1(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.activity_master
                                where c.sno == chsno 
                                select new Activities
                                {
                                    Sno = c.sno,
                                    Name = c.mname,
                                    Mod_Sno = (long)c.msno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.activity_status,
                                    Audit_Date = c.posted_date,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Activities editactText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.activity_master
                                where c.sno == chsno 
                                select new Activities
                                {
                                    Sno = c.sno,
                                    Name = c.mname,
                                    Mod_Sno = (long)c.msno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.activity_status,
                                    Audit_Date = c.posted_date,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public void DeleteAct(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.activity_master
                                   where n.sno == no 
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.activity_master.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateAct(Activities dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.activity_master
                                         where u.sno == dep.Sno
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.descript = dep.Description;
                    UpdateContactInfo.msno = dep.Mod_Sno;
                    UpdateContactInfo.mname = dep.Name;
                    UpdateContactInfo.activity_status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }


        #endregion Methods
    }
}
