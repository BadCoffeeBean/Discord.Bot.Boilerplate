using Discord.Commands;
using System.Net.NetworkInformation;

namespace Discord.Bot.Boilerplate.Services.Commands
{
	public class DemoModule : ModuleBase<SocketCommandContext>
	{
		[Command("say")]
		[Summary("Echoes the message as a reply.")]
		public async Task EchoAsync([Remainder][Summary("The text to echo")] string echo)
		{
			await ReplyAsync(echo);
		}

		[Command("ping")]
		[Summary("Sends ping statistics.")]
		public async Task PingAsync()
		{
			var botServerReplyTime = new Ping().Send("127.0.0.1", 120).RoundtripTime;
			var discordConnectionReplyTime = Context.Client.Latency;

			await ReplyAsync($"\uD83C\uDFD3 Ping Statistics: {Environment.NewLine}{Environment.NewLine}Bot Server Latency: {botServerReplyTime} ms.{Environment.NewLine}Discord Gateway Latency: {discordConnectionReplyTime} ms.");
		}
	}
}
