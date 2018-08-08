using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GroupActivityQuestionModel
    {
        public string QuestionnaireType { get; set; }

        public string QuestionID { get; set; }

        public int DisplayOrder { get; set; }

        public string QuestionType { get; set; }        
    }
}
