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

Zamestnanec zamestnanecCrud = new Zamestnanec
{
    RodneCislo = "990401/4834",
    Meno = "Skusony",
    Priezvisko = "Priezvisko",
    Email = "SK.PR@examsple.com",
    Titul = "MGLR.",
};

Zamestnanec zamestnanecUpdate = new Zamestnanec
{
    RodneCislo = "990401/4834",
    Titul = "Mgr.",
    OddelenieId = 1
};

string rodnecisloDelete = "990401/4834";

tp.SetVariable("NewZamestnanec", zamestnanecCrud.ToJsonString());
tp.SetVariable("UpdateZamestnanec", zamestnanecUpdate.ToJsonString());
tp.SetVariable("DeleteZamestnanec", rodnecisloDelete);