
tp.Test("Vytvor projekt. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["deleteProjekt"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

await tp.Test("Vymaz projekt  (200)", async () =>
{
    var statusCode = tp.Responses["deleteProjekt"].StatusCode();
    var statusCodeGet = tp.Responses["getProjekt"].StatusCode();
    Equal(200, statusCode);
    Equal(404, statusCodeGet);

});


