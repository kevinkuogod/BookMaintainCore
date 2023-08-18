using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Table.Food
{
    //書籍類別檔				
    public class Food
    {
        [DisplayName("食物id")]
        public long ID { get; set; }

        [DisplayName("圖片與食物代號")]
        public string? ImgSrc { get; set; }


        [DisplayName("食物總類")]
        public string? Type { get; set; }

        [DisplayName("食物庫存")]
        public int Quantity { get; set; }

        [DisplayName("食物形容詞")]
        public string? Content { get; set; }

        [DisplayName("食物創建時間")]
        public DateTime CREATE_DATE { get; set; }

        [DisplayName("創建食物使用者")]
        public string? CREATE_USER { get; set; }

        [DisplayName("更改食物日期")]
        public DateTime MODIFY_DATE { get; set; }

        [DisplayName("修改食物使用者")]
        public string? MODIFY_USER { get; set; }

        [DisplayName("價格")]

        //public string? Price { get; set; }
        public int Price { get; set; }

        [DisplayName("名稱")]
        public string? Name { get; set; }

        [DisplayName("食物初始購買數量")]
        public int Number { get; set; }
    }
}
