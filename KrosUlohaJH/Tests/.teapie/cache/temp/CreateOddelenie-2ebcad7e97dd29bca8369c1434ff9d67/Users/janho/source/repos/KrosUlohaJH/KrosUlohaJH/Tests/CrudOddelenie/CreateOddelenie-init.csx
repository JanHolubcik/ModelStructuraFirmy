#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateOddelenie-2ebcad7e97dd29bca8369c1434ff9d67\.teapie\Definitions\CrudtestData.csx"
string kodGet = OddelenieCrud.Kod;
tp.SetVariable("newOddelenie", OddelenieCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);