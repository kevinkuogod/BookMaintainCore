using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface ILoginDao
    {
        dynamic Insert(Login insertArg);

        dynamic InsertMysql(Login insertArg);

        int ForgetPassword(Login forgetPasswordArg);
    }
}