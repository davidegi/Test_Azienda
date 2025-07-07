using Microsoft.EntityFrameworkCore;

namespace Test_Azienda1.Domain.Table;
public partial class MyDbContext : DbContext
{
    public MyDbContext() { }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options){ }

    public virtual DbSet<Azienda> Azienda { get; set; }

    public virtual DbSet<Dipendente> Dipendente { get; set; }

    public virtual DbSet<DipendenteAnagrafica> DipendenteAnagrafica { get; set; }

    public virtual DbSet<HistoryLog> HistoryLog { get; set; }

    public virtual DbSet<Ruolo> Ruolo { get; set; }

    public virtual DbSet<Utente> Utente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Azienda>(e =>
        {
            e.HasKey(a => a.IDAzienda);
            e.ToTable("Azienda");

            e.HasOne(e => e.Ruolo)
             .WithMany(e => e.Azienda)
             .HasForeignKey(e => e.IDRuolo);
        }); 
        modelBuilder.Entity<HistoryLog>(e =>
        {
            e.HasKey(a => a.Chiave);
        });

        modelBuilder.Entity<Dipendente>(e =>
        {
            e.HasKey(a => a.IDDipendente);
            e.ToTable("Dipendente");

            e.HasOne(e => e.Azienda)
                    .WithMany(e => e.Dipendente)
                    .HasForeignKey(e => e.IDAzienda);
        });

        modelBuilder.Entity<DipendenteAnagrafica>(e =>
        {
            e.HasKey(a => a.IDDipendenteAnag);
            e.ToTable("DipendenteAnagrafica");
        });

        modelBuilder.Entity<Ruolo>(e =>
        {
            e.HasKey(a => a.IDRuolo);
            e.ToTable("Ruolo");
        });

        modelBuilder.Entity<Utente>(e =>
        {
            e.HasKey(a => a.IDUtente);
            e.ToTable("Utente");

            e.HasOne(e => e.Ruolo)
            .WithMany(e => e.Utente)
            .HasForeignKey(e => e.IDRuolo);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
