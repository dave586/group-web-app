//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroupQuestionnaireApp.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupActivityText
    {
        public string QuestionnaireType { get; set; }
        public string QuestionID { get; set; }
        public string Language { get; set; }
        public string QuestionNumber { get; set; }
        public string Text { get; set; }
    
        public virtual GroupActivityQuestion GroupActivityQuestion { get; set; }
    }
}