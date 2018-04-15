namespace Wizard.Model
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class NavigationCommand<T> where T : WizardStep
    {
        private readonly IWizardNavigator<T> navigator;

        protected NavigationCommand(IWizardNavigator<T> navigator)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException("navigator");
            }

            this.navigator = navigator;
        }

        protected IWizardNavigator<T> Navigator
        {
            get
            {
                return this.navigator;
            }
        }

        public abstract string Text
        {
            get;
        }

        public abstract string Id
        {
            get;
        }

        public abstract bool Enabled
        {
            get;
        }

        public virtual bool Visible
        {
            get
            {
                return true;
            }
        }

        public void Execute(object context, bool isStepCompleted)
        {
            this.OnExecute(context, isStepCompleted);
        }

        protected abstract void OnExecute(object context, bool isStepCompleted);
    }
}
