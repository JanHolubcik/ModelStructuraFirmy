tp.Test("Updatne alebo vytvory zamestnanca (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateProjekt"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

 await tp.Test("Update by mal mat tento kod v errors SP2, lebo rodne cislo je v zlom formate. ", async () =>
{
    dynamic responseJson = await tp.Responses["updateProjektBulk"].GetBodyAsExpandoAsync();
    string kod = responseJson.chyby[0].kod;

    Equal("SP2", kod);
   // var statusCode = tp.Responses["updateProjekt"].StatusCode();

});

