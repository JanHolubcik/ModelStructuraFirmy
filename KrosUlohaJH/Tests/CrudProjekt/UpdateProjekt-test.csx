tp.Test("Updatne alebo vytvory projekt (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["updateProjekt"].StatusCode();
    Contains(statusCode, new[] { 200, 201 });

});

var newFirma = tp.GetVariable<string>("newProjekt");
dynamic obj = newFirma.ToExpando();
var kod = obj.Kod;
var Nazov = obj.Nazov;
var DiviziaId = obj.DiviziaId;
var VeduciProjektuRC = obj.VeduciProjektuRC;

await tp.Test("Update by mal mat tento kod v errors SP2, lebo rodne cislo je v zlom formate. ", async () =>
{
    dynamic responseJsonBulk = await tp.Responses["updateProjektBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getProjekt"].GetBodyAsExpandoAsync();


    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;

    NotEqual(kod, responseJsonNew.Kod);
    NotEqual(Nazov, responseJsonNew.Nazov);
    NotEqual(DiviziaId, Convert.ToInt64(responseJsonNew.DiviziaId));

    NotEqual(VeduciProjektuRC, responseJsonNew.VeduciProjektuRC);

    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));
    // var statusCode = tp.Responses["updateProjekt"].StatusCode();

});

