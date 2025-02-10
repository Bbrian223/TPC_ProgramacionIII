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

	IF EXISTS (SELECT 1 FROM Empleados WHERE DOCUMENTO = @pDNI)
	BEGIN
		RETURN
	END

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


/*		Productos		*/

-- Lista de productos de todas las categorias
CREATE VIEW vw_ListaProductos
AS
SELECT	P.IDPRODUCTO,C.IDCATEGORIA AS IDCATEGORIA,C.NOMBRE AS CATEGORIA,P.NOMBRE,P.PRECIO,P.STOCK,P.DESCRIPCION,
		I.IDIMAGEN,I.NOMBRE AS ARCHNOMB,P.ESTADO FROM Productos P
INNER JOIN Categorias C ON  P.IDCATEGORIA = C.IDCATEGORIA
INNER JOIN Imagenes I ON P.IDPRODUCTO = I.IDPRODUCTO
GO

-- Agregar producto
CREATE PROCEDURE sp_AgregarProd(
	@pIdCategoria BIGINT,
	@pNombre VARCHAR(100),
	@pPrecio MONEY,
	@pStock int,
	@pDescripcion TEXT
	)AS
BEGIN 
	DECLARE @IdProducto BIGINT

	INSERT Productos (IDCATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION)
	VALUES (@pIdCategoria,@pNombre,@pPrecio,@pStock,@pDescripcion)

	SELECT @IdProducto = IDENT_CURRENT('Productos')

	INSERT INTO Imagenes (IDPRODUCTO, NOMBRE)
	VALUES (@IdProducto, 'producto-' + CONVERT(NVARCHAR(50), @IdProducto));		

END
GO

-- Agregar producto de Entradas
CREATE PROCEDURE sp_AgregarProdEntr(
	@pNombre VARCHAR(100),
	@pPrecio MONEY,
	@pStock int,
	@pDescripcion TEXT,
	@pIndividual BIT
	)AS
BEGIN 
	DECLARE @IdProducto BIGINT

	INSERT Productos (IDCATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION)
	VALUES ('2',@pNombre,@pPrecio,@pStock,@pDescripcion)

	SELECT @IdProducto = IDENT_CURRENT('Productos')

	INSERT Entradas(IDPRODUCTO,INDIVIDUAL)
	VALUES (@IdProducto,@pIndividual)

	INSERT INTO Imagenes (IDPRODUCTO, NOMBRE)
	VALUES (@IdProducto, 'producto-' + CONVERT(NVARCHAR(50), @IdProducto));		


END
GO


-- Agregar producto de Postres
CREATE PROCEDURE sp_AgregarProdPost(
	@pNombre VARCHAR(100),
	@pPrecio MONEY,
	@pStock int,
	@pDescripcion TEXT,
	@pAzucar BIT,
	@pGluten BIT
	)AS
BEGIN 
	DECLARE @IdProducto BIGINT

	INSERT Productos (IDCATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION)
	VALUES ('4',@pNombre,@pPrecio,@pStock,@pDescripcion)

	SELECT @IdProducto = IDENT_CURRENT('Productos')

	INSERT Postres(IDPRODUCTO,AZUCAR,GLUTEN)
	VALUES (@IdProducto,@pAzucar,@pGluten)

	INSERT INTO Imagenes (IDPRODUCTO, NOMBRE)
	VALUES (@IdProducto, 'producto-' + CONVERT(NVARCHAR(50), @IdProducto));		

END
GO

-- Agregar producto de Bebidas
CREATE PROCEDURE sp_AgregarProdBeb(
	@pNombre VARCHAR(100),
	@pPrecio MONEY,
	@pStock int,
	@pDescripcion TEXT,
	@pAlcohol BIT,
	@pVolumen INT
	)AS
BEGIN 
	DECLARE @IdProducto BIGINT

	INSERT Productos (IDCATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION)
	VALUES ('5',@pNombre,@pPrecio,@pStock,@pDescripcion)

	SELECT @IdProducto = IDENT_CURRENT('Productos')

	INSERT Bebidas(IDPRODUCTO,ALCOHOL,VOLUMEN)
	VALUES (@IdProducto,@pAlcohol,@pVolumen)

	INSERT INTO Imagenes (IDPRODUCTO, NOMBRE)
	VALUES (@IdProducto, 'producto-' + CONVERT(NVARCHAR(50), @IdProducto));		
END
GO


