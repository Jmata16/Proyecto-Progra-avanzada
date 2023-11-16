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
    Contrasenna NVARCHAR(255) NOT NULL,
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
CREATE TABLE Bitacora (
    IDBitacora uniqueidentifier PRIMARY KEY,
    IDUsuario int,
    FechaEvento datetime,
    TablaAfectada nvarchar(255),
    TipoAccion nvarchar(50),
    Detalles nvarchar(max)
);


CREATE PROCEDURE RegistrarCuentaSP
    @Nombre NVARCHAR(250),
	@Apellidos NVARCHAR(250),
    @CorreoElectronico NVARCHAR(100),
    @Contrasenna NVARCHAR(25)
AS
BEGIN
    DECLARE @IDUsuario BIGINT

    SELECT @IDUsuario = ISNULL(MAX(IDUsuario), 0) + 1 FROM Usuarios

    INSERT INTO Usuarios (IDUsuario, Nombre, Apellidos, Contrasenna, CorreoElectronico, IDRol)
    VALUES (@IDUsuario, @Nombre, '', @Contrasenna, @CorreoElectronico, 2) 
END