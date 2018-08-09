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
        public int ProgramID { get; set; }
        [Required]
        public string ProgramName { get; set; }
        public List<GroupProgram> Programs { get; set; }

        public SelectGroupProgramModel()
        {
            Programs = new List<GroupProgram>();
        }

        public static SelectGroupProgramModel GenerateSelectGroupProgramModel(int programID, string programName)
        {
            SelectGroupProgramModel gp = new SelectGroupProgramModel { ProgramID = programID , ProgramName = programName };

            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                var wasProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "WAS");
                var oedProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "OED");
                var beProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "BE");
                var bldProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "BLD");
                var ynaProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "YNA");
                var tftbProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "TFTB");
                var rcmProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "RCM");
                var rcwProgram = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "RCW");

                var programs = new List<GroupProgram>();

                GroupProgram was = new GroupProgram()
                {
                    ID = wasProgram.ID,
                    ProgramDisplayName = wasProgram.ProgramDisplayName,
                    ProgramName = wasProgram.ProgramName,
                    StartDate = wasProgram.StartDate,
                    EndDate = wasProgram.EndDate,
                    ProgramLength = wasProgram.ProgramLength,
                    ProgramID = wasProgram.ProgramID
                };

                programs.Add(was);

                gp.Programs = programs;
            }
            return gp;
        }
    }
}