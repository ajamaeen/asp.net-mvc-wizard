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

    public class PreviousNavigationCommand : NavigationCommand<WizardStep>
    {
        public PreviousNavigationCommand(IWizardNavigator<WizardStep> navigator)
            : base(navigator)
        {
            
        }

        public override string Text => "Previous";

        public override string Id => "Previous";

        public override bool Enabled => throw new NotImplementedException();

        public override bool Visible => !(this.Navigator.ActiveStepIndex == 0);

        protected override void OnExecute(object context, bool isStepCompleted)
        {
            var actionExecutingContext = (ActionExecutedContext)context;

            if (this.Navigator.MoveToPreviousStep())
            {
                this.Navigator.SaveWizardState();
                actionExecutingContext.Result = new RedirectToActionResult((string)Navigator.ActiveStep["action"], (string)Navigator.ActiveStep["controllername"], null);
            }
        }
    }
}
