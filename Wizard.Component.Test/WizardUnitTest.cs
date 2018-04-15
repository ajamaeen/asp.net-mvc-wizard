using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wizard.Model;

namespace Wizard.Component.Test
{
    [TestClass]
    public class WizardUnitTest
    {
        WizardStepCollection steps;
        BaseWizard<WizardStep> wizard;
        IWizardNavigator<WizardStep> navigator;

        [TestInitialize]
        public void Init()
        {
            //Create steps
            this.steps = new WizardStepCollection();            
            this.steps.Add(new WizardStep("1", "Login Info", false));
            this.steps.Add(new WizardStep("2", "User Info", false));
            this.steps.Add(new WizardStep("3", "Profile Info", false));
            this.steps.Add(new WizardStep("4", "Account Info", false));
            this.steps.Add(new WizardStep("5", "Summary", false));
            Assert.IsTrue(this.steps.Count == 5);

            //Create navigatro
            this.navigator = new WizardNavigator<WizardStep>(this.steps,null);
            Assert.IsTrue(this.navigator != null);
            Assert.IsTrue(!this.navigator.Started);

            //Create Wizard
            //this.wizard = new Wizard<Step>(this.steps, this.navigator);
            Assert.IsTrue(this.wizard != null);

            //Test wizard indexer (access step by it's id)
             Assert.IsTrue(this.wizard["1"].Id == this.steps[0].Id);

        }
        
        [TestMethod]
        public void TestNavigator()
        {
            //Will test going forward and backward in navigator

            //Start navigator (Auto will go to step 1)
            this.navigator.Start(null);
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[0]);
            Assert.IsTrue(this.navigator.Started);
            this.steps[0].IsCompleted=true ;//So we can move to next step

            //Go to next step (ID = 2)            
            this.navigator.MoveToNextStep();
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[1]);
            this.steps[1].IsCompleted = true;//So we can move to next step

            //Go to next step (ID = 3)
            this.navigator.MoveToNextStep();
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[2]);
            this.steps[2].IsCompleted = true;//So we can move to next step

            //Return Back to step 2 (ID = 2)
            this.navigator.MoveToPreviousStep();
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[1]);

            //Return to first step
            this.navigator.MoveToFirstStep();
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[0]);

            //Go to last step
            this.navigator.MoveToLastStep();
            //Navigator will go to first step incase can't go to last step.
            Assert.IsTrue(this.navigator.ActiveStep == this.steps[0]);
            this.steps[3].IsCompleted = true;//mark it as completed so we can go to  4th step
            
            // You can't go beyond last step
            this.navigator.MoveToLastStep();
            var canGoNext = this.navigator.AllowMoveToNextStep;
            Assert.IsFalse(canGoNext);

            //Stop navigation
            this.navigator.Stop();
            Assert.IsTrue(!this.navigator.Started);
        }
    }
}
