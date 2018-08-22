using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupQuestionnaireApp.EFModel;
using GroupQuestionnaireApp.Signals;

namespace GroupQuestionnaireApp.Signals
{
    public static class GroupPackageRepository
    {
        public static List<Package> GetPackageActivities (int programID)
        {
            List<Package> activities = new List<Package>();
            try
            {
                using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
                {
                    var dbActivities = dbContext.GroupPrograms.Include("GroupPackageActivities").Where(g => g.ID == programID).ToList();
                    foreach (GroupPackageActivity gp in dbActivities[0].GroupPackageActivities.OrderBy(g => g.ActivityDisplayOrder))
                    {
						Package p = new Package();

						p.PackageName = gp.GroupPackageName.PackageName;
						p.ProgramName = gp.GroupProgram.ProgramName;
						p.ActivityName = gp.GroupActivityType.ActivityName;
						p.ActivityDisplayName = gp.GroupActivityType.ActivityDisplayName;
						p.ActivityID = gp.QuestionnaireType;

						if (gp.PackageType == 1)
						{
							string language = "en-CA";
							p.Activities = QuestionnaireRepository.GetQuestionnaireQuestions((string)gp.QuestionnaireType, language);
							activities.Add(p);
						}
						//activities.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return activities;
        }
    }
}