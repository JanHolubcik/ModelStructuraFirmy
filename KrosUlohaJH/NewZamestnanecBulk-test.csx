


tp.Test("Vytvor oddelenia. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["newOddeleniaBulk"].StatusCode();
    Equal(200, statusCode);
});

tp.Test("Vytvor Zamestnancov. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["newZamestnanecBulk"].StatusCode();
    Equal(200, statusCode);
});

