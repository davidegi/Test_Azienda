namespace Test_Azienda.Domain.Table;
public partial class Dipendente
{
    public int IDDipendente { get; set; }
    public string? CodiceFiscale { get; set; }
    public string? PartitaIva { get; set; }
    public string? Iban { get; set; }
    public decimal? RAL { get; set; }
    public int IDAzienda { get; set; }
    public string? Professione { get; set; }
    public string? Qualifica { get; set; }
    public DateOnly DataAssunzione { get; set; }
    public DateOnly? DataFineAssunzione { get; set; }
    public virtual Azienda? Azienda { get; set; }
}
