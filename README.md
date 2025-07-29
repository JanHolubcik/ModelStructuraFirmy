# Štruktúra firmy

## Prerequisites
.Net 9.0

Použité technológie: **C# (ASP.NET)**,  **Microsoft SQL Server** a **TeaPie** (testovanie).

Predtým ako sa spustí inicializácia, by sa mala vytvoriť databáza cez:


```sh
update-database
```
Pre spustenie testov treba nainštalovať Teapie: https://github.com/Kros-sk/TeaPie?tab=readme-ov-file

Inicializácia databázy

```sh
teapie test KrosUlohaJH\Tests\InitTestBulk\InicializaciaDatabazyBulk-req.http 
```


Testy zamestnanec
 ```sh
teapie test KrosUlohaJH\Tests\CrudZamestnanec\CreateZamestnanec-req.http
teapie test KrosUlohaJH\Tests\CrudZamestnanec\DeleteZamestnanec-req.http
teapie test KrosUlohaJH\Tests\CrudZamestnanec\UpdateZamestnanec-req.http
```

Testy oddelenie
 ```sh
teapie test KrosUlohaJH\Tests\CrudOddelenie\CreateOddelenie-req.http
teapie test KrosUlohaJH\Tests\CrudOddelenie\DeleteOddelenie-req.http
teapie test KrosUlohaJH\Tests\CrudOddelenie\UpdateOddelenie-req.http
```

Testy projekt
 ```sh
teapie test KrosUlohaJH\Tests\CrudProjekt\CreateProjekt-req.http
teapie test KrosUlohaJH\Tests\CrudProjekt\DeleteProjekt-req.http
teapie test KrosUlohaJH\Tests\CrudProjekt\UpdateProjekt-req.http
```

Testy divízia
 ```sh
teapie test KrosUlohaJH\Tests\CrudDivizia\CreateDivizia-req.http
teapie test KrosUlohaJH\Tests\CrudDivizia\DeleteDivizia-req.http
teapie test KrosUlohaJH\Tests\CrudDivizia\UpdateDivizia-req.http
```

Testy firma
 ```sh
teapie test KrosUlohaJH\Tests\CrudFirma\CreateFirma-req.http
teapie test KrosUlohaJH\Tests\CrudFirma\DeleteFirma-req.http
teapie test KrosUlohaJH\Tests\CrudFirma\UpdateFirma-req.http
```
