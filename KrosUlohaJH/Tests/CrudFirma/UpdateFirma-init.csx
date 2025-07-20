#load "$teapie/Definitions/CrudtestData.csx"


string kodGet = FirmaCrud.Kod;



tp.SetVariable("newFirma", FirmaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);
