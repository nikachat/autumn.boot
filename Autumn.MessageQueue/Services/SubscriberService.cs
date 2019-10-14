using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Text;
using Autumn.MessageQueue.IServices;
using Autumn.MessageQueue.Models;

namespace Autumn.MessageQueue.Services
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("services.show.time.without")]
        public void ReceivedMessageWithout(DateTime datetime)
        {
            Console.WriteLine(datetime);
        }

        [CapSubscribe("services.show.time.adonet")]
        public void ReceivedMessageAdonet(UserModel user)
        {
            Console.WriteLine(user.UserId + user.UserName);
        }
    }
}
