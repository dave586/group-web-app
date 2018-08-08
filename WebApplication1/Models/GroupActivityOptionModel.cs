using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GroupActivityOptionModel
    {
        public string QuestionnaireType { get; set; }

        public string QuestionID { get; set; }

        public int DisplayOrder { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }        
    }
}
