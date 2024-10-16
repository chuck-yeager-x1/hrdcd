namespace HRDCD.Order.DataModel.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Класс для конфигурации сущности <see cref="Order"/>.
/// </summary>
public class OrderConfig : IEntityTypeConfiguration<Entity.OrderEntity>
{
    public void Configure(EntityTypeBuilder<Entity.OrderEntity> builder)
    {
        builder.ToTable("order");
        
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.OrderNumber).IsUnique();
        
        builder.Property(_ => _.Id).HasColumnName("id");
        
        builder.Property(_ => _.InsertDate).HasColumnName("ins_date")
            .IsRequired();
        
        builder.Property(_ => _.UpdateDate).HasColumnName("upd_date")
            .IsRequired();
        
        builder.Property(_ => _.DeleteDate).HasColumnName("del_date")
            .IsRequired();
        
        builder.Property(_ => _.IsDeleted).HasColumnName("is_del")
            .HasDefaultValue(null)
            .IsRequired();
        
        builder.Property(_ => _.OrderNumber).HasColumnName("order_num")
            .IsRequired();
        
        builder.Property(_ => _.IsSent).HasColumnName("is_sent")
            .HasDefaultValue(null)
            .IsRequired();
        
        builder.Property(_ => _.OrderName).HasColumnName("order_name")
            .IsRequired(false);
        
        builder.Property(_ => _.OrderDescription).HasColumnName("order_desc")
            .IsRequired(false);
    }
}