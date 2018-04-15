namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class WizardStepCollection : KeyedCollection<string, WizardStep>
    {
        protected override string GetKeyForItem(WizardStep item)
        {
            return item.Id;
        }
    }
}
