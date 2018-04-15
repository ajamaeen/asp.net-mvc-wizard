namespace Wizard.Web.Infrastructure
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;
    using Wizard.Web.Models;

    public abstract class WizardBaseController : Controller
    {
        private readonly BaseWizard<WizardStep> wizard;

        private bool stepCompleted = false;

        public WizardBaseController(IConfiguration configuration,
            IWizardNavigatorService<WizardStep> navigatorPersistenceService)
        {
            var builder = new LinearWizardBuilder(this.WizardName, configuration, navigatorPersistenceService);
            wizard = builder.Build();
            this.wizard.Navigator.ActiveStepChanged += Navigator_ActiveStepChanged;
        }

        private void Navigator_ActiveStepChanged(object sender, EventArgs args)
        {
            var navigationEvent = (WizardNavigationEventArgs)args;

            if (navigationEvent.Cancel)
            {

            }
        }

        protected IWizardNavigator<WizardStep> Navigator
        {
            get
            {
                return this.wizard.Navigator;
            }
        }

        public IActionResult StepResult()
        {
            this.ViewBag.WizardView = this.WizardView;
            return View();
        }

        public IActionResult StepResult(object model)
        {
            this.ViewBag.WizardView = this.WizardView;
            return View(model);
        }

        public IActionResult StepResult(bool stepCompleted)
        {
            this.stepCompleted = stepCompleted;
            this.ViewBag.WizardView = this.WizardView;
            return View();
        }

        public IActionResult StepResult(object model, bool stepCompleted)
        {
            this.stepCompleted = stepCompleted;
            this.ViewBag.WizardView = this.WizardView;
            return View(model);
        }

        protected WizardViewModel WizardView
        {
            get
            {
                return new WizardViewModel(this.wizard);
            }
        }

        public IActionResult Bookmark(string stepId)
        {
            Bookmark<WizardStep> bookmark = new Bookmark<WizardStep>(wizard.Navigator, wizard[stepId]);
            bookmark.GoTo();
            return RedirectToAction(this.WizardView.Action, this.WizardView.ControllerName);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var commandId = context.HttpContext.Request.Query["commandid"];
            var command = this.wizard.NavigationCommands.FirstOrDefault(cmd => cmd.Id == commandId);

            if (command != null)
            {
                command.Execute(context, stepCompleted);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (descriptor != null)
            {
                if (!string.Equals(descriptor.ActionName, "Bookmark", StringComparison.OrdinalIgnoreCase))
                {
                    //TODO : in article write more info about this
                    //To avoid playing with Url and incase the state has been lost or 
                    //To avoid playing with url
                    if (!string.Equals(descriptor.ActionName, this.WizardView.Action, StringComparison.OrdinalIgnoreCase)
                     || !string.Equals(descriptor.ControllerName, this.WizardView.ControllerName, StringComparison.OrdinalIgnoreCase))
                    {
                        context.Result = new RedirectToActionResult(this.WizardView.Action, this.WizardView.ControllerName, null);
                    }
                }
            }
        }

        public virtual string WizardName
        {
            get
            {
                return "wizard1";
            }
        }
    }
}
