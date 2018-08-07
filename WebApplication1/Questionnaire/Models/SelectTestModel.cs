using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SnapFramework.EFModel;
using SnapFramework.Signals;

namespace Questionnaire.Models
{
    public class SelectTestModel
    {
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int IntakeFileID { get; set; }
        public List<QuestionnaireType> Tests { get; set; }

        [Required]
        public bool IsPreSession { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }

        public int ClientBD_Day { get; set; }
        public int ClientBD_Month { get; set; }
        public int ClientBD_Year { get; set; }

        public DateTime SessionTimeStamp { get; set; }

        public SelectTestModel()
        {
            Tests = new List<QuestionnaireType>();
        }

        public static SelectTestModel GenerateSelectTestModel(int clientID, int intakeFileID, bool IsPreSession)
        {
            SelectTestModel stm = new SelectTestModel {ClientID = clientID, IntakeFileID = intakeFileID};

            using (CCCSnapEntities dbContext = new CCCSnapEntities())
            {
                if (IsPreSession)
                {
                    //List<QuestionnaireType> tests = dbContext.QuestionnaireTypes.Where(t => t.QuestionnaireName != "SRS").ToList(); //filter out POST SESSION tests

                    //if (SnapFramework.Signals.ClientValidation.GetCounsellingType(intakeFileID) == SnapFramework.Signals.ClientValidation.CounsellingType.Couple
                    //    || SnapFramework.Signals.ClientValidation.GetCounsellingType(intakeFileID) == SnapFramework.Signals.ClientValidation.CounsellingType.Individual)
                    //{
                    //    tests = tests.Where(t => t.QuestionnaireName == "OQ" || t.QuestionnaireName == "URICA").ToList();
                    //}

                    var oqTest = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "OQ");
                    var uricaTest = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "URICA");
                    var yoqSRTest = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "YOQ-S");
                    var yoqPTest = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "YOQ-P");

                    var tests = new List<QuestionnaireType>();

                    ClientValidation.CounsellingType ctype = ClientValidation.GetCounsellingType(intakeFileID);
                    bool isFirstSession = TransactionRepository.IsFirstSession(clientID, intakeFileID);

