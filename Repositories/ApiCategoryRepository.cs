using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiCategoryRepository
{
    private readonly HttpClient _httpClient;

    public ApiCategoryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Category[]> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("/api/categories");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Category[]>();
    }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}