CREATE DATABASE Proyecto;
GO

USE Proyecto;
GO

CREATE TABLE Categoria (
    IDCategoria BIGINT PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Roles (
    IDRol INT PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Usuarios (
    IDUsuario BIGINT PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Apellidos NVARCHAR(255) NOT NULL,
    Contraseña NVARCHAR(255) NOT NULL,
    CorreoElectronico NVARCHAR(255) NOT NULL,
    IDRol INT,
    FOREIGN KEY (IDRol) REFERENCES Roles(IDRol)
);
GO

CREATE TABLE Producto (
    IDProducto BIGINT PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(18, 2) NOT NULL,
    Stock INT NOT NULL,
    IDCategoria BIGINT,
    FOREIGN KEY (IDCategoria) REFERENCES Categoria(IDCategoria)
);
GO

CREATE TABLE Carrito (
    IDCarrito BIGINT PRIMARY KEY,
    IDUsuario BIGINT,
    IDProducto BIGINT,
    FechaCarrito DATETIME,
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto)
);
GO

CREATE TABLE Venta (
    IDVentas BIGINT PRIMARY KEY,
    IDPedido BIGINT,
    IDProducto BIGINT,
    FechaVenta DATETIME,
    Total DECIMAL(18, 2),
    FOREIGN KEY (IDPedido) REFERENCES Carrito(IDCarrito),
    FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto)
);
GO
