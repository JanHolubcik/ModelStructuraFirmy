tp.Test("Vytvor Oddelenie. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createOddelenie"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });
});