using Discord.Bot.Boilerplate.Models;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace Discord.Bot.Boilerplate.Services.Infrastructure
{
	public class DiscordConnection : IDiscordConnection
	{
		private readonly DiscordSettings _settings;
		private readonly DiscordSocketClient _client;

		public DiscordConnection(IOptions<DiscordSettings> settings, DiscordSocketClient client)
		{
			_settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task Connect()
		{
			await _client.LoginAsync(TokenType.Bot, _settings.Token);
			await _client.StartAsync();
			await Task.Delay(-1);
		}

		public async Task Disconnect()
		{
			await _client.StopAsync();
		}
	}
}
