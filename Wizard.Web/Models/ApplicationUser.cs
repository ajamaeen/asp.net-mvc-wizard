namespace Wizard.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    
    public class ApplicationUser : IdentityUser
    {
        public string WizardState
        {
            get;
            set;
        }
    }
}
