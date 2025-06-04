using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;

namespace TanoshiBot.Commands;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly BaseCommands _baseCommands;
    private readonly GuildCommands _guildCommands;
    
    public CommandHandler(DiscordSocketClient client)
    {
        _client = client;
        _baseCommands = new BaseCommands();
        _guildCommands = new GuildCommands();
    }
    
    /*
    public async Task InstallCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;

        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
    }
    
    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        int argPos = 0;

        if (!(message.HasCharPrefix('%', ref argPos) ||
              message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;
        
        var context = new SocketCommandContext(_client, message);
        
        await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);
    }*/

    public Task InstallCommandsAsync()
    {
        _client.Ready += Client_Ready;
        _client.SlashCommandExecuted += HandleSlashCommand;
        return Task.CompletedTask;
    }

    private async Task DeleteCommandsAsync()
    {
        var globalCommands = await _client.GetGlobalApplicationCommandsAsync();
        foreach (var c in globalCommands)
        {
            await c.DeleteAsync();
        }
        
        var guildCommands = await _client.GetGuild(_guildCommands.GuildID).GetApplicationCommandsAsync();
        foreach (var c in guildCommands)
        {
            await c.DeleteAsync();       
        }
    }

    private async Task Client_Ready()
    {
        try
        {
            // Delete all commands to prevent duplicates, uncomment when dupes
            // await DeleteCommandsAsync();
            
            foreach (SlashCommandBuilder cmd in BaseCommands.BuildBaseCommands())
            {
                await _client.CreateGlobalApplicationCommandAsync(cmd.Build());
            }
            
            // Aka test commands
            foreach (SlashCommandBuilder cmd in GuildCommands.BuildGuildCommands())
            {
                await _client.GetGuild(_guildCommands.GuildID).CreateApplicationCommandAsync(cmd.Build());
            }
        }
        catch (ApplicationCommandException e)
        {
            Console.WriteLine("Error creating commands.");
        }
    }
    
    private async Task HandleSlashCommand(SocketSlashCommand cmd)
    {
        try
        {
            switch (cmd.Data.Name)
            {
                case "hi":
                    await _baseCommands.HiCommand(cmd);
                    break;
                case "guildid":
                    await _guildCommands.GuildIdObtainCommand(cmd);
                    break;
                default: 
                    await cmd.RespondAsync("Unknown command.");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}