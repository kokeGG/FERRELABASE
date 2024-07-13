/*----------------------------SPS PARA Producto--------------------------------------------*/
CREATE PROCEDURE SP_RegistrarProducto(
@Codigo VARCHAR(20),
@Nombre VARCHAR(30),
@Descripcion VARCHAR(30),
@IdCategoria INT,
@Stock INT,
@Precio DECIMAL(10,2),
@Estado BIT,
@ClaveSat VARCHAR(50),
@UnidadSat VARCHAR(50),
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Codigo = @Codigo)
	BEGIN
		INSERT INTO PRODUCTO(Codigo, Nombre, Descripcion, IdCategoria, Stock, Precio, Estado, ClaveSat, UnidadSat) 
		VALUES (@Codigo, @Nombre, @Descripcion, @IdCategoria, @Stock, @Precio, @Estado, @ClaveSat, @UnidadSat)
		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'Ya existe un producto con el mismo código'
END

GO

-- SP PARA MODIFICAR PRODUCTO
CREATE PROCEDURE SP_EditarProducto(
@IdProducto INT,
@Codigo VARCHAR(20),
@Nombre VARCHAR(30),
@Descripcion VARCHAR(30),
@IdCategoria INT,
@Stock INT,
@Precio DECIMAL(10,2),
@Estado BIT,
@ClaveSat VARCHAR(50),
@UnidadSat VARCHAR(50),
@UltimoPrecio DATETIME,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Codigo = @Codigo AND IdProducto != @IdProducto)

		UPDATE PRODUCTO SET
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		Stock = @Stock,
		Precio = @Precio,
		Estado = @Estado,
		ClaveSat = @ClaveSat,
		UnidadSat = @UnidadSat,
		UltimoPrecio = @UltimoPrecio
		WHERE IdProducto = @IdProducto
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe el producto con el mismo código'
	END
END

GO

-- SP PARA ELIMINAR PRODUCTO
CREATE PROCEDURE SP_EliminarProducto(
@IdProducto INT,
@Respuesta BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @PasoReglas BIT = 1

	IF EXISTS (
	SELECT * FROM DETALLE_COMPRA dc
	INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una Compra\n'
	END

	IF EXISTS (SELECT * FROM DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a una Venta\n'
	END

	IF(@PasoReglas = 1)
	BEGIN
		DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto
		SET @Respuesta = 1
	END
END