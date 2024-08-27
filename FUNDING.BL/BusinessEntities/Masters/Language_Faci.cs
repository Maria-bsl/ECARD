using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public  class Language_Faci
    {
        #region Properties
        public long Loc_Sno { get; set; }
        public string AuditBy { get; set; }
        public string Loc_Swa { get; set; }
        public string Loc_Name { get; set; }
        public string Loc_Eng { get; set; }
        public string Loc_Eng1 { get; set; }
        public string Dyn_Eng { get; set; }
        public string Table_name { get; set; }
        public string Col_name { get; set; }
        public string Loc_Lang { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        #endregion Properties
        #region Method
        public long AddLang(Language_Faci sc)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                temp_localization pc = new temp_localization()
                {
                    loc_eng = sc.Loc_Eng,
                    dyn_eng = sc.Loc_Eng1,
                    table_name = sc.Table_name,
                    column_name = sc.Col_name,
                };
                context.temp_localization.Add(pc);
                context.SaveChanges();
                return pc.loc_sno;
            }
        }
        public bool ValidateColumn(string tble, string colm)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.temp_localization
                                  where c.table_name == tble && c.column_name == colm //&& (c.history == null || c.history != "yes")
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool validatesno()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.temp_localization
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public Language_Faci validatelang(long sno/*, long ssno*/)
        {

            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {

                var edetails = (from c in context.emp_detail
                                .Where(c => c.emp_detail_id == sno /*&& c.insti_reg_sno == ssno*/)
                                select new Language_Faci
                                {
                                    Loc_Name = c.localization,

                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }

        public bool Validate(string tble)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var validation = (from c in context.temp_localization
                                  where c.table_name == tble //&& (c.history == null || c.history != "yes")
                                  select c);
                if (validation.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<Language_Faci> Getlang(string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.temp_localization
                                where c.table_name == name

                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                  //  Loc_Oth = c.loc_other,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Language_Faci> Getlocaleng()
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.temp_localization
                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Loc_Eng1 = c.dyn_eng,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).OrderBy(z => z.Loc_Sno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }
        public List<Language_Faci> GetScreens(string tble)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from c in context.localization_master
                                where c.table_name == tble
                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Loc_Swa = c.loc_other,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).OrderBy(z => z.Loc_Sno).ToList();
                if (adetails != null && adetails.Count > 0)
                    return adetails;
                else
                    return null;
            }
        }


        public Language_Faci EditColumn(string tble, string colm)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.temp_localization
                                where c.table_name == tble && c.column_name == colm
                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public Language_Faci EditColumn1(string tble, string colm)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.localization_master
                                where c.table_name == tble && c.column_name == colm
                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Loc_Swa = c.loc_other,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        public void UpdateLan(Language_Faci dep)
        {
            if (dep.Loc_Sno != 0)
            {
                using (ECARDAPPEntities context = new ECARDAPPEntities())
                {
                    var UpdateContactInfo = (from u in context.localization_master
                                             where u.table_name == dep.Table_name && u.column_name == dep.Col_name
                                             select u).FirstOrDefault();
                    if (UpdateContactInfo != null)
                    {
                        UpdateContactInfo.table_name = dep.Table_name;
                        UpdateContactInfo.column_name = dep.Col_name;
                        UpdateContactInfo.loc_eng = dep.Loc_Eng;
                        UpdateContactInfo.loc_other = dep.Loc_Swa;
                        UpdateContactInfo.updated_by = dep.Updated_By;
                        UpdateContactInfo.updated_date = DateTime.Now;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (ECARDAPPEntities context = new ECARDAPPEntities())
                {
                    localization_master pc = new localization_master()
                    {
                        loc_eng = dep.Loc_Eng,
                        table_name = dep.Table_name,
                        column_name = dep.Col_name,
                        loc_other = dep.Loc_Swa,
                        posted_by = dep.AuditBy,
                        posted_date = DateTime.Now,

                    };
                    context.localization_master.Add(pc);
                    context.SaveChanges();
                }
            }
        }

        public void Updatelang(Language_Faci dep)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var UpdateContactInfo = (from u in context.temp_localization
                                         where u.loc_sno == dep.Loc_Sno
                                         select u).FirstOrDefault();
                if (UpdateContactInfo != null)
                {
                    UpdateContactInfo.table_name = dep.Table_name;
                    UpdateContactInfo.column_name = dep.Col_name;
                    UpdateContactInfo.loc_eng = dep.Loc_Eng;
                    context.SaveChanges();
                }
            }
        }
        public Language_Faci EditLang(string name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var edetails = (from c in context.temp_localization
                                where c.table_name == name
                                select new Language_Faci
                                {
                                    Loc_Sno = c.loc_sno,
                                    Loc_Eng = c.loc_eng,
                                    Col_name = c.column_name,
                                    Table_name = c.table_name,
                                }).FirstOrDefault();
                if (edetails != null)
                    return edetails;
                else
                    return null;
            }
        }
        #endregion Method
    }
}
