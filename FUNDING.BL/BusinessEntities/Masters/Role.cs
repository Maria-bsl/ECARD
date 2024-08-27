using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
   public class Role
    {
        #region Properties
        public long Sno { get; set; }
        public string Code { get; set; }
        public string Admin1 { get; set; }
        public long Inst_reg { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long User_Sno { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long AddRole(Role sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                roles_master ps = new roles_master()
                {
                    //code = sc.Code,
                    descript = sc.Description,
                    role_status = sc.Status,
                   //admin1 = sc.Admin1,
                   // insti_reg_sno = sc.Inst_reg,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.roles_master.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        //public bool ValidateRole(String name, long sno)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var validation = (from c in context.roles_master
        //                          where (c.descript.ToLower().Equals(name)) && c.insti_reg_sno == sno
        //                          select c);
        //        if (validation.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        //public bool ValidateDeletion(string no, long sno)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var validation = (from c in context.institution_users
        //                          where (c.user_type == no && c.insti_reg_sno == sno)
        //                          select c);
        //        var validation1 = (from c in context.arights_master
        //                           where (c.rcode == no && c.insti_reg_sno == sno)
        //                           select c);
        //        if (validation.Count() > 0 && validation1.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        //public long GetLastInsertedId()
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var getsno = context.roles_master.OrderByDescending(x => x.sno).FirstOrDefault().code;
        //        if (getsno != null)
        //            return long.Parse(getsno.ToString());
        //        else
        //            return 0;
        //    }
        //}

        public List<Role> GetRoleunq(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.roles_master
                                where c.role_status == "Active"
                                && c.sno == sno 
                                select new Role
                                {
                                    Sno = c.sno,
                                    // Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Role> GetRole(/*long sno*/)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.roles_master
                                where c.role_status == "Active" /*&& c.insti_reg_sno == sno*/
                                select new Role
                                {
                                    Sno = c.sno,
                                   // Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Role> GetRole2(/*long sno*/)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.roles_master
                                where c.role_status == "Active" /*&& c.insti_reg_sno == sno*/
                                select new Role
                                {
                                    Sno = c.sno,
                                    //Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Role> GetRole1()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.roles_master /*join chu in context.institution_users on c.code equals chu.user_type*/
                                where c.role_status == "Active" /*&& c.insti_reg_sno == snox && c.admin1 != "Y" GetRolesAct1 */
                                select new Role
                                {
                                    Sno = c.sno,
                                   // Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Admin1 = c.admin1,
                                    //   User_Sno = chu.insti_users_sno,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                }).OrderBy(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Role editroleText(long chsno/*, long isno*/)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.roles_master
                                where c.sno == chsno /*&& c.insti_reg_sno == isno*/
                                select new Role
                                {
                                    Sno = c.sno,
                                   // Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                    Admin1 = c.admin1,
                                    AuditBy = c.posted_by

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Role editroleText1(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.roles_master
                                where c.sno == chsno
                                select new Role
                                {
                                    Sno = c.sno,
                                    //Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Role editroleText2(string chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.roles_master
                                where c.code == chsno
                                select new Role
                                {
                                    Sno = c.sno,
                                   // Inst_reg = (long)c.insti_reg_sno,
                                    Description = c.descript,
                                    Code = c.code,
                                    Status = c.role_status,
                                    Audit_Date = c.posted_date,
                                    AuditBy = c.posted_by
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void Deleterole(string no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.roles_master
                                   where n.code == no 
                                   select n).First();
                if (noteDetails != null)
                {
                    context.roles_master.Remove(noteDetails);
                    context.SaveChanges();
                }
            }
        }
        public void UpdateRole(Role dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.roles_master
                                         where u.sno == dep.Sno
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.descript = dep.Description;
                    UpdateContactInfo.role_status = dep.Status;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        #endregion Methods
   }
}
