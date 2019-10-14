using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.SignalrChat
{
    /// <summary>
    /// 消息处理
    /// </summary>
    public class MessageDealWidth
    {
        public static async Task DealWidth(string message, ChatHub chatHub)
        {
            await Task.Run(() => {
                try
                {
                    MessageData data = JsonConvert.DeserializeObject<MessageData>(message);
                    if (data != null)
                    {
                        ConnectionUser connectionUser = null;
                        MessageData sendMsg = null;
                        switch (data.MessageType)
                        {
                            case MessageType.Line:
                                connectionUser = ConnectionManager.ConnectionUsers.Where(m => m.ConnectionId == chatHub.Context.ConnectionId).FirstOrDefault();
                                //处理连接消息
                                if (connectionUser == null)
                                {
                                    connectionUser = new ConnectionUser();
                                    connectionUser.ConnectionId = chatHub.Context.ConnectionId;
                                    connectionUser.UserId = data.SendUserId;
                                    ConnectionManager.ConnectionUsers.Add(connectionUser);
                                }
                                //处理发送回执消息
                                sendMsg = new MessageData();
                                sendMsg.MessageBody = "";
                                sendMsg.MessageType = MessageType.LineReceipt;
                                sendMsg.SendUserId = "0";
                                chatHub.Clients.Client(chatHub.Context.ConnectionId).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(sendMsg));
                                break;
                            case MessageType.Text:
                                //处理普通文字消息
                                ChatHub.SendMessage(chatHub, data);
                                break;
                            case MessageType.LineReceipt:
                                //处理连接回执消息
                                ChatHub.SendMessage(chatHub, data);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
