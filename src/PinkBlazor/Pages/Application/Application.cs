using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinkBlazor
{
    public class Application
    {
        public Guid ApplicationId { get; set; } = default!;
        public string Purpose { get; set; } = default!;
        public ApplicationType Type { get; set; } = default!;
        public bool Approved { get; set; } = default!;
        public bool Failed { get; set; } = default!;
        public List<ApplicationStep> Steps { get; set; } = default!;
    }
}
