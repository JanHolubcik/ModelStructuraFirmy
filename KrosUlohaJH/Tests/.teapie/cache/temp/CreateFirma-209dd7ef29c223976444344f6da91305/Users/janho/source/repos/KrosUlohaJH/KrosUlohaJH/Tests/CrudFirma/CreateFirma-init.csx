#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateFirma-209dd7ef29c223976444344f6da91305\.teapie\Definitions\CrudtestData.csx"
string kodGet = FirmaCrud.Kod;
tp.SetVariable("newFirma", FirmaCrud.ToJsonString());
tp.SetVariable("GetFirma", kodGet);