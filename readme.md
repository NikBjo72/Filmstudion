# Filmstudion
Ett API för att kunna hyra filmer samt ett enklare webbaserat klientgränssnitt

När du startar applikationen så seedas några filmer samt filmkopior.
Ingen seedad användare finns, så först måste användare (admin och filmstudio) registreras, sedan måste de autentiseras för att hämta en token. Loggar en filmstudio in med användargränssnittet, så hämtas token automatiskt.

*** Köra applikationen ***
1. Installera ASP.NET Core 5.0
2. Öppna applikationen i valfri codeditor.
3. I terminalen gå till mappen Filmstudion/src/SFF.API och skriv: dotnet run
4. Öppna https://localhost:[portnummer] i valfri webbläsare. Portnumret och hela URL:en visas i terminalfönstret när servern startar.
5. UI ligger i wwwwroot och index.html startar direkt på ovan adress.
6. skriver du /swagger efter URL:en så kommer du till en enklare dokumentation av API:et.
7. Det klientgränssnittet (webbapplikation) är mobilampassat.
8. God utvärdering!