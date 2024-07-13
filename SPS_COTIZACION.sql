CREATE TYPE [dbo].[EDetalle_Cotizacion] AS TABLE(
	[IdProducto] int NULL,
	[PrecioVenta] decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)

GO

CREATE PROCEDURE usp_RegistrarCotizacion(
@IdUsuario INT,
@Folio VARCHAR(500),
@RFC VARCHAR(500),
@CodigoCliente VARCHAR(500),
@NombreCliente VARCHAR(500),
@MontoPago DECIMAL(18,2),
@MontoCambio DECIMAL(18,2),
@MontoTotal DECIMAL(18,2),
@DetalleCotizacion [EDetalle_Cotizacion] READONLY,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	BEGIN TRY
		DECLARE @idcotizacion INT = 0
		SET @Resultado = 1
		SET @Mensaje = ''

		BEGIN TRANSACTION registro

		INSERT INTO COTIZACION(IdUsuario, Folio, RFC, CodigoCliente, NombreCliente, MontoPago, MontoCambio, MontoTotal)
		VALUES (@IdUsuario, @Folio, @CodigoCliente, @RFC, @NombreCliente, @MontoPago, @MontoCambio, @MontoTotal)

		SET @idcotizacion = SCOPE_IDENTITY()

		INSERT INTO DETALLE_COTIZACION(IdCotizacion, IdProducto, PrecioVenta, Cantidad, SubTotal)
		SELECT @idcotizacion, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleCotizacion

		COMMIT TRANSACTION registro
	END TRY
	BEGIN CATCH
		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION registro
	END CATCH
END

GO

CREATE PROC usp_EliminarCotizacion(
@IdCotizacion INT,
@Respuesta BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM DETALLE_COTIZACION dc
	INNER JOIN COTIZACION c ON c.IdCotizacion = dc.IdCotizacion
	WHERE c.IdCotizacion = @IdCotizacion
	)
	BEGIN 
		SET @pasoreglas = 1
		SET @Respuesta = 1
		DELETE FROM DETALLE_COTIZACION WHERE IdCotizacion = @IdCotizacion
		DELETE FROM COTIZACION WHERE IdCotizacion = @IdCotizacion
	END

	IF(@pasoreglas = 0)
	BEGIN
	 SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque NO se encuentra relacionado a una COTIZACION\n'
	END
END

GO

SELECT * FROM COTIZACION;
SELECT * FROM DETALLE_COTIZACION;

DROP PROCEDURE usp_RegistrarCotizacion;