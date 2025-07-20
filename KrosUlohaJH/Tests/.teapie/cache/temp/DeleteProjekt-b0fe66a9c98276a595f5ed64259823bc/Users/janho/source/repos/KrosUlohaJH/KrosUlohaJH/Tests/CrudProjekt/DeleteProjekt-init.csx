#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteProjekt-b0fe66a9c98276a595f5ed64259823bc\.teapie\Definitions\CrudtestData.csx"
string kodGet = ProjektCrud.Kod;
tp.SetVariable("newProjekt", ProjektCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);