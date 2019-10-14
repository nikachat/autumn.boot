using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.SignalrChat
{
    public class ChatHub :Hub
    {
        /// <summary>
        /// 群发消息（发送给所有连接的客户端）
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task SendMessage(string msg)
        {
            return Clients.All.SendAsync("ReceiveMessage", msg);
        }

        /// <summary>
        /// 发送消息（发送给指定用户）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendPrivateMessage(string userId, string message)
        {
            return Clients.User(userId).SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// 开启连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var item = ConnectionManager.ConnectionUsers.Where(m => m.ConnectionId == Context.ConnectionId).FirstOrDefault();
                //移除相关联用户
                ConnectionManager.ConnectionUsers.Remove(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hub"></param>
        public static void SendMessage(ChatHub chatHub, MessageData messageData)
        {
            var sendMsg = JsonConvert.SerializeObject(messageData);
            foreach (ConnectionUser user in ConnectionManager.ConnectionUsers)
            {
                chatHub.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", sendMsg);
            }
        }

        /// <summary>
        /// 服务器端中转消息处理
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ServerTransferMessage(string message)
        {
            await MessageDealWidth.DealWidth(message, this);
        }

    }
}
