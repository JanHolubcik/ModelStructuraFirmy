tp.Test("Updatne alebo vytvory firmu (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateFirma"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Error by mal mat v sebe kod Tatra, lebo rodne cislo je v zlom formate. ", async () =>
{
     dynamic responseJson = await tp.Responses["updateFirmaBulk"].GetBodyAsExpandoAsync();
     string kod = responseJson.chyby[0].kod;
    Equal("Tatra", kod);
});

