

public class Divizia
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }

    public int? FirmaId { get; set; }


    public string? VeduciRC { get; set; }


}



Divizia DiviziaCrud = new Divizia { Nazov = "Košice", Kod = "KS", VeduciRC = "900101/1234", FirmaId = 1 };


string kodGet = "KS";



tp.SetVariable("newDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);

