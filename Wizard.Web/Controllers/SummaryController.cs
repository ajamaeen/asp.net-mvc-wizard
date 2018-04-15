namespace Wizard.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;
    using Wizard.Web.Infrastructure;

    [Authorize]
    public class SummaryController : WizardBaseController
    {
        public SummaryController(IConfiguration configuration,
            IWizardNavigatorService<WizardStep> navigatorPersistenceService,
            IHttpContextAccessor httpContextAccessor) :
            base(configuration, navigatorPersistenceService)
        {
        }

        public IActionResult Index()
        {
            return StepResult();
        }

        [HttpPost]
        public IActionResult Index(object model)
        {
            return StepResult(model, true);
        }

        public override string WizardName => "Sample3";
    }
}
