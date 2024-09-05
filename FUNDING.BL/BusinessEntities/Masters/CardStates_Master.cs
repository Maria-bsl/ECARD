using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class CardStates_Master
    {

        public static readonly long? DEFAULT_CARBD_STATE_ID = 1;

        public CardStates_Master()
        {
            VisitorsVerification = new HashSet<Visitors_Verification>();
            Visitor = new HashSet<Visitors>();
        }

        public long card_state_mas_sno { get; set; }

        [Required(ErrorMessage = "Please enter card state.")]
        [MaxLength(20, ErrorMessage = "The field allows only 20 characters.")]
        [Display(Name = "Card State")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z0-9])?[a-zA-Z0-9]*)*$", ErrorMessage = "Please enter a valid card state.")]
        public string card_state { get; set; }

        [Display(Name = "Posted By")]
        public string posted_by { get; set; }

        [Display(Name = "Posted Date")]
        public System.DateTime posted_date { get; set; }

        public virtual ICollection<Visitors_Verification> VisitorsVerification { get; set; }

        public virtual ICollection<Visitors> Visitor { get; set; }
    }
}
