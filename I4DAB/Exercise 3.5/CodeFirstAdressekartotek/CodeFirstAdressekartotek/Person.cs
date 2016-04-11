namespace CodeFirstAdressekartotek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Telefons = new HashSet<Telefon>();
        }

        public long PersonID { get; set; }

        [Required]
        [StringLength(500)]
        public string Efternavn { get; set; }

        [Required]
        [StringLength(500)]
        public string Fornavn { get; set; }

        [Required]
        [StringLength(500)]
        public string Mellemnavn { get; set; }

        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        public long AdresseID { get; set; }

        public virtual Adresse Adresse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Telefon> Telefons { get; set; }
    }
}
