using CRUDOperations.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperations.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenAIController : Controller
{
    private readonly ILogger<OpenAIController> _logger;

    private readonly IOpenAIService _openAIService;

    public OpenAIController(
        ILogger<OpenAIController> logger,
        IOpenAIService openAIService){
        _logger = logger;
        _openAIService = openAIService;
    }

    // [HttpGet]
    //     [Route("Index")]
    //     public IActionResult Index()
    //     {
    //         return View();
    //     }

    [HttpPost]
    [Route("CompleteSentence") ]

    public async Task<IActionResult> CompleteSentence(string text)
    {
        var result = await _openAIService.CompleteSentence(text);
        return Ok(result);
    }

    // public class InputModel
    // {
    //     public string Text { get; set; }
    // }
}