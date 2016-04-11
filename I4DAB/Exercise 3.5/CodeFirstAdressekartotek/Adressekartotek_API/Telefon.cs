namespace Adressekartotek_API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Telefon")]
    public partial class Telefon
    {
        public long TelefonID { get; set; }

        public long Nummer { get; set; }

        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        public long PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
