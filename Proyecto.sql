-- --------------------------------------------------
-- Crear base de datos
-- --------------------------------------------------
CREATE DATABASE GO_Proyecto;
GO

USE GO_Proyecto;
GO

-- Crear tablas
CREATE TABLE [dbo].[Rol] (
    [IdRol] tinyint IDENTITY(1,1) NOT NULL,
    [NombreRol] varchar(75)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRol] ASC)
);

CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] bigint IDENTITY(1,1) NOT NULL,
    [CorreoElectronico] varchar(50)  NOT NULL,
    [Contrasenna] varchar(20)  NOT NULL,
    [Identificacion] varchar(20)  NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Estado] bit  NOT NULL,
    [IdRol] tinyint  NOT NULL,
    [UsaClaveTemporal] bit  NOT NULL,
    [FechaCaducidad] datetime2  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Rol] ([IdRol]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE [dbo].[Bitacora] (
    [IdBitacora] bigint IDENTITY(1,1) NOT NULL,
    [FechaHora] datetime  NOT NULL,
    [Origen] varchar(5000)  NOT NULL,
    [Mensaje] varchar(5000)  NOT NULL,
    [IdUsuario] bigint  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdBitacora] ASC),
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE [dbo].[Categoria] (
    [IdCategoria] tinyint IDENTITY(1,1) NOT NULL,
    [NombreCategoria] varchar(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC)
);

CREATE TABLE [dbo].[Producto] (
    [IdProducto] bigint IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(200)  NOT NULL,
    [Descripcion] varchar(5000)  NOT NULL,
    [Precio] decimal(18,2)  NOT NULL,
    [Stock] int NOT NULL,
    [Imagen] varchar(5000)  NOT NULL,
    [IdCategoria] tinyint NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProducto] ASC),
    FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[Categoria] ([IdCategoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE [dbo].[Carrito] (
    [IdCarrito] bigint IDENTITY(1,1) NOT NULL,
    [IdUsuario] bigint  NOT NULL,
    [IdProducto] bigint  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCarrito] ASC),
    FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([IdProducto]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE [dbo].[Compra] (
    [IdCompra] bigint IDENTITY(1,1) NOT NULL,
    [IdUsuario] bigint  NOT NULL,
    [IdProducto] bigint  NOT NULL,
    [FechaCompra] datetime  NOT NULL,
    [PrecioPagado] decimal(18,2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCompra] ASC),
    FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([IdProducto]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Inserts de datos para la tabla Rol
INSERT INTO [dbo].[Rol] ([NombreRol]) VALUES ('Administrador'), ('Usuario');

-- Inserts de datos para la tabla Categoria
INSERT INTO [dbo].[Categoria] ([NombreCategoria]) VALUES ('Audífonos');
INSERT INTO [dbo].[Categoria] ([NombreCategoria]) VALUES ('Monitores');
INSERT INTO [dbo].[Categoria] ([NombreCategoria]) VALUES ('Teclados');
INSERT INTO [dbo].[Categoria] ([NombreCategoria]) VALUES ('Mouse');

-- Inserts de datos para la tabla Usuario

INSERT INTO [dbo].[Usuario] ([CorreoElectronico], [Contrasenna], [Identificacion], [Nombre], [Estado], [IdRol], [UsaClaveTemporal], [FechaCaducidad])
VALUES ('admin@example.com', 'admin_password', '123456789', 'Admin User', 1, (SELECT [IdRol] FROM [dbo].[Rol] WHERE [NombreRol] = 'Administrador'), 0, GETDATE());

INSERT INTO [dbo].[Usuario] ([CorreoElectronico], [Contrasenna], [Identificacion], [Nombre], [Estado], [IdRol], [UsaClaveTemporal], [FechaCaducidad])
VALUES ('user@example.com', 'user_password', '987654321', 'Normal User', 1, (SELECT [IdRol] FROM [dbo].[Rol] WHERE [NombreRol] = 'Usuario'), 0, GETDATE());


INSERT INTO [dbo].[Producto] ([Nombre], [Descripcion], [Precio], [Stock], [Imagen], [IdCategoria])
VALUES ('Logitech g502', 'Mouse para juegos de alto desempeño', 39.99, 100, '\images\1.png', 4);


INSERT INTO [dbo].[Producto] ([Nombre], [Descripcion], [Precio], [Stock], [Imagen], [IdCategoria])
VALUES ('Astro a50', 'Audífonos con micrófono inalámbricos', 200.00, 75, '\images\3.png', 1);

INSERT INTO [dbo].[Producto] ([Nombre], [Descripcion], [Precio], [Stock], [Imagen], [IdCategoria])
VALUES ('Optix G241', 'Monitor para juegos', 100.99, 50, '\images\2.png', 2);

INSERT INTO [dbo].[Producto] ([Nombre], [Descripcion], [Precio], [Stock], [Imagen], [IdCategoria])
VALUES ('G213 Logitech', 'Teclado para juegos con rgb', 99.99, 50, '\images\4.png', 3);