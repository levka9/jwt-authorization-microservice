using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Models.Responses
{
    public class BaseHttpResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Object { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
