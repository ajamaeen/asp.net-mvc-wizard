using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wizard.Model;
using Wizard.Web.Models;

namespace Wizard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWizardNavigatorService<WizardStep> navigatorPersistenceService;

        public HomeController(IWizardNavigatorService<WizardStep> navigatorPersistenceService)
        {
            this.navigatorPersistenceService = navigatorPersistenceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult Completed()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
                
        public IActionResult ClearStat()
        {
            this.navigatorPersistenceService.ClearWizardState();
            return RedirectToAction(nameof(Index));
        }
    }
}
