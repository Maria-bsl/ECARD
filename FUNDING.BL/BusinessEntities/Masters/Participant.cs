// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.Participant
// Assembly: FUNDING.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A421DCBA-0154-4E02-9814-774D89923779
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.BL.dll

using System;

#nullable disable
namespace FUNDING.BL.BusinessEntities.Masters
{
  internal class Participant
  {
    public long pat_sno { get; set; }

    public string fullname { get; set; }

    public string gender { get; set; }

    public DateTime? dob { get; set; }

    public string region { get; set; }

    public string district { get; set; }

    public string ward { get; set; }

    public string posted_by { get; set; }

    public DateTime posted_date { get; set; }
  }
}
