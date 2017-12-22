using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>

    {
        private object GetHeapSize()
        {
            throw new NotImplementedException();
        }

        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.User.Username + " said:");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("select")]
        public async Task PickOne([Remainder]string message)
        {
            string[] option = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = option[r.Next(0, option.Length)];


            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username + ":");
            embed.WithDescription(selection);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("ping")]
        public async Task Ping() => await Context.Channel.SendMessageAsync("Pong! (ping coming soon)");


        [Command("rate")]
        public async Task Rate([Remainder]string message)
        {
            List<int> rateList = new List<int>();
            rateList.Add(-1);
            rateList.Add(1);
            rateList.Add(2);
            rateList.Add(3);
            rateList.Add(4);
            rateList.Add(5);
            rateList.Add(6);
            rateList.Add(7);
            rateList.Add(8);
            rateList.Add(9);
            rateList.Add(10);
            rateList.Add(11);

            Random n = new Random();
            int rating = rateList[n.Next(0, 11)];

            var embed = new EmbedBuilder();
            embed.WithTitle("My rating for " + Context.User.Username + ":");
            embed.WithDescription(rating + "/10");
            embed.WithColor(new Color(0, 0, 255));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("commands")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();
            var application = await Context.Client.GetApplicationInfoAsync();
            embed.WithColor(new Color(0x4900ff))


            .AddField(y =>
            {
                y.Name = "Commands list:";
                y.Value = "List of __all working__ commands.";
                y.IsInline = false;
            })
            .AddField(y =>
            {
                y.Name = "_ping";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_echo";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_select 1|2|3";
                y.IsInline = true;
            }).AddField(y =>
            {
                y.Name = "_rate";
                y.IsInline = false;
            })
            .AddField(y =>
            {
                y.Name = "_dice";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_8ball";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_coin";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_hi";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_info";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_userinfo";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_DM";
                y.IsInline = true;
            })
            .AddField(y =>
            {
                y.Name = "_invite";
                y.IsInline = false;
            })
            .AddField(y =>
            {
                y.Name = "_oof";
                y.IsInline = false;
            });
            await this.ReplyAsync("", embed: embed);
        }


        [Command("dice")]
        public async Task Roll()
        {
            List<int> dice = new List<int>();
            dice.Add(1);
            dice.Add(2);
            dice.Add(3);
            dice.Add(4);
            dice.Add(5);
            dice.Add(6);

            Random d = new Random();
            int rolled = dice[d.Next(0, 5)];

            await Context.Channel.SendMessageAsync(Context.User.Username + " threw: " + "***" + rolled + "***");
        }

        [Command("8ball")]
        public async Task answer([Remainder]string message)
        {
            List<string> answers = new List<string>();
            answers.Add("Yes.");
            answers.Add("No.");
            answers.Add("Try again later.");
            answers.Add("Of course! :smiley:");
            answers.Add("Meh...");

            Random a = new Random();
            string odp = answers[a.Next(0, 4)];

            await Context.Channel.SendMessageAsync("Answer for " + "**" + Context.User.Username + "**" + ": " + odp);
        }

        [Command("coin")]
        public async Task Coin()
        {
            List<string> flip = new List<string>();
            flip.Add("Heads");
            flip.Add("Tails");

            Random c = new Random();
            string flipped = flip[c.Next(0, 1)];

            await Context.Channel.SendMessageAsync(flipped);
        }

        [Command("hi")]
        public async Task sayingHi()
        {
            List<string> powitania = new List<string>();
            powitania.Add("Yo");
            powitania.Add("Hi");
            powitania.Add("Hello");
            powitania.Add("Sup");


            Random p = new Random();
            string wita = powitania[p.Next(0, 4)];

            await Context.Channel.SendMessageAsync(wita + " " + Context.User.Username);
        }

        [Command("info")]
        public async Task Info()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var embed = new EmbedBuilder();
                var application = await Context.Client.GetApplicationInfoAsync();
                embed.ImageUrl = application.IconUrl;
                embed.WithColor(new Color(0x4900ff))

                .AddField(y =>
                {

                    y.Name = "Bot Creator";
                    y.Value = application.Owner.Username; application.Owner.Id.ToString();
                    y.IsInline = false;
                })
                .AddField(y =>
                {
                    y.Name = "Active for";
                    var time = DateTime.Now - process.StartTime;
                    var sb = new StringBuilder();
                    if (time.Days > 0)
                    {
                        sb.Append($"{time.Days}d ");
                    }
                    if (time.Hours > 0)
                    {
                        sb.Append($"{time.Hours}h ");
                    }
                    if (time.Minutes > 0)
                    {
                        sb.Append($"{time.Minutes}m ");
                    }
                    sb.Append($"{time.Seconds}s ");
                    y.Value = sb.ToString();
                    y.IsInline = true;
                })
                .AddField(y =>
                {
                    y.Name = "Discord.net";
                    y.Value = DiscordConfig.Version;
                    y.IsInline = true;
                }).AddField(y =>
                {
                    y.Name = "Server Count";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Count.ToString();
                    y.IsInline = false;
                })
                .AddField(y =>
                {
                    y.Name = "User Count";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count).ToString();
                    y.IsInline = false;
                })
                .AddField(y =>
                {
                    y.Name = "Channels";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count).ToString();
                    y.IsInline = false;
                })
                .AddField(y =>
                {
                    y.Name = "Status";
                    y.Value = "oof";
                    y.IsInline = false;
                });
                await this.ReplyAsync("", embed: embed);
            }
        }


        [Command("userinfo")]
        public async Task UserInfo(IGuildUser user)
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            var date = $"{user.CreatedAt.Month}/{user.CreatedAt.Day}/{user.CreatedAt.Year}";
            var auth = new EmbedAuthorBuilder()

            {

                Name = user.Username,
            };

            var embed = new EmbedBuilder()

            {
                Color = new Color(29, 140, 209),
                Author = auth
            };

            var us = user as SocketGuildUser;

            var D = us.Username;

            var A = us.Discriminator;
            var T = us.Id;
            var S = date;
            var C = us.Status;
            var CC = us.JoinedAt;
            var O = us.Game;
            embed.Title = $"**{us.Username}**";
            embed.Description = $"\nUsername: **{D}**\nDiscriminator: **{A}**\nID: **{T}**\nJoined Discord on: **{S}**\nStatus: **{C}**\nServer Join Date: **{CC}**\nStatus: **{O}**";

            await ReplyAsync("", false, embed.Build());
        }

        [Command("DM")]
        public async Task sendmsgtoowner([Remainder] string text)
        {
            var embed = new EmbedBuilder()
            {
                Color = new Color(231, 31, 31)
            };
            var application = await Context.Client.GetApplicationInfoAsync();
            var user = application.Owner.GetOrCreateDMChannelAsync();
            var z = await application.Owner.GetOrCreateDMChannelAsync();
            embed.Description = $"`{Context.User.Username}` z `{Context.Guild.Name}` wysłał/-a ci wiadomość!: \n\n{text}";
            await z.SendMessageAsync("", false, embed);
        }

        [Command("invite")]
        public async Task Invite() => await Context.Channel.SendMessageAsync("I'm not ready for this! :scream:");

        [Command("oof")]
        public async Task dead() => await Context.Channel.SendMessageAsync(":regional_indicator_o::regional_indicator_o::regional_indicator_f:");
    }
}
