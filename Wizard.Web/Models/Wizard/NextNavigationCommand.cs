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

    public class NextNavigationCommand : NavigationCommand<WizardStep>
    {
        public NextNavigationCommand(IWizardNavigator<WizardStep> navigator)
            : base(navigator)
        {

        }

        public override string Text => "Next";

        public override string Id => "Next";

        public override bool Enabled => !this.Navigator.ActiveStepIsLastStep;

        public override bool Visible => !this.Navigator.ActiveStepIsLastStep;

        protected override void OnExecute(object context, bool isStepCompleted)
        {
            var actionExecutingContext = (ActionExecutedContext)context;

            if(isStepCompleted)
            {
                this.Navigator.ActiveStep.IsCompleted = isStepCompleted;

                if (this.Navigator.MoveToNextStep())
                {
                    this.Navigator.SaveWizardState();
                    actionExecutingContext.Result = new RedirectToActionResult((string)Navigator.ActiveStep["action"], (string)Navigator.ActiveStep["controllername"], null);
                }
            }           
        }
    }
}
