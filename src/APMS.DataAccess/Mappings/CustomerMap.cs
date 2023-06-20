using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using APMS.Domain.Entities;

namespace APMS.DataAccess.Mappings
{
    public class CustomerMap
    {
        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder)
        {
            //Table
            entityBuilder.ToTable("customers");

            //Primary Key
            entityBuilder.HasKey(t => t.Id);

            #region Properties
            
            entityBuilder.Property(e => e.Name).IsRequired().HasMaxLength(1000);
            entityBuilder.Property(e => e.Phone).IsRequired(false).HasMaxLength(250);
            entityBuilder.Property(e => e.Address).IsRequired(false).HasMaxLength(300);
            entityBuilder.Property(e => e.ZipCode).IsRequired(false).HasMaxLength(50);
            entityBuilder.Property(e => e.ContactName).IsRequired(false).HasMaxLength(500);
            entityBuilder.Property(e => e.Description).IsRequired(false);
            
            #endregion
            
            #region Relations
            
            entityBuilder.HasOne(c => c.Affiliate)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.IdAffiliate)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            
            #endregion

            #region Indexes
            
            entityBuilder.HasIndex(e => new { e.IdAffiliate, e.Name });
            
            #endregion
        }
    }
}