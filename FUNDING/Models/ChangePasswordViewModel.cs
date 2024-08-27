// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ChangePasswordViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

 
namespace FUNDING.Models
{
  public class ChangePasswordViewModel
  {
    [Display(Name = "Current Password")]
    [Required(ErrorMessage = "Current password is required")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "This field allows only alphanumeric")]
    public string CurrentPassword { get; set; }

    [Display(Name = "New Password")]
    [Required(ErrorMessage = "New password is required")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "This field allows only alphanumeric")]
    public string NewPassword { get; set; }

    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "This field allows only alphanumeric")]
    public string ConfirmPassword { get; set; }

    public bool ValidatePassword(string password)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
        return ecardappEntities.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (c => c.password == password)).Count<cust_users>() > 0;
    }

    public void UpdatePassword(long userID, string password)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        cust_users entity = ecardappEntities.cust_users.Find(new object[1]
        {
          (object) userID
        });
        if (entity == null)
          return;
        entity.password = password;
        ecardappEntities.Entry<cust_users>(entity).State = EntityState.Modified;
        ecardappEntities.SaveChanges();
      }
    }
  }
}
