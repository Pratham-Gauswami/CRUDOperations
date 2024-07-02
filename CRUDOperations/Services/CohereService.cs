using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using CRUDOperations.Configuration;

namespace CRUDOperations.Services
{
    public class CohereService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CohereService(HttpClient httpClient, IOptions<CohereAIConfig> cohereAIConfig)
        {
            _httpClient = httpClient;

            try
            {   
                 // Check if BaseAddress has already been set
                if (_httpClient.BaseAddress == null)
                {
                // Example usage of IOptions<>
                string baseUrl = "https://api.cohere.com/v1/chat"; // Replace with your actual base URL
                
                // Logging the base URL
                Console.WriteLine($"Base URL: {baseUrl}");

                // Check if baseUrl is a valid URI
                if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri? validUri))
                {
                    throw new InvalidOperationException("Invalid base URL");
                }

                // Setting the BaseAddress
                _httpClient.BaseAddress = validUri;
                
                // Ensure the API key is configured properly
                _apiKey = cohereAIConfig.Value.Cohere_Key;

                // Setting Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                // Logging headers (optional for debugging)
                foreach (var header in _httpClient.DefaultRequestHeaders)
                {
                    Console.WriteLine($"Header: {header.Key}, Value: {string.Join(",", header.Value)}");
                }
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in constructor: {ex.Message}");
                throw;
            }
        }

        public async Task<JObject> SendChatRequestAsync(string message)
        {
            var requestBody = new
            {
                chat_history = new[]
                {
                    new { role = "USER", message = "Who discovered gravity?" },
                    new { role = "CHATBOT", message = "The man who is widely credited with discovering gravity is Sir Isaac Newton" }
                },
                message = message,
                connectors = new[]
                {
                    new { id = "web-search" }
                }
            };

            string jsonContent = JsonConvert.SerializeObject(requestBody);
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JObject.Parse(jsonResponse);
            }
            else
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error: {response.StatusCode}\nError Details: {errorResponse}");
            }
        }
    }
}


// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using Microsoft.Extensions.Options;
// using System.Net.Http.Headers;
// using CRUDOperations.Configuration;

// namespace CRUDOperations.Services;

// public class CohereService
// {
//     private readonly HttpClient _httpClient;
//     private readonly string _apiKey;
//     private readonly string _url;

//     public CohereService(HttpClient httpClient, IOptions<CohereAIConfig> cohereAIConfig)
//     {
//         _httpClient = httpClient;

//         {
//         // Example usage of IOptions<>
//         string baseUrl = "https://api.cohere.com/v1/chat"; // Replace with your actual base URL
//         _httpClient.BaseAddress = new Uri(baseUrl);

//         // Ensure the API key is configured properly
//         string apiKey = cohereAIConfig.Value.Cohere_Key;
        
//         // Setting Authorization header
//         _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

//         // Logging headers (optional for debugging)
//         foreach (var header in _httpClient.DefaultRequestHeaders)
//         {
//             Console.WriteLine($"Header: {header.Key}, Value: {string.Join(",", header.Value)}");
//         }
//     }

//         // _apiKey = "w9BCnpVENCLMBfaUkuSH1hEGrWNKexfD4N9aq3X3"; // Replace with your actual API key
//         // _url = "https://api.cohere.com/v1/chat"; // Assuming this is the correct endpoint
//         // _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
//         // _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

//         // foreach (var header in _httpClient.DefaultRequestHeaders)
//         // {
//         //     Console.WriteLine($"Header: {header.Key}, Value: {string.Join(",", header.Value)}");
//         // }
//     }

//     public async Task<JObject> SendChatRequestAsync(string message)
//     {
//         var requestBody = new
//         {
//             chat_history = new[]
//             {
//                 new { role = "USER", message = "Who discovered gravity?" },
//                 new { role = "CHATBOT", message = "The man who is widely credited with discovering gravity is Sir Isaac Newton" }
//             },
//             message = message,
//             connectors = new[]
//             {
//                 new { id = "web-search" }
//             }
//         };

//         string jsonContent = JsonConvert.SerializeObject(requestBody);
//         StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//         HttpResponseMessage response = await _httpClient.PostAsync(_url, httpContent);

//         if (response.IsSuccessStatusCode)
//         {
//             string jsonResponse = await response.Content.ReadAsStringAsync();
//             return JObject.Parse(jsonResponse);
//         }
//         else
//         {
//             string errorResponse = await response.Content.ReadAsStringAsync();
//             throw new HttpRequestException($"Error: {response.StatusCode}\nError Details: {errorResponse}");
//         }
//     }
// }
