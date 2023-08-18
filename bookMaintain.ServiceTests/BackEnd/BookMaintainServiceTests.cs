using Microsoft.VisualStudio.TestTools.UnitTesting;
using bookMaintain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service.Tests
{
    [TestClass()]
    public class BookMaintainServiceTests
    {
        [TestMethod()]
        public void GetTableTest()
        {
            var bookMaintainDao = new BookMaintainDao();
            //SearchArg arg = new SearchArg() { BOOK_CLASS_ID="",CODE_ID="",BOOK_NAME="", pageSize = 4, USER_ID="1"  };
            SearchArg arg = new SearchArg() { };
            bookMaintainDao.GetTable(arg);
            //Assert.Fail();
        }
    }
}