using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GroupPackageActivityModel
    {
        public int GroupPackageActID { get; set; }

        public int PackageType { get; set; }

        public int ProgramID { get; set; }

        public int ActivityDisplayOrder { get; set; }

        public string QuestionnaireType { get; set; }
    }
}
