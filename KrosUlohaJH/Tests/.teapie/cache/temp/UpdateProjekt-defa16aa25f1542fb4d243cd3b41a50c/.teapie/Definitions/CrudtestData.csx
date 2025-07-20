#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\Firma.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\Divizia.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\Oddelenie.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\Projekt.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\Zamestnanec.csx"
DiviziaTest DiviziaCrud = new DiviziaTest { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };
OddelenieTest OddelenieCrud = new OddelenieTest
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};
FirmaTest FirmaCrud = new FirmaTest { Nazov = "Skusobny", Kod = "SY", RiaditelRc = "920402/0001" };
ProjektTest ProjektCrud = new ProjektTest
{
    Nazov = "Projekt Mars",
    Kod = "PRJ-Mars",
    DiviziaId = 1,
    VeduciProjektuRC = "870927/4444"
};