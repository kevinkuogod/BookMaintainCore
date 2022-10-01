using bookMaintain.Model.BackEnd.Arg.Select;
using System.Collections.Generic;

namespace bookMaintain.Dao.Component
{
    public interface ISelectDao
    {
        List<SelectListValue> GetBookClass();
        List<SelectListValue> GetBookCodeStatus(string status);
        List<SelectListValue> GetBookKeeper(string type);
    }
}