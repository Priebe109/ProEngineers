namespace Adressekartotek_API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adresse")]
    public partial class Adresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adresse()
        {
            People = new HashSet<Person>();
        }

        public long AdresseID { get; set; }

        [Required]
        [StringLength(500)]
        public string Bynavn { get; set; }

        public long Husnummer { get; set; }

        public long Postnummer { get; set; }

        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        [Required]
        [StringLength(500)]
        public string Vejnavn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}