                    if (ctype == ClientValidation.CounsellingType.Individual || ctype == ClientValidation.CounsellingType.Couple)
                    {
                        //tests.Add(dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "OQ"));
                        QuestionnaireType oq = new QuestionnaireType()
                        {
                            SelectorId = oqTest.ID,
                            QuestionnaireDisplayName = oqTest.QuestionnaireDisplayName,
                            QuestionnaireName = oqTest.QuestionnaireName
                            
                        };
                        tests.Add(oq);
                        if (isFirstSession)
                        {
                            //tests.Add(dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "URICA"));
                            QuestionnaireType urica = new QuestionnaireType()
                            {
                                SelectorId = uricaTest.ID,
                                QuestionnaireDisplayName = uricaTest.QuestionnaireDisplayName,
                                QuestionnaireName = uricaTest.QuestionnaireName
                            };

                            tests.Add(urica);
                        }
                    }
                    else if (ctype == ClientValidation.CounsellingType.Family)
                    {
                        //tests.AddRange(dbContext.QuestionnaireTypes.Where(t => t.QuestionnaireName == "OQ" || t.QuestionnaireName == "YOQ-P" || t.QuestionnaireName == "YOQ-S" || t.QuestionnaireName == "ACE"));
                        //if (isFirstSession)
                        //{
                        //    tests.Add(dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "URICA"));
                        //}

                        // get the intake client roles
                        IntakeFile inf = dbContext.IntakeFiles.Include("IntakeClientRoles").Where(i => i.IntakeFileID == intakeFileID).FirstOrDefault();
                        // if user is parent, show child for each
                        IntakeClientRole thisClient = inf.IntakeClientRoles.FirstOrDefault(i => i.ClientID == clientID);
                        // if user is child, do not show OQ, YOQ-P
                        if (thisClient.RoleID == (int)SnapFramework.Entities.IntakeClientRole.ClientRole.Child || (thisClient.IsChild.HasValue && thisClient.IsChild.Value))
                        {
                            //tests.AddRange(dbContext.QuestionnaireTypes.Where(t => t.QuestionnaireName == "YOQ-S"));
                            QuestionnaireType yoqsr = new QuestionnaireType()
                            {
                                SelectorId = yoqSRTest.ID,
                                QuestionnaireDisplayName = yoqSRTest.QuestionnaireDisplayName,
                                QuestionnaireName = yoqSRTest.QuestionnaireName
                            };

                            tests.Add(yoqsr);

                            if (isFirstSession)
                            {
                                //tests.Add(dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "URICA"));
                                QuestionnaireType urica = new QuestionnaireType()
                                {
                                    SelectorId = uricaTest.ID,
                                    QuestionnaireDisplayName = uricaTest.QuestionnaireDisplayName,
                                    QuestionnaireName = uricaTest.QuestionnaireName
                                };

                                tests.Add(urica);
                            }
                        }
                        else
                        {
                            //tests.AddRange(dbContext.QuestionnaireTypes.Where(t => t.QuestionnaireName == "OQ"));
                            QuestionnaireType oq = new QuestionnaireType()
                            {
                                SelectorId = oqTest.ID,
                                QuestionnaireDisplayName = oqTest.QuestionnaireDisplayName,
                                QuestionnaireName = oqTest.QuestionnaireName
                            };
                            tests.Add(oq);
                            if (isFirstSession)
                            {
                                // tests.Add(dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "URICA"));
                                QuestionnaireType urica = new QuestionnaireType()
                                {
                                    SelectorId = uricaTest.ID,
                                    QuestionnaireDisplayName = uricaTest.QuestionnaireDisplayName,
                                    QuestionnaireName = uricaTest.QuestionnaireName
                                };

                                tests.Add(urica);
                            }
                            // ds - 2017-06-02 - fix child role check
                            if(inf.IntakeClientRoles.Count(icr => icr.RoleID == (int)SnapFramework.Entities.IntakeClientRole.ClientRole.Child || icr.IsChild.GetValueOrDefault(false) == true) > 0)
                            {
                                // add a test per child
                                foreach(IntakeClientRole child in inf.IntakeClientRoles.Where(icr => icr.RoleID == (int)SnapFramework.Entities.IntakeClientRole.ClientRole.Child || icr.IsChild.GetValueOrDefault(false) == true))
                                {
                                    //QuestionnaireType yoqP = (dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "YOQ-P"));
                                    QuestionnaireType yoqP = new QuestionnaireType();
                                    yoqP.SelectorId = child.ClientID;

                                    Client cl = dbContext.Clients.FirstOrDefault(c => c.ClientID == child.ClientID);
                                    if (cl != null)
                                    {
                                        yoqP.QuestionnaireDisplayName = "Parent of " + cl.FirstName + " " + cl.LastName;
                                    }
                                   
                                    yoqP.QuestionnaireName = yoqPTest.QuestionnaireName;
                                    tests.Add(yoqP);
                                }
                            }
                        }

                    }
                    
                    stm.Tests = tests;
                    stm.IsPreSession = true;
                }
                else
                {
                    List<QuestionnaireType> tests = new List<QuestionnaireType>();// dbContext.QuestionnaireTypes.Where(t => t.QuestionnaireName == "SRS").ToList();//filter out PRE SESSION tests

                    var srsTest = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "SRS");

                    QuestionnaireType srs = new QuestionnaireType()
                    {
                        SelectorId = srsTest.ID,
                        QuestionnaireDisplayName = srsTest.QuestionnaireDisplayName,
                        QuestionnaireName = srsTest.QuestionnaireName
                    };


                    tests.Add(srs);
                    //if (SnapFramework.Signals.ClientValidation.GetCounsellingType(intakeFileID) == SnapFramework.Signals.ClientValidation.CounsellingType.Couple
                    //    || SnapFramework.Signals.ClientValidation.GetCounsellingType(intakeFileID) == SnapFramework.Signals.ClientValidation.CounsellingType.Individual)
                    //{
                    //    tests = tests.Where(t => t.QuestionnaireName == "OQ" || t.QuestionnaireName == "URICA" || t.QuestionnaireName == "SRS").ToList();
                    //}

                    stm.Tests = tests;
                    stm.IsPreSession = false;
                }
            }

            return stm;
        }

        
    }
}