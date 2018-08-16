using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ClientSelectedProgram
    {
        [Required]
        public int ProgramID { get; set; }
        [Required]
        public int ClientID { get; set; }
        public int PackageType { get; set; }
        public int QuestionnaireType { get; set; }

        public string[] Questionnaires { get; set; }
    }
}
