public class Firma
{
    public int Id { get; set; }
    public required string Nazov { get; set; }
    public required string Kod { get; set; }
    public string? RiaditelRc { get; set; }
}

Firma FirmaDelete = new Firma { Nazov = "Skusobny", Kod = "SA", RiaditelRc = "981231/1234" };

string kodDelete = "SA";


tp.SetVariable("firmaDelete", FirmaDelete.ToJsonString());
tp.SetVariable("deleteFirma", kodDelete, "delete");
