using bookMaintain.Model.BackEnd.Arg.Select;
using System.Collections.Generic;

namespace bookMaintain.Service
{
    public interface ISelectService
    {
        List<SelectListValue> GetBookClass();
        List<SelectListValue> GetBookCodeStatus(string status);
        List<SelectListValue> GetBookKeeper(string type);
    }
}