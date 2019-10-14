using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DotNetCore.CAP;
using Autumn.Common;
using Autumn.MessageQueue.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Autumn.MessageQueue.Controllers
{
    [Route("api/[controller]")]
    public class PublishController : Controller, ICapSubscribe
    {
        private readonly ICapPublisher _producer;

        public PublishController(ICapPublisher producer)
        {
            _producer = producer;
        }

        /// <summary>
        /// 不使用事务
        /// </summary>
        /// <returns></returns>
        [Route("~/WithoutTransaction")]
        public async Task<IActionResult> WithoutTransaction()
        {
            await _producer.PublishAsync("services.show.time.without", DateTime.Now);

            return Ok();
        }

        /// <summary>
        /// Ado.Net 中使用事务，不自动提交
        /// </summary>
        /// <returns></returns>
        [Route("~/AdonetWithTransaction")]
        public IActionResult AdonetWithTransaction()
        {
            using (var connection = new MySqlConnection(DBConfigHelper.ConnectionString))
            {
                using (var transaction = connection.BeginTransaction(_producer, autoCommit: false))
                {
                    //your business code
                    connection.Execute("insert into test(name) values('test')", transaction: (IDbTransaction)transaction.DbTransaction);

                    for (int i = 0; i < 5; i++)
                    {
                        _producer.Publish("services.show.time.adonet", new UserModel { UserId = 100, UserName = "测试" });
                    }

                    transaction.Commit();
                }
            }
            return Ok();
        }

        //[CapSubscribe("sample1")]
        //public void Test(DateTime value)
        //{
        //    Console.WriteLine("Subscriber output message: " + value.ToLongDateString());
        //}

        //[CapSubscribe("sample2")]
        //public void Test2(int value)
        //{
        //    Console.WriteLine("Subscriber output message: " + value);
        //}
    }
}
