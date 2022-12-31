using BSc_Thesis.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BSc_Thesis.DataBase;

public partial class S7plcSqlContext : DbContext
{
    public S7plcSqlContext(DbContextOptions<S7plcSqlContext> options) : base(options)
    {
        
    }

    public DbSet<ProcessDb> Proces { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProcessDb>(entity =>
        {
            entity.HasKey(x => x.ID);
            entity.Property(x => x.Date);
            entity.Property(x => x.Temperature);
            entity.Property(x => x.AmbientTemperature);
            entity.Property(x => x.Glucose);
            entity.Property(x => x.Maltose);
            entity.Property(x => x.Maltotriosis);
            entity.Property(x => x.Sugar);
            entity.Property(x => x.ActiveYeast);
            entity.Property(x => x.DeadYeast);
            entity.Property(x => x.LatticeYeast);
            entity.Property(x => x.Ethanol);

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

