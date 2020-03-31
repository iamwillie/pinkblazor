using Microsoft.AspNetCore.Components;

namespace Share.Components
{
    public partial class NavMenuItem : Component
    {
        [Parameter] public string Href { get; set; }
        [Parameter] public string MenuTitle { get; set; }
        [Parameter] public bool IsDropDownItem { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
