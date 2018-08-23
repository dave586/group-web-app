using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupQuestionnaireApp.EFModel;
using GroupQuestionnaireApp.Signals;

namespace WebApplication1.Models
{
    public class ClientTestDisplay
    {
        public Guid ResponseID { get; set; }
        public Guid ClientQuestionnaireTestId { get; set; }
        public int TestID { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Package> Package { get; set; }
        public bool RightToLeftLanguage { get; set; }
        public string ClientDisplayName { get; set; }


        public ClientTestDisplay()
        {
            Questions = new List<Question>().AsEnumerable();
            Package = new List<Package>().AsEnumerable();
        }

    }
}