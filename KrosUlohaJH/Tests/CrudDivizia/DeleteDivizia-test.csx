
tp.Test("Vytvor diviziu. (200,201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createDivizia"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Vymaz diviziu  (200)", async () =>
{
    var statusCode = tp.Responses["deleteDivizia"].StatusCode();
    var statusCodeGet = tp.Responses["getDivizia"].StatusCode();
    Equal(200, statusCode);
    Equal(404, statusCodeGet);
});


