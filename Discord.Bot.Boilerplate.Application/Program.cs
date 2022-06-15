using Discord.Bot.Boilerplate.Models;
using Discord.Bot.Boilerplate.Services.Infrastructure;
using Discord.Commands;
using Discord.WebSocket;

namespace Discord.Bot.Boilerplate.Application
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.Configure<DiscordSettings>(builder.Configuration.GetSection(nameof(DiscordSettings)));
			builder.Services.AddLogging(l =>
			{
				l.AddConsole();
				l.AddDebug();
			});
			builder.Services.AddSingleton<DiscordSocketClient>();
			builder.Services.AddSingleton<CommandService>();
			builder.Services.AddSingleton<IDiscordConnection, DiscordConnection>();
			builder.Services.AddSingleton<ICommander, Commander>();
			builder.Services.AddHostedService<BotProcess>();

			var app = builder.Build();
			app.Run();
		}
	}
}