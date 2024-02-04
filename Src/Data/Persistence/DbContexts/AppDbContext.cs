﻿using Data.Entities.Category.Aggregate;
using Data.Entities.Ticket.Aggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Persistence.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Ticket> Tickets  => Set<Ticket>();
    public DbSet<Category> Categories  => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
