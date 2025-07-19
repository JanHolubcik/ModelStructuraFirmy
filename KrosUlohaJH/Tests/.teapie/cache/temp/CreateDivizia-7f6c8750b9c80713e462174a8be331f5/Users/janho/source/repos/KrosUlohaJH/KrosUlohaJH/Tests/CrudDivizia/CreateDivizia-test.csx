tp.Test("Vytvor diviziu. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createDivizia"].StatusCode();
    Equal(200, statusCode);
});
var newFirma = tp.GetVariable<string>("newDivizia");
dynamic obj = newFirma.ToExpando();
var kod = obj.Kod;
await tp.Test($"Divizia je vytvorena s timto kodom  .", async () =>
{
    dynamic responseJson = await tp.Responses["getDivizia"].GetBodyAsExpandoAsync();
    // Access JSON properties case-insensitively.
    Equal(kod, (string)responseJson.Kod);
});