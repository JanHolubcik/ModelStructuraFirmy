#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateProjekt-3c4fb30e4d807a34c3420ccd9abbb971\.teapie\Definitions\CrudtestData.csx"
string kodGet = ProjektCrud.Kod;
tp.SetVariable("newProjekt", ProjektCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);