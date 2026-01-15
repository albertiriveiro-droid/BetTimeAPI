BetTimeAPI

BetTimeAPI es una plataforma de apuestas deportivas desarrollada con .NET 6 en el backend y React en el frontend. Permite a los usuarios registrarse, gestionar su cuenta, apostar en partidos y consultar estadísticas de los mismos.

El frontend de la aplicación está disponible en: BetTime Frontend

⚡ Funcionalidades

Para usuarios:

Registrarse e iniciar sesión en la plataforma.

Depositar y retirar dinero de su cuenta.

Actualizar los datos personales desde su perfil.

Consultar todas sus transacciones y apuestas realizadas.

Visualizar estadísticas de partidos, incluyendo los pendientes y los ya determinados.

Apostar a cualquier partido disponible.

Para administradores

Crear, actualizar y eliminar:

Partidos

Equipos

Jugadores

Ligas

Mercados de apuestas

Visualizar estadísticas detalladas de cada partido y de los mercados.

Gestionar apuestas y verificar resultados.

🧑‍💻 Credenciales de administrador

Para probar la aplicación como administrador:

Email: admin@hotmail.es
Password: pass456

🛠 Tecnologías utilizadas

Backend: .NET 6, Entity Framework Core, SQL Server

Frontend: React, React Router, Typescript, HTML, CSS.

Autenticación: JWT (JSON Web Tokens)

Hosting: Azure App Service (API) / Azure Static Web Apps (Frontend)

🔗 Enlaces

Frontend: https://agreeable-pond-0c04b7003.6.azurestaticapps.net/

API: https://bettimeapi-hcaphugjhkabgack.westeurope-01.azurewebsites.net/swagger/index.html (ver logs y endpoints desde Swagger)



📊 Notas adicionales

Todas las rutas del frontend están protegidas según el rol del usuario.

El sistema de odds dinámicas calcula probabilidades de mercado y jugador de manera realista.

La aplicación está diseñada para ser escalable y segura, con autenticación JWT y control de roles.
