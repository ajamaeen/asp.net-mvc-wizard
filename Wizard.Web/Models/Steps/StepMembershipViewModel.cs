namespace Wizard.Web.Models.Steps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class StepMembershipViewModel
    {
        [Required]
        [Display(Name = "Membership NO")]
        public int MembershipNumber
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Issue Place")]
        public string IssuePlace
        {
            get;
            set;
        }
    }
}
