﻿// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.PACKAGES
// Assembly: FUNDING.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A421DCBA-0154-4E02-9814-774D89923779
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.BL.dll

using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace FUNDING.BL.BusinessEntities.Masters
{
    public class PACKAGES
    {
        public long sno { get; set; }

        public string pack_name { get; set; }

        public string pack_description { get; set; }

        public long? pack_price { get; set; }

        public string pack_status { get; set; }

        public DateTime? effective_date { get; set; }

        public string posted_by { get; set; }

        public DateTime? posted_date { get; set; }

        public virtual ICollection<ECARD.DL.EDMX.event_details> event_details { get; set; }

        public long AddPackageDetails(PACKAGES sc)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                package_details entity = new package_details()
                {
                    pack_det_sno = sc.sno,
                    pack_name = sc.pack_name,
                    pack_status = sc.pack_status,
                    pack_description = sc.pack_description,
                    pack_price = sc.pack_price,
                    effective_date = sc.effective_date,
                    posted_by = sc.posted_by,
                    posted_date = new DateTime?(DateTime.Now)
                };
                ecardappEntities.package_details.Add(entity);
                ecardappEntities.SaveChanges();
                return entity.pack_det_sno;
            }
        }

        public bool ValidatePackageDetails(string text)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
                return ecardappEntities.package_details.Where(c => c.pack_name == text).Count() > 0;
        }

        public List<PACKAGES> GetPackageDetails()
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.package_details
                            select new PACKAGES
                  {
                    sno = sc.pack_det_sno,
                    pack_name = sc.pack_name,
                    pack_status = sc.pack_status,
                    pack_description = sc.pack_description,
                    pack_price = sc.pack_price,
                    effective_date = sc.effective_date,
                    posted_by = sc.posted_by,
                    posted_date = new DateTime?(DateTime.Now)
                }).ToList();
                 // Unable to render the statement
                return list != null && list.Count > 0 ? list : null;
            }
        }

        public PACKAGES getPackageText()
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.package_details
                            orderby sc.posted_date descending
                            select new PACKAGES
                            {
                                sno = sc.pack_det_sno,
                                pack_name = sc.pack_name,
                                pack_status = sc.pack_status,
                                pack_description = sc.pack_description,
                                pack_price = sc.pack_price,
                                effective_date = sc.effective_date,
                                posted_by = sc.posted_by,
                                posted_date = sc.posted_date
                            }).FirstOrDefault();
                return list ?? null;
            }
        }

        public PACKAGES getPackageLst(long sno)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.package_details
                            where sc.pack_det_sno == sno
                            select new PACKAGES
                            {
                                sno = sc.pack_det_sno,
                                pack_name = sc.pack_name,
                                pack_status = sc.pack_status,
                                pack_description = sc.pack_description,
                                pack_price = sc.pack_price,
                                effective_date = sc.effective_date,
                                posted_by = sc.posted_by,
                                posted_date = sc.posted_date
                            }).FirstOrDefault();

                return list ?? null;
            }
        }

        public PACKAGES EditPackage(long sno)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.package_details
                            where sc.pack_det_sno == sno
                            select new PACKAGES
                            {
                                sno = sc.pack_det_sno,
                                pack_name = sc.pack_name,
                                pack_status = sc.pack_status,
                                pack_description = sc.pack_description,
                                pack_price = sc.pack_price,
                                effective_date = sc.effective_date,
                                posted_by = sc.posted_by,
                                posted_date = sc.posted_date
                            }).FirstOrDefault();

                return list ?? null;
            }
        }

        public void DeletePackage(long no)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                package_details entity = ecardappEntities.package_details.Where<package_details>((Expression<Func<package_details, bool>>)(n => n.pack_det_sno == no)).FirstOrDefault<package_details>();
                if (entity == null)
                    return;
                ecardappEntities.package_details.Remove(entity);
                ecardappEntities.SaveChanges();
            }
        }

        public void UpdatePackageDetails(PACKAGES dep)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                package_details packageDetails = ecardappEntities.package_details.Where<package_details>((Expression<Func<package_details, bool>>)(u => u.pack_det_sno == dep.sno)).FirstOrDefault<package_details>();
                if (packageDetails == null)
                    return;
                packageDetails.pack_description = dep.pack_description;
                packageDetails.pack_name = dep.pack_name;
                packageDetails.pack_price = dep.pack_price;
                packageDetails.effective_date = dep.effective_date;
                packageDetails.pack_status = dep.pack_status;
                packageDetails.posted_by = dep.posted_by;
                packageDetails.posted_date = new DateTime?(DateTime.Now);
                ecardappEntities.SaveChanges();
            }
        }
    }
}