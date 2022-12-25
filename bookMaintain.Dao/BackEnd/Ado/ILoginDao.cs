using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface ILoginDao
    {
        int Insert(Login insertArg);

        int ForgetPassword(Login forgetPasswordArg);
    }
}