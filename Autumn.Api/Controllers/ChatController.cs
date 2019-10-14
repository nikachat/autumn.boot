using System.Threading.Tasks;
using Autumn.Controllers;
using Autumn.Extension;
using Autumn.Model;
using Autumn.SignalrChat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Autumn.Api.Controllers
{
    /// <summary>
    /// 聊天
    /// </summary>
    public class ChatController : BaseController
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> SendMessageChat([FromBody] UserModel userModel)
        {
            var userId = userModel.UserId.ToString();
            var message = userModel.bRemark;

            if (!string.IsNullOrWhiteSpace(userId))
                await _chatHub.Clients.User(userId).SendAsync("ReceiveMessage", new { message });

            return _ResponseModel;
        }

    }
}