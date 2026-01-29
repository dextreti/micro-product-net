using System;
using Catalog.Order.Postgresql.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Order.Postgresql.Configs;

public class PurchaseOrderConfiguration: IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");
        builder.HasKey(x => x.Id);        

        // 1. Mapear el campo privado '_items' (Backing Field)
        builder.HasMany(x => x.PurchaseOrderItems)
            .WithOne(x => x.PurchaseOrder)
            .HasForeignKey(x => x.PurchaseOrderId) // Sombra (shadow property) en la BD
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Indicarle a EF que use el campo privado para la navegación
        var navigation = builder.Metadata.FindNavigation(nameof(PurchaseOrder.PurchaseOrderItems));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        // 3. Otros campos
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2);
        //builder.Property(x => x.Status).HasConversion<string>(); // Guarda el Enum como texto
        
    }


}
