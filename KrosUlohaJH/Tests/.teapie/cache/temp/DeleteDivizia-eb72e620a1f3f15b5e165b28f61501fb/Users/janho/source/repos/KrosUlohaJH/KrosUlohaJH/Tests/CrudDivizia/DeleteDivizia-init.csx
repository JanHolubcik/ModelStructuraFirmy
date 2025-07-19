#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteDivizia-eb72e620a1f3f15b5e165b28f61501fb\.teapie\Definitions\CrudtestData.csx"
string kodGet = DiviziaCrud.Kod;
tp.SetVariable("newDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);