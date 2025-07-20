tp.Test("Update zamestnanca failne, lebo sa snazim updatnut neexistujuce zamestnanca (200)", () =>
{
    // Access named responses using their names.
    var statusCode = tp.Responses["newZamestnanec"].StatusCode();
    Equal(200, statusCode);
    tp.Logger.LogInformation("Test prebehol uspesne.");
});

var newFirma = tp.GetVariable<string>("NewZamestnanec");
dynamic obj = newFirma.ToExpando();

var Priezvisko = obj.Priezvisko;
var meno = obj.Meno;
var Email = obj.Email;
var Titul = obj.Titul;



await tp.Test("Update by mal obsahovat pocet uspesnich 1 a neuspesnych 1, kvoli zle zadanemu rodnemu cislu. ", async () =>
{
    dynamic responseJsonBulk = await tp.Responses["updateZamestnanecBulk"].GetBodyAsExpandoAsync();
    dynamic responseJsonNew = await tp.Responses["getUser"].GetBodyAsExpandoAsync();

    var uspesne = responseJsonBulk.uspesne;
    var neuspesne = responseJsonBulk.neuspesne;

    NotEqual(Priezvisko, responseJsonNew.Priezvisko);
    NotEqual(Email, responseJsonNew.Email);
    NotEqual(Titul, responseJsonNew.Titul);
    NotEqual(Email, responseJsonNew.Email);

    Equal(1L, Convert.ToInt64(uspesne));
    Equal(1L, Convert.ToInt64(neuspesne));

});



