using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace Discord.Bot.Boilerplate.Services.Infrastructure
{
	public class Commander : ICommander
	{
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;
		private readonly IServiceProvider _services;

		public Commander(
			DiscordSocketClient client, 
			CommandService commands,
			IServiceProvider services)
		{
			_client = client;
			_commands = commands;
			_services = services;
		}

		public async Task InstallCommandsAsync()
		{
			_client.MessageReceived += HandleCommandAsync;
			await _commands.AddModulesAsync(assembly: Assembly.GetExecutingAssembly(), services: _services);
		}

		public async Task HandleCommandAsync(SocketMessage messageParam)
		{
			await HandleCommandAsyncInternal(messageParam);
		}
		
		private async Task HandleCommandAsyncInternal(SocketMessage messageParam)
		{
			// Don't process the command if it was a system message
			var message = messageParam as SocketUserMessage;
			if (message == null) return;

			// Create a number to track where the prefix ends and the command begins
			int argPos = 0;

			// Determine if the message is a command based on the prefix and make sure no bots trigger commands.
			if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || 
				message.Author.IsBot)
				return;

			var context = new SocketCommandContext(_client, message);
			await _commands.ExecuteAsync(context: context, argPos: argPos, services: _services);
		}
	}
}
