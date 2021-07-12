using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_fx.Service;

namespace WebApi_fx.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IValuesService _service;

        public ValuesController(IValuesService service)
        {
            _service = service;
        }
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return _service.GetValues();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
