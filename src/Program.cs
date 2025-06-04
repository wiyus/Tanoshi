using Discord;
using Discord.WebSocket;

namespace TanoshiBot;

public class Program
{
    private static DiscordSocketClient _client;
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        
        var token = File.ReadAllText("../../../token.txt");
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        
        await Task.Delay(-1);
    }
}