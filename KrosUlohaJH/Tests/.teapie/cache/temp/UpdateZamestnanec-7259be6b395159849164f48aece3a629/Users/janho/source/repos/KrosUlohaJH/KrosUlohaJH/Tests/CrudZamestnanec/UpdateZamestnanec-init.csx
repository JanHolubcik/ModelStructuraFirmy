#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\UpdateZamestnanec-7259be6b395159849164f48aece3a629\.teapie\Definitions\CrudtestData.csx"
string kodGet = ZamestnanecCrud.RodneCislo;
tp.SetVariable("NewZamestnanec", ZamestnanecCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet); 