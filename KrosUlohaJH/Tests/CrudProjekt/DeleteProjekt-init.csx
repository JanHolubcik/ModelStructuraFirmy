

#load "$teapie/Definitions/CrudtestData.csx"


string kodGet = ProjektCrud.Kod;



tp.SetVariable("newProjekt", ProjektCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);
