namespace BlazorApp.Components;

using BlazorApp.Models;

public class CraftingRecipe
{
    public Item Give { get; set; }
    public List<List<string>> Have { get; set; }
}