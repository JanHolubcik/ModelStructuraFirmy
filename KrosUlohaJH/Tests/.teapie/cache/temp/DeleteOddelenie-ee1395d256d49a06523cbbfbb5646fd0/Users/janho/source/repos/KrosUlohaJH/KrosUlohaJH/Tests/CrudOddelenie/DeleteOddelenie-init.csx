#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteOddelenie-ee1395d256d49a06523cbbfbb5646fd0\.teapie\Definitions\CrudtestData.csx"
string kodGet = OddelenieCrud.Kod;
tp.SetVariable("newOddelenie", OddelenieCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);