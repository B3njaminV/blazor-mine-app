using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Models;

namespace BlazorApp.Components;

public partial class InventoryItem
{
    [Parameter] public int Index { get; set; }

    [Parameter] public Item Item { get; set; }

    [Parameter] public bool NoDrop { get; set; }

    [CascadingParameter] public Inventory Parent { get; set; }

    private bool isOutSide = false;

    internal void OnDragEnter()
    {
        if (NoDrop)
        {
            return;
        }

        isOutSide = false;
        Parent.Actions.Add(new ItemAction { Action = "Drag Enter", Item = this.Item, Index = this.Index });
    }

    internal void OnDragLeave()
    {
        if (NoDrop)
        {
            return;
        }

        isOutSide = true;
        Parent.Actions.Add(new ItemAction { Action = "Drag Leave", Item = this.Item, Index = this.Index });
    }

    internal void OnDragEnd()
    {
        Parent.Actions.Add(new ItemAction { Action = "Drag End", Item = this.Item, Index = this.Index });
        if (isOutSide)
        {
            Parent.Actions.Add(new ItemAction { Action = "Delete Item", Item = this.Item, Index = this.Index });
            Parent.InventoryItems.Remove(this.Item);
            this.Item = null;
        }
    }

    internal void OnDrop()
    {
        if (NoDrop)
        {
            return;
        }

        this.Item = Parent.CurrentItem;
        Parent.InventoryItems[this.Index] = this.Item;

        Parent.Actions.Add(new ItemAction { Action = "Drop", Item = this.Item, Index = this.Index });
    }

    private void OnDragStart()
    {
        Parent.CurrentItem = this.Item;

        Parent.Actions.Add(new ItemAction { Action = "Drag Start", Item = this.Item, Index = this.Index });
    }
}