namespace TheFabricsLab.Models
{
    public class SearchViewModel
    { 
        public string SearchTerm { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
