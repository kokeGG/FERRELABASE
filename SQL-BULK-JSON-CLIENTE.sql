declare @json_data varchar(max)

select @json_data = BulkColumn
from openrowset 
(
	BULK 'C:\Users\MLS\Downloads\csvjson.json', SINGLE_CLOB
) AS datasource

--print @json_data

INSERT INTO CLIENTE (Documento, NombreCompleto, RazonSocial, DIRECCION, Numero, Colonia, CP, Ciudad, Edo, RFC, Correo)
select * from openjson (@json_data)
WITH
(
	Documento VARCHAR(50),
	NOMBRECOMPLETO VARCHAR(50),
	RazonSocial VARCHAR(50),
	DIRECCION VARCHAR(50),
	NUMERO VARCHAR(10),
	COLONIA VARCHAR(100),
	CP VARCHAR(10),
	CIUDAD VARCHAR(100),
	Edo VARCHAR(100),
	RFC VARCHAR(50),
	CORREO VARCHAR(50)
)

SELECT * FROM CLIENTE;