
tp.Test("Vytvor firmu. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createFirma"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

var newFirma = tp.GetVariable("newFirma");
dynamic obj = newFirma.ToExpando();
var kod = obj.kod;

await tp.Test($"Firma je vytvorena s timto kodom '{kod}' .", async () =>
{
    dynamic responseJson = await tp.Responses["getFirma"].GetBodyAsExpandoAsync();
    Console.WriteLine("Raw response: " + responseJson);
    // Access JSON properties case-insensitively.

    Equal(kod, (string)responseJson.kod);
});