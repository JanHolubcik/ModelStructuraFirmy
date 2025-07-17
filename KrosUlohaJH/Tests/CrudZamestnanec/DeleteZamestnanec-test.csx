
tp.Test("Vytvor zamestnanca. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createZamestnanec"].StatusCode();
    Equal(201, statusCode);

});

await tp.Test("Vymaz zamestnanca  (200)", async () =>
{
    var statusCode = tp.Responses["deleteUser"].StatusCode();

    Equal(200, statusCode);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});


