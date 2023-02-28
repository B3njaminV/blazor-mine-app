using System.Collections.ObjectModel;
using System.Collections.Specialized;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorApp.Models;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Api.Models;
using Item = BlazorApp.Models.Item;

namespace BlazorApp.Components;

public partial class Inventory
{
    public ObservableCollection<ItemAction> Actions { get; set; }
    
    [Parameter]
    public List<Item> Items { get; set; }
    
    [Inject]
    public IStringLocalizer<Inventory> Localizer { get; set; }
    
    private Item recipeResult;
    
    public Item CurrentDragItem { get; set; }

    public int CurrentStackSize { get; set; }
    
    public ObservableCollection<Item> InventoryItems { get; set; }

    [Inject]
    internal IJSRuntime JavaScriptRuntime { get; set; }
    
    [Parameter]
    public List<CraftingRecipe> Recipes { get; set; }

    public Item RecipeResult
    {
        get => recipeResult;
        set
        {
            if (recipeResult == value)
            {
                return;
            }
            recipeResult = value;
        }
    }
    
    public Inventory()
    {
        Actions = new ObservableCollection<ItemAction>();
        Actions.CollectionChanged += OnActionsCollectionChanged;
        this.InventoryItems = new ObservableCollection<Item> { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
    }
    
    public void CheckInventory()
    {
        RecipeResult = null;
        var currentModel = string.Join("|", this.InventoryItems.Select(s => s != null ? s.Name : string.Empty));
        this.Actions.Add(new ItemAction { Action = $"Items : {currentModel}" });
    }
        
    private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
    }
}