using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupQuestionnaireApp.EFModel
{
    public partial class GroupProgram
    {
        public int SelectorID { get; set; }

        public virtual GroupProgram Program { get; set; }
    }
}
