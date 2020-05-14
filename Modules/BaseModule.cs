using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthentication.Modules
{
    public class BaseModule
    {
        protected ILogger logger;

        public BaseModule(ILogger Logger)
        {
            logger = Logger;
        }
    }
}
