using Discord;
using Discord.WebSocket;
using TanoshiBot.Commands;

namespace TanoshiBot;

public class Program
{
    private static DiscordSocketClient _client;
    private static CommandHandler _commandHandler;
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;

        _commandHandler = new CommandHandler(_client);
        await _commandHandler.InstallCommandsAsync();
        
        var token = File.ReadAllText("../../../token.txt");
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        
        await Task.Delay(-1);
    }
}