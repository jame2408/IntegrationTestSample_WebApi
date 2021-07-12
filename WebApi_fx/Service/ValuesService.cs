using System.Collections.Generic;

namespace WebApi_fx.Service
{
    public class ValuesService : IValuesService
    {
        public IEnumerable<string> GetValues()
        {
            return new[] { "value1", "value2" };
        }
    }
}