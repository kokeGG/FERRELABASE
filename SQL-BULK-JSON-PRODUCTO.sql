declare @json_data varchar(max)

select @json_data = BulkColumn
from openrowset 
(
	BULK 'C:\Users\MLS\Downloads\csvcatalogo.json', SINGLE_CLOB
) AS datasource

--print @json_data

INSERT INTO PRODUCTO (Codigo, Nombre, Descripcion, IdCategoria, Stock, Precio, Estado, ClaveSat, UnidadSat, UltimoPrecio)
select * from openjson (@json_data)
WITH
(
	Codigo VARCHAR(50),
	Nombre VARCHAR(50),
	Descripcion VARCHAR(100),
	IdCategoria INT,
	Stock INT,
	Precio DECIMAL(10,2),
	Estado BIT,
	ClaveSat VARCHAR(50),
	UnidadSat VARCHAR(50),
	UltimoPrecio DATE
)

SELECT * FROM PRODUCTO;