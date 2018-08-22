using System;
using System.Collections.Generic;


namespace GroupQuestionnaireApp.Signals
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string QuestionID { get; set; }
        public string QuestionNumber { get; set; }
        public List<Option> Options { get; set; }
        public string Answer { get; set; }
        public string QuestionType { get; set; }

        public Question()
        {
            Options = new List<Option>();
        }
    }

    public class Answer
    {
        public string QuestionID { get; set; }
        public string Value { get; set; }
    }

    public class Option
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}