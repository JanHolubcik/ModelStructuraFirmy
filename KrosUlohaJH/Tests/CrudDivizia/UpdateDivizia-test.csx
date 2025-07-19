tp.Test("Updatne alebo vytvory zamestnanca (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateDivizia"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Update prejde len jeden lebo rodne cislo je v zlom formate.", async () =>
{
    dynamic responseJson = await tp.Responses["updateDiviziaBulk"].GetBodyAsExpandoAsync();
    string kod = responseJson.chyby[0].kod;
    Equal("KR", kod);
});

