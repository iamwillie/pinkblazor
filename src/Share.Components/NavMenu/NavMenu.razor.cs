using Microsoft.AspNetCore.Components;

namespace PinkBlazor
{
    public partial class NavMenu : Component
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
