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
tp.Test("Vytvor Firmy. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["NewFirmyBulk"].StatusCode();
    Equal(200, statusCode);
});
tp.Test("Vytvor Divizie. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["NewDivizieBulk"].StatusCode();
    Equal(200, statusCode);
});
tp.Test("Vytvor Projekty. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["NewProjektyBulk"].StatusCode();
    Equal(200, statusCode);
});
tp.Test("Aktualizuj oddelenia. (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["NewOddeleniaRCBulk"].StatusCode();
    Equal(200, statusCode);
});