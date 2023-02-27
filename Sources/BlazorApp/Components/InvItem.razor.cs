using System.Text.Json;
using BlazorApp.Models;
using BlazorApp.Pages;
using Microsoft.AspNetCore.Components;
using Action = BlazorApp.Models.Action;

namespace BlazorApp.Components;

public partial class InvItem
{
    [Parameter] 
    public int index { get; set; }

    [Parameter] 
    public Item item { get; set; }
    
    [CascadingParameter] 
    public Inventory inventaire { get; set; }
    
    internal void OnDragEnter()
    {
        var action = new Action();
        action.action = "Drag Enter";
        action.item = this.item;
        action.index = this.index;
        //inventaire.ObservableListOfActions.Add(action);
    }
}