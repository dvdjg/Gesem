using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebGestion.Hubs
{
    [HubName("MvcHub")]
    public class MvcHub : Hub
    {
        MvcHub()
        {
            
        }

        string ConnectionId { get { return Context.ConnectionId; } }

    }
}