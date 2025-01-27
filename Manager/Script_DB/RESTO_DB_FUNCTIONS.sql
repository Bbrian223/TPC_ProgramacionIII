USE RESTO_DB
GO

/*		Usuarios		*/

CREATE VIEW vw_ListaEmpleados
AS
SELECT E.IDEMPLEADO,E.IDUSUARIO,E.NOMBRE,E.APELLIDO,E.DOCUMENTO,E.FECHA_NACIMIENTO,
	   E.FECHA_INGRESO,D.IDDIRECCION,D.CALLE,D.NUM_DIRECCION,D.LOCALIDAD,D.COD_POSTAL,
	   E.EMAIL,E.TELEFONO,U.IDROL,U.ESTADO 
FROM EMPLEADOS E
INNER JOIN Usuarios U ON E.IDUSUARIO = U.IDUSUARIO
INNER JOIN Direcciones D ON E.IDDIRECCION = D.IDDIRECCION
GO

SELECT * FROM vw_ListaEmpleados
GO

CREATE PROCEDURE sp_AgregarEmpleado(
	@pNombre VARCHAR(100),
	@pApellido VARCHAR(100),
	@pDni VARCHAR(10),
	@pFechaNac DATE,
	@pCalle VARCHAR(50),
	@pNumDir VARCHAR(4),
	@pLocalidad VARCHAR(100),
	@pCodPos VARCHAR(5),
	@pEmail VARCHAR(100),
	@pTelefono VARCHAR(15),
	@pIdRol INT
	)AS
BEGIN 
	
	DECLARE @IdUsuario BIGINT
	DECLARE @IdDireccion BIGINT

	-- Corroborar si el dni esta asignado a un empleado activo.

	INSERT Usuarios (NOMBRE,CONTRASENIA,IDROL)
	VALUES (@pDni,@pDni,@pIdRol)

	INSERT Direcciones (CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL)
	VALUES (@pCalle,@pNumDir,@pLocalidad,@pCodPos)

	SELECT @IdDireccion = IDENT_CURRENT('Direcciones')
	SELECT @IdUsuario = IDENT_CURRENT('Usuarios')

	INSERT INTO Empleados(IDUSUARIO,IDDIRECCION,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,EMAIL,TELEFONO)
	VALUES (@IdUsuario,@IdDireccion,@pNombre,@pApellido,@pDni,@pFechaNac,@pEmail,@pTelefono)

END
GO


EXEC sp_AgregarEmpleado 'Graciela','Rios','12345678','1975/01/08','Joaqion V Gonzales','1932','Villa adelina','1607','grace@gmail.com','','1'

SELECT* FROM vw_ListaEmpleados

