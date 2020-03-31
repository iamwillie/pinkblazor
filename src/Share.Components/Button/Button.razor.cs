using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Components
{
    public partial class Button : Component
    {
        #region Parameters
        [Parameter] public string Id { get; set; }
        [Parameter] public string Color { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Hide { get; set; }
        [Parameter] public Size ButtonSize { get; set; }
        [Parameter] public string SizeClass { get; set; }
        [Parameter] public ButtonType ButtonType { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string Text { get; set; }

        #endregion

        protected override void OnInitialized()
        {
            switch (ButtonSize)
            {
                case Size.Small:
                    SizeClass = "btn-sm";
                    break;
                case Size.Large:
                    SizeClass = "btn-lg";
                    break;
                default:
                    SizeClass = "";
                    break;
            }

            if (string.IsNullOrEmpty(Color))
                Color = "primary";
        }
    }

    public enum ButtonType
    {
        Button,
        Reset,
        Submit
    }

    public enum Size
    {
        Default,
        Small,
        Large
    }
}
