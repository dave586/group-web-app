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
    
    public partial class GroupPackageName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupPackageName()
        {
            this.GroupPackageActivities = new HashSet<GroupPackageActivity>();
        }
    
        public int PackageTypeID { get; set; }
        public string PackageName { get; set; }
        public string PackageDisplayName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupPackageActivity> GroupPackageActivities { get; set; }
    }
}
