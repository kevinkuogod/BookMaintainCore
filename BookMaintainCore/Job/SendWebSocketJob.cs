using bookMaintain.Common;
using bookMaintain.Service;
using Quartz;
using bookMaintain.Model.BackEnd.Arg.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BookMaintainCore.Job
{
    public class SendWebSocketJob : IJob
    {
        private IWebSocketService webSocketService;
        public SendWebSocketJob()
        {
            this.webSocketService = new WebSocketService();
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Code that sends a periodic email to the user (for example)
            // Note: This method must always return a value 
            // This is especially important for trigger listers watching job execution
            try
            {
                List<string> searchListRange = RedisTool.SearchListRange(0);
                foreach (var searchData in searchListRange)
                {
                    dynamic registerData = JObject.Parse(searchData);
                    string type = registerData.type;
                    switch (type)
                    {
                        case "<1>":
                            string Name = registerData.name;
                            string CREATE_DATE = registerData.getDate;
                            string Content = registerData.msg;
                            webSocketService.InsertWebSocketContent(new ChatroomContent()
                            {
                                Name = Name,
                                Content = Content,
                                CREATE_DATE = CREATE_DATE
                            });
                            break;
                    }
                }
                if (searchListRange.Count() != 0)
                {
                    RedisTool.DeleteListRange(0);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return Task.FromResult(true);
        }
    }
}
