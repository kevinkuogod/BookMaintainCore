using System;
using System.Net;
using bookMaintain.Service;
using Newtonsoft.Json;
using bookMaintain.Common;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using Azure.Core;
using System.IO.Pipelines;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;

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
        //[RequestSizeLimit(100*1024*1024)] //限制http大小
        [HttpPost()]
        //[Route("Vocabulary/Insert")]
        //[ValidateAntiForgeryToken]
        //List<IFormFile> files //多檔案上傳
        public async Task<JsonResult> Insert(InsertArg formData)
        {
            //有值
            if (!ModelState.IsValid)
            {
                return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增值為空", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    //Model會上參數驗證
                    InsertArg vocabularyInsert = new InsertArg()
                    {
                        English_Name = formData.English_Name,
                        Chinese_Name = formData.Chinese_Name,
                        Part_Of_Speech = formData.Part_Of_Speech,
                        Part_Of_Speech_Detial = formData.Part_Of_Speech_Detial,
                        Example_Sentences = formData.Example_Sentences,
                        Example_Sentences_Translation = formData.Example_Sentences_Translation,
                        Provenance = formData.Provenance,
                        Editor = formData.Editor,
                        Kenyon_And_Knott = formData.Kenyon_And_Knott,
                        Professional_Field = formData.Professional_Field,
                        Extra_Matters = formData.Extra_Matters,
                        Tense = formData.Tense,
                        Remark = formData.Remark,
                        Perfect_Tense = formData.Perfect_Tense,
                        English_File = formData.English_File
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
                    /*return this.Json(new { type = "error", message = "新增過程錯誤" });*/
                    return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}