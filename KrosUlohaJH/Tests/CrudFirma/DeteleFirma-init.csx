using System.Diagnostics;

public class Zamestnanec
{
    public int Id { get; set; }
    public string RodneCislo { get; set; }
    public string Meno { get; set; }
    public string Priezvisko { get; set; }
    public string Email { get; set; }
    public string Titul { get; set; }
    public int? OddelenieId { get; set; }
}

string rodnecisloDelete = "990401/4834";
Zamestnanec zamestnanecCrud = new Zamestnanec
{
    RodneCislo = "990401/4834",
    Meno = "Skusony",
    Priezvisko = "Priezvisko",
    Email = "SK.PR@examsple.com",
    Titul = "MGLR.",
};

tp.SetVariable("NewZamestnanec", zamestnanecCrud.ToJsonString());

tp.SetVariable("DeleteZamestnanec", rodnecisloDelete);