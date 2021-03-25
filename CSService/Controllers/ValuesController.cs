using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<string> Get()
        {
            List<string> hellList = new List<string>();
            hellList.Add("hello");
            hellList.Add("pozhao");
            return hellList;
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
