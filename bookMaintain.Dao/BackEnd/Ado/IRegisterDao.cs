using bookMaintain.Model.BackEnd.Arg.BookMaintain;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IRegisterDao
    {
        int Insert(Register insertArg);
    }
}