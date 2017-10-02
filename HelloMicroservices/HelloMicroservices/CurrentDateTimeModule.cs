using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Nancy;

namespace HelloMicroservices
{
    public class CurrentDateTimeModule : NancyModule
    {
        public CurrentDateTimeModule()
        {
            Get("/", _ => DateTime.UtcNow);
        }
    }
}
