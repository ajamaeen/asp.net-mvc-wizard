namespace Wizard.Web.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;

    public class WizardViewModel
    {
        public WizardViewModel(BaseWizard<WizardStep> wizard)
        {
            this.Wizard = wizard ?? throw new ArgumentException("wizard");
        }

        public BaseWizard<WizardStep> Wizard { get; }

        public String ControllerName
        {
            get
            {
                return (string)this.Wizard.ActiveWizardStep["controllername"];
            }
        }

        public String Action
        {
            get
            {
                return (string)this.Wizard.ActiveWizardStep["action"];
            }
        }

        public String WizardTitle
        {
            get
            {
                return "Registration Wizard";
            }
        }

        public IList<NavigationCommandView> Commands
        {
            get
            {
                return this.Wizard.NavigationCommands.Where(nva => nva.Visible).Select(nav => new NavigationCommandView(nav)).ToList();
            }
        }

        public IEnumerable<BookmarkView> Bookmarks
        {
            get
            {
                return Wizard.WizardSteps.Select(stp => new BookmarkView(new Bookmark<WizardStep>(this.Wizard.Navigator, stp)));
            }
        }
    }
}
