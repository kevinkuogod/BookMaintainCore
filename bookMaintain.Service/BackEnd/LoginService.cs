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

        public int Insert(Login insertArg)
        {
            return registerDao.Insert(insertArg);
        }

        public int ForgetPassword(Login forgetPasswordArg)
        {
            return registerDao.ForgetPassword(forgetPasswordArg);
        }
    }
}