namespace Wizard.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;

    public class NavigationCommandView
    {
        private readonly NavigationCommand<WizardStep> navigationCommand;

        public NavigationCommandView(NavigationCommand<WizardStep> navigationCommand)
        {
            this.navigationCommand = navigationCommand ?? throw new ArgumentNullException("navigationCommand");            
        }

        public string Text
        {
            get
            {
                return this.navigationCommand.Text;
            }
        }

        public string Id
        {
            get
            {
                return this.navigationCommand.Id;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.navigationCommand.Enabled;
            }
        }

        public bool Visible
        {
            get
            {
                return this.navigationCommand.Visible;
            }
        }

        public string CssClass
        {
            get
            {
                if (navigationCommand is NextNavigationCommand ||
                    navigationCommand is FinishNavigationCommand)
                {
                    return "pull-right";
                }
                if (navigationCommand is PreviousNavigationCommand)
                {
                    return "pull-left";
                }
                return "";
            }
        }
    }
}
