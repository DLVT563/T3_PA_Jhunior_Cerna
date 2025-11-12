# T3_PA_Jhunior_Cerna

Este repositorio contiene una aplicación ASP.NET Core MVC llamada **TechSolutionsApp** que implementa un sistema interno para administrar roles, usuarios, proyectos y tareas.

## Requisitos previos

* .NET 8 SDK

## Configuración

1. Ubicarse en la carpeta del proyecto:

   ```bash
   cd TechSolutionsApp
   ```

2. Restaurar paquetes y aplicar la migración inicial (crea la base de datos SQLite con los datos semilla):

   ```bash
   dotnet restore
   dotnet ef database update
   ```

3. Ejecutar la aplicación:

   ```bash
   dotnet run
   ```

4. Ingresar con el usuario administrador precargado:

   * Correo: `admin@techsolutions.com`
   * Contraseña: `Admin123!`

Desde la interfaz se pueden gestionar los usuarios, proyectos y tareas según los requerimientos del enunciado.
