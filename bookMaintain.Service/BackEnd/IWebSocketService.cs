using bookMaintain.Model.BackEnd.Arg.WebSocket;

namespace bookMaintain.Service
{
    public interface IWebSocketService
    {
        Task<int> InsertWebSocketContent(ChatroomContent chatroomContent);
    }
}