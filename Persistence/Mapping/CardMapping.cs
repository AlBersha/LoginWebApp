using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping
{
    public class CardMapping: IEntityTypeConfiguration<CardModel>
    {
        private const string TableName = "CardsData";
        
        public void Configure(EntityTypeBuilder<CardModel> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(user => user.UserName);
            builder.Property(e => e.CardNumber);
            builder.Property(e => e.Nonce);
        }
    }
}