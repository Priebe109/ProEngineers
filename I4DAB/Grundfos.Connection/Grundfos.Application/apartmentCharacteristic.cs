namespace Grundfos.Application
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apartmentCharacteristic")]
    public partial class apartmentCharacteristic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public apartmentCharacteristic()
        {
            readings = new HashSet<reading>();
        }

        [Key]
        public long apartmentId { get; set; }

        public long floor { get; set; }

        public long roomNumber { get; set; }

        public float roomSize { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reading> readings { get; set; }
    }
}
