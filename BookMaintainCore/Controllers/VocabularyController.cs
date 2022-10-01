using System;
using System.Net;
using bookMaintain.Service;
using Newtonsoft.Json;
using bookMaintain.Common;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;
using Microsoft.AspNetCore.Mvc;

namespace BookMaintain.Controllers
{
    public class VocabularyController : Controller
    {
        private IVocabularyService vocabularyService;
        public VocabularyController()
        {
            this.vocabularyService = new VocabularyService();
        }
        /// <summary>
        /// 登入系統，比較特殊需要另外開資料夾
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View("Index");
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return View("Error");
            }
        }

        /// <summary>
        /// 新增圖書維護(新增)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public JsonResult Insert(string insertJson)
        {
            //ModelState.IsValid
            if (string.IsNullOrEmpty(insertJson))
            {
                return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增值為空", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    dynamic insertData = JsonConvert.DeserializeObject(insertJson);
                    //Model會上參數驗證

                    InsertArg vocabularyInsert = new InsertArg()
                    {
                        English_Name = insertData.English_Name.Value,
                        Chinese_Name = insertData.Chinese_Name.Value,
                        Part_Of_Speech = insertData.Part_Of_Speech.Value,
                        Part_Of_Speech_Detial = insertData.Part_Of_Speech_Detial.Value,
                        Example_Sentences = insertData.Example_Sentences.Value,
                        Example_Sentences_Translation = insertData.Example_Sentences_Translation.Value,
                        Provenance = insertData.Provenance.Value,
                        Editor = insertData.Editor.Value,
                        Kenyon_And_Knott = insertData.Kenyon_And_Knott.Value,
                        Professional_Field = insertData.Professional_Field.Value,
                        Extra_Matters = insertData.Extra_Matters.Value,
                        Tense = insertData.Tense.Value,
                        Remark = insertData.Remark.Value,
                        Perfect_Tense = insertData.Perfect_Tense.Value
                    };
                    int errorNumber = vocabularyService.InsertVocabulary(vocabularyInsert);
                    if (errorNumber > 0)
                    {
                        return this.Json(new { type = "insert", message = "新增成功" });
                    }
                    else
                    {
                        return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增過程錯誤", code = errorNumber }
                            , HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception parameterEx)
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                    return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}