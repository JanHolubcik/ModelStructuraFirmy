#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateProjekt-defa16aa25f1542fb4d243cd3b41a50c\.teapie\Definitions\CrudtestData.csx"
string kodGet = ProjektCrud.Kod;
tp.SetVariable("newProjekt", ProjektCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);