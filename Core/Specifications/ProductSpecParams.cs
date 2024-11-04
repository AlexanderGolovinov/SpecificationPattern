namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 10;

    private List<string> _brands = [];

    private int _pageSize = 6;
    private List<string> _types = [];

    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? Sort { get; set; }

    private string _search;

    public string? Search
    {
        get => _search ?? "";
        set => _search = value;
    }
    
    

    public List<string> Brands
    {
        get => _brands; //brands=angular,react
        set { _brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }

    public List<string> Types
    {
        get => _types; //types=rectangular,reactive
        set { _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }
}