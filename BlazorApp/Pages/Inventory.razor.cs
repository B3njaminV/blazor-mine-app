using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;
using BlazorApp.Models;
using BlazorApp.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Action = System.Action;

namespace BlazorApp.Pages;

public partial class Inventory
{
    [Inject] public IStringLocalizer<List> Localizer { get; set; }
    
    [Inject]
    public IDataService DataService { get; set; }
    
    [Inject]
    internal IJSRuntime JavaScriptRuntime { get; set; }
    
    public List<Item> AllItems { get; set; }

    public List<Item> items;
    
    private DataGrid<Item> dataGrid;
    
    public ObservableCollection<Action> ObservableListOfActions { get; set; }
    
    public Item CurrentDragItem { get; set; }
    
    private string texteRecherche { get; set; }

    private int totalItem;
    private int totalSizeByPage = 5;
    
    public Inventory()
    {
        ObservableListOfActions = new ObservableCollection<Action>();
        ObservableListOfActions.CollectionChanged += OnActionsCollectionChanged;
        
    }
    
    private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
    {
        if (e.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (!e.CancellationToken.IsCancellationRequested)
        {
            items = await DataService.List();
            
            if (!string.IsNullOrEmpty(texteRecherche))
            {
                items = items.Where(i => i.DisplayName.Contains(texteRecherche)).ToList();
            }
            
            totalItem = items.Count;
            int page = dataGrid.CurrentPage;
            items = items.Skip((page - 1) * totalSizeByPage).Take(totalSizeByPage).ToList();
            StateHasChanged();
        }
    }

    private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // je fais appel au fichier js de crafting, car je n'ai pas su résoudre le problème avec celui Inventaire.razor.js
        JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
    }
}