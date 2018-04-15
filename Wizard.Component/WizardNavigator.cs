namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WizardNavigator<T> : IWizardNavigator<T> where T : WizardStep
    {
        private T activeStep;

        private bool started;

        private readonly WizardStepCollection wizardSteps;

        private readonly IWizardNavigatorService<T> persistenceService;

        public event EventHandler ActiveStepChanged;

        public WizardNavigator(WizardStepCollection wizardSteps, IWizardNavigatorService<T> persistenceService)
        {
            this.wizardSteps = wizardSteps ?? throw new ArgumentNullException("wizardSteps");
            this.persistenceService = persistenceService ?? throw new ArgumentNullException("persistenceService");
        }

        public WizardStepCollection WizardSteps
        {
            get
            {
                return this.wizardSteps;
            }
        }

        public T ActiveStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                return this.activeStep;
            }

            private set
            {
                if (this.activeStep != value)
                {
                    this.activeStep = value;
                    this.ActiveStepChanged?.Invoke(this, new WizardNavigationEventArgs(this.ActiveStepIndex, this.NextStepIndex));
                }
            }
        }

        public bool AllowMoveToNextStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                T nextStep;

                return this.activeStep != null &&
                    this.TryGetNextStep(this.activeStep, out nextStep);
            }
        }

        public bool AllowMoveToPreviousStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                T previousStep;

                return this.activeStep != null &&
                    this.TryGetPreviousStep(this.activeStep, out previousStep);
            }
        }

        public bool AllowMoveToFirstStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                T firstStep;

                return this.activeStep != null && this.TryGetFirstStep(this.ActiveStep, out firstStep);
            }
        }

        public bool AllowMoveToLastStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                T lastStep;

                return this.activeStep != null && this.TryGetLastStep(this.activeStep, out lastStep);
            }
        }

        public bool ActiveStepIsFirstStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                return this.activeStep == this.WizardSteps[0];
            }
        }

        public bool ActiveStepIsLastStep
        {
            get
            {
                this.VerifyNavigatorStarted();

                return this.activeStep == this.WizardSteps[this.WizardSteps.Count - 1];
            }
        }

        public int ActiveStepIndex
        {
            get
            {
                if (this.started)
                {
                    return this.WizardSteps.IndexOf(this.activeStep);
                }
                else
                {
                    throw new InvalidOperationException("Navigator not started yet.");
                }
            }
        }

        public int NextStepIndex
        {
            get
            {
                if (this.started)
                {
                    if (this.ActiveStepIsLastStep)
                    {
                        return -1;
                    }
                    else
                    {
                        return this.ActiveStepIndex + 1;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Navigator not started yet.");
                }
            }
        }

        public bool Started
        {
            get
            {
                return this.started;
            }
        }

        public void Start(T step)
        {
            if (this.started)
            {
                throw new InvalidOperationException("Navigator is already started.");
            }

            this.started = true;

            if (step != null)
            {
                this.started = this.MoveToStep(step);

                if (!this.started)
                {
                    throw new InvalidOperationException($"Navigator start failed. Unable to go to step {step.Id}");
                }
            }
            else if (!this.MoveToFirstNonCompletedStep())
            {
                this.started = false;

                throw new InvalidOperationException("Navigator start failed. Unable to go to first step.");
            }
        }

        public void Stop()
        {
            if (!this.started)
            {
                throw new InvalidOperationException("Navigator is already stopped.");
            }

            this.activeStep = null;
            this.started = false;
        }

        public bool MoveToStep(T step)
        {
            this.VerifyNavigatorStarted();

            if (this.AllowMoveToStep(this.activeStep, step))
            {
                this.ActiveStep = step;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveToLastStep()
        {
            this.VerifyNavigatorStarted();

            T lastStep;

            if (this.TryGetLastStep(this.activeStep, out lastStep))
            {
                this.ActiveStep = lastStep;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveToFirstStep()
        {
            this.VerifyNavigatorStarted();

            T firstStep;

            if (this.TryGetFirstStep(this.ActiveStep, out firstStep))
            {
                if (Object.ReferenceEquals(this.activeStep, firstStep))
                {
                    return false;
                }
                else
                {
                    this.ActiveStep = firstStep;

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool MoveToNextStep()
        {
            this.VerifyNavigatorStarted();

            var nextStep = this.activeStep;

            while (this.TryGetNextStep(nextStep, out nextStep))
            {
                this.ActiveStep = nextStep;

                return true;
            }

            return false;
        }

        public bool MoveToPreviousStep()
        {
            this.VerifyNavigatorStarted();

            var previousStep = this.activeStep;

            while (this.TryGetPreviousStep(previousStep, out previousStep))
            {
                this.ActiveStep = previousStep;

                return true;
            }

            return false;
        }

        private bool TryGetNextStep(T fromStep, out T nextStep)
        {
            // Do not move to the next step in case the current step is not complete.
            if (!fromStep.IsCompleted)
            {
                nextStep = null;

                return false;
            }

            int index = this.WizardSteps.IndexOf(fromStep);

            // check if this is the last page
            if (index == this.WizardSteps.Count - 1)
            {
                nextStep = null;

                return false;
            }
            else
            {
                nextStep = (T)this.WizardSteps[++index];

                return true;
            }
        }

        private void VerifyNavigatorStarted()
        {
            if (!this.started)
            {
                throw new InvalidOperationException("Navigator is not started.");
            }
        }

        private bool TryGetFirstStep(T fromStep, out T firstStep)
        {
            T firstStep1 = (T)this.WizardSteps[0];

            if (this.AllowMoveToStep(fromStep, firstStep1))
            {
                firstStep = firstStep1;

                return true;
            }
            else
            {
                firstStep = null;

                return false;
            }
        }

        private bool TryGetLastStep(T fromStep, out T lastStep)
        {
            T lastStep1 = (T)this.WizardSteps[this.WizardSteps.Count - 1];

            if (this.AllowMoveToStep(fromStep, lastStep1))
            {
                lastStep = lastStep1;

                return true;
            }
            else
            {
                lastStep = null;

                return false;
            }
        }

        private bool TryGetPreviousStep(T fromStep, out T previousStep)
        {
            WizardStepCollection steps = this.WizardSteps;

            int index = steps.IndexOf(fromStep);

            if (index == 0)
            {
                previousStep = null;

                return false;
            }
            else
            {
                previousStep = (T)steps[--index];

                return true;
            }
        }

        private bool AllowMoveToStep(T fromStep, T toStep)
        {
            WizardStepCollection steps = this.WizardSteps;

            int fromStepIndex;

            if (fromStep != null)
            {
                fromStepIndex = steps.IndexOf(fromStep);
            }
            else
            {
                fromStepIndex = 0;
            }

            int toStepIndex = steps.IndexOf(toStep);

            if (this.IsForwardNavigation(fromStep, toStep))
            {
                for (int i = fromStepIndex; i < toStepIndex; i++)
                {
                    if (!steps[i].IsCompleted)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        private bool IsForwardNavigation(T fromStep, T toStep)
        {
            int newStepIndex = this.WizardSteps.IndexOf(toStep);
            int fromStepIndex = this.WizardSteps.IndexOf(fromStep);

            return newStepIndex > fromStepIndex;
        }

        private bool MoveToFirstNonCompletedStep()
        {
            foreach (var step in this.WizardSteps)
            {
                if (!step.IsCompleted)
                {
                    this.ActiveStep = (T)step;

                    break;
                }
            }

            return this.ActiveStep != null;
        }

        public void SaveWizardState()
        {
            this.persistenceService.SaveWizardState(new WizardState<T>(WizardSteps, ActiveStep.Id));
        }

        public T LoadWizardState()
        {
            var state = persistenceService.LoadWizardState();

            if (state != null)
            {
                foreach (var stepState in state.WizardSteps)
                {
                    var step = this.WizardSteps.FirstOrDefault(st => st.Id == stepState.Id);
                    if (step != null)
                    {
                        if (stepState.IsCompleted)
                        {
                            step.IsCompleted = true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(state.ActiveStepId))
                {
                    var activeStep = this.WizardSteps.FirstOrDefault(st => st.Id == state.ActiveStepId);

                    if (activeStep != null)
                    {
                        return (T)activeStep;
                    }
                }
            }
            return null;
        }
    }
}
