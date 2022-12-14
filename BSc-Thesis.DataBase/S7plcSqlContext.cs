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
            entity.HasNoKey();
            //entity.Property(x => x.Id);
            entity.Property(x => x.Temperatura);
            entity.Property(x => x.TemperaturaOtoczenia);
            entity.Property(x => x.Glukoza);
            entity.Property(x => x.Maltoza);
            entity.Property(x => x.Maltortioza);
            entity.Property(x => x.Cukier);
            entity.Property(x => x.DrozdzeAktywne);
            entity.Property(x => x.DrozdzeMartwe);
            entity.Property(x => x.DrozdzeLatecyjne);
            entity.Property(x => x.Etanol);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
