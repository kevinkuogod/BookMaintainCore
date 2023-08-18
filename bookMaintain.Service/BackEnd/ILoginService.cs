using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service
{
    public interface ILoginService
    {
        dynamic Insert(Login insertArg);

        int ForgetPassword(Login forgetPasswordArg);
    }
}