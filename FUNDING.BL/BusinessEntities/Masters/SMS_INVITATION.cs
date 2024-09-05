// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.SMS_INVITATION
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
    public class SMS_INVITATION
    {
        public long sno { get; set; }

        public long? event_sno { get; set; }

        public string sms_text { get; set; }

        public string event_name { get; set; }

        public string posted_by { get; set; }

        public DateTime? posted_date { get; set; }

        public virtual event_details event_details { get; set; }

        public long AddSMSEvents(SMS_INVITATION sc)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                sms_invitation entity = new sms_invitation()
                {
                    sms_inv_sno = sc.sno,
                    sms_text = sc.sms_text,
                    event_det_sno = sc.event_sno,
                    posted_by = sc.posted_by,
                    posted_date = new DateTime?(DateTime.Now)
                };
                ecardappEntities.sms_invitation.Add(entity);
                ecardappEntities.SaveChanges();
                return entity.sms_inv_sno;
            }
        }

        public bool ValidateSMSEvent(string text, long? csno)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
                return ecardappEntities.sms_invitation.Where(c => c.sms_text == text && c.event_det_sno == csno).Count() > 0;
        }

        public List<SMS_INVITATION> GetSMSEvent()
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.sms_invitation
                            select new SMS_INVITATION
                            {
                                sno = sc.sms_inv_sno,
                                sms_text = sc.sms_text,
                                event_sno = sc.event_det_sno,
                                event_name = sc.event_details.event_name,
                                posted_by = sc.posted_by,
                                posted_date = sc.posted_date

                            }).ToList();

                
                return (list != null && list.Count > 0) ? list : null;
            }
        }

        public SMS_INVITATION getSMSText()
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.sms_invitation
                            orderby sc.posted_date descending
                            select new SMS_INVITATION
                            {
                                sno = sc.sms_inv_sno,
                                sms_text = sc.sms_text,
                                event_sno = sc.event_det_sno,
                                posted_by = sc.posted_by,
                                posted_date = new DateTime?(DateTime.Now)

                            }).FirstOrDefault();


                return list ?? null;
            }
        }

        public SMS_INVITATION getSMSLst(long sno)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.sms_invitation
                            where sc.sms_inv_sno == sno
                            select new SMS_INVITATION
                            {
                                sno = sc.sms_inv_sno,
                                sms_text = sc.sms_text,
                                event_sno = sc.event_det_sno,
                                posted_by = sc.posted_by,
                                posted_date = new DateTime?(DateTime.Now)

                            }).FirstOrDefault();


                return list ?? null;
               // return ecardappEntities.sms_invitation.Where<sms_invitation>((Expression<Func<sms_invitation, bool>>)(c => c.sms_inv_sno == sno)).Select<sms_invitation, SMS_INVITATION>(Expression.Lambda<Func<sms_invitation, SMS_INVITATION>>((Expression)Expression.MemberInit(Expression.New(typeof(SMS_INVITATION)), (MemberBinding)Expression.Bind((MethodInfo)MethodBase.GetMethodFromHandle(__methodref(SMS_INVITATION.set_sno)), )))); // Unable to render the statement
            }
        }

        public SMS_INVITATION EditSMS(long sno)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                var list = (from sc in ecardappEntities.sms_invitation
                            where sc.sms_inv_sno == sno
                            select new SMS_INVITATION
                            {
                                sno = sc.sms_inv_sno,
                                sms_text = sc.sms_text,
                                event_sno = sc.event_det_sno,
                                posted_by = sc.posted_by,
                                posted_date = new DateTime?(DateTime.Now)

                            }).FirstOrDefault();


                return list ?? null;
                //return ecardappEntities.sms_invitation.Where<sms_invitation>((Expression<Func<sms_invitation, bool>>)(c => c.sms_inv_sno == sno)).Select<sms_invitation, SMS_INVITATION>(Expression.Lambda<Func<sms_invitation, SMS_INVITATION>>((Expression)Expression.MemberInit(Expression.New(typeof(SMS_INVITATION)), (MemberBinding)Expression.Bind((MethodInfo)MethodBase.GetMethodFromHandle(__methodref(SMS_INVITATION.set_sno)), )))); // Unable to render the statement
            }
        }

        public void DeleteSMS(long no)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                sms_invitation entity = ecardappEntities.sms_invitation.Where<sms_invitation>((Expression<Func<sms_invitation, bool>>)(n => n.sms_inv_sno == no)).FirstOrDefault<sms_invitation>();
                if (entity == null)
                    return;
                ecardappEntities.sms_invitation.Remove(entity);
                ecardappEntities.SaveChanges();
            }
        }

        public void UpdateSMSEvent(SMS_INVITATION dep)
        {
            using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
            {
                sms_invitation smsInvitation = ecardappEntities.sms_invitation.Where<sms_invitation>((Expression<Func<sms_invitation, bool>>)(u => u.sms_inv_sno == dep.sno)).FirstOrDefault<sms_invitation>();
                if (smsInvitation == null)
                    return;
                smsInvitation.sms_text = dep.sms_text;
                smsInvitation.posted_by = dep.posted_by;
                smsInvitation.posted_date = new DateTime?(DateTime.Now);
                ecardappEntities.SaveChanges();
            }
        }
    }
}
