declare @json_data varchar(max)

select @json_data = BulkColumn
from openrowset 
(
	BULK 'C:\Users\MLS\Downloads\csvClientes.json', SINGLE_CLOB
) AS datasource

--print @json_data

INSERT INTO CLIENTE (Codigo, NombreCompleto, Calle, NoExt, NoInt, Colonia, CodigoPostal, Municipio, Poblacion, Edo, RFC, Regimen, Correo)
select * from openjson (@json_data)
WITH
(
	Codigo VARCHAR(50),
	Nombre VARCHAR(50),
	Calle VARCHAR(50), 
	NoExt VARCHAR(50),
	NoInt VARCHAR(10),
	Colonia VARCHAR(100),
	CodigoPostal VARCHAR(10),
	Municipio VARCHAR(100),
	Poblacion VARCHAR(100),
	Edo VARCHAR(50),
	RFC VARCHAR(50),
	Regimen VARCHAR(5),
	Correo VARCHAR(50)

)

SELECT * FROM CLIENTE;


UPDATE Cliente
SET NombreCompleto = 
    CASE 
        WHEN CHARINDEX(' ', NombreCompleto, CHARINDEX(' ', NombreCompleto) + 1) = 0 THEN NombreCompleto
        ELSE 
            SUBSTRING(NombreCompleto, CHARINDEX(' ', NombreCompleto, CHARINDEX(' ', NombreCompleto) + 1) + 1, LEN(NombreCompleto)) + 
            ' ' + 
            SUBSTRING(NombreCompleto, 1, CHARINDEX(' ', NombreCompleto, CHARINDEX(' ', NombreCompleto) + 1))
    END


	SELECT IdCliente, Documento, NombreCompleto, RazonSocial, RFC, Correo, Telefono, Estado, CP, DIRECCION, Colonia, Numero, Ciudad, Edo, RegimenFiscal FROM CLIENTE