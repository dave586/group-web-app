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
    
    public partial class GroupActivityTestScore
    {
        public System.Guid ID { get; set; }
        public int CompletedActivityID { get; set; }
        public Nullable<System.Guid> ClientGroupQuestionnaireTestID { get; set; }
        public string TestScoreType { get; set; }
        public Nullable<decimal> CalculatedValue { get; set; }
    
        public virtual GroupCompletedActivity GroupCompletedActivity { get; set; }
    }
}
