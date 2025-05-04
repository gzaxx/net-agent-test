using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;

var client = new AnthropicClient();

var messages = new List<Message>
{
    new Message(RoleType.User, "Hello, Claude! Tell me about yourself.")
};

// Create message parameters
var parameters = new MessageParameters
{
    Messages = messages,
    MaxTokens = 1000,
    Temperature = 0.7m,
    Model = AnthropicModels.Claude35Sonnet
};

// Get a response from Claude via Vertex AI
var response = await client.Messages
    .GetClaudeMessageAsync(parameters);

// Print the response
Console.WriteLine($"Model: {response.Model}");
Console.WriteLine($"Response: {response.Content[0]}");

Console.ReadLine();