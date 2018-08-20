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
                    foreach (GroupPackageActivity gp in dbActivities[0].GroupPackageActivities)
                    {

                       

                        Package p = new Package();
                        p.PackageName = gp.GroupPackageName.PackageName;

                        if (gp.PackageType == 1)
                        {
                            var a = gp.GroupActivityType.ActivityName;
                            var b = gp.GroupActivityType.GroupActivityQuestions.ToList();

                            foreach ( var entry in b) {
                                var c = entry.GroupActivityTexts.ToList();
                                c[0].Text.ToString();



                            }






                        }
                        else {








                        }

                      foreach (GroupPackageActivity gpa in gp.GroupPackageActivities)
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