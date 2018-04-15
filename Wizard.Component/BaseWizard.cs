namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BaseWizard<T> where T : WizardStep
    {        
        private readonly WizardStepCollection _steps;

        private readonly IWizardNavigator<T> _navigator;
        
        private IList<NavigationCommand<T>> _navigationCommands;
                
        protected BaseWizard(WizardStepCollection steps, IWizardNavigator<T> navigator)
        {
            if (steps == null)
            {
                this._steps = new WizardStepCollection();
            }
            else
            {
                this._steps = steps;
            }

            this._navigator = navigator ?? throw new ArgumentNullException("navigator");
        }

        public WizardStepCollection WizardSteps
        {
            get
            {
                return this._steps;
            }
        }

        public IWizardNavigator<T> Navigator
        {
            get
            {
                return this._navigator;
            }
        }
        
        public IReadOnlyList<Bookmark<T>> Bookmarks
        {
            get
            {
                return new List<Bookmark<T>>(this._navigator.WizardSteps.Select(s => new Bookmark<T>(_navigator, (T)s)));
            }
        }

        public IReadOnlyList<NavigationCommand<T>> NavigationCommands
        {
            get
            {
                if (this._navigationCommands == null)
                {
                    this._navigationCommands = new List<NavigationCommand<T>>(this.CreateNavigationCommands());
                }

                return new ReadOnlyCollection<NavigationCommand<T>>(this._navigationCommands);
            }
        }

        public T ActiveWizardStep
        {
            get
            {
                return this._navigator.ActiveStep;
            }
        }
                
        public T this[string id]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentNullException("id");
                }

                var target = this._steps.FirstOrDefault(step => string.Equals(step.Id, id, StringComparison.Ordinal));

                if (target == null)
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    return (T)target;
                }
            }
        }
                                        
        public bool Contains(string id)
        {
            bool isFound = false;
            
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Argument cannot be empty string", "id");
            }

            foreach (var step in this._steps)
            {
                if (string.Equals(step.Id, id, StringComparison.Ordinal))
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }
        
        public void Start()
        {
            var activeStep = this.LoadState();
            this._navigator.Start(activeStep);
        }

        public T LoadState()
        {
            return _navigator.LoadWizardState();
        }

        protected abstract IEnumerable<NavigationCommand<T>> CreateNavigationCommands();
    }
}
