#load "C:\Users\janho\source\repos\KrosUlohaJH\KrosUlohaJH\Tests\.teapie\cache\temp\InicializaciaDatabazyBulk-6718f60bfca94967bd03818872d3ec74\.teapie\Definitions\BulkData.csx"
tp.SetVariable("NewProjekty", projekty.ToJsonString(), "projekt");
tp.SetVariable("NewDivizie", Divizie.ToJsonString(), "divizie");
tp.SetVariable("NewFirmy", Firmy.ToJsonString(), "firmy");
tp.SetVariable("Newoddelenia", oddelenia.ToJsonString(), "oddelenia");
tp.SetVariable("NewoddeleniaRC", oddeleniaWithRC.ToJsonString(), "oddeleniaRC");
tp.SetVariable("NewZamestnanci", zamestnanci.ToJsonString(), "zamestnanci");