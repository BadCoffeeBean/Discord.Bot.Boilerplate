using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discord.Bot.Boilerplate.Services.Infrastructure
{
	public class BotProcess : BackgroundService
	{
		private readonly IDiscordConnection _discordConnection;
		private readonly ILogger<IDiscordConnection> _logger;
		private readonly ICommander _commander;

		public BotProcess(
			IDiscordConnection discordConnection,
			ILogger<IDiscordConnection> logger,
			ICommander commander)
		{
			_discordConnection = discordConnection ?? throw new ArgumentNullException(nameof(discordConnection));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_commander = commander ?? throw new ArgumentNullException(nameof(commander));
		}
		
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				_logger.LogInformation("Bot process started.");
				await _commander.InstallCommandsAsync();
				await _discordConnection.Connect();
			}

			catch (TaskCanceledException)
			{
				_logger.LogInformation("Bot process was stopped manually.");
			}

			catch (Exception e)
			{
				_logger.LogError(e, $"Bot process stopping because it has encountered an error: {e.Message}");
			}

			finally
			{
				await _discordConnection.Disconnect();
				_logger.LogInformation("Bot process stopped.");
			}
		}

		public override async Task StopAsync(CancellationToken stoppingToken)
		{
			await _discordConnection.Disconnect();
			_logger.LogInformation("Bot process stopped.");
			await base.StopAsync(stoppingToken);
		}

		
	}
}
