using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.WebSocket
{
    //聊天室內容				
    public class ChatroomContent
    {
        /// <summary>
        /// 聊天室姓名
        /// </summary>
        [DisplayName("聊天室姓名")]
        public string? Name { get; set; }

        /// <summary>
        /// 聊天室名稱
        /// </summary>
        [DisplayName("聊天室名稱")]
        public string? Content { get; set; }

        /// <summary>
        /// 聊天內容創建時間
        /// </summary>
        [DisplayName("聊天內容創建時間")]
        public string? CREATE_DATE { get; set; }
    }
}
