namespace Wizard.Web.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;
    using Wizard.Web.Data;
    using Wizard.Web.Models;

    public class NavigatorPersistenceService : IWizardNavigatorService<WizardStep>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> manager;
        private readonly ApplicationDbContext context;

        public NavigatorPersistenceService(IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.manager = manager;
        }

        public void ClearWizardState()
        {
            var user = manager.GetUserAsync(this.httpContextAccessor.HttpContext.User).Result;

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            user.WizardState = null;
            IdentityResult result = this.manager.UpdateAsync(user).Result;
            this.context.SaveChanges();
        }

        public WizardState<WizardStep> LoadWizardState()
        {
            var user = manager.GetUserAsync(this.httpContextAccessor.HttpContext.User).Result;

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!string.IsNullOrWhiteSpace(user.WizardState))
            {
                return JsonConvert.DeserializeObject<WizardState<WizardStep>>(user.WizardState);
            }
            else
            {
                return null;
            }
        }

        public string LoadWizardStateAsString()
        {
            var user = manager.GetUserAsync(this.httpContextAccessor.HttpContext.User).Result;

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return user.WizardState;
        }

        public void SaveWizardState(WizardState<WizardStep> state)
        {
            var user = manager.GetUserAsync(this.httpContextAccessor.HttpContext.User).Result;

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var json = JsonConvert.SerializeObject(state);

            user.WizardState = json;
            IdentityResult result = this.manager.UpdateAsync(user).Result;
            this.context.SaveChanges();
        }
    }
}
