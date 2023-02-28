using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components;

public partial class InventoryListItem
{
    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public Item Item { get; set; }

    [Parameter]
    public bool NoDrop { get; set; }

    [CascadingParameter]
    public Inventory Parent { get; set; }

    internal void OnDragEnter()
    {
        if (NoDrop) return;
    }

    internal void OnDragLeave()
    {
        if (NoDrop) return;
    }

    internal void OnDrop()
    {
        if (NoDrop) return;
    }

    private void OnDragStart()
    {
        Parent.CurrentDragItem = this.Item;

        Parent.Actions.Add(new ItemAction { Action = "Drag Start", Item = this.Item, Index = this.Index });
    }
}