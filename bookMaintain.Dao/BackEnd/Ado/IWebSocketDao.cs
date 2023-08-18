using bookMaintain.Model.BackEnd.Arg.WebSocket;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IWebSocketDao
    {
        Task<int> InsertWebSocketContent(ChatroomContent content);
    }
}