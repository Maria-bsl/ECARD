// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CollectionModule.Contributions
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

 
namespace FUNDING.Models.CollectionModule
{
  public class Contributions
  {
    public const string PENDING_STATUS = "pending";
    public const string AFFIRMATIVE_STATUS = "affirmed";
    public const string UNSETTLED_STATUS = "unsettled";
    public const string DEFAULT_CURRENCY_CODE = "TZS";

    public long Contribution_id { get; set; }

    [Required(ErrorMessage = "Please enter full name.")]
    [RegularExpression("^(([a-zA-Z]+)([',.-]?))*(([a-zA-Z]+)([',.-]?[ ]?))*[a-zA-Z]+[ ]{0,3}$", ErrorMessage = "Please enter a valid full name.")]
    [Display(Name = "Full Name")]
    public string Contributor_name { get; set; }

    [Required(ErrorMessage = "Please enter pledged amount.")]
    [Display(Name = "Pledged Amount (TZS)")]
    [RegularExpression("^[1-9]{1}(\\d{1,2})*(,\\d{3})*$", ErrorMessage = "Enter valid pledged amount.")]
    public string Contribution_amount { get; set; }

    [Required(ErrorMessage = "Please enter mobile number.")]
    [RegularExpression("^\\d+$", ErrorMessage = "Please enter a valid mobile number.")]
    [Display(Name = "Mobile Number")]
    public string Mobile_number { get; set; }

    public string Int_Mobile_number { get; set; }

    [Display(Name = "Email Address")]
    [DataType(DataType.EmailAddress)]
    public string Email_address { get; set; }

