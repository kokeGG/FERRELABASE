/*----------------------------SPS PARA Cliente--------------------------------------------*/
DROP PROCEDURE sp_RegistrarCliente;

CREATE PROC sp_RegistrarCliente(
@Codigo VARCHAR(50),
@RFC VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Calle VARCHAR(50),
@NoExt VARCHAR(100),
@NoInt VARCHAR(100),
@Colonia VARCHAR(100),
@CodigoPostal VARCHAR(10),
@Municipio VARCHAR(100),
@Poblacion VARCHAR(100),
@Edo VARCHAR(50),
@Correo VARCHAR(50),
@Regimen VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Codigo = @Codigo)
	BEGIN
		INSERT INTO CLIENTE(Codigo, RFC, NombreCompleto, Calle, NoExt, NoInt, Colonia, CodigoPostal, Municipio, Poblacion, Edo, Correo, Regimen, Estado) VALUES (
		@Codigo, @RFC, @NombreCompleto, @Calle, @NoExt, @NoInt, @Colonia, @CodigoPostal, @Municipio, @Poblacion, @Edo, @Correo, @Regimen, @Estado)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El numero de documento ya existe'
END

GO

DROP PROCEDURE sp_ModificarCliente;

CREATE PROC sp_ModificarCliente(
@IdCliente INT,
@Codigo VARCHAR(50),
@3
RFC VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Calle VARCHAR(50),
@NoExt VARCHAR(100),
@NoInt VARCHAR(100),
@Colonia VARCHAR(100),
@CodigoPostal VARCHAR(10),
@Municipio VARCHAR(100),
@Poblacion VARCHAR(100),
@Edo VARCHAR(50),
@Correo VARCHAR(50),
@Regimen VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN
	SET @Resultado = 1
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Codigo = @Codigo and IdCliente != @IdCliente)
	BEGIN
		UPDATE CLIENTE SET
		Codigo = @Codigo,
		RFC = @RFC,
		NombreCompleto = @NombreCompleto,
		Calle = @Calle,
		NoExt = @NoExt,
		NoInt = @NoInt,
		Colonia = @Colonia,
		CodigoPostal = @CodigoPostal,
		Municipio = @Municipio,
		Poblacion = @Poblacion,
		Edo = @Edo,
		Correo = @Correo,
		Estado = @Estado,
		Regimen = @Regimen
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