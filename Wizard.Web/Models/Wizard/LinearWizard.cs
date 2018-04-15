namespace Wizard.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;

    public class LinearWizard : BaseWizard<WizardStep>
    {
        public LinearWizard(WizardStepCollection steps, IWizardNavigator<WizardStep> navigator)
         : base(steps, navigator)
        {
            
        }

        protected override IEnumerable<NavigationCommand<WizardStep>> CreateNavigationCommands()
        {
            return new List<NavigationCommand<WizardStep>>
           {
               new NextNavigationCommand(this.Navigator),
               new PreviousNavigationCommand(this.Navigator),
               new FinishNavigationCommand(this.Navigator)
           };
        }
    }
}
