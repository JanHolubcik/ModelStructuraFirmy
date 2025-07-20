tp.Test("Vytvori alebo edituje zamestnanca", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["newZamestnanec"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });
    tp.Logger.LogInformation("Test prebehol uspesne.");
});
var newFirma = tp.GetVariable<string>("NewZamestnanec");
dynamic obj = newFirma.ToExpando();
var Priezvisko = obj.Priezvisko;
var meno = obj.Meno;
var Email = obj.Email;
var Titul = obj.Titul;
var TelefonneCislo = obj.TelefonneCislo;
var OddelenieId = obj.OddelenieId;
await tp.Test("Update by mal obsahovat pocet uspesnich 1 a neuspesnych 1, kvoli zle zadanemu rodnemu cislu. ", async () =>
{
    dynamic responseJsonBulk = await tp.Responses["updateZamestnanecBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getUser"].GetBodyAsExpandoAsync();
    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;
    NotEqual(Priezvisko, responseJsonNew.Priezvisko);
    NotEqual(Email, responseJsonNew.Email);
    NotEqual(Titul, responseJsonNew.Titul);
    NotEqual(Email, responseJsonNew.Email);
    NotEqual(TelefonneCislo, responseJsonNew.TelefonneCislo);
    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));
});