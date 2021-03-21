using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace DiscordEduBot
{
    public class DiscordEdu : ConsoleAppBase
    {
        private IOptions<Config> _config;
        private DiscordSocketClient _client;
        private CommandService _command;

        public DiscordEdu(IOptions<Config> config)
        {
            _config = config;
        }

        public async Task ExecuteAsync()
        {
            _client = new DiscordSocketClient();
            _command = new CommandService();
            _client.Log += message =>
            {
                Console.WriteLine($"{message.Message} : {message.Exception}");
                return Task.CompletedTask;
            };

            _client.MessageReceived += MessageHandle;
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            await _client.LoginAsync(TokenType.Bot, _config.Value.Token);
            await _client.StartAsync();

            await Task.Delay(-1, Context.CancellationToken);

            await _client.StopAsync();
        }

        private async Task MessageHandle(SocketMessage message)
        {
            if (!(message is SocketUserMessage msg) || msg.Author.IsBot) return;
            await msg.Channel.SendMessageAsync(
                $"ここは、サーバー{(msg.Channel is IGuildChannel ? "です" : "ではありません")}");
        }
    }
}
