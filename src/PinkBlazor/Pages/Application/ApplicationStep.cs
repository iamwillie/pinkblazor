using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinkBlazor
{
    public class ApplicationStep
    {
        public int StepId { get; set; }
        public int StepNumber { get; set; }
        public string StepTitle { get; set; }
        public ApplicationState State { get; set; }
        public string StepUrl { get; set; }
        public bool IsFinalStep { get; set; }
        public bool IsActiveStep { get; set; }
    }
}
