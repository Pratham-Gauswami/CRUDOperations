using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CRUDOperations.Services;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Text;



namespace CohereAIController.Controllers
{
    public class CohereAIController : Controller
    {
        private readonly string _cohereApiKey = "w9BCnpVENCLMBfaUkuSH1hEGrWNKexfD4N9aq3X3"; // Replace with your actual API key
        private readonly string _cohereUrl = "https://api.cohere.com/v1/chat"; // Assuming this is the correct endpoint

        // GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Home/GetCohereResponse
        [HttpPost]
        public async Task<IActionResult> GetCohereResponse()
        {
            string connectionString = "server=localhost;database=Trial1;User Id=sa;Password=Pratham72;TrustServerCertificate=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var tables = await connection.QueryAsync<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'");
                ViewBag.UsableTables = tables;

                var users = await connection.QueryAsync("SELECT * FROM Users");
                ViewBag.UsersTable = users;

                var question = "How many users are there with the name Naman?";
                var query = "SELECT COUNT(*) AS UserCount FROM Users WHERE username = 'Naman'";
                var result = await connection.QuerySingleAsync<int>(query);

                var answerPrompt = $@"
Given the following user question, corresponding SQL query, and SQL result, answer the user question.

Question: {question}
SQL Query: {query}
SQL Result: {result}
Answer: ";

                var response = await GetCohereResponseFromApi(answerPrompt);
                ViewBag.CohereResponse = response;
            }

            return View("GetCohereResponse");
        }

        private async Task<string> GetCohereResponseFromApi(string prompt)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_cohereApiKey}");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var requestBody = new
                {
                    chat_history = new object[] { },
                    message = prompt,
                    connectors = new[]
                    {
                        new { id = "web-search" }
                    }
                };

                string jsonContent = JsonConvert.SerializeObject(requestBody);
                StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(_cohereUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject parsedResponse = JObject.Parse(jsonResponse);
                    return parsedResponse.ToString();
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}\nError Details: {errorResponse}";
                }
            }
        }
    }
}


// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json.Linq;
// using System.Threading.Tasks;
// using CRUDOperations.Services;

// public class CohereAIController : Controller
// {
//     private readonly CohereService _cohereService;

//     public CohereAIController(CohereService cohereService)
//     {
//         _cohereService = cohereService;
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     [HttpPost]
//     public async Task<IActionResult> GetChatResponse(string message)
//     {
//         try
//         {
//             JObject response = await _cohereService.SendChatRequestAsync(message);
//             ViewBag.Response = response.ToString();
//         }
//         catch (HttpRequestException ex)
//         {
//             ViewBag.Error = ex.Message;
//         }

//         return View("Index");
//     }
// }
