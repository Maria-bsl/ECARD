using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public  class Comments
    {
        #region Properties
        public long C_ID { set; get; }
        public long C_From_User { set; get; }
        public long C_To_User { set; get; }
        public long Sno { set; get; }
        public String C_Screen { get; set; }
        public Decimal Percentage_Limit { get; set; }
        public String Posted_by { get; set; }
        public DateTime C_Date { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public String Status { get; set; }
        #endregion  properties
        #region methods
        public long Comment_Add(Comments SA)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //comment ALadd = new comment()
                //{
                //    c_id = SA.C_ID,
                //    c_screen = SA.C_Screen,
                //    c_from_user = SA.C_From_User,
                //    c_to_user = SA.C_To_User,
                //    comments = SA.Comment,
                //    c_date=DateTime.Now,
                //    status = SA.Status,
                //};
                //context.comments.Add(ALadd);
                context.SaveChanges();
                return 90;
            }
        }
        public List<Comments> GetComment(String name,long sno)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var getdata = (from c in context.comments
                //               join det in context.emp_detail on c.c_from_user equals det.emp_detail_id
                //               where c.c_screen == name && c.c_id == sno
                //               select new Comments
                //               {
                //                   C_Screen = c.c_screen,
                //                   Comment = c.comments,
                //                   UserName = det.full_name,
                //                   C_Date = (DateTime)c.c_date,
                //                   Sno = c.sno,
                //               }).OrderByDescending(g => g.Sno).ToList();
                //if (getdata != null && getdata.Count > 0)
                //    return getdata;
                //else
                    return null;
            }
        }
        public List<Comments> GetCommentList(String name)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                //var getdata = (from c in context.comments 
                //               where  c.c_screen == name
                //               select new Comments
                //               {
                //                   C_Screen = c.c_screen,
                //                   Comment = c.comments,
                //                   Status = c.status,
                //                   C_Date=(DateTime)c.c_date,
                //                   Sno=c.sno,
                //               }).OrderByDescending(g => g.Sno).ToList();
                //if (getdata != null && getdata.Count > 0)
                //    return getdata;
                //else
                    return null;
            }
        }
        #endregion methods
    }
}
