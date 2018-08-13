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
        public int ClientID { get; set; }
        [Required]
        public int IntakeFileID { get; set; }

        public string[] SelectedProgram { get; set; }
    }
}
