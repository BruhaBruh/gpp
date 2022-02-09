using GPPlanetGQL.Models;
using GPPlanetGQL.Services;
using Quartz;

namespace GPPlanetGQL.Jobs
{
    public class UserInitJob : IJob
    {
        public readonly LimeService _lime;
        public readonly gpplanetContext _ctx;

        public UserInitJob([Service] LimeService lime, [Service] gpplanetContext ctx)
        {
            _lime = lime;
            _ctx = ctx;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            var data = await _lime.GetUserDatas();

            foreach (var discord in data.Keys.ToArray())
            {
                var u = _ctx.Users.Where(u => u.DiscordId == long.Parse(discord)).FirstOrDefault();
                if (u != null) continue;
                _ctx.Users.Add(new User
                {
                    DiscordId = long.Parse(discord)
                });
            }
            _ctx.SaveChanges();
        }
    }
}
