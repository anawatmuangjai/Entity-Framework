//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdmxApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ninja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ninja()
        {
            this.NinjaEquipments = new HashSet<NinjaEquipment>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public int ClanId { get; set; }
        public System.DateTime DateOfBrith { get; set; }
    
        public virtual Clan Clan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NinjaEquipment> NinjaEquipments { get; set; }
    }
}
