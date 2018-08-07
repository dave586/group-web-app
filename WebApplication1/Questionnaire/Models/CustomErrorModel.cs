using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class CustomErrorModel
    {
        public int HttpStatusCode { get; set; }

        public Exception Exception { get; set; }
    }
}