using System.Data.Entity.Validation;

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

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
