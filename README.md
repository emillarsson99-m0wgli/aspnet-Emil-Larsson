# CoreFitnessClub

Detta är en webbapplikation byggd i ASP.NET Core MVC där användare kan skapa konto, boka träningspass och hantera sitt medlemskap.

---

## Funktioner

- Registrering & inloggning
- Tvåstegs-registrering (email → lösenord)
- Profil (ändra uppgifter + profilbild)
- Medlemskap (skapa / ta bort)
- Boka och avboka träningspass
- Ta bort konto (inkl all data)

---

## Teknik

- ASP.NET Core MVC  
- Entity Framework Core (Code First)  
- SQL Server  
- ASP.NET Identity  
- xUnit (tester)

---

## Starta projektet

1. Klona repot: https://github.com/emillarsson99-m0wgli/aspnet-Emil-Larsson
 
2. Uppdatera databasen, kör: dotnet ef database update --project CoreFitnessClub.Infrastructure --startup-project CoreFitnessClub.Web

3. Starta appen, kör: dotnet run --project CoreFitnessClub.Web

4. Kör tester i "Test explorer"

## Övrigt

- Profilbilder sparas i `wwwroot/images/profiles`
- Byggt enligt Clean Architecture (Domain, Application, Infrastructure, Web)

---

Projekt gjort som skoluppgift i .NET.
