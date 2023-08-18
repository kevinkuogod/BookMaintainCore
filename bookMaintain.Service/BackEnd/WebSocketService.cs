using System.Collections.Generic;
using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;
using bookMaintain.Model.BackEnd.Arg.WebSocket;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class WebSocketService : IWebSocketService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private IWebSocketDao webSocketDao;
        public WebSocketService()
        {
            this.webSocketDao = new WebSocketDao();
        }

        /// <summary>
        /// 需要換成Book換Table
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public async Task<int> InsertWebSocketContent(ChatroomContent chatroomContent)
        {
            return await webSocketDao.InsertWebSocketContent(chatroomContent);
        }
    }
}