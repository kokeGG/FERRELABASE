CREATE TYPE [dbo].[EDetalle_Cotizacion] AS TABLE(
	[IdProducto] int NULL,
	[PrecioVenta] decimal(18,2) NULL,
	[Cantidad] int NULL,
	[SubTotal] decimal(18,2) NULL
)

GO

CREATE PROCEDURE usp_RegistrarCotizacion(
@IdUsuario INT,
@TipoDocumento VARCHAR(500),
@NumeroDocumento VARCHAR(500),
@RFC VARCHAR(500),
@DocumentoCliente VARCHAR(500),
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

		INSERT INTO COTIZACION(IdUsuario, TipoDocumento, NumeroDocumento, RFC, DocumentoCliente, NombreCliente, MontoPago, MontoCambio, MontoTotal)
		VALUES (@IdUsuario, @TipoDocumento, @NumeroDocumento, @DocumentoCliente, @RFC, @NombreCliente, @MontoPago, @MontoCambio, @MontoTotal)

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

SELECT * FROM COTIZACION;

DROP PROCEDURE usp_RegistrarCotizacion;