namespace Wizard.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Wizard.Model;
    using Wizard.Web.Infrastructure;
    using Wizard.Web.Models;
    using Wizard.Web.Models.Steps;

    [Authorize]
    public class MembershipController : WizardBaseController
    {
        public MembershipController(IConfiguration configuration,
          IWizardNavigatorService<WizardStep> navigatorPersistenceService,
          IHttpContextAccessor httpContextAccessor) :
          base(configuration, navigatorPersistenceService)
        {
        }

        public IActionResult Index()
        {
            return StepResult(new StepMembershipViewModel());
        }

        [HttpPost]
        public IActionResult Index(StepMembershipViewModel model)
        {
            if (model.MembershipNumber < 0)
            {
                this.ModelState.AddModelError("", "Membership must be greater than zero");
            }

            if (ModelState.IsValid)
            {
                return StepResult(model, true);
            }
            else
            {
                return StepResult(model, false);
            }
        }

        public override string WizardName => "Sample2";
    }
}