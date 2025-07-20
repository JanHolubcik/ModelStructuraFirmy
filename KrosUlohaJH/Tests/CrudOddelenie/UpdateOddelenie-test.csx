tp.Test("Updatne alebo vytvory zamestnanca (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateOddelenie"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

var newFirma = tp.GetVariable<string>("newOddelenie");
dynamic obj = newFirma.ToExpando();
var kod = obj.Kod;
var Nazov = obj.Nazov;
var ProjektId = obj.ProjektId;
var VeduciOddeleniaRc = obj.VeduciOddeleniaRc;

await tp.Test("Update by mal obsahovat pocet uspesnich 1 a neuspesnych 1, kvoli zle zadanemu rodnemu cislu.  ", async () =>
{
    dynamic responseJsonBulk = await tp.Responses["updateOddelenieBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getOddelenie"].GetBodyAsExpandoAsync();


    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;

    Equal(kod, responseJsonNew.Kod);
    NotEqual(Nazov, responseJsonNew.Nazov);
    NotEqual(ProjektId, Convert.ToInt64(responseJsonNew.ProjektId));

    NotEqual(VeduciOddeleniaRc, responseJsonNew.VeduciOddeleniaRc);

    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));
});

