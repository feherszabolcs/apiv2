# Plander v2 API (WIP)

## Leírás
Az újjádolgozott Plander (Polgárőrségnek szánt naplózó és szolgálattervező) ASP.NET nyelven írt backendje. Adatbázisnak jelenleg MSSQL van használva. Fejlesztés alatt lévő projekt
lévén találhatóak benne még *hard-codeolt URL*-ek. 

## Funkciók
- Regisztráció
- Bejelentkezés + hitelesítés
- Jogosultságok megkülönböztetése
- Email küldés
- Egyesületek listázása, felvétele
- ***Jővőben bővűl...***

## Futtatás lokálisan
Amennyiben lokális adatbázis szerver rendelkezésre áll: *dotnet-ef database update*, ezt követően pedig a *dotnet watch run* paranccsal hallgatni fog a kijelzett porton a 
backend szerverünk.
