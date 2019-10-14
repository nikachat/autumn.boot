using Autumn.MessageQueue.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.MessageQueue.IServices
{
    public interface ISubscriberService
    {
        void ReceivedMessageWithout(DateTime datetime);

        void ReceivedMessageAdonet(UserModel user);
    }
}
