#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\Firma.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\Divizia.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\Oddelenie.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\Projekt.csx"
#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\Zamestnanec.csx"
DiviziaTest DiviziaCrud = new DiviziaTest { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };
OddelenieTest OddelenieCrud = new OddelenieTest
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};
FirmaTest FirmaCrud = new FirmaTest { Nazov = "Skusobny", Kod = "SY", RiaditelRc = "920402/0001" };