namespace Wizard.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Wizard.Model;
    using Wizard.Web.Infrastructure;
    using Wizard.Web.Models;
    using Wizard.Web.Models.Steps;

    [Authorize]
    public class Sample1Controller : WizardBaseController
    {   
        public Sample1Controller(IConfiguration configuration,
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

        public IActionResult Step3()
        {
            return StepResult();
        }

        [HttpPost]
        public IActionResult Step3(IFormCollection formData)
        {
            return StepResult(true);
        }

        public IActionResult Step4()
        {
            return StepResult();
        }

        [HttpPost]
        public IActionResult Step4(IFormCollection formData)
        {
            return StepResult(true);
        }

        public IActionResult Step5()
        {
            return StepResult();
        }

        [HttpPost]
        public IActionResult Step5(IFormCollection formData)
        {
            return StepResult(true);
        }

        public override string WizardName => "Sample1";
    }
}