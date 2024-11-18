using Microsoft.AspNetCore.SignalR;

namespace Backoffice.Hubs
{
    public sealed class YoutubeNotificationHub : Hub<IYoutubeNotificationHub>
    {
        public async Task SendMessage(string channel, string link)
        {
            await Clients.All.ReceiveMessage(channel, link);
        }
    }

    public interface IYoutubeNotificationHub
    {
       Task ReceiveMessage(string channel, string link);
    }
}
