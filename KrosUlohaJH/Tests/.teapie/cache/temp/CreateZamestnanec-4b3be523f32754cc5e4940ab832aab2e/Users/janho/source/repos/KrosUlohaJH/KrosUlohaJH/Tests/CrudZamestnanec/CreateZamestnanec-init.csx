#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\CreateZamestnanec-4b3be523f32754cc5e4940ab832aab2e\.teapie\Definitions\CrudtestData.csx"
string kodGet = ZamestnanecCrud.RodneCislo;
tp.SetVariable("NewZamestnanec", ZamestnanecCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet);