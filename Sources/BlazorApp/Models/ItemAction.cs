using Minecraft.Crafting.Api.Models;

namespace BlazorApp.Models;

public class ItemAction
{
    public string Action { get; set; }
    public int Index { get; set; }
    public Item Item { get; set; }
    
    public InventoryModel? InventoryModel { get; set; }
}
