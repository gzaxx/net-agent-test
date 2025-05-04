using Anthropic.SDK;
using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using System.ComponentModel.DataAnnotations;

var client = new AnthropicClient();

var messages = new List<Message>
{
    new Message(RoleType.User, "What's tax I have to pay in 2023 based on the rules in 2023?")
};

var tools = Anthropic.SDK.Common.Tool.GetAllAvailableTools(includeDefaults: false, forceUpdate: true, clearCache: true);

var parameters = new MessageParameters()
{
    Messages = messages,
    MaxTokens = 512,
    Model = AnthropicModels.Claude35Sonnet,
    Stream = true,
    Temperature = 1.0m,
    Tools = tools.ToList()
};

var outputs = new List<MessageResponse>();

await foreach (var res in client.Messages.StreamClaudeMessageAsync(parameters))
{
    if (res.Delta != null)
    {
        Console.Write(res.Delta.Text);
    }

    outputs.Add(res);
}

var handledToolCalls = new HashSet<string>();
messages.Add(new Message(outputs));

foreach (var output in outputs)
{
    if (output.ToolCalls is not null)
    {
        foreach (var toolCall in output.ToolCalls)
        {
            var response = await toolCall.InvokeAsync<string>();

            messages.Add(new Message(toolCall, response));
        }
    }
}

await foreach (var res in client.Messages.StreamClaudeMessageAsync(parameters))
{
    if (res.Delta != null)
    {
        Console.Write(res.Delta.Text);
    }

    outputs.Add(res);
}

foreach (var res in outputs)
{
    if (res.ToolCalls is null)
    {
        Console.WriteLine(res.ContentBlock);
    }
}