//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupActivityAnswer
    {
        public System.Guid ID { get; set; }
        public int CompletedActivityID { get; set; }
        public Nullable<System.Guid> GroupActivityAnswerID { get; set; }
        public Nullable<int> QuestionValue { get; set; }
        public string QuestionID { get; set; }
    
        public virtual GroupCompletedActivity GroupCompletedActivity { get; set; }
    }
}
