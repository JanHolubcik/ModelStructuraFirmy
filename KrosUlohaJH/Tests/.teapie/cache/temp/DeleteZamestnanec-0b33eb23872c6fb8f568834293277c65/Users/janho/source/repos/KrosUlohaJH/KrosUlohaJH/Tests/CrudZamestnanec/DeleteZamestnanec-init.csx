#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\DeleteZamestnanec-0b33eb23872c6fb8f568834293277c65\.teapie\Definitions\CrudtestData.csx"
string kodGet = ZamestnanecCrud.RodneCislo;
tp.SetVariable("NewZamestnanec", ZamestnanecCrud.ToJsonString());
tp.SetVariable("GetKod", kodGet); 