tp.Test("Updatne alebo vytvory zamestnanca (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateFirma"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Update by mal mat tento kod v errors KR, lebo sa snazim pridat sefa viacerim firmam. ", async () =>
{
     dynamic responseJson = await tp.Responses["updateFirmaBulk"].GetBodyAsExpandoAsync();
     string kod = responseJson.chyby[0].kod;
    Equal("KR", kod);



});

