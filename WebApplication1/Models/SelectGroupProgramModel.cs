using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupQuestionnaireApp.EFModel;

namespace WebApplication1.Models
{
    public class SelectGroupProgramModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string ProgramName { get; set; }
        [Required]
        public string ProgramDisplayName { get; set; }
        public int QuestionnaireType { get; set; }
        public List<GroupProgram> Programs { get; set; }

        public SelectGroupProgramModel()
        {
            Programs = new List<GroupProgram>();
        }

        public static SelectGroupProgramModel GenerateSelectGroupProgramModel(int programID, string programName)
        {
            SelectGroupProgramModel gp = new SelectGroupProgramModel { ID = programID , ProgramName = programName };

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
                    SelectorID = wasProgram.ID,
                    ProgramDisplayName = wasProgram.ProgramDisplayName,
                    ProgramName = wasProgram.ProgramName,
                    StartDate = wasProgram.StartDate,
                    EndDate = wasProgram.EndDate,
                    ProgramLength = wasProgram.ProgramLength,
                    ProgramID = wasProgram.ProgramID
                };

                programs.Add(was);

                GroupProgram oed = new GroupProgram()
                {
                    SelectorID = oedProgram.ID,
                    ProgramDisplayName = oedProgram.ProgramDisplayName,
                    StartDate = oedProgram.StartDate,
                    EndDate = oedProgram.EndDate,
                    ProgramLength = oedProgram.ProgramLength,
                    ProgramID = oedProgram.ProgramID
                };

                programs.Add(oed);

                GroupProgram be = new GroupProgram()
                {
                    SelectorID = beProgram.ID,
                    ProgramDisplayName = beProgram.ProgramDisplayName,
                    StartDate = beProgram.StartDate,
                    EndDate = beProgram.EndDate,
                    ProgramLength = beProgram.ProgramLength,
                    ProgramID = beProgram.ProgramID
                };

                programs.Add(be);

                GroupProgram bld = new GroupProgram()
                {
                    SelectorID = bldProgram.ID,
                    ProgramDisplayName = bldProgram.ProgramDisplayName,
                    StartDate = bldProgram.StartDate,
                    EndDate = bldProgram.EndDate,
                    ProgramLength = bldProgram.ProgramLength,
                    ProgramID = bldProgram.ProgramID
                };

                programs.Add(bld);

                GroupProgram yna = new GroupProgram()
                {
                    SelectorID = ynaProgram.ID,
                    ProgramDisplayName = ynaProgram.ProgramDisplayName,
                    StartDate = ynaProgram.StartDate,
                    EndDate = ynaProgram.EndDate,
                    ProgramLength = ynaProgram.ProgramLength,
                    ProgramID = ynaProgram.ProgramID
                };

                programs.Add(yna);

                GroupProgram tftb = new GroupProgram()
                {
                    SelectorID = tftbProgram.ID,
                    ProgramDisplayName = tftbProgram.ProgramDisplayName,
                    StartDate = tftbProgram.StartDate,
                    EndDate = tftbProgram.EndDate,
                    ProgramLength = tftbProgram.ProgramLength,
                    ProgramID = tftbProgram.ProgramID
                };

                programs.Add(tftb);

                GroupProgram rcm = new GroupProgram()
                {
                    SelectorID = rcmProgram.ID,
                    ProgramDisplayName = rcmProgram.ProgramDisplayName,
                    StartDate = rcmProgram.StartDate,
                    EndDate = rcmProgram.EndDate,
                    ProgramLength = rcmProgram.ProgramLength,
                    ProgramID = rcmProgram.ProgramID
                };

                programs.Add(rcm);

                GroupProgram rcw = new GroupProgram()
                {
                    SelectorID = rcwProgram.ID,
                    ProgramDisplayName = rcwProgram.ProgramDisplayName,
                    StartDate = rcwProgram.StartDate,
                    EndDate = rcwProgram.EndDate,
                    ProgramLength = rcwProgram.ProgramLength,
                    ProgramID = rcwProgram.ProgramID
                };

                programs.Add(rcw);

                gp.Programs = programs;
            }
            return gp;
        }
    }
}