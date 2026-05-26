-- Crear base de datos
CREATE DATABASE AlmacenElDorado;
GO

USE AlmacenElDorado;
GO

-- Tabla de Categorias
CREATE TABLE Categorias (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);
GO

-- Tabla de Proveedores
CREATE TABLE Proveedores (
    IdProveedor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(200),
    Contacto VARCHAR(100),
    Email VARCHAR(100)
);
GO

-- Tabla de Productos
CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Codigo VARCHAR(50) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    IdCategoria INT,
    IdProveedor INT,
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria),
    FOREIGN KEY (IdProveedor) REFERENCES Proveedores(IdProveedor)
);
GO

-- Tabla de Movimientos
CREATE TABLE Movimientos (
    IdMovimiento INT PRIMARY KEY IDENTITY(1,1),
    Tipo VARCHAR(10) NOT NULL CHECK (Tipo IN ('ENTRADA', 'SALIDA')),
    Cantidad INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    IdProducto INT NOT NULL,
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);
GO

-- Insertar datos de ejemplo
INSERT INTO Categorias (Nombre) VALUES ('Electronica'), ('Ropa'), ('Hogar'), ('Alimentos');
GO

INSERT INTO Proveedores (Nombre, Telefono, Direccion, Contacto, Email) 
VALUES ('ElectroTech', '12345678', 'Zona 1', 'Juan Perez', 'ventas@electrotech.com'),
       ('Moda Express', '87654321', 'Zona 4', 'Maria Lopez', 'info@modaexpress.com');
GO

INSERT INTO Productos (Codigo, Nombre, Precio, Stock, IdCategoria) 
VALUES ('PROD001', 'Laptop HP', 5000, 10, 1),
       ('PROD002', 'Mouse USB', 150, 50, 1),
       ('PROD003', 'Camisa Polo', 250, 30, 2);
GO

SELECT * FROM Categorias;
SELECT * FROM Proveedores;
SELECT * FROM Productos;
SELECT * FROM Movimientos;
GO