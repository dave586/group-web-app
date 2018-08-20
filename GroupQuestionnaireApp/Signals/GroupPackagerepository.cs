using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupQuestionnaireApp.EFModel;

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
                    foreach (GroupProgram gp in dbActivities)
                    {
                        Package p = new Package();
                        p.ProgramName = gp.ProgramName;

                        gp.GroupPackageActivities.FirstOrDefault(gpa => gpa.PackageType == 1);

                        foreach(GroupPackageActivity gpa in gp.GroupPackageActivities)
                        {
                            
                        }

                        activities.Add(p);
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