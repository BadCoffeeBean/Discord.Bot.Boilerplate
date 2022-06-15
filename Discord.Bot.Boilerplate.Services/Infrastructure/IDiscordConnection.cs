using Discord.WebSocket;

namespace Discord.Bot.Boilerplate.Services.Infrastructure
{
	public interface IDiscordConnection
	{
		Task Connect();

		Task Disconnect();
	}
}
