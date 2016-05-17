namespace Grundfos.Application
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("reading")]
    public partial class reading
    {
        public long readingValue { get; set; }

        public DateTime readingTimestamp { get; set; }

        public long readingId { get; set; }

        public long apartmentId { get; set; }

        public long sensorId { get; set; }

        public virtual apartmentCharacteristic apartmentCharacteristic { get; set; }

        public virtual sensorCharacteristic sensorCharacteristic { get; set; }
    }
}
