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
	RFC VARCHAR(30),
	Correo VARCHAR(50),
	Telefono VARCHAR(50),
	Estado bit,
	FechaRegistro DATETIME DEFAULT GETDATE()
);

ALTER TABLE PROVEEDOR
ADD RFC VARCHAR(30) NULL;

SELECT * FROM PROVEEDOR;

--añadir campo de razón social
CREATE TABLE CLIENTE(
	IdCliente INT PRIMARY KEY IDENTITY(1,1),
	Codigo VARCHAR(50),
	RFC VARCHAR(50),
	NombreCompleto VARCHAR(50),
	Calle VARCHAR(200),
	NoExt VARCHAR(50),
	NoInt VARCHAR(50),
	Colonia VARCHAR(200),
	CodigoPostal VARCHAR(6),
	Municipio VARCHAR(100),
	Poblacion VARCHAR(100),
	Edo VARCHAR(100),
	Correo VARCHAR(50),
	Estado bit,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	Regimen VARCHAR(5)
);
ALTER TABLE CLIENTE
ADD DIRECCION VARCHAR(100) NULL;

ALTER TABLE CLIENTE
ADD CP VARCHAR(10) NULL;

ALTER TABLE CLIENTE
ADD Numero VARCHAR(10) NULL;

ALTER TABLE CLIENTE
ADD Colonia VARCHAR(100) NULL;

ALTER TABLE CLIENTE
ADD Ciudad VARCHAR(100) NULL;

ALTER TABLE CLIENTE
ADD Edo VARCHAR(100) NULL;

SELECT * FROM CLIENTE;

CREATE TABLE USUARIO(
	IdUsuario INT PRIMARY KEY IDENTITY(1,1),
	Codigo VARCHAR(50),
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
	Codigo VARCHAR(50),
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
	Precio DECIMAL(10,2) DEFAULT 0,
	Estado BIT,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	ClaveSat VARCHAR(50),
	UnidadSat VARCHAR(50),
	UltimoPrecio DATETIME DEFAULT GETDATE()
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
	TipoVenta VARCHAR(50),
	Folio VARCHAR(50),
	CodigoCliente VARCHAR(50),
	NombreCliente VARCHAR(100),
	MontoPago DECIMAL(10,2),
	MontoCambio DECIMAL(10,2),
	MontoTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);
 CREATE TABLE FolioFactura(
	IdFactura INT PRIMARY KEY IDENTITY(1,1),
	Folio VARCHAR(50),
);



CREATE TABLE COTIZACION(
	IdCotizacion INT PRIMARY KEY IDENTITY(1,1),
	IdUsuario INT REFERENCES USUARIO(IdUsuario),
	Folio VARCHAR(50),
	RFC VARCHAR(50),
	CodigoCliente VARCHAR(50),
	NombreCliente VARCHAR(100),
	MontoPago DECIMAL(10,2),
	MontoCambio DECIMAL(10,2),
	MontoTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE DETALLE_VENTA(
	IdDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
	IdVenta INT REFERENCES Venta(IdVenta),
	IdProducto INT REFERENCES PRODUCTO(IdProducto),
	PrecioVenta DECIMAL(10,2) DEFAULT 0,
	Cantidad INT,
	SubTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE DETALLE_COTIZACION(
	IdDetalleCotizacion INT PRIMARY KEY IDENTITY(1,1),
	IdCotizacion INT REFERENCES COTIZACION(IdCotizacion),
	IdProducto INT REFERENCES PRODUCTO(IdProducto),
	PrecioVenta DECIMAL(10,2) DEFAULT 0,
	Cantidad INT,
	SubTotal DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE()
);

SELECT * FROM USUARIO;
SELECT * FROM ROL;
SELECT IdUsuario, Codigo, NombreCompleto, Correo, Clave, Estado FROM USUARIO;

INSERT INTO USUARIO(Codigo, NombreCompleto, Correo, Clave, Telefono, IdRol, Estado)
VALUES (
	'101010',
	'ADMIN',
	'CORREO@CORREO.COM',
	'123',
	'2883647584',
	'1',
	'1'
);
/*
INSERT INTO ROL(Descripcion) VALUES ('ADMINISTRADOR');

select * from permiso;
SELECT * FROM ROL;
	
INSERT INTO ROL (Descripcion) VALUES ('EMPLEADO');

INSERT INTO PERMISO(IdRol, NombreMenu) VALUES
('1', 'menuusuarios'),
('1', 'menumantenedor'),
('1', 'menuventas'),
('1', 'menucompras'),
('1', 'menuclientes'),
('1', 'menuproveedores'),
('1', 'menureportes'),
('1', 'acercade')


INSERT INTO PERMISO(IdRol, NombreMenu) VALUES
('2', 'menuventas'),
('2', 'menucompras'),
('2', 'menuclientes'),
('2', 'menuproveedores'),
('2', 'menureportes'),
('2', 'acercade')
*/

SELECT p.IdRol, p.NombreMenu FROM PERMISO p 
INNER JOIN ROL r ON r.IdRol = p.IdRol 
INNER JOIN USUARIO u ON u.IdRol = r.IdRol 
WHERE u.IdUsuario = 1

INSERT INTO USUARIO(Codigo, NombreCompleto, Correo, Clave, IdRol, Estado)
VALUES 
('20', 'EMPLEADO', '@GMAIL.COM', '456', 2, 1)

SELECT * FROM USUARIO;

CREATE PROC SP_REGISTRARUSUARIO(
@Codigo VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(100),
@Clave VARCHAR(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje VARCHAR(500) output
)
AS
BEGIN
	SET @IdUsuarioResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Codigo = @codigo)
	BEGIN
		INSERT INTO USUARIO(Codigo, NombreCompleto, Correo, Clave, IdRol, Estado) VALUES
		(@Codigo, @NombreCompleto, @Correo, @Clave, @IdRol, @Estado)

		SET @IdUsuarioResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el documento para mas de un usuario'
END

DECLARE @IdUsuarioGenerado int
DECLARE @Mensaje VARCHAR(500)
EXEC SP_REGISTRARUSUARIO '123', 'pruebas', 'test@gmail.com', '456', 2, 1, @IdUsuarioGenerado OUTPUT,@Mensaje OUTPUT

SELECT @IdUsuarioGenerado
SELECT @Mensaje
SELECT * FROM USUARIO;
/*-------------------------------------------------------------------------------*/
CREATE PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Codigo VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(100),
@Clave VARCHAR(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje VARCHAR(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Codigo = @codigo AND IdUsuario != @IdUsuario)
	BEGIN
		UPDATE USUARIO SET
		Codigo = @Codigo, 
		NombreCompleto = @NombreCompleto, 
		Correo = @Correo, 
		Clave = @Clave, 
		IdRol = @IdRol, 
		Estado = @Estado
		WHERE IdUsuario = @IdUsuario

		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el documento para mas de un usuario'
END


DECLARE @respuesta bit
DECLARE @mensaje VARCHAR(500)
EXEC SP_EDITARUSUARIO  3, '123', 'pruebas 2', 'test@gmail.com', '456', 2, 1, @respuesta OUTPUT, @mensaje output

SELECT @mensaje

SELECT * FROM USUARIO

/*************************************************************************************************************/

CREATE PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje VARCHAR(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @PasoReglas bit = 1

	IF EXISTS (SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n'
	END

	IF EXISTS(SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n'
	END

	IF (@PasoReglas = 1)
	BEGIN
		DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario
		SET @Respuesta = 1
	END
END

SELECT * FROM VENTA;
