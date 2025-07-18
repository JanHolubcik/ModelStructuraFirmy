tp.Test("Update zamestnanca failne, lebo sa snazim updatnut neexistujuce zamestnanca (400)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateZamestnanec"].StatusCode();
    Equal(400, statusCode);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});

await tp.Test("Update by mal mat toto rodne cislo v errors 990401/9999. lebo sa nachadza v databaze (200)", async () =>
{
    dynamic responseJson = await tp.Responses["updateZamestnanecBulk"].GetBodyAsExpandoAsync();
    string firstErrorRodneCislo = responseJson.chyby[0].rodneCislo;
    Equal("990401/9999", firstErrorRodneCislo);
    tp.SetVariable("GetRC", firstErrorRodneCislo);


});

var newZamestnanec = tp.GetVariable<string>("NewZamestnanec");
dynamic obj = newZamestnanec.ToExpando();
var rc = obj.RodneCislo;


