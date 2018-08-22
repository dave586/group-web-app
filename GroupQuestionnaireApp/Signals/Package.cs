using System;
using System.Collections.Generic;


namespace GroupQuestionnaireApp.Signals
{

    public class Package
    {
        public string ProgramName { get; set; }
		public string ActivityID { get; set; }
		public string ActivityName { get; set; }
        public string PackageName { get; set; }
		public string ActivityDisplayName { get; set; }
        public List<Question> Activities { get; set; }

        public Package()
        {
            Activities = new List<Question>();
        }
    }
    public class Activity
    {
		public string ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDisplayName { get; set; }
	}
}