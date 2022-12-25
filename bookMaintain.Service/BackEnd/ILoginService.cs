using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service
{
    public interface ILoginService
    {
        int Insert(Login insertArg);

        int ForgetPassword(Login forgetPasswordArg);
    }
}