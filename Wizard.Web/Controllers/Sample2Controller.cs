using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wizard.Model;
using Wizard.Web.Infrastructure;
using Wizard.Web.Models.Steps;

namespace Wizard.Web.Controllers
{
    [Authorize]
    public class Sample2Controller : WizardBaseController
    {
        public Sample2Controller(IConfiguration configuration,
            IWizardNavigatorService<WizardStep> navigatorPersistenceService) :
            base(configuration, navigatorPersistenceService)
        {

        }

        public IActionResult Step1()
        {
            return StepResult(new Step1ViewModel());
        }

        [HttpPost]
        public IActionResult Step1(Step1ViewModel model)
        {
            return StepResult(ModelState.IsValid);
        }

        public IActionResult Step2()
        {
            return StepResult(new Step2ViewModel());
        }

        [HttpPost]
        public IActionResult Step2(Step2ViewModel model)
        {
            return StepResult(ModelState.IsValid);
        }

        public override string WizardName => "Sample2";
    }
}