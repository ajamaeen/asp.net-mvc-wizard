namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWizardNavigator<T> where T : WizardStep
    {
        event EventHandler ActiveStepChanged;

        WizardStepCollection WizardSteps
        {
            get;
        }

        T ActiveStep
        {
            get;
        }

        int NextStepIndex
        {
            get;
        }

        void Start(T activeStep);

        bool AllowMoveToNextStep
        {
            get;
        }
     
        bool AllowMoveToPreviousStep
        {
            get;
        }
        
        bool AllowMoveToFirstStep
        {
            get;
        }

        bool AllowMoveToLastStep
        {
            get;
        }
        
        bool ActiveStepIsLastStep
        {
            get;
        }

        int ActiveStepIndex
        {
            get;
        }

        bool Started
        {
            get;
        }

        void Stop();

        bool MoveToStep(T wizardStep);

        bool MoveToLastStep();

        bool MoveToFirstStep();

        bool MoveToNextStep();

        bool MoveToPreviousStep();

        void SaveWizardState();

        T LoadWizardState();
    }
}
