using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Table.Article;
using Confluent.Kafka;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class RabbitMQService
    {
        //https://blog.yowko.com/dotnet-client-rabbitmq-cluster/
        private IArticleDao articleDao;
        public RabbitMQService()
        {
            //this.articleDao = new ArticleDao();
        }

        public static async Task Receive(IFormFile file)
        {
            //初始化連線資訊
            var factory = new ConnectionFactory();
            //設定 RabbitMQ 位置
            //factory.HostName = "localhost";
            //設定連線 RabbitMQ username
            factory.UserName = "yowko";
            //設定 RabbitMQ password
            factory.Password = "pass.123";
            //寫法二 
            //factory.Uri = "amqp://yowko:pass.123@localhost:5672";
            //寫法三
            //var factory = new ConnectionFactory() { Uri = "amqp://yowko:pass.123@192.168.56.101:5672"};
            //開啟連線
            using (var connection = factory.CreateConnection(new string[2] { "192.168.56.101", "localhost" }))
            //開啟 channel
            using (var channel = connection.CreateModel())
            {
                string exchange = "yowko";
                string queue = "eventTest";
                string routingKey = "hello";
                //宣告 exchanges，RabbitMQ提供了四種Exchange模式：fanout,direct,topic,header
                channel.ExchangeDeclare(exchange, ExchangeType.Direct);
                //宣告 queues
                channel.QueueDeclare(queue, true, false, false, null);
                //將 exchnage、queue 依 route rule 綁定
                channel.QueueBind(queue, exchange, routingKey, null);
                channel.BasicQos(0, 1, true);
                string message = $"Hello World-{Guid.NewGuid()}";
                var body = Encoding.UTF8.GetBytes(message);
                //channel.BasicPublish(exchange, routingKey, new RabbitMQ.Client.Framing.BasicProperties { Persistent = true }, body);
                Console.WriteLine($"Send Message：{message};{connection.ToString()}");
            }
        }

        public static void Consumer(string conmment)
        {
            //初始化連線資訊
            var factory = new ConnectionFactory()
            {
                //設定連線 RabbitMQ username
                UserName = "yowko",
                //設定 RabbitMQ password
                Password = "pass.123",
                //自動回復連線
                AutomaticRecoveryEnabled = true,
                //心跳檢測頻率
                RequestedHeartbeat = TimeSpan.FromTicks(486000000000) //TimeSpan要查
            };
            var queueName = "event";
            //連線多個 rabbitmq instance
            using (var connection = factory.CreateConnection(AmqpTcpEndpoint.ParseMultiple("localhost:5672,192.168.56.101:5672")))
            {
                //處理連線中斷
                connection.ConnectionShutdown += (o, e) =>
                {
                    //handle disconnect      
                    Console.WriteLine($"Fail:{0},{e}");
                };
                //開啟 channel
                using (var channel = connection.CreateModel())
                {
                    //宣告 queues
                    channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    Console.WriteLine(" [*] Waiting for messages.");
                    //建立 consumer
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: queueName,
                            //noAck: false,
                            consumer: consumer);
                    //收到訊息時的處理方式
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        //var message = Encoding.UTF8.GetString(body);
                        //Console.WriteLine($" [x] Received {message} from {connection.ToString()}");
                        //手動 ack
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        Console.WriteLine("OK");
                    };
                    Console.WriteLine(" Press [enter] to exit.");
                    //持續等著接收訊息
                    while (true)
                    {
                    }
                }
            }
        }
    }
}