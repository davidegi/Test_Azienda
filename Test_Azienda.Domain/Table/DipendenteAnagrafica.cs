using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Azienda1.Domain.Table;
public partial class DipendenteAnagrafica
{
    public int IDDipendenteAnag { get; set; }

    [Column("nome")]
    public string? Nome { get; set; }

    [Column("cognome")]
    public string? Cognome { get; set; }

    public string? LuogoNascita { get; set; }

    public DateOnly? DataNascita { get; set; }

    public DateOnly? DataDecesso { get; set; }

    public string? Indirizzo { get; set; }

    public int? CAP { get; set; }

    public string? Sesso { get; set; }
}
