using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GroupQuestionnaireApp.EFModel;

namespace GroupQuestionnaireApp.Signals
{
    public static class QuestionnaireRepository
    {
        public static List<Question> GetQuestionnaireQuestions(string questionnaireType, string language)
        {
            List<Question> questions = new List<Question>();

            try
            {
                using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
                {
                    var dbQuestions = dbContext.GroupActivityQuestions.Include("GroupActivityOptions").Include("GroupActivityTexts").Where(
                        q => q.QuestionnaireType == questionnaireType
                        ).OrderBy(q => q.DisplayOrder).ToList();

                    foreach (GroupActivityQuestion dbQ in dbQuestions)
                    {
                        Question q = new Question();
                        q.QuestionID = dbQ.QuestionID;
                        q.QuestionType = dbQ.QuestionType;
                        var qt = dbQ.GroupActivityTexts.FirstOrDefault(t => t.Language == language);
                        if (qt != null)
                        {
                            q.QuestionText = qt.Text;
                            q.QuestionNumber = qt.QuestionNumber;
                        }
                        foreach (GroupActivityOption o in dbQ.GroupActivityOptions.OrderBy(o => o.DisplayOrder))
                        {
                            q.Options.Add(new Option { Value = o.Value });
                        }

                        questions.Add(q);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return questions;
        }
    }
}