using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APMS.Domain.Entities;

namespace APMS.DataAccess.Mappings
{
    public class ApiKeyMap
    {
        public ApiKeyMap(EntityTypeBuilder<ApiKey> entityBuilder)
        {
            //Table
            entityBuilder.ToTable("api_keys");

            //Primary Key
            entityBuilder.HasKey(t => t.KeyId);

            #region Properties

            entityBuilder.Property(e => e.KeyId).IsRequired().HasMaxLength(100);
            entityBuilder.Property(e => e.Secret).IsRequired().HasMaxLength(100);

            #endregion
        }
    }
}