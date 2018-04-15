namespace Wizard.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;
    using Wizard.Web.Infrastructure;

    public class LinearWizardBuilder
    {
        private readonly WizardStepCollection steps;

        private readonly IWizardNavigatorService<WizardStep> navigatorPersistenceService;

        public LinearWizardBuilder(string wizardName, IConfiguration configuration, IWizardNavigatorService<WizardStep> persistenceService)
        {
            Utility.ThrowIfNull(wizardName, nameof(wizardName));

            Utility.ThrowIfNull(configuration, nameof(configuration));

            this.navigatorPersistenceService = persistenceService ?? throw new ArgumentNullException("persistenceService");

            steps = new WizardStepCollection();

            var wizardSection = configuration.GetSection($"Wizards:{wizardName}");

            foreach (IConfigurationSection section in wizardSection.GetChildren())
            {
                AddStep(section.GetValue<string>("id"), section.GetValue<string>("title"),section.GetValue<string>("controllername"), section.GetValue<string>("action"));
            }
        }

        public LinearWizard Build()
        {
            var navigator = new WizardNavigator<WizardStep>(steps, this.navigatorPersistenceService);
            var wizard = new LinearWizard(steps, navigator);
            wizard.Start();
            return wizard;
        }

        private void AddStep(string id, string title, string controllerName, string action)
        {
            Utility.ThrowIfNull(id, nameof(id));
            Utility.ThrowIfNull(title, nameof(title));

            Utility.ThrowIfNull(controllerName, nameof(controllerName));
            Utility.ThrowIfNull(action, nameof(action));

            var step = new WizardStep(id, title);
            step["controllername"] = controllerName;
            step["action"] = action;

            steps.Add(step);
        }
    }
}
