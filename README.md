# Project plan

# Slovak version
## Prvý semester

### Cieľ: funkčný prototyp so sledovaním a rolami

### Výskum domény

0.1 Analyzovať tok dát zo smart merača/panelu a možnosti komunikácie.
0.2 Definovať roly a hlavné funkcie systému.
0.3 Navrhnúť základnú architektúru systému.

### Lokálna databáza SQL Server (Code First + migrácie)

1.1 Navrhnúť tabuľky pre používateľov, merače a odpočty.
1.2 Nastaviť Entity Framework Core a migrácie.
1.3 Pridať počiatočné testovacie dáta (seed).

### Štruktúra MVC aplikácie

2.1 Vytvoriť projekt ASP.NET Core MVC.
2.2 Rozdeliť projekt na oblasti (areas).

### Smerovanie (routing)

3.1 Definovať trasy pre oblasť klienta.
3.2 Definovať trasy pre oblasť zamestnanca.
3.3 Zachovať čistú štruktúru URL pre hlavné stránky.

### Kontroléry v oblasti Admin

4.1 Zobraziť zoznam klientov a ich meračov.
4.2 Zobraziť detaily merača a jeho odpočty.

### Kontroléry v oblasti Client

5.1 Vytvoriť klientské rozhranie s priradenými meračmi.
5.2 Zobraziť históriu spotreby.

### HTML rozhranie

6.1 Vytvoriť spoločné rozloženie (header, navigácia, footer).
6.2 Pripraviť HTML stránky pre oblasť zamestnanca.
6.3 Pripraviť HTML stránky pre oblasť klienta.

### Základný frontend

7.1 Použiť CSS framework (napr. Bootstrap).
7.2 Zabezpečiť základný responzívny dizajn.

### Modul fakturácie a platieb

8.1 Pripraviť iba simuláciu.

## Druhý semester

### Cieľ: monitorovanie v reálnom čase, bezpečnosť, finálne úpravy

### Overovanie a autorizácia

1.1 Integrovať ASP.NET Core Identity.
1.2 Definovať roly Klient / Zamestnanec.
1.3 Použiť riadenie prístupu podľa rolí.

### Modul monitorovania v reálnom čase

2.1 Implementovať gRPC streamovanie z merača/emulátora na server.
2.2 Ukladať prichádzajúce odpočty do databázy.
2.3 Zabezpečiť živé aktualizácie dashboardu (SignalR alebo gRPC-Web).

### Bezpečnosť a logovanie

3.1 Overovať vstupy.
3.2 Logovať kritické operácie a chyby.
3.3 Vynútiť HTTPS a chrániť citlivé akcie.

### Testovanie

4.0 Otestovať celý systém.

(Voliteľné) vylepšenia UI a analytiky

5.1 Zobraziť grafy dennej/mesačnej/ročnej spotreby.
5.2 Pripraviť prehľadné zobrazenia a jednoduché štatistiky pre používateľov.

## Technológie

Backend: C#, .NET 8, ASP.NET Core MVC
Databáza: Microsoft SQL Server, EF Core (Code First, Migrations)
Frontend: Razor Views, HTML5, CSS3, Bootstrap, JavaScript (voliteľné), Chart.js (voliteľné)
Reálny čas: gRPC (merač → server), SignalR alebo gRPC-Web (server → prehliadač)
Bezpečnosť: ASP.NET Core Identity, roly, [Authorize]
Nástroje: Visual Studio / Rider / VS Code, Git

# English version
## First Semester

### Goal: working prototype with monitoring and roles.

### Domain research

0.1 Analyse smart meter/panel data flow and communication options.
0.2 Define roles and key system features.
0.3 Draft the basic system architecture.

### Local SQL Server database (Code First + migrations)

1.1 Design tables for users, meters, and readings.
1.2 Configure Entity Framework Core and migrations.
1.3 Seed initial test data.

### MVC application structure

2.1 Create an ASP.NET Core MVC project.
2.2 Split the project into areas.

### Routing

3.1 Define routes for the client area.
3.2 Define routes for the employee/admin area.
3.3 Keep a clean URL structure for main pages.

### Admin area controllers

4.1 Display a list of clients and their meters.
4.2 View meter details and readings.

### Client area controllers

5.1 Create a client dashboard with assigned meters.
5.2 View consumption history.

### HTML interface

6.1 Create a shared layout.
6.2 Implement HTML pages for the employee area.
6.3 Implement HTML pages for the client area.

### Basic frontend

7.1 Use a CSS framework (e.g. Bootstrap).
7.2 Ensure basic responsive design.

### Billing and payment module

8.1 Provide a simulation only.

## Second Semester

### Goal: real-time monitoring, security, final polishing.

### Authentication and authorization

1.1 Integrate ASP.NET Core Identity.
1.2 Define Client / Admin roles.
1.3 Apply role-based access control.

### Real-time monitoring module

2.1 Implement gRPC streaming from meter/emulator to server.
2.2 Store incoming readings in the database.
2.3 Provide live dashboard updates (SignalR or gRPC-Web).

### Security and logging

3.1 Implement input validation.
3.2 Log critical operations and errors.
3.3 Enforce HTTPS and protect sensitive actions.

### Testing

4.0 Test the entire system.

(Optional) UI and analytics improvements

5.1 Add charts for daily/monthly/yearly usage.
5.2 Provide clear overviews and simple stats for users.

## Technologies

Backend: C#, .NET 8, ASP.NET Core MVC
Database: Microsoft SQL Server, EF Core (Code First, Migrations)
Frontend: Razor Views, HTML5, CSS3, Bootstrap, JavaScript (optional), Chart.js (optional)
Real-time: gRPC (meter → server), SignalR or gRPC-Web (server → browser)
Security: ASP.NET Core Identity, roles, [Authorize]
Tools: Visual Studio / Rider / VS Code, Git
