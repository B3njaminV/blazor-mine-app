using System.Text.RegularExpressions;
using BlazorApp.Models;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components;

public partial class ListItem
{
    [CascadingParameter] 
    public Inventory Parent { get; set; }

    [Inject] 
    public IDataService DataService { get; set; }

    [Inject] 
    public IWebHostEnvironment WebHostEnvironment { get; set; }

    public string TextValue { get; set; }

    private int numPage = 1, nbPage = 1;
    
    private List<Item> items;

    public List<Item> ItemsSave { get; set; }
    public List<Item> ItemsShow { get; set; }

    [Parameter]
    public List<Item> Items
    {
        get { return items; }
        set
        {
            items = value;
            items.Sort(delegate(Item a, Item b)
            {
                return a.DisplayName.Replace(" ", "").CompareTo(b.DisplayName.Replace(" ", ""));
            });
            ItemsSave = items;
            ItemsShow = GetPage(value, 1);
            nbPage = (int)Math.Ceiling((double)value.Count / 10);
        }
    }

    List<Item> GetPage(List<Item> list, int page)
    {
        return list.Skip((page - 1) * 10).Take(10).ToList();
    }


    private void Pagination(bool nextPage, int nb = 1)
    {
        nbPage = (int)Math.Ceiling((double)ItemsSave.Count / 10);
        if (nextPage)
        {
            numPage = (numPage >= (nbPage + 1 - nb)) ? nbPage : numPage + nb;
        }
        else
        {
            numPage = (numPage <= nb) ? numPage : numPage - nb;
        }

        ItemsShow = GetPage(ItemsSave, numPage);
    }

    private string CharModif(string mot)
    {
        string newMot = "";
        if (mot != null)
        {
            foreach (char lettre in Regex.Replace(mot, @"\s", "").ToLower())
            {
                newMot += lettre switch
                {
                    'à' or 'á' or 'â' or 'ä' => 'a',
                    'é' or 'è' or 'ê' or 'ë' => 'e',
                    'ô' or 'ö' or 'ò' or 'ó' => 'o',
                    'ï' or 'î' or 'ì' or 'í' => 'i',
                    'ù' or 'ú' or 'û' or 'ü' => 'u',
                    _ => lettre,
                };
            }
        }

        return newMot;
    }

    private void Search()
    {
        string mot = TextValue;
        ItemsSave = Items.Where(item => CharModif(item.Name).Contains(mot) || CharModif(item.DisplayName).Contains(mot)).ToList();
        numPage = 0;
        Pagination(true);
    }
}