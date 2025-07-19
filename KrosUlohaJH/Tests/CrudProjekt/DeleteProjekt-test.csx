
tp.Test("Vytvor firmu. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createDivizia"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Vymaz firmu  (200)", async () =>
{
    var statusCode = tp.Responses["deleteDivizia"].StatusCode();

    Equal(200, statusCode);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});


