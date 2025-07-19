#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\Firma.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\Divizia.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\Oddelenie.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\Projekt.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\Zamestnanec.csx"
DiviziaTest DiviziaCrud = new DiviziaTest { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };
OddelenieTest OddelenieCrud = new OddelenieTest
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};