#load "$teapie/Definitions/Firma.csx"
#load "$teapie/Definitions/Divizia.csx"
#load "$teapie/Definitions/Oddelenie.csx"
#load "$teapie/Definitions/Projekt.csx"
#load "$teapie/Definitions/Zamestnanec.csx"

var Firmy = new List<FirmaTest>
{
    new FirmaTest { Nazov = "Kros", Kod = "KR",RiaditelRc = "900101/1234"},
    new FirmaTest {  Nazov = "Vúb Banka", Kod = "VUB",RiaditelRc = "890315/5678"},
};


var Divizie = new List<DiviziaTest>
{
    new DiviziaTest {  Nazov = "Žilina", Kod = "KR",VeduciRC = "900101/1234",FirmaId = 1},
    new DiviziaTest {  Nazov = "Bratislava", Kod = "KRBR",VeduciRC = "981231/1234", FirmaId = 1},
    new DiviziaTest {  Nazov = "Vúb Banka", Kod = "VUB",VeduciRC = "890315/5678",FirmaId = 2},
};


var oddelenia = new List<OddelenieTest>
{
    new OddelenieTest {  Nazov = "Informačné technológie", Kod = "IT"},
    new OddelenieTest {  Nazov = "Ľudské zdroje", Kod = "HR",  },
    new OddelenieTest {  Nazov = "Marketing", Kod = "MKT",  },
    new OddelenieTest {  Nazov = "Financie", Kod = "FIN",  },
    new OddelenieTest {  Nazov = "Predajcovia", Kod = "PRE"},
};
var oddeleniaWithRC = new List<OddelenieTest>
{
    new OddelenieTest {  Nazov = "Informačné technológie", Kod = "IT",ProjektId=1, VeduciOddeleniaRc ="920402/0001"},
    new OddelenieTest {  Nazov = "Ľudské zdroje", Kod = "HR",ProjektId=2, VeduciOddeleniaRc ="950125/0002" },
    new OddelenieTest {  Nazov = "Marketing", Kod = "MKT", ProjektId=3, VeduciOddeleniaRc ="920507/1111"  },
    new OddelenieTest {  Nazov = "Financie", Kod = "FIN", ProjektId=4, VeduciOddeleniaRc ="900228/8888" },
    new OddelenieTest {  Nazov = "Predajcovia", Kod = "PRE",ProjektId=2, VeduciOddeleniaRc ="890120/0006" },
};
//Zeditujem si oddelenia po tom ako pridám používateľov, potom môžem postupne pridávať aj projekty atď. aby 
//som nemusel znova pridávať cudzie kľúče.

var projekty = new List<ProjektTest>
{
    new ProjektTest
    {
        Nazov = "Projekt Orion",
        Kod = "PRJ-ORION",
        DiviziaId = 1,
        VeduciProjektuRC ="870927/4444"
    },
    new ProjektTest
    {
        Nazov = "Projekt Apollo",
        Kod = "PRJ-APOLLO",
        DiviziaId = 2,
        VeduciProjektuRC ="930303/5555"

    },
    new ProjektTest
    {
        Nazov = "Projekt Titan",
        Kod = "PRJ-TITAN",
        DiviziaId = 1,
        VeduciProjektuRC ="950709/7777"
    },
    new ProjektTest
    {
        Nazov = "Projekt Mercury",
        Kod = "PRJ-MERC",
        DiviziaId = 2,
        VeduciProjektuRC ="860116/6666"
    },
    new ProjektTest
    {
        Nazov = "Projekt Atlas",
        Kod = "PRJ-ATLAS",
        DiviziaId = 1,
        VeduciProjektuRC ="910812/3333"
    }
};


