using static Script;

public class Oddelenie
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public int? ProjektId { get; set; }
    public string? VeduciOddeleniaRc { get; set; }
}

public class Zamestnanec
{
    public string RodneCislo { get; set; }
    public string Meno { get; set; }
    public string Priezvisko { get; set; }
    public string Email { get; set; }
    public string Titul { get; set; }
    public int? OddelenieId { get; set; }
}

public class Projekt
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public int DiviziaId { get; set; }
    public string? VeduciProjektuRC { get; set; }
}

var oddelenia = new List<Oddelenie>
{
    new() {  Nazov = "Informačné technológie", Kod = "IT"},
    new() {  Nazov = "Ľudské zdroje", Kod = "HR" },
    new() {  Nazov = "Marketing", Kod = "MKT" },
    new() {  Nazov = "Financie", Kod = "FIN"},
    new() {  Nazov = "Logistika", Kod = "LOG" }
};

var projekty = new List<Projekt>
{
    new()
    {
        Nazov = "Projekt Orion",
        Kod = "PRJ-ORION",
    },
    new()
    {
        Nazov = "Projekt Apollo",
        Kod = "PRJ-APOLLO",
    },
    new()
    {
        Nazov = "Projekt Titan",
        Kod = "PRJ-TITAN",
    },
    new()
    {
        Nazov = "Projekt Mercury",
        Kod = "PRJ-MERC",
    },
    new()
    {
        Nazov = "Projekt Atlas",
        Kod = "PRJ-ATLAS",
    }
};


