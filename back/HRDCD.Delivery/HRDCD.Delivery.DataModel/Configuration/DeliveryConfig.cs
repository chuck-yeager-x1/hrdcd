namespace HRDCD.Delivery.DataModel.Configuration;

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeliveryConfig : IEntityTypeConfiguration<DeliveryEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryEntity> builder)
    {
        builder.ToTable("delivery");
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(_ => _.OrderEntityId).IsUnique();
        
        builder.Property(_ => _.Id).HasColumnName("id");
        
        builder.Property(_ => _.OrderEntityId).HasColumnName("order_entity_id")
            .IsRequired();

        builder.Property(_ => _.DeliveryStatus).HasColumnName("delivery_status")
            .IsRequired();
        
        builder.Property(_ => _.InsertDate).HasColumnName("ins_date")
            .IsRequired();
        
        builder.Property(_ => _.UpdateDate).HasColumnName("upd_date")
            .IsRequired();
        
        builder.Property(_ => _.DeleteDate).HasColumnName("del_date")
            .IsRequired();
        
        builder.Property(_ => _.IsDeleted).HasColumnName("is_del")
            .IsRequired();
    }
}