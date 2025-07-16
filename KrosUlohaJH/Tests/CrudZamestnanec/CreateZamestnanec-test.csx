
tp.Test("Vytvor zamestnanca. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["createZamestnanec"].StatusCode();
    Equal(200, statusCode);
});
