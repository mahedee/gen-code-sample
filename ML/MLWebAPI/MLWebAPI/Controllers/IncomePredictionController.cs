using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MLWebAPI.Controllers
{
    //Allow *ALL* Cross-Origin Resource Sharing CORS Requests
    [EnableCors("*","*","*")]
    public class IncomePredictionController : ApiController
    {
        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        public static string outMLResultData = "";

    }
}
