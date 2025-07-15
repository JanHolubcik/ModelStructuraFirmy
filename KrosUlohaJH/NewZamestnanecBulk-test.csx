tp.Test("This user already exists. (400)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["newZamestnanecBulk"].StatusCode();
    Equal(400, statusCode);
});