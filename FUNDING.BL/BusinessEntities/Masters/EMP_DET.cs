using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
    public class EMP_DET
    {
        #region Properties
        public long Detail_Id { get; set; }
        public long RoleSno { get; set; }
        public long Branch_Sno { get; set; }
        public string Emp_Id_No { get; set; }
        public string Full_Name { get; set; }
        public string Branch_Name { get; set; }
        public string First_Name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Desg_name { get; set; }
        public string User_name { get; set; }
        public string Loc_Name { get; set; }
        public string Flag { get; set; }
        public DateTime C_time { get; set; }
        public long Desg_Id { get; set; }
        public string Rol_Code { get; set; }
        public string Email_Address { get; set; }
        public string Password { get; set; }
        public string Mobile_No { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Expiry_Date { get; set; }
        public string F_Login { get; set; }
        public long SNO { get; set; }
        public string Q_Name { get; set; }
        public string Q_Ans { get; set; }
        public String App_Status { get; set; }
        public int Log_Att { get; set; }
        public DateTime? Log_Time { get; set; }
        public String Log_Status { get; set; }
        public String Emp_Status { get; set; }
        public string AuditBy { get; set; }
        public DateTime? Audit_Date { get; set; }

        public long? team_det_sno { get; set; }

        #endregion properties
        #region methods

        public EMP_DET GetmailsEmp(String sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetailsemp = (from c in context.emp_detail.Where(c => c.email_id == sno && c.emp_status == "Active")
                                   select new EMP_DET
                                   {
                                       SNO = c.emp_detail_id,
                                       Q_Name = c.full_name,
                                       User_name = c.username,
                                       Password = c.pwd,
                                   }).FirstOrDefault();
                if (edetailsemp != null)
                    return edetailsemp;
                else
                    return null;
            }
        }

        public void UpdateUsers(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.email_id == dep.Email_Address
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {

                    UpdateContactInfo.f_login = "false";
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void BlockUser(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_status = dep.Log_Status;

                    context.SaveChanges();
                }
            }
        }
        public void Updatelogatt(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    UpdateContactInfo.log_time = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET Checkuseratt(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                var edetails = (from c in context.emp_detail
                                where c.username == name  && c.log_att != null
                                select new EMP_DET
                                {
                                    Log_Att = (int)c.log_att
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateLogDate(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_id_no == dep.Emp_Id_No
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET CheckuserLogStat(String name/*,String pwd1*/)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                var edetails = (from c in context.emp_detail
                                
                                where c.username == name/* && c.pwd==pwd1*/ && c.f_login == "true"
                                select new EMP_DET
                                {
                                    Detail_Id = (long)c.emp_detail_id,
                                    Desg_Id = (long)c.desg_id,
                                    Log_Time = c.log_time,
                                    Log_Status = c.log_status,
                                    C_time=(DateTime)c.ctime,
                                    //team_det_sno=c.team_det_sno,
                                    F_Login=c.f_login,
                                    
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdatelangInstfalse(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id /*&& u.desg_id == dep.Desg_Id*/
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "false";
                    //UpdateContactInfo.ctime = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void UpdatelangInst(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.desg_id == dep.Desg_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.localization = dep.Loc_Name;
                    context.SaveChanges();
                }
            }
        }
        public void UpdatelangInsttrue(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.desg_id == dep.Desg_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "true";
                    UpdateContactInfo.ctime = DateTime.Now;
                    UpdateContactInfo.log_att = 0;
                    context.SaveChanges();
                }
            }
        }
        public void Updatelangafter5min(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.desg_id == dep.Desg_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "false";
                    UpdateContactInfo.ctime = DateTime.Now;
                    UpdateContactInfo.log_att = 0;
                    context.SaveChanges();
                }
            }
        }
        public void Updatelangafterlogsta5min(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         //where u.emp_detail_id == dep.Detail_Id 
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "false";
                    UpdateContactInfo.ctime = DateTime.Now;
                    UpdateContactInfo.log_att = 0;
                    UpdateContactInfo.log_status = dep.Log_Status;
                    UpdateContactInfo.log_time= dep.Log_Time;
                    context.SaveChanges();
                }
            }
        }
        public long AddEMP(EMP_DET sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                emp_detail ps = new emp_detail()
                {
                    emp_id_no = sc.Emp_Id_No,
                    full_name = sc.Full_Name,
                    first_name = sc.First_Name,
                    middle_name = sc.Middle_name,
                    last_name = sc.Last_name,
                    desg_id = sc.Desg_Id,
                    username = sc.User_name,
                    mobile_no = sc.Mobile_No,
                    email_id = sc.Email_Address,
                    pwd = sc.Password,
                    created_date = sc.Created_Date,
                    expiry_date = sc.Expiry_Date,
                    f_login = "false",
                    //flag = "false",
                    app_status = sc.App_Status,
                    emp_status = sc.Emp_Status,
                    posted_by = sc.AuditBy,
                    posted_date = DateTime.Now,
                   // team_det_sno= sc.team_det_sno ,
                };
                context.emp_detail.Add(ps);
                context.SaveChanges();
                return ps.emp_detail_id;
            }
        }
        public List<EMP_DET> GetEMPTeam(long teamid)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.emp_detail
                                    //join d in context.designation_list on c.desg_id equals d.desg_id
                                join d in context.roles_master on c.desg_id equals d.sno
                                //where c.team_det_sno == teamid
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Desg_name = d.descript,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public EMP_DET valiadte_Login(long sno, long instd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var dt = (from sc in context.emp_detail.Where(c => c.emp_detail_id == instd && c.desg_id == sno)
                          select new EMP_DET
                          {
                              Flag = sc.flag,
                              C_time = (DateTime)sc.ctime,
                          }).FirstOrDefault();

                if (dt != null)
                    return dt;
                else
                    return null;
            }
        }
        public EMP_DET vailadatetym(long sno, long instd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var dt = (from sc in context.emp_detail.Where(c => c.emp_detail_id == instd && c.desg_id == sno)
                          select new EMP_DET
                          {
                              //Flag = sc.flag,
                              C_time = (DateTime)sc.ctime,
                          }).FirstOrDefault();

                if (dt != null)
                    return dt;
                else
                    return null;
            }
        }
        public bool ValidateLoginorNot(long sno, long Instd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from sc in context.emp_detail.Where(c => c.emp_detail_id == sno   && c.desg_id == Instd && c.flag=="true")
                                select sc);
                if (adetails.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<EMP_DET> GetEMPuser(long roleid)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.emp_detail
                                    //join d in context.designation_list on c.desg_id equals d.desg_id
                                join d in context.roles_master on c.desg_id equals d.sno
                                where c.desg_id == roleid
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Desg_name = d.descript,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<EMP_DET> GetEMPNew(string rcode)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                if (rcode == "Admin")
                {
                    var adetails = (from c in context.emp_detail
                                        //join d in context.designation_list on c.desg_id equals d.desg_id
                                    join d in context.roles_master on c.desg_id equals d.sno
                                    select new EMP_DET
                                    {
                                        Detail_Id = c.emp_detail_id,
                                        Emp_Id_No = c.emp_id_no,
                                        Full_Name = c.full_name,
                                        First_Name = c.first_name,
                                        Desg_name = d.descript,
                                        Middle_name = c.middle_name,
                                        Last_name = c.last_name,
                                        User_name = c.username,
                                        Mobile_No = c.mobile_no,
                                        Desg_Id = (long)c.desg_id,
                                        Email_Address = c.email_id,
                                        Password = c.pwd,
                                        Created_Date = c.created_date,
                                        F_Login = c.f_login,
                                        Emp_Status = c.emp_status,
                                        Audit_Date = c.posted_date,
                                    }).OrderByDescending(z => z.Audit_Date).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;
                }
                //else if (rcode == "Board")
                //{
                //    var adetails = (from c in context.emp_detail
                //                        //join d in context.designation_list on c.desg_id equals d.desg_id
                //                    join d in context.roles_master on c.desg_id equals d.sno
                //                    where d.descript == rcode
                //                    select new EMP_DET
                //                    {
                //                        Detail_Id = c.emp_detail_id,
                //                        Emp_Id_No = c.emp_id_no,
                //                        Full_Name = c.full_name,
                //                        First_Name = c.first_name,
                //                        Desg_name = d.descript,
                //                        Middle_name = c.middle_name,
                //                        Last_name = c.last_name,
                //                        User_name = c.username,
                //                        Mobile_No = c.mobile_no,
                //                        Desg_Id = (long)c.desg_id,
                //                        Email_Address = c.email_id,
                //                        Password = c.pwd,
                //                        Created_Date = c.created_date,
                //                        F_Login = c.f_login,
                //                        Emp_Status = c.emp_status,
                //                        Audit_Date = c.posted_date,
                //                    }).OrderByDescending(z => z.Audit_Date).ToList();
                //    if (adetails != null && adetails.Count > 0)
                //        return adetails;
                //    else
                //        return null;

                //}
                else {

                    var adetails = (from c in context.emp_detail
                                        //join d in context.designation_list on c.desg_id equals d.desg_id
                                    join d in context.roles_master on c.desg_id equals d.sno
                                    where d.descript == rcode
                                    //&& c.team_det_sno == teamid
                                    select new EMP_DET
                                    {
                                        Detail_Id = c.emp_detail_id,
                                        Emp_Id_No = c.emp_id_no,
                                        Full_Name = c.full_name,
                                        First_Name = c.first_name,
                                        Desg_name = d.descript,
                                        Middle_name = c.middle_name,
                                        Last_name = c.last_name,
                                        User_name = c.username,
                                        Mobile_No = c.mobile_no,
                                        Desg_Id = (long)c.desg_id,
                                        Email_Address = c.email_id,
                                        Password = c.pwd,
                                        Created_Date = c.created_date,
                                        F_Login = c.f_login,
                                        Emp_Status = c.emp_status,
                                        Audit_Date = c.posted_date,
                                    }).OrderByDescending(z => z.Audit_Date).ToList();
                    if (adetails != null && adetails.Count > 0)
                        return adetails;
                    else
                        return null;

                }

                    //return null;
            }
        }
        public List<EMP_DET> GetEMP()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.emp_detail
                                //join d in context.designation_list on c.desg_id equals d.desg_id
                                join d in context.roles_master on c.desg_id equals d.sno
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Desg_name = d.descript,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    Audit_Date = c.posted_date,
                                }).OrderByDescending(z => z.Audit_Date).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<EMP_DET> GetEMPAct()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.emp_detail
                                //join d in context.designation_list on c.desg_id equals d.desg_id
                                join d in context.roles_master on c.desg_id equals d.sno
                                where c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Desg_name = d.descript,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Mobile_No = c.mobile_no,
                                    Desg_Id = (long)c.desg_id,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }

        public bool Validateemail(string email) {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.email_id.ToLower() == email)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool Validateemonbile(string mobile)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.mobile_no.ToLower() == mobile)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateusername(String uname)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.username.ToLower() == uname)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateuser(String id)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.emp_id_no.ToLower()==id)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateduplicate(String id)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.username == id)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateLoginAlreadyExist(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail.Where(c =>c.flag == "true" && c.emp_detail_id == sno)
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public EMP_DET Validdateloginornot(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail.Where(c=>c.emp_detail_id == chsno && c.emp_status == "Active")
                                select new EMP_DET
                                {
                                    Flag=c.flag,
                                    C_time=(DateTime)c.ctime,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMP_DET getEMPText(long chsno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail
                                join d in context.designation_list on c.desg_id equals d.desg_id
                                where c.emp_detail_id == chsno && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Last_name = c.last_name,
                                    Desg_Id = (long)c.desg_id,
                                    Desg_name = d.desg_name,
                                    User_name = c.username,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMP_DET EditEMP(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail
                                //join d in context.designation_list on c.desg_id equals d.desg_id
                                join d in context.roles_master on c.desg_id equals d.sno
                                where c.emp_detail_id == sno
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Mobile_No = c.mobile_no,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Desg_Id = (long)c.desg_id,
                                    Desg_name = d.descript,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    Expiry_Date = c.expiry_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                    AuditBy = c.posted_by,
                                    Audit_Date = c.posted_date,
                                    //team_det_sno= (long)c.team_det_sno 
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        //public bool ValidateuserId(String id)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var validation = (from c in context.emp_detail
        //                          where (c.username == id && c.emp_status == "Approved")
        //                          select c);
        //        var validationTU = (from c in context.institution_users
        //                            where (c.username == id)
        //                            select c);
        //        var validationM = (from c in context.member_registration
        //                           where (c.username == id && c.status == "Approved")
        //                           select c);
        //        if (validation.Count() > 0 || validationTU.Count() > 0 || validationM.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        public bool Validateuserpwd(String id)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where (c.pwd == id)
                                  select c);
               
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        
        public bool ValidateCustomerPassword(string password)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.cust_users
                                  where (c.password == password)
                                  select c);
               
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool ValidateuserpwdInst(String id,  long uid)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                var validationTu = (from c in context.emp_detail
                                    where (c.pwd == id  && c.emp_detail_id == uid)
                                    select c);

                if (validationTu.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validatepwdbank(String name, long Userid)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                var validationemp = (from c in context.emp_detail
                                     where (c.pwd == name && c.f_login == "false" && c.emp_detail_id == Userid)
                                     select c);

                if (validationemp.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        //public bool ValidateuserpwdMeg(String id, long sno,long uid)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {

        //        var validationPwd = (from c in context.member_registration
        //                             where (c.password == id && c.insti_reg_sno == sno && c.member_reg_sno==uid)
        //                             select c);
        //        if (validationPwd.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
      
        public EMP_DET CheckLogin(string uname, string pwd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())

            {

                var edetails = (from c in context.emp_detail
                                join r in context.roles_master on c.desg_id equals r.sno

                                where (c.username == uname && c.pwd == pwd && c.emp_status == "Active" && c.log_status == null)
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Emp_Id_No = c.emp_id_no,
                                    Full_Name = c.full_name,
                                    First_Name = c.first_name,
                                    Middle_name = c.middle_name,
                                    Mobile_No = c.mobile_no,
                                    Desg_name = r.descript,
                                    //C_time = (DateTime)c.ctime,
                                    Rol_Code = r.code,
                                    Last_name = c.last_name,
                                    User_name = c.username,
                                    Desg_Id = (long)c.desg_id,
                                    Expiry_Date = c.expiry_date,
                                    Email_Address = c.email_id,
                                    Password = c.pwd,
                                    Created_Date = c.created_date,
                                    F_Login = c.f_login,
                                    Emp_Status = c.emp_status,
                                   // team_det_sno = (long)c.team_det_sno,
                                    RoleSno = r.sno,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        //public bool ValidateLoginorNot(long sno, long Instd)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var adetails = (from sc in context.emp_detail.Where(c => c.emp_id_no == sno && c.flag == "true" && c.desg_id == Instd)
        //                        select sc);
        //        if (adetails.Count() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        public void DeleteEMP(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.emp_detail
                                   where n.emp_detail_id == no
                                   select n).First();

                if (noteDetails != null)
                {
                    context.emp_detail.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }
        public void UpdateEMP(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.emp_id_no = dep.Emp_Id_No;
                    UpdateContactInfo.full_name = dep.Full_Name;
                    UpdateContactInfo.first_name = dep.First_Name;
                    UpdateContactInfo.middle_name = dep.Middle_name;
                    UpdateContactInfo.last_name = dep.Last_name;
                    UpdateContactInfo.desg_id = dep.Desg_Id;
                    UpdateContactInfo.emp_status = dep.Emp_Status;
                    UpdateContactInfo.username = dep.User_name;
                    UpdateContactInfo.email_id = dep.Email_Address;
                    UpdateContactInfo.mobile_no = dep.Mobile_No;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    //UpdateContactInfo.team_det_sno  = dep.team_det_sno  ;
                    context.SaveChanges();
                }
            }
        }
        public void DeleteStatus(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.emp_status = "InActive";
                    context.SaveChanges();
                }
            }
        }
        //public void Updatelang(EMP_DET dep)
        //{
        //    using (ECARDAPPEntities context = new ECARDAPPEntities())
        //    {
        //        var UpdateContactInfo = (from u in context.emp_detail
        //                                 where u.emp_detail_id == dep.Detail_Id
        //                                 select u).FirstOrDefault();
        //        if (UpdateContactInfo != null)
        //        {
        //            UpdateContactInfo.localization = dep.Loc_Name;
        //            context.SaveChanges();
        //        }
        //    }
        //}
        public void Updatelang(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.localization = dep.Loc_Name;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateQuestionEMP(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id && u.f_login == "false"
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    //UpdateContactInfo.sno = dep.SNO;
                    //UpdateContactInfo.q_name = dep.Q_Name;
                    //UpdateContactInfo.q_ans = dep.Q_Ans;
                    UpdateContactInfo.pwd = dep.Password;
                    UpdateContactInfo.f_login = dep.F_Login;
                    UpdateContactInfo.posted_by = dep.AuditBy;
                    UpdateContactInfo.posted_date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateOnlyflsg(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "false";
                    context.SaveChanges();
                }
            }
        }
        public void UpdateOnlypwd(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.pwd =dep.Password;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateOnlyCustomerPassword(cust_users customer)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.cust_users.Where(c => c.cust_users_sno == customer.cust_users_sno)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.password =customer.password;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateOnlyflsgtrue(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail.Where(c => c.emp_detail_id == dep.Detail_Id)
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.flag = "true";
                    UpdateContactInfo.ctime = DateTime.Now;
                    UpdateContactInfo.log_att = 0;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET validatelang(long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail.Where(c => c.emp_detail_id==sno && c.emp_status == "Active")
                                select new EMP_DET
                                {
                                    Loc_Name=c.localization,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public EMP_DET CheckuserLogStatbnk(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail
                                where c.username == name && c.emp_status == "Active"
                                select new EMP_DET
                                {
                                    Detail_Id = c.emp_detail_id,
                                    Log_Time = c.log_time,
                                    Log_Status = c.log_status,
                                    User_name = c.username
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateLogDatebnk(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    context.SaveChanges();
                }
            }
        }
        public void Updatelogattbnk(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_att = dep.Log_Att;
                    UpdateContactInfo.log_time = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void BlockUserbnk(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_status = dep.Log_Status;
                    context.SaveChanges();
                }
            }
        }
        public void UnblockUserbnk(EMP_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.emp_detail
                                         where u.emp_detail_id == dep.Detail_Id
                                         select u).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.log_status = null;
                    UpdateContactInfo.log_att = 0;
                    UpdateContactInfo.f_login = "false";
                    UpdateContactInfo.pwd = dep.Password;
                    context.SaveChanges();
                }
            }
        }
        public EMP_DET Checkuserattbnk(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.emp_detail
                                where c.username == name && c.emp_status == "Active" && c.log_att != null
                                select new EMP_DET
                                {
                                    Log_Att = (int)c.log_att,
                                    Log_Status = c.log_status
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public bool Validateuserblo(long usno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                 //join det in context.designation_list on c.desg_id equals det.desg_id
                                  join det in context.roles_master on c.desg_id equals det.sno
                                  where c.emp_detail_id == usno && det.descript == "Administrator"
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Validateuserblosuper(string usno, string pwd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.emp_detail
                                  where c.username == "super" && c.pwd != pwd
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        #endregion Methods
    }
}
