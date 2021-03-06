using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping
{
    public class UserMapping: IEntityTypeConfiguration<UserModel>
    {
        
        private const string TableName = "UsersData";
        
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(user => user.UserName);
            builder.Property(e => e.Email);
            builder.Property(e => e.Password);
            builder.Property(e => e.Salt);
        }
    }
}