
tp.Test("Vytvor Oddelenie. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createOddelenie"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Vymaz Oddelenie (200)", async () =>
{
    var statusCode = tp.Responses["deleteOddelenie"].StatusCode();
    var statusCodeGet = tp.Responses["getOddelenie"].StatusCode();
    Equal(200, statusCode);
    Equal(404, statusCodeGet);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});


