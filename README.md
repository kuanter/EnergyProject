ProjektovýPlán

Prvý semester

Cieľ: funkčný prototyp so sledovaním a rolami

Výskum domény

0.1 Analyzovať tok dát zo smart merača/panelu a možnosti komunikácie.
0.2 Definovať roly (klient, zamestnanec) a hlavné funkcie systému.
0.3 Navrhnúť základnú architektúru systému.

Lokálna databáza SQL Server (Code First + migrácie)

1.1 Navrhnúť tabuľky pre používateľov, merače a odpočty.
1.2 Nastaviť Entity Framework Core a migrácie.
1.3 Pridať počiatočné testovacie dáta (seed).

Štruktúra MVC aplikácie

2.1 Vytvoriť projekt ASP.NET Core MVC.
2.2 Rozdeliť projekt na oblasti (areas).

Smerovanie (routing)

3.1 Definovať trasy pre oblasť klienta (/Client).
3.2 Definovať trasy pre oblasť zamestnanca (/Admin).
3.3 Zachovať čistú štruktúru URL pre hlavné stránky.

Kontroléry v oblasti Admin

4.1 Zoznam klientov a ich meračov.
4.2 Zobraziť detaily merača a jeho odpočty.

Kontroléry v oblasti Client

5.1 Klientské rozhranie s priradenými meračmi.
5.2 Zobraziť históriu spotreby.

HTML rozhranie

6.1 Spoločné rozloženie (header, navigácia, footer).
6.2 HTML stránky pre oblasť zamestnanca.
6.3 HTML stránky pre oblasť klienta.

Základný frontend

7.1 Použiť CSS framework (napr. Bootstrap).
7.2 Zabezpečiť základný responzívny dizajn.

Modul fakturácie a platieb.
8.1 Iba simulácia.

Druhý semester

Cieľ: monitorovanie v reálnom čase, bezpečnosť, finálne úpravy

Overovanie a autorizácia

1.1 Integrovať ASP.NET Core Identity.
1.2 Definovať roly Klient / Zamestnanec.
1.3 Použiť riadenie prístupu podľa rolí (role-based access control).

Modul monitorovania v reálnom čase

2.1 Implementovať gRPC streamovanie z merača/emulátora na server.
2.2 Ukladať prichádzajúce odpočty do databázy.
2.3 Živé aktualizácie dashboardu (SignalR alebo gRPC-Web).

Bezpečnosť a logovanie

3.1 Overovanie vstupov (input validation).
3.2 Logovanie kritických operácií a chýb.
3.3 Vynútiť HTTPS a chrániť citlivé akcie.

Testovanie

4.0 Testovanie celého systému.

?Vylepšenia UI a analytiky

?5.1 Grafy dennej/mesačnej/ročnej spotreby.
?5.2 Prehľadné zobrazenia a jednoduché štatistiky pre používateľov.

Technológie 

Backend: C#, .NET 8, ASP.NET Core MVC
Databáza: Microsoft SQL Server, EF Core (Code First, Migrations)
Frontend: Razor Views, HTML5, CSS3, Bootstrap, ?JavaScript?, ?Chart.js?
Reálny čas: gRPC (merač → server), SignalR alebo gRPC-Web (server → prehliadač) ?
Bezpečnosť: ASP.NET Core Identity, role, [Authorize]
Nástroje: Visual Studio / Rider / VS Code, Git



English version


First Semester
Goal: working prototype with monitoring and roles.

Domain research
0.1 Analyse smart meter/panel data flow and communication options.
0.2 Define roles (client, employee) and key system features.
0.3 Draft the basic system architecture.

Local SQL Server database (Code First + migrations)
1.1 Design tables for users, meters, and readings.
1.2 Configure Entity Framework Core and migrations.
1.3 Seed initial test data.

MVC application structure
2.1 Create ASP.NET Core MVC project.
2.2 Split into areas.

Routing
3.1 Define routes for client area (/Client).
3.2 Define routes for employee area (/Admin).
3.3 Keep a clean URL structure for main pages.

Admin area controllers
4.1 List clients and their meters.
4.2 View meter details and readings.

Client area controllers
5.1 Client dashboard with assigned meters.
5.2 View consumption history....

HTML interface
6.1 Shared layout (header, navigation, footer).
6.2 HTML pages for employee area.
6.3 HTML pages for client area.

Basic frontend
7.1 Use CSS framework (e.g. Bootstrap).
7.2 Basic responsive design.

Billing and payment module
8.1 Simulation only

Second Semester
Goal: real-time monitoring, security, final polishing.

Authentication and authorization
1.1 Integrate ASP.NET Core Identity.
1.2 Define Client / Employee roles.
1.3 Apply role-based access control.

Real-time monitoring module
2.1 Implement gRPC streaming from meter/emulator to server.
2.2 Store incoming readings in the database.
2.3 Live dashboard updates (SignalR or gRPC-Web).

Security and logging
3.1 Input validation.
3.2 Log critical operations and errors.
3.3 Enforce HTTPS and protect sensitive actions.

4.0 Testing

?UI and analytics improvements
?5.1 Charts for daily/monthly/yearly usage.
?5.2 Clear overviews and simple stats for users.

Technologies 

Backend: C#, .NET 8, ASP.NET Core MVC
Database: Microsoft SQL Server, EF Core (Code First, Migrations)
Frontend: Razor Views, HTML5, CSS3, Bootstrap, ?JavaScript?, ?Chart.js?
Real-time: gRPC (meter → server), SignalR or gRPC-Web (server → browser) ?
Security: ASP.NET Core Identity, roles, [Authorize]
Tools: Visual Studio / Rider / VS Code, Git
