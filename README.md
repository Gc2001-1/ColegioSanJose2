-----------------------Colegio San José - Sistema de Gestión Académica----------------------------

📘 Descripción general
------------------------
El sistema Colegio San José es una aplicación web desarrollada con ASP.NET Core MVC y Entity Framework Core, diseñada para gestionar la información académica de alumnos, materias y expedientes (notas finales y observaciones).
Permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) y generar reportes como el promedio de notas por alumno.

⚙️ Tecnologías utilizadas
---------------------------
*ASP.NET Core MVC (.NET 6 / .NET 7) → arquitectura basada en Modelo - Vista - Controlador, que separa la lógica de negocio, la presentación y el acceso a datos.

*Entity Framework Core → ORM que facilita la comunicación con la base de datos usando clases y modelos C#.

*SQL Server LocalDB → base de datos ligera y local, ideal para desarrollo y pruebas.

*Visual Studio Community → entorno de desarrollo integrado (IDE) usado para crear, compilar y ejecutar el proyecto.

🗄️ Base de datos y conexión
-----------------------------
El sistema utiliza una base de datos SQL Server LocalDB, configurada a través de Entity Framework Core.
La conexión se define en el archivo appsettings.json:

|"ConnectionStrings":                                                                                                                    |
|  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ColegioSanJoseDB;Trusted_Connection=True;MultipleActiveResultSets=true" |
|  }                                                                                                                                     |
|----------------------------------------------------------------------------------------------------------------------------------------|

El contexto de datos (ApplicationDbContext) gestiona tres tablas principales:
Alumnos
Materias
Expedientes
Las migraciones y la creación de la base de datos se realizan con los comandos:
|  Add-Migration InitialCreate |
|Update-Database               |
-------------------------------                    

Funcionalidad del sistema
*****************************
El sistema permite:

📋 Administrar alumnos (nombre, apellido, fecha de nacimiento, grado).

📚 Administrar materias (nombre y docente responsable).

🗃️ Gestionar expedientes académicos, asociando alumnos con materias y asignando notas finales y observaciones.

📈 Visualizar promedios de notas por alumno, mostrando la cantidad de materias cursadas y su promedio general.

Módulos principales
Módulo	Descripción
Alumnos	Alta, baja, modificación y listado de estudiantes.
Materias	Gestión de asignaturas y docentes.
Expedientes	Registro de notas y observaciones por alumno y materia.
Promedios	Cálculo y visualización del promedio de notas por alumno.

🧠 Ventajas del diseño y arquitectura
**************************************
Arquitectura MVC
Permite una separación clara entre la interfaz (Vistas), la lógica de negocio (Controladores) y el modelo de datos (Modelos), lo que mejora la organización y mantenibilidad del código.

Uso de Entity Framework Core
Simplifica la conexión con la base de datos mediante migraciones automáticas y consultas LINQ, evitando la necesidad de escribir SQL manualmente.

Escalabilidad y reutilización
La estructura modular (Modelos, Controladores y Vistas) facilita la ampliación del sistema con nuevos módulos o reportes sin afectar las partes existentes.

Desarrollo rápido con scaffolding
Visual Studio permite generar automáticamente controladores y vistas CRUD, reduciendo el tiempo de desarrollo y evitando errores repetitivos.

Reportes dinámicos
La vista de Promedio por alumno muestra datos calculados directamente desde la base de datos, demostrando el potencial de EF Core para generar estadísticas.

Ejecución del proyecto

Abrir el proyecto en Visual Studio Community.

Configurar la cadena de conexión en appsettings.json.

Crear la base de datos











