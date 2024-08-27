// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.SMS_EVENT_TEXT
// Assembly: FUNDING.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A421DCBA-0154-4E02-9814-774D89923779
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.BL.dll

using ECARD.DL.EDMX;
using System;

#nullable disable
namespace FUNDING.BL.BusinessEntities.Masters
{
  public class SMS_EVENT_TEXT
  {
    public long sms_e_t_sno { get; set; }

    public long? event_det_sno { get; set; }

    public long? s_e_i_sno { get; set; }

    public string sms_text { get; set; }

    public string card_size { get; set; }

    public string includes { get; set; }

    public string excludes { get; set; }

    public string posted_by { get; set; }

    public DateTime? posted_date { get; set; }

    public virtual event_details event_details { get; set; }

    public virtual sms_email_instances sms_email_instances { get; set; }
  }
}
