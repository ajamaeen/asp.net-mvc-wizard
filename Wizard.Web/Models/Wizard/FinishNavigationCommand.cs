namespace Wizard.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;

    public class FinishNavigationCommand : NavigationCommand<WizardStep>
    {
        public FinishNavigationCommand(IWizardNavigator<WizardStep> navigator)
            : base(navigator)
        {

        }

        public override string Text => "Finish";

        public override string Id => "Finish";

        protected override void OnExecute(object context, bool isStepCompleted)
        {
            var actionExecutingContext = (ActionExecutedContext)context;              
            if (isStepCompleted)
            {
                actionExecutingContext.HttpContext.Response.Redirect("/Home/Completed");
            }
        }

        public override bool Enabled => this.Navigator.ActiveStepIsLastStep;

        public override bool Visible => this.Navigator.ActiveStepIsLastStep;
    }
}
