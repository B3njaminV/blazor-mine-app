using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BlazorApp.Pages;

public partial class InventoryCraft
{
    [Inject]
    public IDataService DataService { get; set; }
    
    [Inject]
    public IStringLocalizer<InventoryCraft> Localizer { get; set; }

    public List<Item> Items { get; set; } = new List<Item>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        StateHasChanged();

        if (!firstRender)return;

        Items = await DataService.List(0, await DataService.Count());

        StateHasChanged();
    }
}