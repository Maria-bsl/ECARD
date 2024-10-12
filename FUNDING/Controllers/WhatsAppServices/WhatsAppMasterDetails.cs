using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECARD.DL.EDMX;
using FUNDING.Models.WhatsApp;



namespace FUNDING.Controllers.WhatsAppServices
{
    public class WhatsAppMasterDetails
    {
        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
        public WhatsAppMaster GetConfigs()
        {
            var whatsAppdetails = (from cs in _dbContext.whatsapp_master
                                    where cs.status.ToLower() == "active"
                                    select new WhatsAppMaster 
                                    {
                                        Account_sid = cs.account_sid,
                                        Auth_token = cs.auth_token,
                                        Status = cs.status,
                                        Effective_date = cs.effective_date,
                                        Misc_column1 = cs.misc_column1,
                                        Misc_column2 = cs.misc_column2,
                                        Misc_column3 = cs.misc_column3,
                                        Posted_date = cs.posted_date,
                                        Posted_by = cs.posted_by,
                                        Whatsapp_sender = cs.whatsapp_sender,
                                        Callback_url = cs.callback_url,
                                        Webhook_error_log = cs.webhook_error_log,
                                        Webhook_incoming = cs.webhook_incoming
                                        
                                    }).OrderByDescending(w => w.Effective_date).FirstOrDefault();

            return whatsAppdetails;
        }
        public List<WhatsAppMaster> GetConfigsAll()
        {
            var whatsAppdetails = (from cs in _dbContext.whatsapp_master
                                   where cs.status.ToLower() == "active"
                                   select new WhatsAppMaster
                                   {
                                       Account_sid = cs.account_sid,
                                       Auth_token = cs.auth_token,
                                       Status = cs.status,
                                       Effective_date = cs.effective_date,
                                       Misc_column1 = cs.misc_column1,
                                       Misc_column2 = cs.misc_column2,
                                       Misc_column3 = cs.misc_column3,
                                       Posted_date = cs.posted_date,
                                       Posted_by = cs.posted_by,
                                       Whatsapp_sender = cs.whatsapp_sender,
                                       Callback_url = cs.callback_url,
                                       Webhook_error_log = cs.webhook_error_log,
                                       Webhook_incoming = cs.webhook_incoming

                                   }).OrderByDescending(w => w.Effective_date).ToList();

            return whatsAppdetails;
        }

        public List<WhatsAppConfig> GetTemplatesContentAll()
        {
            var whatsAppdetails = (from cs in _dbContext.contents_template_master
                                   where cs.status.ToLower() == "active"
                                   select new WhatsAppConfig
                                   {
                                       Content_code = cs.content_code,
                                       Content_name = cs.content_name,
                                       Content_sid = cs.content_sid,
                                       Content_variables = cs.content_variables,
                                       Messaging_service_sid = cs.messaging_service_sid,
                                       Posted_by = cs.posted_by,
                                       Posted_date = cs.posted_date,
                                       Status = cs.status,
                                       Misc_column1 = cs.misc_column1,
                                       Effective_date = cs.effective_date
                                       
                                   }).OrderByDescending(w => w.Posted_date).ToList();

            return whatsAppdetails;
        }

        public WhatsAppConfig GetTemplatesContentBySno(long sno)
        {
            var whatsAppdetails = (from cs in _dbContext.contents_template_master
                                   where cs.status.ToLower() == "active" && cs.cont_mas_sno == sno
                                   select new WhatsAppConfig
                                   {
                                       Content_code = cs.content_code,
                                       Content_name = cs.content_name,
                                       Content_sid = cs.content_sid,
                                       Content_variables = cs.content_variables,
                                       Messaging_service_sid = cs.messaging_service_sid,
                                       Posted_by = cs.posted_by,
                                       Posted_date = cs.posted_date,
                                       Status = cs.status,
                                       Misc_column1 = cs.misc_column1,
                                       Effective_date = cs.effective_date

                                   }).OrderByDescending(w => w.Posted_date).FirstOrDefault();

            return whatsAppdetails;
        }




    }
}