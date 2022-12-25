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


        /*public async Task<ActionResult> Index(string insertJson, IFormFile file)
        {
            IFormFile file1 = file;
            FileInfo fi = new FileInfo(file1.FileName);
            string path = "C:\\Users\\kevin\\Desktop\\" + fi.Name;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file1.CopyToAsync(fileStream);
            }*/
        /// <summary>
        /// 新增圖書維護(新增)
        /// </summary>
        /// <returns></returns>
        //[RequestSizeLimit(100*1024*1024)] //限制http大小
        [HttpPost()]
        //[Route("Vocabulary/Insert")]
        //[ValidateAntiForgeryToken]
        //List<IFormFile> files //多檔案上傳
        public JsonResult Insert(string insertJson, IFormFile English_File)
        //public ActionResult Insert(InsertArg English_File)
        //public JsonResult Insert()
        //public ActionResult Insert(string insertJson, IFormFile files)
        //public ActionResult Insert(FormContext context)
        //public IActionResult Index(IFormFile formData)
        //public async Task<JsonResult> Index(string insertJson, IFormFile EnglishFile)
        //public async Task<JsonResult> Index(IFormFile formData)
        //public async Task<IActionResult> Index(IList<IFormFile> formData)
        {
            //有值
            var file = Request.Form["English_File"];
            var fileCount = Request.Form.Files.Count;
            IFormFile file1 = Request.Form.Files["English_File"];
            //FileInfo fi = new FileInfo(file1.FileName);
            //string path = "C:\\Users\\kevin\\Desktop\\" + fi.Name;
            //using (var fileStream = new FileStream(path, FileMode.Create))
            {
                //await file1.CopyToAsync(fileStream);
            }

            //string insertJson= "";
            //string[] files = { "Volvo", "BMW", "Ford", "Mazda" };
            //ModelState.IsValid
            //files1 = Request.Form.Files;
            //var file = files1.First();
            /*foreach (var file in files1)
            {
                if (file.Length > 0)
                {
                    Console.WriteLine("友直了");
                }
            }*/
            /*foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    Console.WriteLine("友直了");
                }
            }*/
            //string.IsNullOrEmpty(insertJson)
            if (string.IsNullOrEmpty("123"))
            {
                return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增值為空", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    /*dynamic insertData = JsonConvert.DeserializeObject(insertJson);
                    //Model會上參數驗證
                    string path2 = insertData.English_File.Value;
                    FileInfo fi = new FileInfo(path2);
                    string path = "C:\\Users\\kevin\\Desktop\\"+ fi.Name;
                    //path2 = System.IO.Path.GetFullPath(path2);
                    using (FileStream fileStream = System.IO.File.Open(path2, FileMode.Open, FileAccess.Read))
                    {
                        var stream = System.IO.File.Create(path);
                        fileStream.CopyTo(stream);
                    }*/
                    /*HttpPostedFile file = Request.Files[i];
                    HttpContext.Request.Files
                    HttpContext.Current
                    Request.
                    Request.Files["userUploadedFile"];
                    Path.GetFileName(file);
                    var file = Request.Files[insertData.EnglishFile];
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        uploadStream.Seek(0, SeekOrigin.Begin);
                        uploadStream.CopyTo(fileStream);
                    }
                    using (FileStream objfilestream = new FileStream(insertData.EnglishFile, FileMode.Create, FileAccess.ReadWrite))
                    {
                        objfilestream.CopyToAsync(path);
                        objfilestream.CopyTo(path);
                        objfilestream.Write(bytes, 0, bytes.Length);
                    }*/

                    /*InsertArg vocabularyInsert = new InsertArg()
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
                    };*/
                    int errorNumber = 0;
                    //int errorNumber = vocabularyService.InsertVocabulary(vocabularyInsert);
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