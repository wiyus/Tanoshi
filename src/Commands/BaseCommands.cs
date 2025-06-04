using Discord;
using Discord.WebSocket;

namespace TanoshiBot.Commands;

public class BaseCommands
{
    public async Task HiCommand(SocketSlashCommand cmd)
    {
        await cmd.RespondAsync("Hey.");
    }

    public static SlashCommandBuilder[] BuildBaseCommands()
    {
        return new[]
        {
            new SlashCommandBuilder().WithName("hi").WithDescription("Say hi")
        };
    }
}