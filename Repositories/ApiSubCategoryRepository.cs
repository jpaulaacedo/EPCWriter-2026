using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EPCWriter.Repositories
{
    public class ApiSubCategoryRepository
    {
        private readonly HttpClient _httpClient;

        public ApiSubCategoryRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubCategory>> GetSubCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("api/subcategories");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SubCategory>>(content);
            }

            return new List<SubCategory>();
        }

        public async Task<string> GeneratePropertyNumberAsync(SubCategory subCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/propertynumbers", subCategory);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}