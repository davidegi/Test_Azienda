namespace Test_Azienda.Domain.Table;

public partial class Azienda
{
    public Azienda()
    {
        Dipendente = new HashSet<Dipendente>();
    }
    public int IDAzienda { get; set; }
    public string? CodiceFiscale { get; set; }
    public string? PartitaIva { get; set; }
    public string? Descrizione { get; set; }
    public decimal? Capitale { get; set; }
    public int IDRuolo { get; set; }
    public DateOnly? DataInizioAtt { get; set; }
    public DateOnly? DataFineAtt { get; set; }
    public DateTime? DataCancellazione { get; set; }
    public virtual ICollection<Dipendente> Dipendente { get; set; }
    public virtual Ruolo? Ruolo { get; set; }
}
