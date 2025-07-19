
#load "$teapie/Definitions/CrudtestData.csx"


string kodGet = OddelenieCrud.Kod;

tp.SetVariable("newOddelenie", OddelenieCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);
