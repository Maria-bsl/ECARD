using ECARD.DL.EDMX;
using FUNDING.Models.WhatsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
    public class WhatsAppConfigController : Controller
    {


        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

        // GET: WhatsAppConfig
        public ActionResult Index()
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.RedirectToAction("Login", "Login");
            return View();
        }


        [HttpPost]
        public ActionResult GetTemplateDetails()
        {
            try
            {

                var list = (from w in _dbContext.contents_template_master
                            select new WhatsAppConfig
                            {
                                Cont_mas_sno = w.cont_mas_sno,
                                Content_code = w.content_code,
                                Status = w.status,
                                Content_name = w.content_name,
                                Content_sid = w.content_sid,
                                Content_variables = w.content_variables,
                                Messaging_service_sid = w.messaging_service_sid,
                                Posted_by = w.posted_by,
                                Posted_date = w.posted_date,
                                Misc_column1 = w.misc_column1,
                                Misc_column2 = w.misc_column2

                            }).ToList();

                var contentDetails = (list != null && list.Count > 0) ? list : null;
                return contentDetails != null ? Json(contentDetails, JsonRequestBehavior.AllowGet) : Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return null;
        }


        
        [HttpPost]
        public ActionResult AddTemplateDetail(WhatsAppConfig whatsAppConfig)
        {

            try
            {
                if (this.ModelState.IsValid)
                {
                    if (whatsAppConfig.Sno == 0)
                    {
                            contents_template_master entity = new contents_template_master()
                            {
                                content_name = whatsAppConfig.Content_name.Trim(),
                                content_code = whatsAppConfig.Content_code.Trim(),
                                content_sid = whatsAppConfig.Content_sid.Trim(),
                                content_variables = whatsAppConfig.Content_variables.Trim().Replace("/", "").Replace("\\", ""),
                                misc_column1 = whatsAppConfig.Misc_column1,
                                messaging_service_sid = whatsAppConfig.Messaging_service_sid.Trim(),
                                status = whatsAppConfig.Status,
                                posted_by = Session["UserID"].ToString(),
                                posted_date = new DateTime?(DateTime.Now)
                            };

                            try { 
                                _dbContext.contents_template_master.Add(entity);
                                _dbContext.SaveChanges();
                                var sno = entity.cont_mas_sno;
                            return Json(sno, JsonRequestBehavior.AllowGet);

                            }
                                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                            {
                                foreach (var validationErrors in ex.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        // Log the property and error for debugging
                                        System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                                    }
                                }

                                // Optionally return some error response or view
                                ModelState.AddModelError("", "Validation error occurred. Check the inputs.");
                                return View(whatsAppConfig); // Return the view with the current data to correct
                        }

                    }
                    else if (whatsAppConfig.Sno > 0)
                    {
                        var config = _dbContext.contents_template_master.Where(c => c.cont_mas_sno == whatsAppConfig.Sno).FirstOrDefault();
                        
                        if (config.ToString() != null)
                        {

                            config.content_name = whatsAppConfig.Content_name.Trim();
                            config.content_code = whatsAppConfig.Content_code.Trim();
                            config.content_sid = whatsAppConfig.Content_sid.Trim();
                            config.content_variables = whatsAppConfig.Content_variables.Trim().Replace("/","").Replace("\\","");
                            config.misc_column1 = whatsAppConfig.Misc_column1;
                            config.messaging_service_sid = whatsAppConfig.Messaging_service_sid.Trim();
                            config.status = whatsAppConfig.Status;
                            config.posted_by = Session["UserID"].ToString();
                            config.posted_date = new DateTime?(DateTime.Now);

                        try { 
                           
                                _dbContext.SaveChanges();
                                var sno = config.cont_mas_sno;

                              return Json(sno, JsonRequestBehavior.AllowGet);

                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                                {
                                    foreach (var validationErrors in ex.EntityValidationErrors)
                                    {
                                        foreach (var validationError in validationErrors.ValidationErrors)
                                        {
                                            // Log the property and error for debugging
                                            System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                                        }
                                    }

                                    // Optionally return some error response or view
                                    ModelState.AddModelError("", "Validation error occurred. Check the inputs.");
                                    return View(whatsAppConfig); // Return the view with the current data to correct
                    }
                                }
                            }
                        }
                         ModelState.AddModelError("", "Validation error occurred. Check the inputs."); 
                        return View(whatsAppConfig);
                    }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            //return null;
        }

        /*
         *  package_details entity = ecardappEntities.package_details.Where<package_details>((Expression<Func<package_details, bool>>)(n => n.pack_det_sno == no)).FirstOrDefault<package_details>();
                if (entity == null)
                    return;
                ecardappEntities.package_details.Remove(entity);
                ecardappEntities.SaveChanges();

         */


        // whatsappconfig/remtemplatedetail
        [HttpPost]
        public ActionResult RemTemplateDetail(int Sno)
        {
           //if( _dbContext.contents_template_master.Where(ev => ev.cont_mas_sno == Sno).FirstOrDefault() == null)
            //{
                try
                {
                    if (Sno > 0)
                    {
                        var entity = _dbContext.contents_template_master.Where(n => n.cont_mas_sno == Sno).FirstOrDefault();
                        if (entity == null)
                            return Json(0, JsonRequestBehavior.AllowGet);

                        _dbContext.contents_template_master.Remove(entity);
                        _dbContext.SaveChanges();
                        return Json(Sno, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                }
        //}
            return Json(0, JsonRequestBehavior.AllowGet);
    }




    }
}