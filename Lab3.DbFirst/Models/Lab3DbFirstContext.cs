using Microsoft.EntityFrameworkCore;

namespace Lab3.DbFirst.Models;

public partial class Lab3DbFirstContext : DbContext
{
    public Lab3DbFirstContext()
    {
    }

    public Lab3DbFirstContext(DbContextOptions<Lab3DbFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; } = null!;
    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-FD3HEDI;Initial Catalog=Lab3DbFirst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Group)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Events_Groups");

            entity.HasOne(d => d.Professor)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK_Events_Users");

            entity.Navigation(e => e.Professor).AutoInclude();
            entity.Navigation(e => e.Group).AutoInclude();
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Groups");

            entity.HasIndex(e => e.Name, "UQ__Groups__737584F637CA4309")
                .IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Navigation(e => e.Events).AutoInclude();
            entity.Navigation(e => e.Students).AutoInclude();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C98397A9")
                .IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Group)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Groups");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}