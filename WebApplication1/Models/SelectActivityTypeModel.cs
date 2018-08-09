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
                var oqTest = dbContext.GroupActivityTypes.FirstOrDefault(t => t.ActivityName == "OQ");

            }
            return satm;
        }
    }
}
