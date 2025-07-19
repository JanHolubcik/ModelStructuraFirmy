#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateFirma-2ef529641b16f8cf1fabcd3ff3cf55df\.teapie\Definitions\CrudtestData.csx"
string kodGet = FirmaCrud.Kod;
tp.SetVariable("newFirma", FirmaCrud.ToJsonString());
tp.SetVariable("GetFirma", kodGet);