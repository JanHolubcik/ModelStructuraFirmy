#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateDivizia-7f6c8750b9c80713e462174a8be331f5\.teapie\Definitions\CrudtestData.csx"
string kodGet = DiviziaCrud.Kod;
tp.SetVariable("newDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);