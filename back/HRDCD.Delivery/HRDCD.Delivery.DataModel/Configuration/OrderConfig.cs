namespace HRDCD.Delivery.DataModel.Configuration;

using HRDCD.Delivery.DataModel.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig : IEntityTypeConfiguration<OrderEntity>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");

        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.OrderNumber).IsUnique();

        builder
            .HasMany(_ => _.DeliveryEntities)
            .WithOne(_ => _.OrderEntity)
            .HasForeignKey(_ => _.OrderEntityId)
            .HasPrincipalKey(_ => _.Id);

        builder.Property(_ => _.Id).HasColumnName("id");

        builder.Property(_ => _.InsertDate).HasColumnName("ins_date")
            .IsRequired();

        builder.Property(_ => _.UpdateDate).HasColumnName("upd_date")
            .IsRequired();

        builder.Property(_ => _.DeleteDate).HasColumnName("del_date")
            .IsRequired();

        builder.Property(_ => _.IsDeleted).HasColumnName("is_del")
            .IsRequired();

        builder.Property(_ => _.OrderNumber).HasColumnName("order_num")
            .IsRequired();

        builder.Property(_ => _.OrderName).HasColumnName("order_name")
            .IsRequired(false);

        builder.Property(_ => _.OrderDescription).HasColumnName("order_desc")
            .IsRequired(false);
    }
}