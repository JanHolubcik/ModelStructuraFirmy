#load "$teapie/Definitions/CrudtestData.csx"


string kodGet = ZamestnanecCrud.RodneCislo;


tp.SetVariable("NewZamestnanec", ZamestnanecCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);