var zamestnanci = new List<ZamestnanecTest>
{
    new ZamestnanecTest
    {
        RodneCislo = "900101/1234",
        Meno = "Jana",
        Priezvisko = "Nováková",
        Email = "jana.novakova@example.com",
        Titul = "Mgr.",
        OddelenieId = 1,
        TelefonneCislo = "+421901234567"
    },
    new ZamestnanecTest
    {
        RodneCislo = "890315/5678",
        Meno = "Peter",
        Priezvisko = "Horváth",
        Email = "peter.horvath@example.com",
        Titul = "",
        OddelenieId = 2,
        TelefonneCislo = "+421902345678"
    },
    new ZamestnanecTest
    {
        RodneCislo = "920507/1111",
        Meno = "Lucia",
        Priezvisko = "Bieliková",
        Email = "lucia.bielikova@example.com",
        Titul = "Ing.",
        OddelenieId = 3,
        TelefonneCislo = "+421903456789"
    },
    new ZamestnanecTest
    {
        RodneCislo = "850201/2222",
        Meno = "Marek",
        Priezvisko = "Kováč",
        Email = "marek.kovac@example.com",
        Titul = "",
        OddelenieId = null,
        TelefonneCislo = "+421904567890"
    },
    new ZamestnanecTest
    {
        RodneCislo = "910812/3333",
        Meno = "Anna",
        Priezvisko = "Tóthová",
        Email = "anna.tothova@example.com",
        Titul = "Bc.",
        OddelenieId = 4,
        TelefonneCislo = "+421905678901"
    },
    new ZamestnanecTest
    {
        RodneCislo = "870927/4444",
        Meno = "Juraj",
        Priezvisko = "Szabó",
        Email = "juraj.szabo@example.com",
        Titul = "",
        OddelenieId = 5,
        TelefonneCislo = "+421906789012"
    },
    new ZamestnanecTest
    {
        RodneCislo = "930303/5555",
        Meno = "Zuzana",
        Priezvisko = "Králová",
        Email = "zuzana.kralova@example.com",
        Titul = "",
        OddelenieId = 1,
        TelefonneCislo = "+421907890123"
    },
    new ZamestnanecTest
    {
        RodneCislo = "860116/6666",
        Meno = "Tomáš",
        Priezvisko = "Farkaš",
        Email = "tomas.farkas@example.com",
        Titul = "",
        OddelenieId = 2,
        TelefonneCislo = "+421908901234"
    },
    new ZamestnanecTest
    {
        RodneCislo = "950709/7777",
        Meno = "Monika",
        Priezvisko = "Urbanová",
        Email = "monika.urbanova@example.com",
        Titul = "",
        OddelenieId = 3,
        TelefonneCislo = "+421909012345"
    },
    new ZamestnanecTest
    {
        RodneCislo = "900228/8888",
        Meno = "Martin",
        Priezvisko = "Baláž",
        Email = "martin.balaz@example.com",
        Titul = "Mgr.",
        OddelenieId = 4,
        TelefonneCislo = "+421910123456"
    },
    new ZamestnanecTest
    {
        RodneCislo = "910101/9999",
        Meno = "Simona",
        Priezvisko = "Rybárová",
        Email = "simona.rybarova@example.com",
        Titul = "",
        OddelenieId = 5,
        TelefonneCislo = "+421911234567"
    },
    new ZamestnanecTest
    {
        RodneCislo = "920402/0001",
        Meno = "Dominik",
        Priezvisko = "Pavlík",
        Email = "dominik.pavlik@example.com",
        Titul = "",
        OddelenieId = 1,
        TelefonneCislo = "+421912345678"
    },
    new ZamestnanecTest
    {
        RodneCislo = "880313/0002",
        Meno = "Veronika",
        Priezvisko = "Michalová",
        Email = "veronika.michalova@example.com",
        Titul = "",
        OddelenieId = null,
        TelefonneCislo = "+421913456789"
    },
    new ZamestnanecTest
    {
        RodneCislo = "870714/0003",
        Meno = "Andrej",
        Priezvisko = "Blažek",
        Email = "andrej.blazek@example.com",
        Titul = "PhDr.",
        OddelenieId = 2,
        TelefonneCislo = "+421914567890"
    },
    new ZamestnanecTest
    {
        RodneCislo = "900408/0004",
        Meno = "Barbora",
        Priezvisko = "Holá",
        Email = "barbora.hola@example.com",
        Titul = "",
        OddelenieId = 3,
        TelefonneCislo = "+421915678901"
    },
    new ZamestnanecTest
    {
        RodneCislo = "930926/0005",
        Meno = "Filip",
        Priezvisko = "Gregor",
        Email = "filip.gregor@example.com",
        Titul = "Bc.",
        OddelenieId = 4,
        TelefonneCislo = "+421916789012"
    },
    new ZamestnanecTest
    {
        RodneCislo = "890120/0006",
        Meno = "Lenka",
        Priezvisko = "Janíková",
        Email = "lenka.janikova@example.com",
        Titul = "",
        OddelenieId = 5,
        TelefonneCislo = "+421917890123"
    },
    new ZamestnanecTest
    {
        RodneCislo = "950510/0007",
        Meno = "Matej",
        Priezvisko = "Hudec",
        Email = "matej.hudec@example.com",
        Titul = "Ing.",
        OddelenieId = 1,
        TelefonneCislo = "+421918901234"
    },
    new ZamestnanecTest
    {
        RodneCislo = "880216/0008",
        Meno = "Kristína",
        Priezvisko = "Poláková",
        Email = "kristina.polakova@example.com",
        Titul = "",
        OddelenieId = 2,
        TelefonneCislo = "+421919012345"
    },
    new ZamestnanecTest
    {
        RodneCislo = "900703/0009",
        Meno = "Róbert",
        Priezvisko = "Medveď",
        Email = "robert.medved@example.com",
        Titul = "",
        OddelenieId = 3,
        TelefonneCislo = "+421920123456"
    },
    new ZamestnanecTest
    {
        RodneCislo = "870812/0010",
        Meno = "Ivana",
        Priezvisko = "Lacková",
        Email = "ivana.lackova@example.com",
        Titul = "",
        OddelenieId = 4,
        TelefonneCislo = "+421921234567"
    },
    new ZamestnanecTest
    {
        RodneCislo = "890130/0011",
        Meno = "Michal",
        Priezvisko = "Jankovič",
        Email = "michal.jankovic@example.com",
        Titul = "",
        OddelenieId = 5,
        TelefonneCislo = "+421922345678"
    },
    new ZamestnanecTest
    {
        RodneCislo = "910912/0012",
        Meno = "Dana",
        Priezvisko = "Švecová",
        Email = "dana.svecova@example.com",
        Titul = "Mgr.",
        OddelenieId = 1,
        TelefonneCislo = "+421923456789"
    },
    new ZamestnanecTest
    {
        RodneCislo = "950423/0013",
        Meno = "Emil",
        Priezvisko = "Mlynár",
        Email = "emil.mlynar@example.com",
        Titul = "",
        OddelenieId = 2,
        TelefonneCislo = "+421924567890"
    },
    new ZamestnanecTest
    {
        RodneCislo = "940620/0014",
        Meno = "Soňa",
        Priezvisko = "Kubišová",
        Email = "sona.kubisova@example.com",
        Titul = "",
        OddelenieId = 3,
        TelefonneCislo = "+421925678901"
    },
    new ZamestnanecTest
    {
        RodneCislo = "910101/0001",
        Meno = "Ján",
        Priezvisko = "Novotný",
        Email = "jan.novotny@example.com",
        Titul = "Ing.",
        OddelenieId = 1,
        TelefonneCislo = "+421926789012"
    },
    new ZamestnanecTest
    {
        RodneCislo = "880305/1234",
        Meno = "Lucia",
        Priezvisko = "Kováčová",
        Email = "lucia.kovacova@example.com",
        Titul = "",
        OddelenieId = 2,
        TelefonneCislo = "+421927890123"
    },
    new ZamestnanecTest
    {
        RodneCislo = "970215/4567",
        Meno = "Peter",
        Priezvisko = "Baláž",
        Email = "peter.balaz@example.com",
        Titul = "Mgr.",
        OddelenieId = 3,
        TelefonneCislo = "+421928901234"
    },
    new ZamestnanecTest
    {
        RodneCislo = "850610/7890",
        Meno = "Mária",
        Priezvisko = "Zelená",
        Email = "maria.zelena@example.com",
        Titul = "",
        OddelenieId = 4,
        TelefonneCislo = "+421929012345"
    },
    new ZamestnanecTest
    {
        RodneCislo = "930101/1111",
        Meno = "Róbert",
        Priezvisko = "Horváth",
        Email = "robert.horvath@example.com",
        Titul = "Bc.",
        OddelenieId = 5,
        TelefonneCislo = "+421930123456"
    },
    new ZamestnanecTest
    {
        RodneCislo = "890512/2222",
        Meno = "Eva",
        Priezvisko = "Tóthová",
        Email = "eva.tothova@example.com",
        Titul = "",
        OddelenieId = 1,
        TelefonneCislo = "+421931234567"
    },
    new ZamestnanecTest
    {
        RodneCislo = "860823/3333",
        Meno = "Tomáš",
        Priezvisko = "Urban",
        Email = "tomas.urban@example.com",
        Titul = "Ing.",
        OddelenieId = 2,
        TelefonneCislo = "+421932345678"
    },
    new ZamestnanecTest
    {
        RodneCislo = "950307/4444",
        Meno = "Zuzana",
        Priezvisko = "Farkašová",
        Email = "zuzana.farkasova@example.com",
        Titul = "",
        OddelenieId = 3,
        TelefonneCislo = "+421933456789"
    },
    new ZamestnanecTest
    {
        RodneCislo = "920102/5555",
        Meno = "Michal",
        Priezvisko = "Kollár",
        Email = "michal.kollar@example.com",
        Titul = "Mgr.",
        OddelenieId = 4,
        TelefonneCislo = "+421934567890"
    },
    new ZamestnanecTest
    {
        RodneCislo = "870918/6666",
        Meno = "Andrea",
        Priezvisko = "Vargová",
        Email = "andrea.vargova@example.com",
        Titul = "",
        OddelenieId = 5,
        TelefonneCislo = "+421935678901"
    },
    new ZamestnanecTest
    {
        RodneCislo = "990321/7777",
        Meno = "Martin",
        Priezvisko = "Biely",
        Email = "martin.biely@example.com",
        Titul = "Bc.",
        OddelenieId = null,
        TelefonneCislo = "+421936789012"
    },
    new ZamestnanecTest
    {
        RodneCislo = "940417/8888",
        Meno = "Kristína",
        Priezvisko = "Čierna",
        Email = "kristina.cierna@example.com",
        Titul = "",
        OddelenieId = 1,
        TelefonneCislo = "+421937890123"
    },
    new ZamestnanecTest
    {
        RodneCislo = "930908/9999",
        Meno = "Samuel",
        Priezvisko = "Majer",
        Email = "samuel.majer@example.com",
        Titul = "Ing.",
        OddelenieId = 2,
        TelefonneCislo = "+421938901234"
    },
    new ZamestnanecTest
    {
        RodneCislo = "950125/0002",
        Meno = "Tatiana",
        Priezvisko = "Mikušová",
        Email = "tatiana.mikusova@example.com",
        Titul = "",
        OddelenieId = null,
        TelefonneCislo = "+421939012345"
    },
    new ZamestnanecTest
    {
        RodneCislo = "981231/1234",
        Meno = "Igor",
        Priezvisko = "Šimek",
        Email = "igor.simek@example.com",
        Titul = "Mgr.",
        OddelenieId = 5,
        TelefonneCislo = "+421940123456"
    }
};