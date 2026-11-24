# Ejercicios MVC

Ejercicios de arquitectura MVC hechos con ASP.NET Core 10, Entity Framework Core (Code First) y SQL Server.

## Ejercicios

| # | Ejercicio | Controlador | Qué incluye |
|---|---|---|---|
| 1 | Lista de tareas | `ToDoTasksController` | Agregar, completar y eliminar tareas |
| 2 | Calculadora de propinas | `TipCalculatorController` | Cálculo con validaciones, sin base de datos |
| 3 | Generador de contraseñas | `PasswordGeneratorController` | Longitud y tipos de caracteres, azar criptográfico |
| 4 | Gestor de gastos | `ExpensesController` | Registro por categoría y resumen mensual |
| 5 | Sistema de reservas | `ReservationsController` | Disponibilidad, reserva, confirmación y cancelación |
| 6 | Gestor de notas | `NotesController`, `NoteCategoriesController` | Categorías, búsqueda y edición |
| 8 | Plataforma de recetas | `RecipesController` | Filtros, favoritas y compartir |
| 9 | Juego de memoria | `MemoryGameController` | Niveles de dificultad y tabla de puntajes |
| 11 | Cronómetro | `StopwatchController` | Inicio, pausa, reinicio y vueltas |

## Cómo ejecutarlo

1. Revisar la cadena de conexión `DefaultConnection` en `EjerciciosMVC.Web/appsettings.json` y poner el nombre del servidor de SQL Server.
2. En la Consola del Administrador de paquetes de Visual Studio, ejecutar:
   ```
   Update-Database
   ```
   Esto crea la base de datos `EjerciciosMVC` con todas las tablas y los datos iniciales (categorías, servicios y recetas de ejemplo).
3. Ejecutar el proyecto con F5.

## Estructura

- `Controllers`: un controlador por ejercicio.
- `Models`: los ViewModels que usan las vistas y los formularios.
- `Data/Entities`: las entidades, una por tabla.
- `Data/DataContext.cs`: la base de datos, sus tablas, relaciones y datos iniciales.
- `Migrations`: las migraciones de Entity Framework.
- `Views`: una carpeta por controlador.
- `wwwroot/js`: el JavaScript del cronómetro y del juego de memoria.
