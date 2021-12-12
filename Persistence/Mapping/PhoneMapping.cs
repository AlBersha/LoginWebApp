using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping
{
    public class PhoneMapping: IEntityTypeConfiguration<PhoneModel>
    {
        private const string TableName = "PhonesData";
        
        public void Configure(EntityTypeBuilder<PhoneModel> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(user => user.UserName);
            builder.Property(e => e.Phone);
            builder.Property(e => e.Nonce);
        }
    }
}