using Microsoft.EntityFrameworkCore;

namespace BSc_Thesis.Models;

public partial class S7plcSqlContext : DbContext
{
    public S7plcSqlContext()
    {
    }

    public S7plcSqlContext(DbContextOptions<S7plcSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Proce> Proces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Server=127.0.0.1,1433;database=S7PLC_SQL;Persist Security Info=True;User ID=sa;password=admin!; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proce>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}