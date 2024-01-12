CREATE DATABASE FERRELABASE;

USE FERRELABASE;

CREATE TABLE ROL(
	IdRol INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(50),
	FechaRegistro DATETIME DEFAULT GETDATE()	
);


CREATE TABLE PERMISO(
	IdPermiso INT PRIMARY KEY IDENTITY(1,1),
	IdRol INT REFERENCES ROL(IdRol),
	NombreMenu VARCHAR(50),
	FechaRegistro DATETIME DEFAULT GETDATE()	
);

CREATE TABLE PROVEEDOR(
	IdProveedor INT PRIMARY KEY IDENTITY(1,1),
	Documento VARCHAR(50),
	RazonSocial VARCHAR(50),
	Correo VARCHAR(50),
	Telefono VARCHAR(50),
	Estado bit,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE CLIENTE(
	IdCliente INT PRIMARY KEY IDENTITY(1,1),
	Documento VARCHAR(50),
	NombreCompleto VARCHAR(50),
	Correo VARCHAR(50),
	Telefono VARCHAR(50),
	Estado bit,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE USUARIO(
	IdUsuario INT PRIMARY KEY IDENTITY(1,1),
	Documento VARCHAR(50),
	NombreCompleto VARCHAR(50),
	Correo VARCHAR(50),
	Clave VARCHAR(50),
	Telefono VARCHAR(50),
	IdRol INT REFERENCES ROL(IdRol),
	Estado bit,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE COMPRA(
	IdCompra INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT REFERENCES USUARIO(IdUsuario),
	IdProveedor INT REFERENCES PROVEEDOR(IdProveedor),
	TipoDocumento VARCHAR(50),
	NumeroDocumento VARCHAR(50),
	MontoTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE CATEGORIA(
	IdCategoria INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(100),
	Estado BIT,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE PRODUCTO(
	IdProducto INT PRIMARY KEY IDENTITY(1,1),
	Codigo VARCHAR(50),
	Nombre VARCHAR(50),
	Descripcion VARCHAR(100),
	IdCategoria INT REFERENCES CATEGORIA(IdCategoria),
	Stock INT NOT NULL DEFAULT 0,
	PrecioCompra DECIMAL(10,2) DEFAULT 0,
	PrecioVenta DECIMAL(10,2) DEFAULT 0,
	Estado BIT,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE DETALLE_COMPRA(
	IdDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
	IdCompra INT REFERENCES COMPRA(IdCompra),
	IdProducto INT REFERENCES PRODUCTO(IdProducto),
	PrecioCompra DECIMAL(10,2) DEFAULT 0,
	PrecioVenta DECIMAL(10,2) DEFAULT 0,
	Cantidad INT,
	MontoTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE VENTA(
	IdVenta INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT REFERENCES USUARIO(IdUsuario),
	TipoDocumento VARCHAR(50),
	NumeroDocumento VARCHAR(50),
	DocumentoCliente VARCHAR(50),
	NombreCliente VARCHAR(100),
	MontoPago DECIMAL(10,2),
	MontoCambio DECIMAL(10,2),
	MontoTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE DETALLE_VENTA(
	IdDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
	IdVenta INT REFERENCES COMPRA(IdCompra),
	IdProducto INT REFERENCES PRODUCTO(IdProducto),
	PrecioVenta DECIMAL(10,2) DEFAULT 0,
	Cantidad INT,
	SubTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

SELECT * FROM USUARIO;
SELECT * FROM ROL;
SELECT IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado FROM USUARIO;

INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave, Telefono, IdRol, Estado)
VALUES (
	'101010',
	'ADMIN',
	'CORREO@CORREO.COM',
	'123',
	'2883647584',
	'1',
	'1'
);

INSERT INTO ROL(Descripcion) VALUES ('ADMINISTRADOR');
