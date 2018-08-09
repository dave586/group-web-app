using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GroupQuestionnaireApp.EFModel;

namespace WebApplication1.Models
{
    public class SelectGroupProgramModel
    {
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int IntakeFileID { get; set; }
        public List<GroupProgram> Program { get; set; }

        public SelectGroupProgramModel()
        {
            Program = new List<GroupProgram>();
        }

        public static SelectGroupProgramModel GenerateSelectGroupProgramModel(int clientID, int intakeFileID)
        {
            SelectGroupProgramModel gp = new SelectGroupProgramModel { ClientID = clientID, IntakeFileID = intakeFileID };

            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                var was  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "WAS");
                var oed  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "OED");
                var be   = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "BE");
                var bld  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "BLD");
                var yna  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "YNA");
                var tftb = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "TFTB");
                var rcm  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "RCM");
                var rcw  = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "RCW");

                var programs = new List<GroupProgram>();
            }
            return gp;
        }
    }
}
