using System;
[assembly: CLSCompliant(true)]
namespace Wizard.Model
{
    public class Bookmark<T> where T : WizardStep
    {
        private readonly T step;

        private readonly IWizardNavigator<T> navigator;

        public Bookmark(IWizardNavigator<T> navigator, T step)
        {
            if (step == null)
            {
                throw new ArgumentNullException("step");
            }

            this.step = step;
            this.navigator = navigator;
        }

        public BookmarkState State
        {
            get
            {
                if (this.step == this.navigator.ActiveStep)
                {
                    return BookmarkState.Current;
                }
                else
                {
                    if (this.step.IsCompleted)
                    {
                        return BookmarkState.Enabled;
                    }
                    else
                    {
                        var stepIndex = this.navigator.WizardSteps.IndexOf(this.step);
                        if (this.navigator.WizardSteps[stepIndex - 1].IsCompleted)
                        {
                            return BookmarkState.Enabled;
                        }
                        else
                        {
                            return BookmarkState.Disabled;
                        }
                    }
                }
            }
        }

        public string Id
        {
            get
            {
                return this.step.Id;
            }
        }

        public string Text
        {
            get
            {
                return this.step.Title;
            }
        }

        public bool GoTo()
        {
            var result = this.navigator.MoveToStep(this.step);

            if (result)
            {
                this.navigator.SaveWizardState();
            }

            return result;
        }
    }
}
