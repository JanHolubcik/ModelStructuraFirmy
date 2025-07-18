
tp.Test("Vytvor firmu. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createFirma"].StatusCode();
    Equal(201, statusCode);

});

var newFirma = tp.GetVariable<string>("newFirma");
dynamic obj = newFirma.ToExpando();
var kod = obj.kod;

await tp.Test($"Firma je vytvorena s timto kodom '{kod}' .", async () =>
{
    dynamic responseJson = await tp.Responses["getFirma"].GetBodyAsExpandoAsync();
    Console.WriteLine("Raw response: " + responseJson);
    // Access JSON properties case-insensitively.

    Equal("SY", (string)responseJson.kod);
});