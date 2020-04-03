using Microsoft.AspNetCore.Components;

namespace PinkBlazor
{
    public partial class Main : Component
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
