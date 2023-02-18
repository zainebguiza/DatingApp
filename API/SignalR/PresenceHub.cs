using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{   
    [Authorize]
    public class PresenceHub   :Hub
    {
        private readonly PresenceTracker _trakcer;
        public PresenceHub(PresenceTracker trakcer)  
        {
            _trakcer = trakcer;
            
        }
        public override async Task OnConnectedAsync()
        {   var isOnline=await _trakcer.UserConnected(Context.User.GetUsername(),Context.ConnectionId);
            if(isOnline)
               await Clients.Others.SendAsync("UserIsOnline",Context.User.GetUsername());

            var currentUsers=await _trakcer.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers",currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {   var isOffline= await _trakcer.UserDisconnected(Context.User.GetUsername(),Context.ConnectionId);
            if(isOffline)
              await Clients.Others.SendAsync("UserIsOffline",Context.User.GetUsername());
             
            await base.OnDisconnectedAsync(exception);
        }
    }
}