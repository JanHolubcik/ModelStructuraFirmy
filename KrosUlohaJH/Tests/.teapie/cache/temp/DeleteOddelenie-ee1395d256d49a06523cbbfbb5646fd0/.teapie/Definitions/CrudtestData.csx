#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\Firma.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\Divizia.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\Oddelenie.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\Projekt.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\Zamestnanec.csx"
DiviziaTest DiviziaCrud = new DiviziaTest { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };
OddelenieTest OddelenieCrud = new OddelenieTest
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};