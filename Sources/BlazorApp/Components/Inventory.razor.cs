using System.Collections.ObjectModel;
using System.Collections.Specialized;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorApp.Models;
using Minecraft.Crafting.Api.Models;
using Item = BlazorApp.Models.Item;

namespace BlazorApp.Components;

public partial class Inventory
{
    public ObservableCollection<ItemAction> Actions { get; set; }
    
    [Parameter]
    public List<Item> Items { get; set; }
    
    public Item CurrentItem { get; set; }

    public ObservableCollection<Item> InventoryItems { get; set; }

    [Inject]
    internal IJSRuntime JavaScriptRuntime { get; set; }
    
    public List<InventoryModel> InventoryContent { get; set; } = Enumerable.Range(1, 18).Select(_ => new InventoryModel()).ToList();
    
    public Inventory()
    {
        Actions = new ObservableCollection<ItemAction>();
        Actions.CollectionChanged += OnActionsCollectionChanged;
        this.InventoryItems = new ObservableCollection<Item> { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
        InventoryItems.CollectionChanged += OnItemCollectionChanged;
    }
    
    public void CheckRecipe()
    {
        var currentModel = string.Join("|", this.InventoryItems.Select(s => s != null ? s.Name : string.Empty));
        this.Actions.Add(new ItemAction { Action = $"Items : {currentModel}" });
    }
        
    private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
    }

    private void OnItemCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        this.StateHasChanged();
    }
}