using System;
using System.Collections.Generic;


namespace GroupQuestionnaireApp.Signals
{

    public class Package
    {
        public string ProgramName { get; set; }
        public string ActivityName { get; set; }
        public string PackageName { get; set; }
        public List<Activity> Activities { get; set; }

        public Package()
        {
            Activities = new List<Activity>();
        }
    }
    public class Activity
    {
        public string ActivityName { get; set; }
        //public string Value { get; set; }
    }
}