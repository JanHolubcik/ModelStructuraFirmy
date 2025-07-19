tp.Test("Updatne alebo vytvori diviziu (200,201)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateDivizia"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

var newFirma = tp.GetVariable<string>("newDivizia");
dynamic obj = newFirma.ToExpando();
var kod = obj.Kod;
var Nazov = obj.Nazov;
var FirmaId =obj.FirmaId;
var VeduciRC = obj.VeduciRC;

await tp.Test("Update by mal obsahovat pocet uspesnich 1 a neuspesnych 1, kvoli zle zadanemu rodnemu cislu.", async () =>
{

    dynamic responseJsonBulk = await tp.Responses["updateDiviziaBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getDivizia"].GetBodyAsExpandoAsync();

 
    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;

    Equal(kod, responseJsonNew.Kod);
    NotEqual(Nazov, responseJsonNew.Nazov);
    NotEqual(FirmaId, Convert.ToInt64(responseJsonNew.FirmaId));

    NotEqual(VeduciRC, responseJsonNew.VeduciRC);

    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));
});

