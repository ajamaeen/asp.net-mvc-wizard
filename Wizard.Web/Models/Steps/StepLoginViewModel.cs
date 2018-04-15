using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wizard.Web.Infrastructure;

namespace Wizard.Web.Models.Steps
{
    public class StepLoginViewModel 
    {
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}
