#load "$teapie/Definitions/Firma.csx"
#load "$teapie/Definitions/Divizia.csx"
#load "$teapie/Definitions/Oddelenie.csx"
#load "$teapie/Definitions/Projekt.csx"
#load "$teapie/Definitions/Zamestnanec.csx"


DiviziaTest DiviziaCrud = new DiviziaTest { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };
OddelenieTest OddelenieCrud = new OddelenieTest
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};
FirmaTest FirmaCrud = new FirmaTest { Nazov = "Skusobny", Kod = "SY", RiaditelRc = "920402/0001" };