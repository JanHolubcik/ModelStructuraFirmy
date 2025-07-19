
tp.Test("Vytvor diviziu. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createDivizia"].StatusCode();
    Equal(201, statusCode);

});

var newFirma = tp.GetVariable<string>("createDivizia");
dynamic obj = newFirma.ToExpando();
var kod = obj.kod;

await tp.Test($"Firma je vytvorena s timto kodom '{kod}' .", async () =>
{
    dynamic responseJson = await tp.Responses["getDivizia"].GetBodyAsExpandoAsync();
    Console.WriteLine("Raw response: " + responseJson);
    // Access JSON properties case-insensitively.

    Equal(kod, (string)responseJson.kod);
});