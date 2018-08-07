using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.Models
{
    public class ClientSelectedTests
    {
        [Required]
        public int ClientID { get; set; }

        [Required]
        public int IntakeFileID { get; set; }

        public string[] SelectedTests { get; set; }

       // public string SRSTest { get; set; }

        public bool IsPreSession { get; set; }
    }
}