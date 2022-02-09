using GPPlanetGQL.Discord;
using Quartz;

namespace GPPlanetGQL.Jobs
{
    public class DiscordOnlineJob : IJob
    {
        private readonly DiscordBot _bot;
        public DiscordOnlineJob([Service] DiscordBot bot)
        {
            _bot = bot;
        }
        async Task IJob.Execute(IJobExecutionContext context)
        {
            await _bot.UpdateStatus();
        }
    }
}
