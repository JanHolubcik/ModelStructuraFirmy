using System.Diagnostics;

public class Firma
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public string? RiaditelRc { get; set; }
}



Firma FirmaCrud = new Firma { Nazov = "Skusobny", Kod = "SY", RiaditelRc = "940417/8888" };

string kodGet = "SY";


tp.SetVariable("newFirma", FirmaCrud.ToJsonString());
tp.SetVariable("GetFirma", kodGet);
