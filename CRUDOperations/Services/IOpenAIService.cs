namespace CRUDOperations.Services;

public interface IOpenAIService
{
    Task<string> CompleteSentence(string text);
}