using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class LoginService : ILoginService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private ILoginDao registerDao;
        public LoginService()
        {
            this.registerDao = new LoginDao();
        }

        public dynamic Insert(Login insertArg)
        {
            //return registerDao.Insert(insertArg);
            return registerDao.InsertMysql(insertArg);
        }

        public int LoginNumber()
        {
            return registerDao.LoginNumber();
        }

        public int ForgetPassword(Login forgetPasswordArg)
        {
            return registerDao.ForgetPassword(forgetPasswordArg);
        }
    }
}