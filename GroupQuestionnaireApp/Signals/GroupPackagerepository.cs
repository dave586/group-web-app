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
                    var dbActivities = dbContext.GroupPrograms.Include("GroupPackageActivities").Include("GroupPackageNames").Include("GroupActivityTypes").Where(
                        a => a.ID == programID);
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