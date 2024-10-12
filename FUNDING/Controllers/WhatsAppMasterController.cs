using ECARD.DL.EDMX;
using System;
using FUNDING.Models.WhatsApp;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.Controllers
{

    public class WhatsAppMasterController : Controller
    {

        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

        // GET: WhatsAppMaster
        public ActionResult Index()
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.RedirectToAction("Login", "Login");
            return View();
        }


        [HttpPost]
        public ActionResult GetDetails()
        {
            try
            {

                var list = (from w in _dbContext.whatsapp_master select new WhatsAppMaster
                {
                    Account_sid = w.account_sid,
                    Auth_token = w.auth_token,
                    Status = w.status,
                    Whatsapp_sender = w.whatsapp_sender,
                    Whats_mas_sno = w.whats_mas_sno,
                    Webhook_error_log = w.webhook_error_log ?? "",
                    Callback_url = w.callback_url ?? "",
                    Webhook_incoming = w.webhook_incoming ?? "",
                    Posted_by = w.posted_by,
                    Posted_date = w.posted_date,
                    Effective_date = w.effective_date,
                    Misc_column1 = w.misc_column1,
                    Misc_column2 = w.misc_column2 ?? ""

                }).ToList();
           
                var Details =  (list != null && list.Count > 0) ? list : null;
                return Details != null ? Json(Details, JsonRequestBehavior.AllowGet) : Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json(new { response = "Success", message = "" });
        }






        [HttpPost]
        public ActionResult AddMasterDetail(WhatsAppMaster whatsAppMaster)
        {

            try
            {
                if (this.ModelState.IsValid)
                {


                    if (whatsAppMaster.Sno == 0)
                    {

                        whatsapp_master entity = new whatsapp_master()
                        {
                            whatsapp_sender = whatsAppMaster.Whatsapp_sender.Trim(),
                            whats_mas_sno = whatsAppMaster.Whats_mas_sno,
                            webhook_error_log = whatsAppMaster.Webhook_error_log ?? "",
                            callback_url = whatsAppMaster.Callback_url ?? "",
                            account_sid = whatsAppMaster.Account_sid.Trim(),
                            auth_token = whatsAppMaster.Auth_token.Trim(),
                            effective_date = whatsAppMaster.Effective_date,
                            status = whatsAppMaster.Status,
                            webhook_incoming = whatsAppMaster.Webhook_incoming ?? "",
                            posted_by = Session["UserID"].ToString(),
                            posted_date = new DateTime?(DateTime.Now)
                        };
                        _dbContext.whatsapp_master.Add(entity);
                        _dbContext.SaveChanges();
                        var sno = entity.account_sid;

                        return Json(sno, JsonRequestBehavior.AllowGet);


                    }
                    else if (whatsAppMaster.Sno > 0)
                    {
                        var config = _dbContext.whatsapp_master.Where(c => c.whats_mas_sno == whatsAppMaster.Sno).FirstOrDefault();

                        if (config.ToString() != null)
                        {

                            config.account_sid = whatsAppMaster.Account_sid.Trim();
                            config.whatsapp_sender = whatsAppMaster.Whatsapp_sender.Trim();
                            config.auth_token = whatsAppMaster.Auth_token.Trim();
                            config.callback_url = whatsAppMaster.Callback_url ?? "";
                            config.webhook_error_log = whatsAppMaster.Webhook_error_log ?? "";
                            config.effective_date = whatsAppMaster.Misc_column1;
                            config.webhook_incoming = whatsAppMaster.Webhook_incoming ?? "";
                            config.status = whatsAppMaster.Status;
                            config.posted_by = Session["UserID"].ToString();
                            config.posted_date = new DateTime?(DateTime.Now);

                            _dbContext.SaveChanges();
                            var sno = config.whats_mas_sno;

                            return Json(sno, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                return (ActionResult)null;
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            //return null;
        }



        [HttpPost]
        public ActionResult RemMasterDetail(int Sno)
        {
            
            try
            {
                if (Sno > 0)
                {
                    var entity = _dbContext.whatsapp_master.Where(n => n.whats_mas_sno == Sno).FirstOrDefault();
                    if (entity == null)
                        return Json(0, JsonRequestBehavior.AllowGet);

                    _dbContext.whatsapp_master.Remove(entity);
                    _dbContext.SaveChanges();
                    return Json(Sno, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
          
            return Json(0, JsonRequestBehavior.AllowGet);
        }


    }
}