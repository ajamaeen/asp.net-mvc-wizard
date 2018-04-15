namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWizardNavigatorService<T> where T : WizardStep
    {
        void SaveWizardState(WizardState<T> state);

        WizardState<T> LoadWizardState();

        string LoadWizardStateAsString();

        void ClearWizardState();
    }
}
