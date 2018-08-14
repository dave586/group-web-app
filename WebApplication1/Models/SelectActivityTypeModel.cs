using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GroupQuestionnaireApp.EFModel;

namespace WebApplication1.Models
{
    public class SelectActivityTypeModel
    {
        public List<GroupActivityType> Activity { get; set; }
        public SelectActivityTypeModel()
        {
            Activity = new List<GroupActivityType>();
        }

        public static SelectActivityTypeModel GenerateSelectTestModel()
        {
            SelectActivityTypeModel satm = new SelectActivityTypeModel();

            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                //All activities in all group programs/workshops
                var oqTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "OQ");
                var uricaTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "URICA-DV");
                var pyoqTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PYOQ");
                var yoqTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "YOQ");
                var gcsTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "GCS");
                var icsTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "ICS");
                var sdtTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SDT");
                var rseTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "RSE");
                var pasTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PAS");
                var tsclTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "TSCL-40");
                var preRSFVP = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre RS-FVP");
                var famIII = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "FAM-III");
                var pasph = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PASPH");
                var pasnp = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PASNP");
                var npaps = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "NPAPS");
                var paps = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PAPS");
                var ses = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SES");
                var ise = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "ISE");
                var edi3 = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "EDI-3");
                var csq8 = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "CSQ-8");
                var pfRCM = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-RCM");
                var dair = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "DAI-R");
                var cdi2 = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "CDI-2");
                var sipa = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SIPA");
                var psi = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PSI");
                var marFeed = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Marriage Feedback");
                var rcForm = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "RC");
                var preDemoFVP = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre Demo-FVP");
                var pes = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PES");
                var sea = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SEA Exit");
                var preWorkDemo = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre Workshop Demo");
                var demoOED = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Demo-OED");
                var pfOED = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-OED");
                var demoBEBLDWAS = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Demo-BE/BLD/WAS");
                var pfBE = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-BE");
                var postDemoFVP = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Post Demo-FVP");
                var pfTFTB = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-TFTB");
                var postRSFVP = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Post RS-FVP");
                var pfYNA = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-YNA");
                var pfBLD = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-BLD");
                var pfWAS = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-WAS");
                var pfRCW = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-RCW");
                var codExit = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "COD Exit");

                var activity = new List<GroupActivityType>();

                GroupActivityType oq = new GroupActivityType()
                {
                    ActivityTypeID = oqTest.ActivityTypeID,
                    ActivityDisplayName = oqTest.ActivityDisplayName,
                    ActivityName = oqTest.ActivityName
                };
                activity.Add(oq);

                satm.Activity = activity;
            }

            return satm;
        }
    }
}
