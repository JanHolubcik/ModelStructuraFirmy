tp.Test("This user already exists. (409)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["skuska"].StatusCode();
    Equal(409, statusCode);
});