
tp.Test("Vytvor oddelenie. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createOddelenie"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});



var newFirma = tp.GetVariable<string>("newOddelenie");
dynamic obj = newFirma.ToExpando();
var kod = obj.kod;

await tp.Test($"Oddelenie je vytvorene s timto kodom  '{kod}'.", async () =>
{
    dynamic responseJson = await tp.Responses["getOddelenie"].GetBodyAsExpandoAsync();
    Console.WriteLine("Raw response: " + responseJson);
    // Access JSON properties case-insensitively.

    Equal(kod, (string)responseJson.kod);
});
