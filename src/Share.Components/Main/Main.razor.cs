using Microsoft.AspNetCore.Components;

namespace Share.Components
{
    public partial class Main : Component
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
