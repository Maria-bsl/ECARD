using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECARD.DL.EDMX;
namespace FUNDING.BL.BusinessEntities.Masters
{
   public class Tab_Details
    {
        #region Properties
        public string tab_name { get; set; }
        public long Sno { get; set; }
        public String Relation { get; set; }
        #endregion Properties
        #region Method
        public Tab_Details Getlog(string tab)
        {
            //DateTime add = to.AddDays(1);
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var adetails = (from mr in context.table_details
                                where mr.table_name == tab
                                select new Tab_Details
                                {
                                    tab_name = mr.table_name,
                                    Sno = mr.sno,
                                    Relation = mr.table_relation
                                }).FirstOrDefault();
                if (adetails != null )
                    return adetails;
                else
                    return null;
            }
        }
        #endregion Method
   }
}
