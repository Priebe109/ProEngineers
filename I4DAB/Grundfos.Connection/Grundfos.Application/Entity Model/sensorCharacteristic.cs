namespace Grundfos.Application
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sensorCharacteristic")]
    public partial class sensorCharacteristic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sensorCharacteristic()
        {
            readings = new HashSet<reading>();
        }

        [Key]
        public long sensorId { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }

        [Required]
        [StringLength(500)]
        public string unit { get; set; }

        public DateTime calibrationDate { get; set; }

        [Required]
        [StringLength(500)]
        public string calibrationEquation { get; set; }

        [Required]
        [StringLength(500)]
        public string calibrationCoefficient { get; set; }

        [Required]
        [StringLength(500)]
        public string externalRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reading> readings { get; set; }
    }
}
