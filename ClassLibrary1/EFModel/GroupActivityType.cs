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
    
    public partial class GroupActivityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupActivityType()
        {
            this.GroupActivityQuestions = new HashSet<GroupActivityQuestion>();
            this.GroupPackageActivities = new HashSet<GroupPackageActivity>();
        }
    
        public string ActivityTypeID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDisplayName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupActivityQuestion> GroupActivityQuestions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupPackageActivity> GroupPackageActivities { get; set; }
    }
}
