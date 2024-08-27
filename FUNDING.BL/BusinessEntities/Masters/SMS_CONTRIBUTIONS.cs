// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.SMS_CONTRIBUTION
// Assembly: FUNDING.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A421DCBA-0154-4E02-9814-774D89923779
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.BL.dll

using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace FUNDING.BL.BusinessEntities.Masters
{
  public class SMS_CONTRIBUTION
  {
    public string event_name { get; set; }

    public long sms_csno { get; set; }

    public long? event_sno { get; set; }

    public string sms_text { get; set; }

    public string posted_by { get; set; }

    public DateTime? posted_date { get; set; }

    public virtual event_details event_details { get; set; }

    public long AddSMSContributions(SMS_CONTRIBUTION sc)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        sms_contribution entity = new sms_contribution()
        {
          sms_con_sno = sc.sms_csno,
          sms_text = sc.sms_text,
          event_det_sno = sc.event_sno,
          posted_by = sc.posted_by,
          posted_date = new DateTime?(DateTime.Now)
        };
        ecardappEntities.sms_contribution.Add(entity);
        ecardappEntities.SaveChanges();
        return entity.sms_con_sno;
      }
    }

    public bool ValidateSMSContribution(string text, long? csno)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
        return ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (c => c.sms_text == text && c.event_det_sno == csno)).Count<sms_contribution>() > 0;
    }

    public List<SMS_CONTRIBUTION> GetSMSContribution()
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        List<SMS_CONTRIBUTION> list = ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (c => (long?) c.event_details.event_det_sno == c.event_det_sno)).Select<sms_contribution, SMS_CONTRIBUTION>(Expression.Lambda<Func<sms_contribution, SMS_CONTRIBUTION>>((Expression) Expression.MemberInit(Expression.New(typeof (SMS_CONTRIBUTION)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SMS_CONTRIBUTION.set_sms_csno)), )))); // Unable to render the statement
        return list != null && list.Count > 0 ? list : (List<SMS_CONTRIBUTION>) null;
      }
    }

    public SMS_CONTRIBUTION getSMSText()
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ecardappEntities.sms_contribution.OrderByDescending<sms_contribution, DateTime?>((Expression<Func<sms_contribution, DateTime?>>) (c => c.posted_date)).Select<sms_contribution, SMS_CONTRIBUTION>(Expression.Lambda<Func<sms_contribution, SMS_CONTRIBUTION>>((Expression) Expression.MemberInit(Expression.New(typeof (SMS_CONTRIBUTION)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SMS_CONTRIBUTION.set_sms_csno)), )))); // Unable to render the statement
      }
    }

    public SMS_CONTRIBUTION getSMSLst(long sno)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (c => c.sms_con_sno == sno)).Select<sms_contribution, SMS_CONTRIBUTION>(Expression.Lambda<Func<sms_contribution, SMS_CONTRIBUTION>>((Expression) Expression.MemberInit(Expression.New(typeof (SMS_CONTRIBUTION)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SMS_CONTRIBUTION.set_sms_csno)), )))); // Unable to render the statement
      }
    }

    public SMS_CONTRIBUTION EditSMS(long sno)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        return ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (c => c.sms_con_sno == sno)).Select<sms_contribution, SMS_CONTRIBUTION>(Expression.Lambda<Func<sms_contribution, SMS_CONTRIBUTION>>((Expression) Expression.MemberInit(Expression.New(typeof (SMS_CONTRIBUTION)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SMS_CONTRIBUTION.set_sms_csno)), )))); // Unable to render the statement
      }
    }

    public void DeleteSMS(long no)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        sms_contribution entity = ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (n => n.sms_con_sno == no)).FirstOrDefault<sms_contribution>();
        if (entity == null)
          return;
        ecardappEntities.sms_contribution.Remove(entity);
        ecardappEntities.SaveChanges();
      }
    }

    public void UpdateSMSContribution(SMS_CONTRIBUTION dep)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        sms_contribution smsContribution = ecardappEntities.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (u => u.sms_con_sno == dep.sms_csno)).FirstOrDefault<sms_contribution>();
        if (smsContribution == null)
          return;
        smsContribution.sms_text = dep.sms_text;
        smsContribution.posted_by = dep.posted_by;
        smsContribution.posted_date = new DateTime?(DateTime.Now);
        ecardappEntities.SaveChanges();
      }
    }
  }
}
