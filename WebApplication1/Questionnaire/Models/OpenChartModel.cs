using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class OpenChartModel
    {
        public Guid ResponseId { get; set; }
        public Boolean IsPreSession { get; set; }
    }
}