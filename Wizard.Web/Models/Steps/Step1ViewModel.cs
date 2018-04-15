namespace Wizard.Web.Models.Steps
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Step1ViewModel
    {
        [Required]        
        public string Step1Value
        {
            get;
            set;
        }
    }
}
