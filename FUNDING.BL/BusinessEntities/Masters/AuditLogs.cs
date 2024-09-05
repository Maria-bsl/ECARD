using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
    public class AuditLogs
    {
        #region properties
        public long Audit_Sno { get; set; }
        public String Audit_Type { get; set; }
        public String Table_Name { get; set; }
        public string Columnsname { get; set; }
        public string Oldvalues { get; set; }
        public string Newvalues { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Audit_Time { get; set; }
        #endregion properties
        #region methods
        public long AddAudit(AuditLogs sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                audit_log ps = new audit_log()
                {
                    audit_type = sc.Audit_Type,
                    table_name = sc.Table_Name,
                    column_name = sc.Columnsname,
                    old_value = sc.Oldvalues,
                    new_value = sc.Newvalues,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                    posted_time = DateTime.Now,
                };
                context.audit_log.Add(ps);
                context.SaveChanges();
                return ps.audit_sno;
            }
        }
        public List<AuditLogs> Getlog(DateTime frm, DateTime to, string tn, string ac)
        {
            //DateTime add = to.AddDays(1);
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from mr in context.audit_log
                                where mr.posted_date >= frm && mr.posted_date <= to && mr.table_name.Contains(tn) &&
                                (ac == "All" ? mr.audit_type == mr.audit_type : mr.audit_type == ac)
                                select new AuditLogs
                                {
                                    Audit_Sno = mr.audit_sno,
                                    Audit_Type = mr.audit_type,
                                    Table_Name = mr.table_name,
                                    Columnsname = mr.column_name,
                                    Oldvalues = mr.old_value,
                                    Newvalues = mr.new_value,
                                    AuditBy = mr.posted_by,
                                    Audit_Date = mr.posted_date,
                                    Audit_Time = mr.posted_time,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<AuditLogs> Getname(string tn,string adtype, int count)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from mr in context.audit_log
                                where mr.table_name.Contains(tn) && mr.audit_type == adtype 
                                select new AuditLogs
                                {
                                    Audit_Sno = mr.audit_sno,
                                    Audit_Type = mr.audit_type,
                                    Table_Name = mr.table_name,
                                    Columnsname = mr.column_name,
                                    Oldvalues = mr.old_value,
                                    Newvalues = mr.new_value,
                                    AuditBy = mr.posted_by,
                                    Audit_Date = mr.posted_date,
                                    Audit_Time = mr.posted_time,
                                }).OrderByDescending(z => z.Audit_Time).Take(count).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<AuditLogs> GetBloglist(DateTime frm, DateTime to, string tn, string ac, String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                if (name == "Bank")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                     v.audit_type == v.audit_type
                                   select new AuditLogs
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
                                      v.audit_type == ac
                                   select new AuditLogs
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.full_name,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                            return det;
                        else
                            return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public List<AuditLogs> Getloglist(DateTime frm, DateTime to, string tn, string ac, String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                if (name == "Bank")
                {
                    if (ac == "All")
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to && v.audit_type == v.audit_type
                                   select new AuditLogs
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.username,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var det = (from v in context.audit_log
                                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
                                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to && v.audit_type == ac
                                   select new AuditLogs
                                   {
                                       Audit_Sno = v.audit_sno,
                                       Audit_Type = v.audit_type,
                                       Table_Name = v.table_name,
                                       Columnsname = v.column_name,
                                       Oldvalues = v.old_value,
                                       Newvalues = v.new_value,
                                       AuditBy = dets.username,
                                       Audit_Date = v.posted_date,
                                       Audit_Time = v.posted_time,
                                   }).ToList();
                        if (det != null && det.Count > 0)
                        {
                            return det;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        //public List<AuditLogs> GetBloglist(DateTime frm, DateTime to, string tn, string ac)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var det = (from v in context.audit_log
        //                   join dets in context.emp_detail on v.posted_by equals dets.emp_detail_id.ToString()
        //                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
        //                    ac == "All" ? v.audit_type == v.audit_type  : v.audit_type == ac 
        //                   select new AuditLogs
        //                   {
        //                       Audit_Sno = v.audit_sno,
        //                       Audit_Type = v.audit_type,
        //                       Table_Name = v.table_name,
        //                       Columnsname = v.column_name,
        //                       Oldvalues = v.old_value,
        //                       Newvalues = v.new_value,
        //                       AuditBy = dets.full_name,
        //                       Audit_Date = v.posted_date,
        //                       Audit_Time = v.posted_time,
        //                   }).ToList();
        //        if (det != null && det.Count > 0)
        //            return det;
        //        else
        //            return null;

        //    }
        //}
        // public List<AuditLogs> Getloglist(DateTime frm, DateTime to, string tn, string ac, long ssno)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var det = (from v in context.audit_log
        //                   join dets in context.facility_users on v.posted_by equals dets.faci_users_sno.ToString()
        //                   where v.table_name == tn && v.posted_date >= frm && v.posted_date <= to &&
        //                    ac == "All" ? v.audit_type == v.audit_type && v.facility_reg_sno == ssno : v.audit_type == ac && v.facility_reg_sno == ssno
        //                   select new AuditLogs
        //                   {
        //                       Audit_Sno = v.audit_sno,
        //                       Audit_Type = v.audit_type,
        //                       Table_Name = v.table_name,
        //                       Columnsname = v.column_name,
        //                       Oldvalues = v.old_value,
        //                       Newvalues = v.new_value,
        //                       AuditBy = dets.user_fullname,
        //                       Audit_Date =  v.posted_date,
        //                       Audit_Time = v.posted_time,
        //                   }).ToList();
        //        if (det != null && det.Count > 0)
        //            return det;
        //        else
        //            return null;

        //    }
        //}
        #endregion methods
    }
}

