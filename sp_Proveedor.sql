/*-------------------------------------SPS PARA CLIENTES -------------------------------------*/

CREATE PROC sp_RegistrarProveedor(
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
) AS 
BEGIN
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento)
	BEGIN
		INSERT INTO PROVEEDOR(Documento, RazonSocial, Correo, Telefono, Estado) VALUES (
		@Documento, @RazonSocial, @Correo, @Telefono, @Estado)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El numero de documento ya existe'
END

GO

CREATE PROC sp_ModificarProveedor(
@IdProveedor INT,
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
) AS
BEGIN
	SET @Resultado = 1
	DECLARE @IdPersona INT
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento AND IdProveedor != @IdProveedor)
	BEGIN
		UPDATE PROVEEDOR SET
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IdProveedor = @IdProveedor
	END
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El numero de documento ya existe'
	END
END

GO
CREATE PROCEDURE sp_EliminarProveedor(
@IdProveedor INT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
) 
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (
	SELECT * FROM PROVEEDOR p
	INNER JOIN COMPRA c ON p.IdProveedor = c.IdProveedor
	WHERE p.IdProveedor = @IdProveedor
	)
	BEGIN
		DELETE TOP(1) FROM PROVEEDOR WHERE IdProveedor = @IdProveedor
	END
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El proveedor se encuentra relacionado a una compra'
	END
END

SELECT * FROM PROVEEDOR;