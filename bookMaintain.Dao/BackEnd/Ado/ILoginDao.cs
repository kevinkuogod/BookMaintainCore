using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface ILoginDao
    {
        dynamic Insert(Login insertArg);

        int ForgetPassword(Login forgetPasswordArg);

        dynamic InsertMysql(Login insertArg);

        int LoginNumber();
    }
}