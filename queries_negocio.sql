CREATE TABLE NEGOCIO(
IdNegocio INT PRIMARY KEY,
Nombre VARCHAR(60),
RUC VARCHAR(60),
Direccion VARCHAR(60),
Logo VARBINARY(MAX) NULL
)

INSERT INTO NEGOCIO (IdNegocio, Nombre, RUC, Direccion)
VALUES (
'1',
'Codigo ferre la base',
'10101',
'av. codigo 123'
)

SELECT * FROM NEGOCIO;