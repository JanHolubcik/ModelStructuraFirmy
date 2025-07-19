#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateDivizia-32972dc9dccebf166101c951d0daee66\.teapie\Definitions\CrudtestData.csx"
string kodGet = DiviziaCrud.Kod;
tp.SetVariable("newDivizia", DiviziaCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);