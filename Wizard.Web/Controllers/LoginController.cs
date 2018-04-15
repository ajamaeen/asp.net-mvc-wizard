namespace Wizard.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Wizard.Model;
    using Wizard.Web.Infrastructure;
    using Wizard.Web.Models.Steps;

    public class LoginController : WizardBaseController
    {
        public LoginController(IConfiguration configuration,
         IWizardNavigatorService<WizardStep> navigatorPersistenceService,
         IHttpContextAccessor httpContextAccessor) :
         base(configuration, navigatorPersistenceService)
        {
            
        }

        public IActionResult Index()
        {
            return StepResult(new StepLoginViewModel());
        }

        [HttpPost]
        public IActionResult Index(StepLoginViewModel model)
        {
            ModelState.AddModelError("", "Intended error message >> amer");
            return StepResult(model, true);
        }

        public override string WizardName => "Sample2";
    }
}