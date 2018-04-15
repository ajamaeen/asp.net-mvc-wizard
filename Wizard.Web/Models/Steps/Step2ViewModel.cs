namespace Wizard.Web.Models.Steps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Step2ViewModel
    {
        [Required]
        [Display(Name = "Duration")]
        [DataType(DataType.Duration)]
        public string Value1
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Value2
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Value3
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public string Value4
        {
            get;
            set;
        }
    }
}
