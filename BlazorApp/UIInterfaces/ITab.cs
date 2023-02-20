using Microsoft.AspNetCore.Components;

namespace BlazorApp.UIInterfaces
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}