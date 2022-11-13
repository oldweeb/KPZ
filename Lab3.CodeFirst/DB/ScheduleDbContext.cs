using Lab3.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.CodeFirst.DB;

public class ScheduleDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Event> Events { get; set; }

    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(ub =>
        {
            ub.HasKey(u => u.Id);

            ub.HasOne(u => u.Group).WithMany(g => g.Students);

            ub.HasIndex(u => u.Email).IsUnique();

            ub.Navigation(u => u.Group).AutoInclude();
        });

        modelBuilder.Entity<Event>(eb =>
        {
            eb.HasKey(e => e.Id);

            eb.HasOne(e => e.Professor).WithMany();

            eb.HasOne(e => e.Group).WithMany();

            eb.Navigation(e => e.Professor).AutoInclude();
            eb.Navigation(e => e.Group).AutoInclude();
        });

        modelBuilder.Entity<Group>(gb =>
        {
            gb.HasKey(g => g.Id);

            gb.HasIndex(g => g.Name).IsUnique();

            gb.Navigation(g => g.Students).AutoInclude();
        });

        base.OnModelCreating(modelBuilder);
    }
}