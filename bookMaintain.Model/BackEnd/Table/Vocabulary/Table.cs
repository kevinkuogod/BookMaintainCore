using System.ComponentModel;


namespace bookMaintain.Model.BackEnd.Table.Vocabulary
{
    //書籍類別檔				
    public class Table
    {
        /// <summary>
        /// 文章id，目前暫定為字串1,2,3
        /// </summary>
        [DisplayName("文章id")]
        public string? ID { get; set; }

        /// <summary>
        /// 單字英文名稱
        /// </summary>
        //[ColumnAttribute]
        [DisplayName("單字英文名稱")]
        public string? English_Name { get; set; }

        /// <summary>
        /// 單字中文名稱
        /// </summary>
        [DisplayName("單字中文名稱")]
        public string? Chinese_Name { get; set; }

        /// <summary>
        /// 單字的國家
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("單字的國家")]
        public string? Language { get; set; }

        /// <summary>
        /// 單字的圖片
        /// </summary>
        [DisplayName("單字的圖片")]
        public string? Image_Id { get; set; }

        /// <summary>
        /// 單字詞性
        /// </summary>
        [DisplayName("單字詞性")]
        public string? Part_Of_Speech { get; set; }

        /// <summary>
        /// 單字詞細項
        /// </summary>
        [DisplayName("單字詞細項")]
        public int Part_Of_Speech_Detial { get; set; }

        /// <summary>
        /// 單字英文例句
        /// </summary>
        [DisplayName("單字英文例句")]
        public string? Example_Sentences { get; set; }

        /// <summary>
        /// 單字英文例句翻譯
        /// </summary>
        [DisplayName("單字英文例句翻譯")]
        public string? Example_Sentences_Translation { get; set; }

        /// <summary>
        /// 出現機率
        /// </summary>
        [DisplayName("出現機率")]
        public string? Chances { get; set; }

        /// <summary>
        /// 單字出處(不明)
        /// </summary>
        [DisplayName("單字出處")]
        public string? Provenance { get; set; }

        /// <summary>
        /// 主編
        /// </summary>
        [DisplayName("主編")]
        public int Editor { get; set; }

        /// <summary>
        /// KK音標，會有UK，US的區分
        /// </summary>
        [DisplayName("KK音標，會有UK，US的區分")]
        public string? Kenyon_And_Knott { get; set; }

        /// <summary>
        /// 常使用的專業領域
        /// </summary>
        [DisplayName("常使用的專業領域")]
        public string? Professional_Field { get; set; }

        /// <summary>
        /// 額外需要注意事項
        /// </summary>
        [DisplayName("額外需要注意事項")]
        public string? Extra_Matters { get; set; }

        /// <summary>
        /// 時態動詞有過去簡單式、現在簡單式、未來簡單式，預設無,無,無
        /// </summary>
        [DisplayName("時態動詞有過去簡單式")]
        public string? Tense { get; set; }

        /// <summary>
        /// 無法區分專業領域時目前有用到
        /// </summary>
        [DisplayName("無法區分專業領域時目前有用到")]
        public string? Remark { get; set; }

        /// <summary>
        /// 時態動詞有過去式完成式
        /// </summary>
        [DisplayName("時態動詞有過去式完成式")]
        public string? Perfect_Tense { get; set; }

        /// <summary>
        /// 判定分析完沒
        /// </summary>
        [DisplayName("判定分析完沒")]
        public string? analyze { get; set; }

        /// <summary>
        /// 更改使用者創建時間
        /// </summary>
        [DisplayName("更改使用者創建時間")]
        public string? Created_At { get; set; }

        /// <summary>
        /// 更改使用者更新時間
        /// </summary>
        [DisplayName("更改使用者更新時間")]
        public string? Updated_At { get; set; }
    }
}
