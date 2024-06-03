/*----------------------------SPS PARA Cliente--------------------------------------------*/
DROP PROCEDURE sp_RegistrarCliente;

CREATE PROC sp_RegistrarCliente(
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@RFC VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@CP VARCHAR(10),
@DIRECCION VARCHAR(100),
@Colonia VARCHAR(100),
@Numero VARCHAR(10),
@Ciudad VARCHAR(100),
@Edo VARCHAR(100),
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
	BEGIN
		INSERT INTO CLIENTE(Documento, RazonSocial, RFC, NombreCompleto, Correo, Telefono, Estado, CP, DIRECCION, Colonia, Numero, Ciudad, Edo) VALUES (
		@Documento, @RazonSocial, @RFC, @NombreCompleto, @Correo, @Telefono, @Estado, @CP, @DIRECCION, @Colonia, @Numero, @Ciudad, @Edo)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El numero de documento ya existe'
END

GO

DROP PROCEDURE sp_ModificarCliente;

CREATE PROC sp_ModificarCliente(
@IdCliente INT,
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@RFC VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@CP VARCHAR(10),
@DIRECCION VARCHAR(100),
@Colonia VARCHAR(100),
@Numero VARCHAR(10),
@Ciudad VARCHAR(100),
@Edo VARCHAR(100),
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN
	SET @Resultado = 1
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento and IdCliente != @IdCliente)
	BEGIN
		UPDATE CLIENTE SET
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		RFC = @RFC,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado,
		CP = @CP,
		DIRECCION = @DIRECCION,
		Colonia = @Colonia,
		Numero = @Numero,
		Ciudad = @Ciudad,
		Edo = @Edo
		WHERE IdCliente = @IdCliente
	END
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El numero de documento ya existe'
	END
END
GO

CREATE PROC sp_EliminarCliente(
@IdProducto INT,
@Respuesta BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM DETALLE_COMPRA dc
	INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una COMPRA\n'
	END

	IF EXISTS (SELECT * FROM DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN 
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una VENTA\n'
	END

	IF(@pasoreglas = 1)
	BEGIN
	 DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto
	 SET @Respuesta = 1
	END
END

GO

SELECT IdCliente, Documento, RazonSocial, RFC, NombreCompleto, Correo, Telefono, Estado FROM CLIENTE