using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Arights
    {
        #region Properties
        public long Sno { get; set; }
        public string RCode { get; set; }
        public string RDesc { get; set; }
        public long? Emp_Id { get; set; }
        public string Radmin { get; set; }
        public string Role_Status { get; set; }
        public string Usernaem { get; set; }
        public string MCode { get; set; }
        public string MName { get; set; }
        public string Module_Status { get; set; }
        public string Acode { get; set; }
        public string Adescrip { get; set; }
        public string AAdd { get; set; }
        public string Aview { get; set; }
        public string ASearch { get; set; }
        public string AUpdate { get; set; }
        public string ADelete { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        #endregion properties
        #region methods
        public long Validatescreen(long id, string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var getdet = (from c in context.emp_detail
                              join det in context.roles_master on c.desg_id equals det.sno
                              where c.emp_detail_id == id
                              select det).FirstOrDefault();
                if (getdet.descript != "Admin")
                    return context.arights_master.Count(c => c.rcode == getdet.code && c.mname == name && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
                else
                    return 1;
            }
        }
        public long checkact(string name, long id)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var getdet = (from c in context.emp_detail
                              join det in context.roles_master on c.desg_id equals det.sno
                              where c.emp_detail_id == id
                              select det).FirstOrDefault();
                if (getdet.descript != "Admin")
                    return context.arights_master.Count(c => c.adescript == name && c.rcode == getdet.code && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
                else
                    return 1;
            }

        }
        public long checkact(string name, long id, string rdesc, string rcode)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                if (rdesc != "Admin")
                    return context.arights_master.Count(c => c.adescript == name && c.rcode == rcode && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
                else
                    return 1;

            }

        }
        public long AddAright(Arights sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                arights_master ps = new arights_master()
                {
                    rcode = sc.RCode,
                    rdesc = sc.RDesc,
                    radmin = sc.Radmin,
                    role_status = sc.Role_Status,
                    emp_detail_id = 0,
                    usernaem = null,
                    mcode = sc.MCode,
                    mname = sc.MName,
                    module_status = sc.Module_Status,
                    acode = sc.Acode,
                    adescript = sc.Adescrip,
                    aadd = sc.AAdd,
                    aview = sc.Aview,
                    asearch = sc.ASearch,
                    aupdate = sc.AUpdate,
                    adelete = sc.ADelete,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                };
                context.arights_master.Add(ps);
                context.SaveChanges();
                return ps.sno;
            }
        }
        public List<Arights> GetAright1(string mcode, string rcode)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.arights_master
                                where  c.mcode == mcode && c.rcode == rcode
                                select new Arights
                                {
                                    Sno = c.sno,
                                    RCode = c.rcode,
                                    RDesc = c.rdesc,
                                    Radmin = c.radmin,
                                    Role_Status = c.role_status,
                                    MCode = c.mcode,
                                    MName = c.mname,
                                    Module_Status = c.module_status,
                                    Acode = c.acode,
                                    Adescrip = c.adescript,
                                    AAdd = c.aadd,
                                    Aview = c.aview,
                                    ASearch = c.asearch,
                                    AUpdate = c.aupdate,
                                    ADelete = c.adelete,
                                    AuditBy = c.posted_by,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Arights> GetAright()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.arights_master
                                select new Arights
                                {
                                    Sno = c.sno,
                                    RCode = c.rcode,
                                    RDesc = c.rdesc,
                                    Radmin = c.radmin,
                                    Role_Status = c.role_status,
                                    Emp_Id = (long)c.emp_detail_id,
                                    Usernaem = c.usernaem,
                                    MCode = c.mcode,
                                    MName = c.mname,
                                    Module_Status = c.module_status,
                                    Acode = c.acode,
                                    Adescrip = c.adescript,
                                    AAdd = c.aadd,
                                    Aview = c.aview,
                                    ASearch = c.asearch,
                                    AUpdate = c.aupdate,
                                    ADelete = c.adelete,
                                    AuditBy = c.posted_by,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public Arights GetAright2(string name,long chsno/*, long usno, */)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var getdet = (from c in context.emp_detail
                              join det in context.roles_master on c.desg_id equals det.sno where c.emp_detail_id == chsno  select det.code).FirstOrDefault();
                var edetails = (from c in context.arights_master
                                where c.rcode == getdet && c.adescript == name
                                select new Arights
                                {
                                    Sno = c.sno,
                                    RCode = c.rcode,
                                    RDesc = c.rdesc,
                                    Radmin = c.radmin,
                                    Role_Status = c.role_status,
                                    Emp_Id = (long)c.emp_detail_id,
                                    Usernaem = c.usernaem,
                                    MCode = c.mcode,
                                    MName = c.mname,
                                    Module_Status = c.module_status,
                                    Acode = c.acode,
                                    Adescrip = c.adescript,
                                    AAdd = c.aadd,
                                    Aview = c.aview,
                                    ASearch = c.asearch,
                                    AUpdate = c.aupdate,
                                    ADelete = c.adelete,
                                    AuditBy = c.posted_by,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public long Validatescreen(String name)//check whether it is facility_users and faci_users_sno
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var getdet = (from c in context.activity_master
                              select c);
                return context.arights_master.Count(c => c.mname == name && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
            }
        }
        public long getaccess( long usno, string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                return context.arights_master.Count(c => c.emp_detail_id == usno && c.mname == name && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
            }
        }
        public bool check(string name, String mn)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.arights_master
                                  where ( c.rdesc == name && c.mname == mn && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"))
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public long checkact(string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var getdet = (from c in context.activity_master
                              select c);
                              /*join det in context.roles_master on c.user_type equals det.code select det.code).FirstOrDefault()*/
                return context.arights_master.Count(c => c.adescript == name && (c.aadd == "Y" || c.adelete == "Y" || c.aupdate == "Y" || c.aview == "Y"));
            }
        }
        public void UpdateAct(Arights sc)
        {
            if (sc.Sno != 0)
            {
                sc.Emp_Id = 0;
                using (ECARDAPPEntities context = new ECARDAPPEntities())
                {
                    var UpdateContactInfo = (from u in context.arights_master
                                             where u.sno == sc.Sno
                                             select u).FirstOrDefault();
                    if (UpdateContactInfo != null)
                    {
                        UpdateContactInfo.rcode = sc.RCode;
                        UpdateContactInfo.rdesc = sc.RDesc;
                        UpdateContactInfo.radmin = sc.Radmin;
                        UpdateContactInfo.role_status = sc.Role_Status;
                        UpdateContactInfo.emp_detail_id = sc.Emp_Id;
                        UpdateContactInfo.usernaem = null;
                        UpdateContactInfo.mcode = sc.MCode;
                        UpdateContactInfo.mname = sc.MName;
                        UpdateContactInfo.module_status = sc.Module_Status;
                        UpdateContactInfo.acode = sc.Acode;
                        UpdateContactInfo.adescript = sc.Adescrip;
                        UpdateContactInfo.aadd = sc.AAdd;
                        UpdateContactInfo.aview = sc.Aview;
                        UpdateContactInfo.asearch = sc.ASearch;
                        UpdateContactInfo.aupdate = sc.AUpdate;
                        UpdateContactInfo.adelete = sc.ADelete;
                        UpdateContactInfo.posted_by = sc.AuditBy;
                        UpdateContactInfo.posted_date = DateTime.Now;
                        context.SaveChanges();
                    }
                }
            }
        }
        public Arights Editscreen(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.arights_master
                                where c.sno == sno 
                                select new Arights
                                {
                                    Sno = c.sno,
                                    RCode = c.rcode,
                                    RDesc = c.rdesc,
                                    Radmin = c.radmin,
                                    Role_Status = c.role_status,
                                    Emp_Id = (long)c.emp_detail_id,
                                    Usernaem = c.usernaem,
                                    MCode = c.mcode,
                                    MName = c.mname,
                                    Module_Status = c.module_status,
                                    Acode = c.acode,
                                    Adescrip = c.adescript,
                                    AAdd = c.aadd,
                                    Aview = c.aview,
                                    ASearch = c.asearch,
                                    AUpdate = c.aupdate,
                                    ADelete = c.adelete,
                                    AuditBy = c.posted_by,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        #endregion
    }
}

