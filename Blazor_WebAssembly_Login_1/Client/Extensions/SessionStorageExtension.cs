using Blazored.SessionStorage;
using System.Text.Json;
using System.Text;
using Blazor_WebAssembly_Login_1.Shared;

namespace Blazor_WebAssembly_Login_1.Client.Extensions
{
    public static class SessionStorageExtension
    {
        // Save Storage Method
        public static async Task SaveStorage<T>(this ISessionStorageService sessionStorageService, string key, T item) where T : class
        {
            // Convert to Json
            var itemJson = JsonSerializer.Serialize(item);

            // Store the Json 
            await sessionStorageService.SetItemAsStringAsync(key, itemJson);
        }

        // Get Storage Method
        public static async Task<T?> GetStorage<T>(this ISessionStorageService sessionStorageService, string key) where T : class
        {
            // Get past storage
            var itemJson = await sessionStorageService.GetItemAsStringAsync(key);

            if (itemJson != null)
            {
                // Deserialize the Json
                var item = JsonSerializer.Deserialize<T>(itemJson);
                return item;
            }

            return null;
        }
    }
}
