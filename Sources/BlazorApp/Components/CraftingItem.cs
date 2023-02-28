namespace BlazorApp.Components;

public partial class CraftingItem
{
}

public static class CraftingItemExtensions
{
    public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CraftingItem>();
    }
}