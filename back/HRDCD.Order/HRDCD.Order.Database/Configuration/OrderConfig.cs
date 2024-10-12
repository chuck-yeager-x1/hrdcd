namespace HRDCD.Order.Database.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Класс для конфигурации сущности "Заказ" (<see cref="Order"/>).
/// </summary>
public class OrderConfig : IEntityTypeConfiguration<DataModel.Entity.Order>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DataModel.Entity.Order> builder)
    {
        builder.ToTable("order");

        builder.Property(_ => _.Id).HasColumnName("id");
        builder.Property(_ => _.InsertDate).HasColumnName("ins_date");
        builder.Property(_ => _.UpdateDate).HasColumnName("upd_date");
        builder.Property(_ => _.DeleteDate).HasColumnName("del_date");
        builder.Property(_ => _.IsDeleted).HasColumnName("is_del");
        builder.Property(_ => _.OrderNumber).HasColumnName("order_num");
        builder.Property(_ => _.OrderName).HasColumnName("order_name");
        builder.Property(_ => _.OrderDescription).HasColumnName("order_desc");
    }
}