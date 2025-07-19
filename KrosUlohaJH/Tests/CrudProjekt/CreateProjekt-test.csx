
tp.Test("Vytvor projekt. (201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createProjekt"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});



var newFirma = tp.GetVariable<string>("newProjekt");
dynamic obj = newFirma.ToExpando();
var kod = obj.kod;

await tp.Test($"Divizia je vytvorena s timto kodom  .", async () =>
{
    dynamic responseJson = await tp.Responses["getProjekt"].GetBodyAsExpandoAsync();
    Console.WriteLine("Raw response: " + responseJson);
    // Access JSON properties case-insensitively.

    Equal(kod, (string)responseJson.kod);
});
