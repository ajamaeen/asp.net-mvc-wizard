namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WizardNavigationEventArgs : EventArgs
    {
        public WizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
        {
            this.CurrentStepIndex = currentStepIndex;
            this.NextStepIndex = nextStepIndex;
        }

        public bool Cancel { get; set; }
    
        public int CurrentStepIndex { get; }
      
        public int NextStepIndex { get; }
    }
}
