#load "$teapie/Definitions/BulkData.csx"


tp.SetVariable("NewProjekty", projekty.ToJsonString(), "projekt");
tp.SetVariable("NewDivizie", Divizie.ToJsonString(), "divizie");
tp.SetVariable("NewFirmy", Firmy.ToJsonString(), "firmy");
tp.SetVariable("Newoddelenia", oddelenia.ToJsonString(), "oddelenia");
tp.SetVariable("NewoddeleniaRC", oddeleniaWithRC.ToJsonString(), "oddeleniaRC");
tp.SetVariable("NewZamestnanci", zamestnanci.ToJsonString(), "zamestnanci");