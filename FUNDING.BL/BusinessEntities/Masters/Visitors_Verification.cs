using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Visitors_Verification
    {
        public long qr_ver_det_sno { get; set; }
        public long? visitor_det_sno { get; set; }
        public long? event_det_sno { get; set; }
        public long? cust_reg_sno { get; set; }
        public string qrcode { get; set; }
        public DateTime? event_start_time { get; set; }
        public long? card_state_mas_sno { get; set; }
        public int? no_of_persons { get; set; }
        public string scan_status { get; set; }
        public string posted_by { get; set; }
        public DateTime posted_date { get; set; }

        public virtual CardStates_Master CardStateMaster { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Events Event { get; set; }
        public virtual Visitors Visitor { get; set; }
    }
}
