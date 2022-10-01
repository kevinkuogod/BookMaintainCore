using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class RegisterService : IRegisterService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private IRegisterDao registerDao;
        public RegisterService()
        {
            this.registerDao = new RegisterDao();
        }

        public int Insert(Register insertArg)
        {
            return registerDao.Insert(insertArg);
        }
    }
}