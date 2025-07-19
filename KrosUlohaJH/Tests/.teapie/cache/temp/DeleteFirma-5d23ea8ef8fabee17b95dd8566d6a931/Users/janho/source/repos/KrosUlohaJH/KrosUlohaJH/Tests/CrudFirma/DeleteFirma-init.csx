#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteFirma-5d23ea8ef8fabee17b95dd8566d6a931\.teapie\Definitions\CrudtestData.csx"
string kodGet = FirmaCrud.Kod;
tp.SetVariable("newFirma", FirmaCrud.ToJsonString());
tp.SetVariable("GetFirma", kodGet);