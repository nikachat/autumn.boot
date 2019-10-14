//====================调用示例=======================
//SendEmailModel sendEmailModel = new SendEmailModel();
//sendEmailModel.Body = "内容";
//sendEmailModel.FromAccount = "jim.fan@xxx.com";
//sendEmailModel.FromAddress = "jim.fan@xxx.com";
//sendEmailModel.FromPassword = "***********";
//sendEmailModel.Subject = "标题";
//sendEmailModel.ToAddress = "jim.fan@xxx.com";
//EmailHelper.SendEmail(sendEmailModel);
//====================调用示例=======================

using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;

namespace Autumn.Common
{
    public class EmailHelper
    {
        /// <summary>
        /// Smtp服务器地址
        /// </summary>
        private static readonly string SmtpServer = Appsettings.Get(new string[] { "Email", "SmtpServer" });

        /// <summary>
        /// Pop服务器地址
        /// </summary>
        private static readonly string PopServer = Appsettings.Get(new string[] { "Email", "PopServer" });

        /// <summary>
        /// Imap服务器地址
        /// </summary>
        private static readonly string ImapServer = Appsettings.Get(new string[] { "Email", "ImapServer" });

        /// <summary>
        /// SMTP端口
        /// </summary>
        private static readonly int SmtpPort = Appsettings.Get(new string[] { "Email", "SmtpPort" }).ToInt();

        /// <summary>
        /// POP端口
        /// </summary>
        private static readonly int PopPort = Appsettings.Get(new string[] { "Email", "PopPort" }).ToInt();

        /// <summary>
        /// IMAP端口
        /// </summary>
        private static readonly int ImapPort = Appsettings.Get(new string[] { "Email", "ImapPort" }).ToInt();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailModel">邮件实体</param>
        public static void SendEmail(SendEmailModel emailModel)
        {
            try
            {
                var message = new MimeMessage();

                if (emailModel.FromName == string.Empty)

                    message.From.Add(new MailboxAddress(emailModel.FromAddress));
                else
                    message.From.Add(new MailboxAddress(emailModel.FromName, emailModel.FromAddress));

                if (emailModel.ToName == string.Empty)

                    message.To.Add(new MailboxAddress(emailModel.ToAddress));
                else
                    message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToAddress));

                message.Subject = emailModel.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = @emailModel.Body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(SmtpServer, SmtpPort, false);

                    client.Authenticate(emailModel.FromAccount, emailModel.FromPassword);

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorLog(ex.Message, ex);
            }
        }

        /// <summary>
        /// POP3接收邮件
        /// </summary>
        /// <param name="FromAccount">邮箱账户</param>
        /// <param name="FromPassword">邮箱密码</param>
        /// <returns></returns>
        public static IList<ReceiveEmailModel> ReceivePop3Email(string FromAccount, string FromPassword)
        {
            List<ReceiveEmailModel> receiveEmailList = new List<ReceiveEmailModel>();

            using (var client = new Pop3Client())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(PopServer, PopPort, false);

                client.Authenticate(FromAccount, FromPassword);

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);

                    ReceiveEmailModel receiveEmailModel = new ReceiveEmailModel();

                    receiveEmailModel.FromAddress = message.From[0].Name;

                    receiveEmailModel.Date = message.Date.DateTime;

                    receiveEmailModel.Subject = message.Subject;

                    receiveEmailModel.Body = message.TextBody;

                    receiveEmailList.Add(receiveEmailModel);
                }
                client.Disconnect(true);
            }
            return receiveEmailList;
        }

        /// <summary>
        /// IMAP接收邮件
        /// </summary>
        /// <param name="FromAccount">邮箱账户</param>
        /// <param name="FromPassword">邮箱密码</param>
        /// <returns></returns>
        public static IList<ReceiveEmailModel> ReceiveImapEmail(string FromAccount, string FromPassword)
        {
            List<ReceiveEmailModel> receiveEmailList = new List<ReceiveEmailModel>();

            using (var client = new ImapClient())
            {
                // For demo-purposes, accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(ImapServer, ImapPort, true);

                client.Authenticate(FromAccount, FromPassword);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;

                inbox.Open(FolderAccess.ReadOnly);

                //Console.WriteLine("Total messages: {0}", inbox.Count);

                //Console.WriteLine("Recent messages: {0}", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);

                    ReceiveEmailModel receiveEmailModel = new ReceiveEmailModel();

                    receiveEmailModel.FromAddress = message.From[0].Name;

                    receiveEmailModel.Date = message.Date.DateTime;

                    receiveEmailModel.Subject = message.Subject;

                    receiveEmailModel.Body = message.TextBody;

                    receiveEmailList.Add(receiveEmailModel);
                }
                client.Disconnect(true);
            }
            return receiveEmailList;
        }

        /// <summary>
        /// 根据唯一号查询信件
        /// </summary>
        /// <param name="mailFromAccount">邮箱账号</param>
        /// <param name="mailPassword">邮箱密码</param>
        /// <param name="id">唯一号</param>
        /// <param name="folderName">文件夹名称</param>
        /// <returns></returns>
        public static MimeMessage GetEmailByUniqueId(string mailFromAccount, string mailPassword, uint id, string folderName)
        {
            //打开收件箱
            var folder = OpenFolder(mailFromAccount, mailPassword, folderName);

            UniqueId emailUniqueId = new UniqueId(id);

            MimeMessage message = folder.GetMessage(emailUniqueId);

            /*将邮件设为已读*/
            MessageFlags flags = MessageFlags.Seen;

            folder.SetFlags(emailUniqueId, flags, true);

            return message;
        }

        /// <summary>
        /// 打开邮箱文件夹
        /// </summary>
        /// <param name="mailFromAccount">邮箱账号</param>
        /// <param name="mailPassword">邮箱密码</param>
        /// <param name="folderName">文件夹名称(INBOX:收件箱名称)</param>
        /// <returns></returns>
        public static IMailFolder OpenFolder(string mailFromAccount, string mailPassword, string folderName)
        {
            ImapClient client = new ImapClient();

            client.Connect(ImapServer, ImapPort);

            client.Authenticate(mailFromAccount, mailPassword);

            //获取所有文件夹
            //List<IMailFolder> mailFolderList = client.GetFolders(client.PersonalNamespaces[0]).ToList();

            var folder = client.GetFolder(folderName);

            //打开文件夹并设置为读的方式
            folder.Open(FolderAccess.ReadWrite);

            return folder;
        }

    }
}
