/*-------------------------------------SPS PARA PROVEEDOR -------------------------------------*/

CREATE PROC sp_RegistrarProveedor(
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@RFC VARCHAR(30),
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
		INSERT INTO PROVEEDOR(Documento, RazonSocial, RFC, Correo, Telefono, Estado) VALUES (
		@Documento, @RazonSocial, @RFC, @Correo, @Telefono, @Estado)

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
@RFC VARCHAR(30),
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
		RFC = @RFC,
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

go

/* PROCESOS PARA REGISTRAR UNA COMPRA */

CREATE TYPE [dbo].[EDetalle_compra] AS TABLE(
	[IdProducto] int NULL,
	[PrecioCompra] decimal (18,2) NULL,
	[PrecioVenta] decimal (18,2) NULL,
	[Cantidad] int NULL,
	[MontoTotal] decimal(18,2) NULL
)

go



CREATE PROCEDURE sp_RegistrarCompra(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento VARCHAR(500),
@Codigo VARCHAR(500),
@MontoTotal decimal(18,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje VARCHAR(500) OUTPUT
)

AS
BEGIN
	BEGIN TRY
		DECLARE @idcompra int = 0
		SET @Resultado = 1
		SET @Mensaje = ''

		BEGIN TRANSACTION registro

		insert into COMPRA(IdUsuario, IdProveedor, TipoDocumento, Codigo, MontoTotal)
		VALUES (@IdUsuario, @IdProveedor, @TipoDocumento, @Codigo, @MontoTotal)

		SET @idcompra = SCOPE_IDENTITY()

		insert into DETALLE_COMPRA(IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, MontoTotal)
		select @idcompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, MontoTotal from @DetalleCompra


		UPDATE p set p.Stock = p.Stock + dc.Cantidad,
		p.Precio = dc.PrecioVenta
		from PRODUCTO p
		INNER JOIN @DetalleCompra dc ON dc.IdProducto = p.IdProducto

		COMMIT TRANSACTION registro


	END TRY
	BEGIN CATCH
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()

		rollback transaction registro
	END CATCH
END