    [Required(ErrorMessage = "Please enter number of people.")]
    [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid input.")]
    [Display(Name = "Number of People")]
    public int? Number_of_people { get; set; }

    public bool HasReservationRequested { get; set; }

    [NotMapped]
    private static Contributions CLASS_INSTANCE => new Contributions();

    public string GetGenerateControlNumber(long contribution_id)
    {
      string generatedControlNumber = (string) null;
      bool flag;
      do
      {
        generatedControlNumber = GenerateControlNumber(contribution_id);
        using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
          flag = ecardappEntities.contributor_details.Where(v => v.contri_det_sno == contribution_id && v.control_no == generatedControlNumber).FirstOrDefault() != null;
      }
      while (flag);
      return generatedControlNumber;
    }

    public static string GenerateControlNumber(long contribution_id)
    {
      return string.Format("C{0}{1}", Convert.ToString(contribution_id).PadLeft(6, '0'), GetGeneratedRandomDigits(6));
    }

    private static string GetGeneratedRandomDigits(int len)
    {
      int num1 = len;
      char[] chArray = new char[30];
      char[] charArray = "1234567890".ToCharArray();
      byte[] data1 = new byte[1];
      RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
      cryptoServiceProvider.GetNonZeroBytes(data1);
      int capacity = num1;
      byte[] data2 = new byte[capacity];
      cryptoServiceProvider.GetNonZeroBytes(data2);
      StringBuilder stringBuilder = new StringBuilder(capacity);
      foreach (byte num2 in data2)
        stringBuilder.Append(charArray[(int) num2 % charArray.Length]);
      return stringBuilder.ToString();
    }

    public List<contributor_details> PledgeDetailsList()
    {
      return new List<contributor_details>()
      {
        new contributor_details()
        {
          event_det_sno = new long?(2L),
          cust_reg_sno = new long?(2L),
          contributor_name = "Mabula Juma",
          contribution_amount = new Decimal?((Decimal) 200000),
          mobile_no = "0757838788",
          contri_status = "pending",
          contri_due_date = new DateTime?(DateTime.Parse("2022-02-14")),
          posted_by = "2",
          posted_date = DateTime.Now
        },
        new contributor_details()
        {
          event_det_sno = new long?(2L),
          cust_reg_sno = new long?(2L),
          contributor_name = "Angela Muna",
          contribution_amount = new Decimal?((Decimal) 250000),
          mobile_no = "0757838788",
          contri_status = "pending",
          contri_due_date = new DateTime?(DateTime.Parse("2022-02-14")),
          posted_by = "2",
          posted_date = DateTime.Now
        }
      };
    }

    public static void SaveNewContributionRecordAndSendRespectiveSMS(
      Contributions contribution,
      long? event_id,
      long? customer_reg_no,
      string user_id)
    {
      using (ECARDAPPEntities _dbContext = new ECARDAPPEntities())
      {
        contributor_details contributionRecord = CLASS_INSTANCE.SaveNewRecord(_dbContext, contribution, event_id, customer_reg_no, user_id);
        CLASS_INSTANCE.SaveContributionControlNumber(_dbContext, contributionRecord, contribution);
        CLASS_INSTANCE.Send_SMS_After_Contributor_Pledges(_dbContext, contributionRecord);
      }
    }

    private contributor_details SaveNewRecord(
      ECARDAPPEntities _dbContext,
      Contributions contribution,
      long? event_id,
      long? customer_reg_no,
      string user_id)
    {
      contributor_details entity = new contributor_details()
      {
        contributor_name = contribution.Contributor_name,
        contribution_amount = CLASS_INSTANCE.UnformattedContributionAmount(contribution.Contribution_amount),
        mobile_no = contribution.Int_Mobile_number.Replace("+", ""),
        email_address = contribution.Email_address,
        event_det_sno = event_id,
        cust_reg_sno = customer_reg_no,
        currency_code = "TZS",
        contri_status = "pending",
        posted_by = user_id,
        posted_date = DateTime.Now
      };
      _dbContext.contributor_details.Add(entity);
      _dbContext.SaveChanges();
      return entity;
    }

    public static void AcceptReservationHandler(
      ECARDAPPEntities dbContext,
      contributor_details contributor,
      Contributions contribution)
    {
      contributor.no_of_persons = contribution.Number_of_people;
      contributor.contri_status = "affirmed";
      dbContext.Entry<contributor_details>(contributor).State = EntityState.Modified;
      dbContext.SaveChanges();
      SaveContributorAsInvitee(dbContext, contributor);
    }

    public static void RejectReservationHandler(
      ECARDAPPEntities dbContext,
      contributor_details contributor)
    {
      contributor.no_of_persons = new int?();
      contributor.contri_status = "unsettled";
      dbContext.Entry<contributor_details>(contributor).State = EntityState.Modified;
      dbContext.SaveChanges();
    }

    public static bool IsContributionRecordUpdated(Contributions contribution)
    {
      using (ECARDAPPEntities _dbContext = new ECARDAPPEntities())
      {
        contributor_details contributorDetails = _dbContext.contributor_details.Find(new object[1]
        {
          (object) contribution.Contribution_id
        });
        if (contributorDetails == null)
          return false;
        contributorDetails.contributor_name = contribution.Contributor_name;
        contributorDetails.mobile_no = contribution.Int_Mobile_number.Replace("+", "");
        contributorDetails.email_address = contribution.Email_address;
        _dbContext.Entry<contributor_details>(contributorDetails).State = EntityState.Modified;
        _dbContext.SaveChanges();
        CLASS_INSTANCE.Send_SMS_After_Contributor_Pledges(_dbContext, contributorDetails);
        return true;
      }
    }

    private void SaveContributionControlNumber(
      ECARDAPPEntities _dbContext,
      contributor_details contributionRecord,
      Contributions contribution)
    {
      contributionRecord.control_no = contribution.GetGenerateControlNumber(contributionRecord.contri_det_sno);
      _dbContext.Entry<contributor_details>(contributionRecord).State = EntityState.Modified;
      _dbContext.SaveChanges();
    }

    public static string FormattedThousandsSeparator(Decimal? amount)
    {
      return amount.HasValue ? string.Format("{0:n0}", (object) amount) : (string) null;
    }

    private Decimal? UnformattedContributionAmount(string formattedAmount)
    {
      return new Decimal?(Convert.ToDecimal(formattedAmount.Replace(",", "")));
    }

    private static void SaveContributorAsInvitee(
      ECARDAPPEntities dbContext,
      contributor_details contributor)
    {
      visitor_details entity = new visitor_details()
      {
        event_det_sno = contributor.event_det_sno,
        cust_reg_sno = contributor.cust_reg_sno,
        visitor_name = contributor.contributor_name,
        card_state_mas_sno = CardStates_Master.DEFAULT_CARBD_STATE_ID,
        no_of_persons = contributor.no_of_persons,
        mobile_no = contributor.mobile_no,
        email_address = contributor.email_address,
        posted_date = DateTime.Now,
        posted_by = contributor.posted_by
      };
      dbContext.visitor_details.Add(entity);
      dbContext.SaveChanges();
      entity.qrcode = Visitors.GetGeneratedQRCodeWithEventId(dbContext,entity.event_det_sno);
      dbContext.Entry<visitor_details>(entity).State = EntityState.Modified;
      dbContext.SaveChanges();
    }

    public static void Send_SMS_All_Contributor_Pledges(long? id)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        List<contributor_details> list = ecardappEntities.contributor_details.Where(iv => iv.event_det_sno == id).ToList();
        sms_contribution smsContribution = ecardappEntities.sms_contribution.Where(ib => ib.event_det_sno == id).First();
        if (list == null && smsContribution == null)
          return;
        foreach (contributor_details contributorDetails in list)
        {
          string contributorName = contributorDetails.contributor_name;
          FormattedThousandsSeparator(contributorDetails.contribution_amount);
          string eventName = smsContribution.event_details.event_name;
          string controlNo = contributorDetails.control_no;
          SMS_Handler.SendLocalSMS(smsContribution.sms_text.Replace("{contributor}", contributorName), contributorDetails.mobile_no);
        }
      }
    }

    private void Send_SMS_After_Contributor_Pledges(
      ECARDAPPEntities _dbContext,
      contributor_details contributionRecord)
    {
      event_details eventDetails = _dbContext.event_details.Find(contributionRecord.event_det_sno);
      var smsContribution = _dbContext.sms_contribution.Where(ic => ic.event_det_sno == contributionRecord.event_det_sno).First();
      if (eventDetails == null && smsContribution == null)
        return;
      string contributorName = contributionRecord.contributor_name;
      FormattedThousandsSeparator(contributionRecord.contribution_amount);
      string eventName = eventDetails.event_name;
      string controlNo = contributionRecord.control_no;
      SMS_Handler.SendLocalSMS(smsContribution.sms_text.Replace("{contributor}", contributorName), contributionRecord.mobile_no);
    }
  }
}
