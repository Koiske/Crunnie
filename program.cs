using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Net.Providers.WS4Net;
using Discord.Net.WebSockets;
using Microsoft.Extensions.Configuration;

namespace DiscordBot
{
    class Program
    {
        public static void Main(string[] args)
{
    var config = new ConfigurationBuilder().AddCommandLine(args).Build();
    var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseConfiguration(config)
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

    host.Run();
}
        
        DiscordSocketClient _client;
        CommandHandler _handler;

        public DiscordSocketClient client { get; private set; }

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();


        public async Task StartAsync()
        {

            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                WebSocketProvider = WS4NetProvider.Instance
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await _client.SetGameAsync("_commands for help");
            await _client.SetStatusAsync(UserStatus.Idle);
            await Task.Delay(-1);
        }

// Gonna apply changes here soon...        

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
