using System.Collections.Generic;
using bookMaintain.Model.BackEnd.Arg.Select;
using System.IO;

namespace bookMaintain.Dao.Component
{
    public class SelectDao : ISelectDao
    {
        private string rootCodeDataFilePath = @"D:\C#\test\BookMaintain\bookMaintain.Common\BookDataFile\";

        public List<SelectListValue> GetBookClass()
        {
            string bookClassFilePath = rootCodeDataFilePath + "BOOK_CLASS.txt";

            var lines = File.ReadAllLines(bookClassFilePath);
            List<SelectListValue> result = new List<SelectListValue>();
            string splitChar = "\t";

            foreach (var item in lines)
            {
                result.Add(new SelectListValue()
                {
                    Text = item.Split(splitChar.ToCharArray())[1],
                    Value = item.Split(splitChar.ToCharArray())[0]
                });
            }
            return result;
        }

        public List<SelectListValue> GetBookKeeper(string type)
        {
            string memborFilePath = rootCodeDataFilePath + "MEMBER_M.txt";

            var lines = File.ReadAllLines(memborFilePath);
            List<SelectListValue> result = new List<SelectListValue>();
            string splitChar = "\t";

            foreach (var item in lines)
            {
                if (type.Equals("EName", System.StringComparison.Ordinal))
                {
                    result.Add(new SelectListValue()
                    {
                        Text = item.Split(splitChar.ToCharArray())[2],
                        Value = item.Split(splitChar.ToCharArray())[0]
                    });
                }
                else if (type.Equals("CEName", System.StringComparison.Ordinal))
                {
                    result.Add(new SelectListValue()
                    {
                        Text = item.Split(splitChar.ToCharArray())[2] + "-" + item.Split(splitChar.ToCharArray())[1],
                        Value = item.Split(splitChar.ToCharArray())[0]
                    });
                }
            }
            return result;
        }

        public List<SelectListValue> GetBookCodeStatus(string status)
        {
            string bookCodeFilePath = rootCodeDataFilePath + "BOOK_CODE.txt";

            var lines = File.ReadAllLines(bookCodeFilePath);
            List<SelectListValue> result = new List<SelectListValue>();
            string splitChar = "\t";

            foreach (var item in lines)
            {
                if (item.Split(splitChar.ToCharArray())[0].Equals(status, System.StringComparison.Ordinal))
                {
                    result.Add(new SelectListValue()
                    {
                        Text = item.Split(splitChar.ToCharArray())[3],
                        Value = item.Split(splitChar.ToCharArray())[1]
                    });
                }

            }
            return result;
        }
    }
}
