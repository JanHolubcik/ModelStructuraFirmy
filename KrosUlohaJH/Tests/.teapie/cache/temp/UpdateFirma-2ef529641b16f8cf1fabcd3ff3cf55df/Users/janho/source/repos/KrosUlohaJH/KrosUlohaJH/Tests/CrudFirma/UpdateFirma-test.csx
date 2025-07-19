tp.Test("Updatne alebo vytvory firmu (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateFirma"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });
});
var newFirma = tp.GetVariable<string>("newFirma");
dynamic obj = newFirma.ToExpando();
var kod = obj.Kod;
var Nazov = obj.Nazov;
var RiaditelRc = obj.RiaditelRc;
await tp.Test("Update by mal obsahovat pocet uspesnich 1 a neuspesnych 1, kvoli zle zadanemu rodnemu cislu. ", async () =>
{
     dynamic responseJsonBulk = await tp.Responses["updateFirmaBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getFirma"].GetBodyAsExpandoAsync();
    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;
    NotEqual(kod, responseJsonNew.Kod);
    NotEqual(Nazov, responseJsonNew.Nazov);
    NotEqual(RiaditelRc, responseJsonNew.RiaditelRc);
    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));
});