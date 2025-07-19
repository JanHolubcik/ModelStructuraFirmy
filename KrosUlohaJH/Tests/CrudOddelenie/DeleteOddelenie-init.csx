

public class Oddelenie
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public int? ProjektId { get; set; }
    public string? VeduciOddeleniaRc { get; set; }
}



Oddelenie OddelenieCrud = new Oddelenie
{
    Nazov = "Skusobne oddelenie",
    Kod = "SO",
    ProjektId = 1,
    VeduciOddeleniaRc = "920402/0001"
};



string kodGet = OddelenieCrud.Kod;



tp.SetVariable("deleteOddelenie", OddelenieCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);

