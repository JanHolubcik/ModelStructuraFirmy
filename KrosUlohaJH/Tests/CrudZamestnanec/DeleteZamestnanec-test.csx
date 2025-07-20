
tp.Test("Vytvor zamestnanca. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createZamestnanec"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Vymaz zamestnanca  (200)", async () =>
{
    var statusCode = tp.Responses["deleteUser"].StatusCode();
    var statusCodeGet = tp.Responses["getUser"].StatusCode();

    Equal(200, statusCode);
    Equal(404, statusCodeGet);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});


