using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PinkBlazor
{
    public class AppState
    {
        public Application Application { get; set; }

        public AppState()
        {
            // NewApplication();
        }

        private ApplicationStep _nextStep;
        public ApplicationStep NextStep
        {
            get
            {
                return _nextStep;
            }
            set
            {
                Application.Steps[value.StepId].IsActiveStep = false;
                _nextStep = value;
            }
        }

        private ApplicationStep _previousStep;
        public ApplicationStep PreviousStep
        {
            get
            {
                return _previousStep;
            }
            set
            {
                Application.Steps[value.StepId].IsActiveStep = false;
                _previousStep = value;
            }
        }

        private ApplicationStep _current;
        public ApplicationStep CurrentStep
        {
            get
            {
                return _current;
            }
            set
            {
                Application.Steps[value.StepId].IsActiveStep = true;
                _current = value;
            }
        }

        public string Message { get; set; }

        public ApplicationStep Next()
        {
            PreviousStep = CurrentStep;
            if (CurrentStep.State == ApplicationState.Failed)
            {
                CurrentStep = Application.Steps.Last();
            }
            else
            {
                CurrentStep = NextStep;

                if (!CurrentStep.IsFinalStep)
                {
                    NextStep = Application.Steps[CurrentStep.StepId + 1];
                }
                else
                {
                    NextStep = Application.Steps.Last();
                }
            }

            Message = string.Empty;
            CurrentStep.State = ApplicationState.Current;
            return CurrentStep;
        }

        public void UpdateStepProgress(ApplicationStep current)
        {
            CurrentStep = current;
            if (CurrentStep.StepId != 0)
            {
                PreviousStep = Application.Steps[CurrentStep.StepId - 1];
            }

            if (!CurrentStep.IsFinalStep)
            {
                NextStep = Application.Steps[CurrentStep.StepId + 1];
            }
            else
            {
                NextStep = Application.Steps.Last();
            }
        }

        public void NewApplication()
        {
            Application = new Application()
            {
                ApplicationId = Guid.NewGuid(),
                Type = ApplicationType.Personal,
                Steps = new List<ApplicationStep>()
                {
                    new ApplicationStep()
                    {
                        StepId = 0,
                        StepNumber = 1,
                        StepTitle = "Personal Information",
                        StepUrl = "/application/personal",
                        State = ApplicationState.Current,
                        IsActiveStep = true
                    },
                    new ApplicationStep()
                    {
                        StepId = 1,
                        StepNumber = 2,
                        StepTitle = "Questionaire",
                        StepUrl = "/application/questionaire",
                        State = ApplicationState.None
                    },
                    new ApplicationStep()
                    {
                        StepId = 2,
                        StepNumber = 3,
                        StepTitle = "Credit Check",
                        StepUrl = "/application/creditcheck",
                        State = ApplicationState.None
                    },
                    new ApplicationStep()
                    {
                        StepId = 3,
                        StepNumber = 4,
                        StepTitle = "Affordability",
                        StepUrl = "/application/affordability",
                        State = ApplicationState.None
                    },
                    new ApplicationStep()
                    {
                        StepId = 4,
                        StepNumber = 5,
                        StepTitle = "Finalize",
                        StepUrl = "/application/finalize",
                        State = ApplicationState.None,
                        IsFinalStep = true
                    }
                }
            };

            CurrentStep = Application.Steps[0];
            NextStep = Application.Steps[1];
        }
    }
}
