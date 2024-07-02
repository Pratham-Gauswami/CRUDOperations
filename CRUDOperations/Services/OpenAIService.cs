using CRUDOperations.Configuration;
using Microsoft.Extensions.Options;

namespace CRUDOperations.Services;

public class OpenAiService : IOpenAIService
{

    private readonly OpenAIConfig _openAIConfig;

    public OpenAiService(IOptionsMonitor<OpenAIConfig> optionsMonitor)
    {
        _openAIConfig = optionsMonitor.CurrentValue;
    }
    public async Task<string> CompleteSentence(string text)
    {
        //Api instance
        var api = new OpenAI_API.OpenAIAPI(_openAIConfig.ApiKey);
        var result = await api.Completions.GetCompletion(text);
        return result;
    }
}