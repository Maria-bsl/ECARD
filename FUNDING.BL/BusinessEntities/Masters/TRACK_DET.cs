using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;

namespace FUNDING.BL.BusinessEntities.Masters
{
  public  class TRACK_DET
    {
        #region Properties
        public long SNO { get; set; }
        
        public string Full_Name { get; set; }
        public string Facility_Reg_No { get; set; }
        public string Facility_Reg_Name { get; set; }
        public string Ipadd { get; set; }
        public string Email { get; set; }
        public DateTime Login_Time { get; set; }
        public string Posted_by { get; set; }
        public string Description { get; set; }
        public DateTime? Logout_Time { get; set; }
       
        #endregion properties
        #region methods

        public void AddTrack(TRACK_DET sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                track_details ps = new track_details()
                {
                    full_name = sc.Full_Name,
                    //facility_reg_no = sc.Facility_Reg_No,
                    ipadd = sc.Ipadd,
                    email = sc.Email,
                    login_time = sc.Login_Time,
                    posted_by = sc.Posted_by,
                    descrip = sc.Description,
                    
                };
                context.track_details.Add(ps);
                context.SaveChanges();
            }
        }
        public bool ValidateTrack(string regno, String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.track_details
                                  where (c.full_name == name )
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<TRACK_DET> GetTRACK()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.track_details
                                    //where c.history == null || c.history != "yes"
                                select new TRACK_DET
                                {
                                    SNO = c.sno,
                                    Full_Name = c.full_name,
                                    //Facility_Reg_No = c.facility_reg_no,
                                    Ipadd = c.ipadd,
                                    Email = c.email,
                                    //Login_Time = c.login_time,
                                    Posted_by = c.posted_by,
                                    Description = c.descrip,
                                    Logout_Time = c.logout_time,
                                    
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public TRACK_DET getTRACKText(String chsno,String psno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.track_details
                                where  c.posted_by==psno
                                select new TRACK_DET
                                {
                                    SNO = c.sno,
                                    Full_Name = c.full_name,
                                    //Facility_Reg_No = c.facility_reg_no,
                                    Ipadd = c.ipadd,
                                    Email = c.email,
                                    Login_Time=(DateTime)c.login_time,
                                    Posted_by = c.posted_by,
                                    Description = c.descrip,
                                    

                                }).OrderByDescending(c => c.Login_Time).Take(1).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public TRACK_DET EditTRACK(String no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.track_details
                                where c.posted_by == no

                                select new TRACK_DET
                                {
                                    SNO = c.sno,
                                    Full_Name = c.full_name,
                                    //Facility_Reg_No = c.facility_reg_no,
                                    Ipadd = c.ipadd,
                                    Email = c.email,
                                    Login_Time =(DateTime)c.login_time,
                                    Posted_by = c.posted_by,
                                    Description = c.descrip,
                                   

                                }).OrderByDescending(c => c.Login_Time).Take(1).FirstOrDefault();

                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void Deletetrack(long no)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var noteDetails = (from n in context.track_details
                                   where n.sno == no
                                   select n).First();

                if (noteDetails != null)
                {
                    //context.DeleteObject(noteDetails);
                    context.track_details.Remove(noteDetails);
                    context.SaveChanges();
                }

            }

        }

        public void UpdateTRACK(TRACK_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.track_details.Where(c => c.sno ==dep.SNO)
                                         select u).OrderByDescending(c => c.login_time).Take(1).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.logout_time = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public void UpdateTRACKEmp(TRACK_DET dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.track_details.Where(c => c.sno == dep.SNO)
                                         select u).OrderByDescending(c => c.login_time).Take(1).FirstOrDefault();

                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.logout_time = DateTime.Now;
                    context.SaveChanges();
                }
                //int count = context.institution_registration.Count(p => p.insti_reg_sno == name);
            }
           
        }

        #endregion Methods
    }
}
