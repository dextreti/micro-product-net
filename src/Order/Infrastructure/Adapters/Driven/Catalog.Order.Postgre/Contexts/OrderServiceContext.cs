using System;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Postgresql.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Order.Postgresql.Contexts;

//# Paquete principal de EF Core
//  dotnet add package Microsoft.EntityFrameworkCore

//# Driver para Postgres
//  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

// Herramientas para migraciones
// dotnet add package Microsoft.EntityFrameworkCore.Design

public class OrderServiceContext : DbContext, IUnitOfWork
{    
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    

    public OrderServiceContext(DbContextOptions<OrderServiceContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderServiceContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


}
