using Giftify.Models;

public class IndexViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public List<int> SelectedCategoryIds { get; set; }
    public string SortOrder { get; set; } // New property for sorting
    public string SearchQuery;
}