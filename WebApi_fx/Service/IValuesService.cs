using System.Collections.Generic;

namespace WebApi_fx.Service
{
    public interface IValuesService
    {
        IEnumerable<string> GetValues();
    }
}