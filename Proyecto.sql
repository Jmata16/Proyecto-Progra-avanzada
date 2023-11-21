-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2023 18:39:31
-- Generated from EDMX file: D:\Proyectos\Avanzada\MN_API\MN_API\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
Use GO_Proyecto;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Bitacora_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bitacora] DROP CONSTRAINT [FK_Bitacora_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Carrito_Producto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Carrito] DROP CONSTRAINT [FK_Carrito_Producto];
GO
IF OBJECT_ID(N'[dbo].[FK_Carrito_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Carrito] DROP CONSTRAINT [FK_Carrito_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Compra_Producto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Compra] DROP CONSTRAINT [FK_Compra_Producto];
GO
IF OBJECT_ID(N'[dbo].[FK_Compra_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Compra] DROP CONSTRAINT [FK_Compra_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Rol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Rol];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bitacora]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bitacora];
GO
IF OBJECT_ID(N'[dbo].[Carrito]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Carrito];
GO
IF OBJECT_ID(N'[dbo].[Compra]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Compra];
GO
IF OBJECT_ID(N'[dbo].[Producto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Producto];
GO
IF OBJECT_ID(N'[dbo].[Rol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rol];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Rol'
CREATE TABLE [dbo].[Rol] (
    [IdRol] tinyint IDENTITY(1,1) NOT NULL,
    [NombreRol] varchar(75)  NOT NULL
);
GO


-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] bigint IDENTITY(1,1) NOT NULL,
    [CorreoElectronico] varchar(50)  NOT NULL,
    [Contrasenna] varchar(20)  NOT NULL,
    [Identificacion] varchar(20)  NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Estado] bit  NOT NULL,
    [IdRol] tinyint  NOT NULL,
    [UsaClaveTemporal] bit  NOT NULL,
    [FechaCaducidad] datetime2  NOT NULL
);
GO

-- Creating table 'Bitacora'
CREATE TABLE [dbo].[Bitacora] (
    [IdBitacora] bigint IDENTITY(1,1) NOT NULL,
    [FechaHora] datetime  NOT NULL,
    [Origen] varchar(5000)  NOT NULL,
    [Mensaje] varchar(5000)  NOT NULL,
    [IdUsuario] bigint  NOT NULL
);
GO

-- Creating table 'Producto'
CREATE TABLE [dbo].[Producto] (
    [IdProducto] bigint IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(200)  NOT NULL,
    [Descripcion] varchar(5000)  NOT NULL,
    [Precio] decimal(18,2)  NOT NULL,
    [Instructor] varchar(200)  NOT NULL,
    [Imagen] varchar(5000)  NOT NULL,
    [Video] varchar(5000)  NOT NULL
);
GO

-- Creating table 'Carrito'
CREATE TABLE [dbo].[Carrito] (
    [IdCarrito] bigint IDENTITY(1,1) NOT NULL,
    [IdUsuario] bigint  NOT NULL,
    [IdProducto] bigint  NOT NULL,
    [FechaRegistro] datetime  NOT NULL
);
GO

-- Creating table 'Compra'
CREATE TABLE [dbo].[Compra] (
    [IdCompra] bigint IDENTITY(1,1) NOT NULL,
    [IdUsuario] bigint  NOT NULL,
    [IdProducto] bigint  NOT NULL,
    [FechaCompra] datetime  NOT NULL,
    [PrecioPagado] decimal(18,2)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdRol] in table 'Rol'
ALTER TABLE [dbo].[Rol]
ADD CONSTRAINT [PK_Rol]
    PRIMARY KEY CLUSTERED ([IdRol] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdBitacora] in table 'Bitacora'
ALTER TABLE [dbo].[Bitacora]
ADD CONSTRAINT [PK_Bitacora]
    PRIMARY KEY CLUSTERED ([IdBitacora] ASC);
GO

-- Creating primary key on [IdProducto] in table 'Producto'
ALTER TABLE [dbo].[Producto]
ADD CONSTRAINT [PK_Producto]
    PRIMARY KEY CLUSTERED ([IdProducto] ASC);
GO

-- Creating primary key on [IdCarrito] in table 'Carrito'
ALTER TABLE [dbo].[Carrito]
ADD CONSTRAINT [PK_Carrito]
    PRIMARY KEY CLUSTERED ([IdCarrito] ASC);
GO

-- Creating primary key on [IdCompra] in table 'Compra'
ALTER TABLE [dbo].[Compra]
ADD CONSTRAINT [PK_Compra]
    PRIMARY KEY CLUSTERED ([IdCompra] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdRol] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Rol]
    FOREIGN KEY ([IdRol])
    REFERENCES [dbo].[Rol]
        ([IdRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Rol'
CREATE INDEX [IX_FK_Usuario_Rol]
ON [dbo].[Usuario]
    ([IdRol]);
GO

-- Creating foreign key on [IdUsuario] in table 'Bitacora'
ALTER TABLE [dbo].[Bitacora]
ADD CONSTRAINT [FK_Bitacora_Usuario]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bitacora_Usuario'
CREATE INDEX [IX_FK_Bitacora_Usuario]
ON [dbo].[Bitacora]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdProducto] in table 'Carrito'
ALTER TABLE [dbo].[Carrito]
ADD CONSTRAINT [FK_Carrito_Producto]
    FOREIGN KEY ([IdProducto])
    REFERENCES [dbo].[Producto]
        ([IdProducto])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Carrito_Producto'
CREATE INDEX [IX_FK_Carrito_Producto]
ON [dbo].[Carrito]
    ([IdProducto]);
GO

-- Creating foreign key on [IdUsuario] in table 'Carrito'
ALTER TABLE [dbo].[Carrito]
ADD CONSTRAINT [FK_Carrito_Usuario]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Carrito_Usuario'
CREATE INDEX [IX_FK_Carrito_Usuario]
ON [dbo].[Carrito]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdProducto] in table 'Compra'
ALTER TABLE [dbo].[Compra]
ADD CONSTRAINT [FK_Compra_Producto]
    FOREIGN KEY ([IdProducto])
    REFERENCES [dbo].[Producto]
        ([IdProducto])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Compra_Producto'
CREATE INDEX [IX_FK_Compra_Producto]
ON [dbo].[Compra]
    ([IdProducto]);
GO

-- Creating foreign key on [IdUsuario] in table 'Compra'
ALTER TABLE [dbo].[Compra]
ADD CONSTRAINT [FK_Compra_Usuario]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Compra_Usuario'
CREATE INDEX [IX_FK_Compra_Usuario]
ON [dbo].[Compra]
    ([IdUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

INSERT INTO [dbo].[Rol] ([NombreRol])
VALUES ('Administrador'), ('Usuario');

-- Creating an 'Administrador' user
INSERT INTO [dbo].[Usuario] ([CorreoElectronico], [Contrasenna], [Identificacion], [Nombre], [Estado], [IdRol], [UsaClaveTemporal], [FechaCaducidad])
VALUES ('admin@example.com', 'admin_password', '123456789', 'Admin User', 1, (SELECT [IdRol] FROM [dbo].[Rol] WHERE [NombreRol] = 'Administrador'), 0, GETDATE());

INSERT INTO [dbo].[Usuario] ([CorreoElectronico], [Contrasenna], [Identificacion], [Nombre], [Estado], [IdRol], [UsaClaveTemporal], [FechaCaducidad])
VALUES ('user@example.com', 'user_password', '987654321', 'Normal User', 1, (SELECT [IdRol] FROM [dbo].[Rol] WHERE [NombreRol] = 'Usuario'), 0, GETDATE());

UPDATE [dbo].[Usuario]
SET [IdRol] = (SELECT [IdRol] FROM [dbo].[Rol] WHERE [NombreRol] = 'Usuario');







	