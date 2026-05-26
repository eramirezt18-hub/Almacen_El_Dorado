# Sistema de Gestión de Inventario - Almacén El Dorado

# Universisdad Mariano Gálvez de Guatemala

# Integrantes del equipo
- Endso Felipe Ramirez Teec 0902-25-22181
- Fredy Rudy Willson Humbler 0902-25-12915

## Descripción del proyecto
Sistema de escritorio para gestión de inventario desarrollado en C# con SQL Server. 
Permite administrar productos, categorías, proveedores y controlar entradas/salidas de inventario.

## Tecnologías utilizadas
- C# .NET Framework (WinForms)
- SQL Server
- ADO.NET para conexión a BD

## Requisitos del sistema
- Windows 7 o superior
- SQL Server Express o superior
- .NET Framework 4.8

## Instrucciones de ejecución
1. Ejecutar script de base de datos: `database/script_creacion_bd.sql`
2. Abrir solución en Visual Studio
3. Compilar y ejecutar (F5)
4. Credenciales: 
   - Usuario: `AdminSistema`
   - Contraseña: `Inventario2024`

## Funcionalidades principales
- CRUD de productos, categorías y proveedores
- Control de entradas y salidas de inventario
- Validación de stock (no permite salidas sin stock)
- Consulta de inventario con filtros por nombre, código o categoría
- Historial de movimientos

## Estructura del proyecto