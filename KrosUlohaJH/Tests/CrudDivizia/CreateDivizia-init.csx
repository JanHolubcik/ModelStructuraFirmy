#load "$teapie/Definitions/CrudtestData.csx"


string kodGet = DiviziaCrud.Kod;

tp.SetVariable("newDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);
