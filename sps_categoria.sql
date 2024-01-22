/*----------------------------SPS PARA CATEGORIA--------------------------------------------*/
CREATE PROCEDURE SP_RegistrarCategoria(
@Descripcion VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	BEGIN
		INSERT INTO CATEGORIA(Descripcion, Estado) VALUES (@Descripcion, @Estado)
		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la descripción de una categoría'
END

GO

select * from CATEGORIA;
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Fierros', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Lijas', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES ('Cables', 1)

GO
-- SP PARA MODIFICAR CATEGORIA
CREATE PROCEDURE SP_EditarCategoria(
@IdCategoria INT,
@Descripcion VARCHAR(50),
@Estado BIT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
		UPDATE CATEGORIA SET
		Descripcion = @Descripcion,
		Estado = @Estado
		WHERE IdCategoria = @IdCategoria
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'No se puede repetir la descripcion de una categoria'
	END
END

GO

-- SP PARA ELIMINAR CATEGORIA
CREATE PROCEDURE SP_EliminarCategoria(
@IdCategoria INT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (
	SELECT * FROM CATEGORIA c
	INNER JOIN PRODUCTO p ON p.IdCategoria = c.IdCategoria
	WHERE c.IdCategoria = @IdCategoria
	)
	BEGIN
		DELETE TOP(1) FROM CATEGORIA WHERE IdCategoria = @IdCategoria
	END
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'La categoria se encuentra relacionada a un producto'
	END
END