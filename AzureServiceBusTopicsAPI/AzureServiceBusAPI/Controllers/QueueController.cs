using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Reflection.Metadata.Ecma335;

namespace AzureServiceBusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;
        private readonly string queueName;

        public QueueController(IConfiguration configuration) {
            _configuration=configuration;
            connectionstring = _configuration["servicebus"];
            queueName = _configuration["serviceName"];
        }
        [Route("send")]
        [HttpPost]
        public async Task<string> MessageSent(string message) {
            try
            {
                var _client = Microsoft.ServiceBus.Messaging.QueueClient.CreateFromConnectionString(connectionstring, queueName);
                BrokeredMessage Message = new BrokeredMessage(message);
                await _client.SendAsync(Message);
                return "Message Send";
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        [Route("recive")]
        [HttpGet]
        public async Task<string> MessageRecive()
        {
            const string queueName = "msgqueue";
            var queueClient = Microsoft.ServiceBus.Messaging.QueueClient.CreateFromConnectionString(connectionstring, queueName);
            BrokeredMessage message = queueClient.Receive();
            string body = message.GetBody<string>();
            message.Complete();
            message.Abandon();
            return body;
        }
    }
}