var zamestnanci = new List<Zamestnanec>
{
    new Zamestnanec
    {
        RodneCislo = "900101/1234",
        Meno = "Jana",
        Priezvisko = "Nováková",
        Email = "jana.novakova@example.com",
        Titul = "Mgr.",
        OddelenieId = 1
    },
    new Zamestnanec
    {
        RodneCislo = "890315/5678",
        Meno = "Peter",
        Priezvisko = "Horváth",
        Email = "peter.horvath@example.com",
        Titul = "",
        OddelenieId = 2
    },
    new Zamestnanec
    {
        RodneCislo = "920507/1111",
        Meno = "Lucia",
        Priezvisko = "Bieliková",
        Email = "lucia.bielikova@example.com",
        Titul = "Ing.",
        OddelenieId = 3
    },
    new Zamestnanec
    {
        RodneCislo = "850201/2222",
        Meno = "Marek",
        Priezvisko = "Kováč",
        Email = "marek.kovac@example.com",
        Titul = "",
        OddelenieId = null
    },
    new Zamestnanec
    {
        RodneCislo = "910812/3333",
        Meno = "Anna",
        Priezvisko = "Tóthová",
        Email = "anna.tothova@example.com",
        Titul = "Bc.",
        OddelenieId = 4
    },
    new Zamestnanec
    {
        RodneCislo = "870927/4444",
        Meno = "Juraj",
        Priezvisko = "Szabó",
        Email = "juraj.szabo@example.com",
        Titul = "",
        OddelenieId = 5
    },
    new Zamestnanec
    {
        RodneCislo = "930303/5555",
        Meno = "Zuzana",
        Priezvisko = "Králová",
        Email = "zuzana.kralova@example.com",
        Titul = "",
        OddelenieId = 1
    },
    new Zamestnanec
    {
        RodneCislo = "860116/6666",
        Meno = "Tomáš",
        Priezvisko = "Farkaš",
        Email = "tomas.farkas@example.com",
        Titul = "",
        OddelenieId = 2
    },
    new Zamestnanec
    {
        RodneCislo = "950709/7777",
        Meno = "Monika",
        Priezvisko = "Urbanová",
        Email = "monika.urbanova@example.com",
        Titul = "",
        OddelenieId = 3
    },
    new Zamestnanec
    {
        RodneCislo = "900228/8888",
        Meno = "Martin",
        Priezvisko = "Baláž",
        Email = "martin.balaz@example.com",
        Titul = "Mgr.",
        OddelenieId = 4
    },
    new Zamestnanec
    {
        RodneCislo = "910101/9999",
        Meno = "Simona",
        Priezvisko = "Rybárová",
        Email = "simona.rybarova@example.com",
        Titul = "",
        OddelenieId = 5
    },
    new Zamestnanec
    {
        RodneCislo = "920402/0001",
        Meno = "Dominik",
        Priezvisko = "Pavlík",
        Email = "dominik.pavlik@example.com",
        Titul = "",
        OddelenieId = 1
    },
    new Zamestnanec
    {
        RodneCislo = "880313/0002",
        Meno = "Veronika",
        Priezvisko = "Michalová",
        Email = "veronika.michalova@example.com",
        Titul = "",
        OddelenieId = null
    },
    new Zamestnanec
    {
        RodneCislo = "870714/0003",
        Meno = "Andrej",
        Priezvisko = "Blažek",
        Email = "andrej.blazek@example.com",
        Titul = "PhDr.",
        OddelenieId = 2
    },
    new Zamestnanec
    {
        RodneCislo = "900408/0004",
        Meno = "Barbora",
        Priezvisko = "Holá",
        Email = "barbora.hola@example.com",
        Titul = "",
        OddelenieId = 3
    },
    new Zamestnanec
    {
        RodneCislo = "930926/0005",
        Meno = "Filip",
        Priezvisko = "Gregor",
        Email = "filip.gregor@example.com",
        Titul = "Bc.",
        OddelenieId = 4
    },
    new Zamestnanec
    {
        RodneCislo = "890120/0006",
        Meno = "Lenka",
        Priezvisko = "Janíková",
        Email = "lenka.janikova@example.com",
        Titul = "",
        OddelenieId = 5
    },
    new Zamestnanec
    {
        RodneCislo = "950510/0007",
        Meno = "Matej",
        Priezvisko = "Hudec",
        Email = "matej.hudec@example.com",
        Titul = "Ing.",
        OddelenieId = 1
    },
    new Zamestnanec
    {
        RodneCislo = "880216/0008",
        Meno = "Kristína",
        Priezvisko = "Poláková",
        Email = "kristina.polakova@example.com",
        Titul = "",
        OddelenieId = 2
    },
    new Zamestnanec
    {
        RodneCislo = "900703/0009",
        Meno = "Róbert",
        Priezvisko = "Medveď",
        Email = "robert.medved@example.com",
        Titul = "",
        OddelenieId = 3
    },
    new Zamestnanec
    {
        RodneCislo = "870812/0010",
        Meno = "Ivana",
        Priezvisko = "Lacková",
        Email = "ivana.lackova@example.com",
        Titul = "",
        OddelenieId = 4
    },
    new Zamestnanec
    {
        RodneCislo = "890130/0011",
        Meno = "Michal",
        Priezvisko = "Jankovič",
        Email = "michal.jankovic@example.com",
        Titul = "",
        OddelenieId = 5
    },
    new Zamestnanec
    {
        RodneCislo = "910912/0012",
        Meno = "Dana",
        Priezvisko = "Švecová",
        Email = "dana.svecova@example.com",
        Titul = "Mgr.",
        OddelenieId = 1
    },
    new Zamestnanec
    {
        RodneCislo = "950423/0013",
        Meno = "Emil",
        Priezvisko = "Mlynár",
        Email = "emil.mlynar@example.com",
        Titul = "",
        OddelenieId = 2
    },
    new Zamestnanec
    {
        RodneCislo = "940620/0014",
        Meno = "Soňa",
        Priezvisko = "Kubišová",
        Email = "sona.kubisova@example.com",
        Titul = "",
        OddelenieId = 3
    }
};

tp.SetVariable("Newoddelenia", zamestnanci.ToJsonString(), "oddelenia");
tp.SetVariable("NewZamestnanci", zamestnanci.ToJsonString(), "zamestnanci");