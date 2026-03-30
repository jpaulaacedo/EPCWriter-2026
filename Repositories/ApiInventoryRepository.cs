// API-based Inventory Repository Implementation

using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiInventoryRepository
{
    private readonly HttpClient _httpClient;
    
    public ApiInventoryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<InventoryItem> GetInventoryItemAsync(int itemId)
    {
        var response = await _httpClient.GetAsync($"/api/inventory/{itemId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<InventoryItem>();
    }
    
    public async Task AddInventoryItemAsync(InventoryItem item)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/inventory", item);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task UpdateInventoryItemAsync(InventoryItem item)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/inventory/{item.Id}", item);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task DeleteInventoryItemAsync(int itemId)
    {
        var response = await _httpClient.DeleteAsync($"/api/inventory/{itemId}");
        response.EnsureSuccessStatusCode();
    }
}