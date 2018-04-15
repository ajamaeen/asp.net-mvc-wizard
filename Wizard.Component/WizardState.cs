namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WizardState<T> where T : WizardStep
    {
        public WizardState(WizardStepCollection wizardSteps, string activeStepId)
        {
            this.WizardSteps = wizardSteps ?? throw new ArgumentNullException("wizardSteps");
            this.ActiveStepId = activeStepId;
        }

        public WizardStepCollection WizardSteps
        {
            get;
        }

        public string ActiveStepId
        {
            get;
        }
    }
}
