﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupQuestionnaireApp.EFModel;
using GroupQuestionnaireApp.Signals;

namespace WebApplication1.Models
{
    public class ClientProgramDisplay
    {
        public int programID { get; set; }
        public IEnumerable<Package> Activities { get; set; }
        //public IEnumerable<Answer> Answers { get; set; }
        public bool RightToLeftLanguage { get; set; }
        public string ClientDisplayName { get; set; }


        public ClientProgramDisplay()
        {
            Activities = new List<Package>().AsEnumerable();
            //Answers = new List<Answer>().AsEnumerable();
        }

    }
}