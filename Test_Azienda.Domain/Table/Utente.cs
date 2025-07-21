namespace Test_Azienda.Domain.Table;
public partial class Utente
{
    public int IDUtente { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int IDRuolo { get; set; }
    public DateTime? DataCancellazione { get; set; }
    public virtual Ruolo? Ruolo { get; set; }
}
