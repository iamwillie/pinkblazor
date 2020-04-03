using Microsoft.AspNetCore.Components;

namespace PinkBlazor
{
    public partial class NavMenuDropdownItem : Component
    {
        [Parameter] public string MenuTitle { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
