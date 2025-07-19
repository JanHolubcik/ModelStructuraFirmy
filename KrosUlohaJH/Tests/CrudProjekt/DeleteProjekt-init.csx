

public class Projekt
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public int DiviziaId { get; set; }
    public string? VeduciProjektuRC { get; set; }
}



Projekt ProjektCrud = new Projekt
{
    Nazov = "Projekt Mars",
    Kod = "PRJ-Mars",
    DiviziaId = 1,
    VeduciProjektuRC = "870927/4444"
};


string kodGet = ProjektCrud.VeduciProjektuRC;



tp.SetVariable("deleteDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);

