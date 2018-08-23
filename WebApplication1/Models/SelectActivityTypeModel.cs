using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                var tscl40Test = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "TSCL-40");
                var preRSFVPTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre RS-FVP");
                var famIIITest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "FAM-III");
                var pasphTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PASPH");
                var pasnpTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PASNP");
                var npapsTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "NPAPS");
                var papsTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PAPS");
                var sesTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SES");
                var iseTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "ISE");
                var edi3Test = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "EDI-3");
                var csq8Test = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "CSQ-8");
                var pfRCMTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-RCM");
                var dairTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "DAI-R");
                var cdi2Test = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "CDI-2");
                var sipaTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SIPA");
                var psiTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PSI");
                var marFeedTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Marriage Feedback");
                var rcFormTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "RC");
                var preDemoFVPTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre Demo-FVP");
                var pesTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PES");
                var seaTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "SEA Exit");
                var preWorkDemoTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Pre Workshop Demo");
                var demoOEDTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Demo-OED");
                var pfOEDTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-OED");
                var demoBEBLDWASTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Demo-BE/BLD/WAS");
                var pfBETest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-BE");
                var postDemoFVPTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Post Demo-FVP");
                var pfTFTBTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-TFTB");
                var postRSFVPTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "Post RS-FVP");
                var pfYNATest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-YNA");
                var pfBLDTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-BLD");
                var pfWASTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-WAS");
                var pfRCWTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "PF-RCW");
                var codExitTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "COD Exit");

                var activity = new List<GroupActivityType>();

                GroupActivityType oq = new GroupActivityType()
                {
                    ActivityTypeID = oqTest.ActivityTypeID,
                    ActivityDisplayName = oqTest.ActivityDisplayName,
                    ActivityName = oqTest.ActivityName
                };

                activity.Add(oq);

				GroupActivityType urica = new GroupActivityType()
				{
					ActivityTypeID = uricaTest.ActivityTypeID,
					ActivityDisplayName = uricaTest.ActivityDisplayName,
					ActivityName = uricaTest.ActivityName
				};

				activity.Add(urica);

				GroupActivityType pyoq = new GroupActivityType()
				{
					ActivityTypeID = pyoqTest.ActivityTypeID,
					ActivityDisplayName = pyoqTest.ActivityDisplayName,
					ActivityName = pyoqTest.ActivityName
				};

				activity.Add(pyoq);

				GroupActivityType yoq = new GroupActivityType()
				{
					ActivityTypeID = yoqTest.ActivityTypeID,
					ActivityDisplayName = yoqTest.ActivityDisplayName,
					ActivityName = yoqTest.ActivityName
				};

				activity.Add(yoq);

				GroupActivityType gcs = new GroupActivityType()
				{
					ActivityTypeID = gcsTest.ActivityTypeID,
					ActivityDisplayName = gcsTest.ActivityDisplayName,
					ActivityName = gcsTest.ActivityName
				};

				activity.Add(gcs);

				GroupActivityType ics = new GroupActivityType()
				{
					ActivityTypeID = icsTest.ActivityTypeID,
					ActivityDisplayName = icsTest.ActivityDisplayName,
					ActivityName = icsTest.ActivityName
				};

				activity.Add(ics);

				GroupActivityType sdt = new GroupActivityType()
				{
					ActivityTypeID = sdtTest.ActivityTypeID,
					ActivityDisplayName = sdtTest.ActivityDisplayName,
					ActivityName = sdtTest.ActivityName
				};

				activity.Add(sdt);

				GroupActivityType rse = new GroupActivityType()
				{
					ActivityTypeID = rseTest.ActivityTypeID,
					ActivityDisplayName = rseTest.ActivityDisplayName,
					ActivityName = rseTest.ActivityName
				};

				activity.Add(rse);

				GroupActivityType pas = new GroupActivityType()
				{
					ActivityTypeID = pasTest.ActivityTypeID,
					ActivityDisplayName = pasTest.ActivityDisplayName,
					ActivityName = pasTest.ActivityName
				};

				activity.Add(pas);

				GroupActivityType tscl40 = new GroupActivityType()
				{
					ActivityTypeID = tscl40Test.ActivityTypeID,
					ActivityDisplayName = tscl40Test.ActivityDisplayName,
					ActivityName = tscl40Test.ActivityName
				};

				activity.Add(tscl40);

				GroupActivityType preRSFVP = new GroupActivityType()
				{
					ActivityTypeID = preRSFVPTest.ActivityTypeID,
					ActivityDisplayName = preRSFVPTest.ActivityDisplayName,
					ActivityName = preRSFVPTest.ActivityName
				};

				activity.Add(preRSFVP);

				GroupActivityType famIII = new GroupActivityType()
				{
					ActivityTypeID = famIIITest.ActivityTypeID,
					ActivityDisplayName = famIIITest.ActivityDisplayName,
					ActivityName = famIIITest.ActivityName
				};

				activity.Add(famIII);

				GroupActivityType pasph = new GroupActivityType()
				{
					ActivityTypeID = pasphTest.ActivityTypeID,
					ActivityDisplayName = pasphTest.ActivityDisplayName,
					ActivityName = pasphTest.ActivityName
				};

				activity.Add(pasph);

                satm.Activity = activity;
            }

            return satm;
        }
    }
}
