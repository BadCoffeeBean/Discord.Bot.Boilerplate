using Discord.WebSocket;

namespace Discord.Bot.Boilerplate.Services.Infrastructure
{
	public interface ICommander
	{
		Task InstallCommandsAsync();

		Task HandleCommandAsync(SocketMessage messageParam);
	}
}
