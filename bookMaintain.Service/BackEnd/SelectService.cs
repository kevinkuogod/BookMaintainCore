using System.Collections.Generic;
using bookMaintain.Dao.Component;
using bookMaintain.Model.BackEnd.Arg.Select;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class SelectService : ISelectService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private ISelectDao selectDao;
        public SelectService()
        {
            this.selectDao = new SelectDao();
        }

        public List<SelectListValue> GetBookClass()
        {
            return selectDao.GetBookClass();
        }
        public List<SelectListValue> GetBookCodeStatus(string status)
        {
            return selectDao.GetBookCodeStatus(status);
        }
        public List<SelectListValue> GetBookKeeper(string type)
        {
            return selectDao.GetBookKeeper(type);
        }
    }
}