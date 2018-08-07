using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnapFramework.Signals;

namespace Questionnaire.Models
{
    public static class TestOQ
    {

        public static IEnumerable<Question> GetQuestions(string language)
        {

            var qs = QuestionnaireRepository.GetQuestionnaireQuestions(1, language);

            return qs.AsEnumerable();

        }
    }

    public static class TestUrica
    {
        public static IEnumerable<Question> GetQuestions(string language)
        {
            var qs = QuestionnaireRepository.GetQuestionnaireQuestions(2, language);

            return qs.AsEnumerable();
        }
    }

    public static class TestYOQP
    {
        public static IEnumerable<Question> GetQuestions(string language)
        {
            var qs = QuestionnaireRepository.GetQuestionnaireQuestions(3, language);

            return qs.AsEnumerable();
        }
    }
}