This application consists of two applications:
- Ncx.Exam.Api: a REST API service written in ASP.NET Core 2.2.
- web-app: a SPA web site calling the APIs in Ncx.Exam.Api. This web site is written in Angular 7.

Everything is in HTTP protocol just for demo. However, make sure to change it to HTTPS for production.

To launch the Ncx.Exam.Api service:
1. Make sure .NET Core 2.2 SDK has been installed on your machine.
2. Launch a .NET Core command line shell.
3. CD to the folder: ncx-code-exam/src/Ncx.Exam.Api
4. Execute "dotnet ef database update" to create a local Sqlite DB.
5. Execute "dotnet run" and wait the service to start.
6. Open a web browser.
7. Type "http://localhost:6100/swagger/" in the address bar and press Enter. Then you should see all the APIs implemented in this service.

To launch the web-app web site:
1. Make sure Node.js and NPM are installed on your machine. Recommanded versions are Node v11.1.0 and NPM v6.4.1.
2. Make sure the Ncx.Exam.Api service is running.
3. Launch a Node.js command line shell.
4. CD to the folder: ncx-code-exam/src/web-app
5. Execute "npm install" to install all packages.
6. Execute "npm run build".
7. Execute "npm run start" and wait the web site to start.
8. A web browser should pop up automatically. If not, open it and type "http://localhost:4200" in the address bar and press Enter.