using BlazorApp.Components;
using BlazorApp.Models;
namespace BlazorApp.Services;

public interface IDataService
{
    Task Add(ItemModel model);

    Task<int> Count();

    Task<List<Item>> List(int currentPage, int pageSize);
    
    Task<List<Item>> List();
    
    Task<Item> GetById(int id);
    
    Task Update(int id, ItemModel model);
    
    Task Delete(int id);
    
    Task<List<CraftingRecipe>> GetRecipes();
}