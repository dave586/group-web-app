using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnapFramework.EFModel;

namespace Questionnaire.Models
{
    public class SelectCounsellorModel
    {
        public int ClientID { get; set; }
        public List<SelectIntakeDetails> Intakes { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }

        public int ClientBD_Day { get; set; }
        public int ClientBD_Month { get; set; }
        public int ClientBD_Year { get; set; }

        public DateTime SessionTimeStamp { get; set; }

        public SelectCounsellorModel()
        {
            Intakes = new List<SelectIntakeDetails>();
        }

        public static SelectCounsellorModel GetSelectCounsellorModel(int clientID, List<IntakeFile> intakes)
        {

            SelectCounsellorModel scm = new SelectCounsellorModel();

            using (CCCSnapEntities dbContext = new CCCSnapEntities())
            {

                scm.ClientID = clientID;

                foreach (IntakeFile i in intakes)
                {
                    SelectIntakeDetails intakeDetails = new SelectIntakeDetails();
                    intakeDetails.IntakeFileID = i.IntakeFileID;

                    StaffMember staff = dbContext.StaffMembers.FirstOrDefault(s => s.StaffID == i.AssignedStaffID);

                    if (staff != null)
                    {
                        intakeDetails.CounsellorName = staff.FirstName + " " + staff.LastName;
                    }
                    else
                    {
                        intakeDetails.CounsellorName = "N/A";
                    }
                    
                    string ct = "";

                    switch (SnapFramework.Signals.ClientValidation.GetCounsellingType(i.IntakeFileID))
                    {
                        case SnapFramework.Signals.ClientValidation.CounsellingType.Family:
                            ct = Resources.Home.Views.Resource.Family;
                            break;
                        case SnapFramework.Signals.ClientValidation.CounsellingType.Couple:
                            ct = Resources.Home.Views.Resource.Couple;
                            break;
                        case SnapFramework.Signals.ClientValidation.CounsellingType.Individual:
                            ct = Resources.Home.Views.Resource.Individual;
                            break;
                        default:
                            ct = "";
                            break;
                    }

                    intakeDetails.CounsellingType = ct; 

                    scm.Intakes.Add(intakeDetails);
                }

            }


            return scm;
        }

    }

    public class SelectIntakeDetails
    {
        public int IntakeFileID { get; set; }
        public string CounsellingType { get; set; }
        public string CounsellorName { get; set; }
    }
}