namespace Grundfos.Application
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GrundfosModel : DbContext
    {
        public GrundfosModel()
            : base("name=GrundfosModel")
        {
        }

        public virtual DbSet<apartmentCharacteristic> apartmentCharacteristics { get; set; }
        public virtual DbSet<reading> readings { get; set; }
        public virtual DbSet<sensorCharacteristic> sensorCharacteristics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
