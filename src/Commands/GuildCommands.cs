using Discord;
using Discord.WebSocket;

namespace TanoshiBot.Commands;

public class GuildCommands
{
    // This class is used purely for testing, ignore it
    public ulong GuildID = ulong.Parse(File.ReadAllText("../../../guildID.txt"));
    
    public async Task GuildIdObtainCommand(SocketSlashCommand cmd)
    {
        await cmd.RespondAsync($"{cmd.GuildId} is this guild's ID.");
    }

    public static SlashCommandBuilder[] BuildGuildCommands()
    {
        return new[]
        {
            new SlashCommandBuilder().WithName("guildid").WithDescription("Obtain this guild's ID.")
        };
    }
}