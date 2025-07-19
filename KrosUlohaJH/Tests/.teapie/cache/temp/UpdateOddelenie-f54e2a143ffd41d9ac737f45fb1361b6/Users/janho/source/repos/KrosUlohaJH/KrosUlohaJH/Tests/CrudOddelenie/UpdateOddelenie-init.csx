#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateOddelenie-f54e2a143ffd41d9ac737f45fb1361b6\.teapie\Definitions\CrudtestData.csx"
string kodGet = OddelenieCrud.Kod;
tp.SetVariable("newOddelenie", OddelenieCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);