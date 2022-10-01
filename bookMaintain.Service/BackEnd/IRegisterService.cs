using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Service
{
    public interface IRegisterService
    {
        int Insert(Register insertArg);
    }
}