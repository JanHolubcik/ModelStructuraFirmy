
tp.Test("Vytvor zamestnanca. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createZamestnanec"].StatusCode();
    Equal(201, statusCode);

});

var newZamestnanec = tp.GetVariable<string>("NewZamestnanec");
dynamic obj = newZamestnanec.ToExpando();
var rc = obj.RodneCislo;

await tp.Test($"Novy zamestnanec by mal mat toto rodne cislo '{rc}' .", async () =>
{
    dynamic responseJson = await tp.Responses["getZamestnanec"].GetBodyAsExpandoAsync();



    Equal(rc, responseJson.rodneCislo);
});