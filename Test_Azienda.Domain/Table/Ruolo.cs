namespace Test_Azienda1.Domain.Table;
public partial class Ruolo
{
    public Ruolo() { 
        Azienda = new HashSet<Azienda>();
        Utente = new HashSet<Utente>();
    }
    public int IDRuolo { get; set; }
    public string? Tipo { get; set; }
    public string? Descrizione { get; set; }
    public virtual ICollection<Azienda> Azienda { get; set; }
    public virtual ICollection<Utente> Utente { get; set; }
}
