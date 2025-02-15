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
		I.IDIMAGEN,I.NOMBRE AS ARCHNOMB,P.ESTADO,P.GUARNICION FROM Productos P
INNER JOIN Categorias C ON  P.IDCATEGORIA = C.IDCATEGORIA
INNER JOIN Imagenes I ON P.IDPRODUCTO = I.IDPRODUCTO
GO

-- Lista de Guarniciones
CREATE VIEW vw_ListaGuarniciones
AS
SELECT* FROM vw_ListaProductos WHERE IDCATEGORIA = 6
GO

-- Lista de Leches
CREATE VIEW vw_ListaLeches
AS
SELECT* FROM vw_ListaProductos WHERE IDCATEGORIA = 7
GO

-- Lista de Tazas
CREATE VIEW vw_ListaTazas
AS
SELECT* FROM vw_ListaProductos WHERE IDCATEGORIA = 8
GO

-- Agregar producto
CREATE PROCEDURE sp_AgregarProd(
	@pIdCategoria BIGINT,
	@pNombre VARCHAR(100),
	@pPrecio MONEY,
	@pStock int,
	@pDescripcion TEXT,
	@pGuarnicion BIT
	)AS
BEGIN 
	DECLARE @IdProducto BIGINT

	INSERT Productos (IDCATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,GUARNICION)
	VALUES (@pIdCategoria,@pNombre,@pPrecio,@pStock,@pDescripcion,@pGuarnicion)

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

-- Agregar productoS Extra
CREATE PROCEDURE sp_AgregarProdExtra(
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
	VALUES (@IdProducto, 'sin-imagen');		

END
GO

/*		Ventas		*/

--	Lista de Productos asignados a una mesa y un pedido
CREATE FUNCTION fn_ObtenerProductos(@IDMESA BIGINT)
RETURNS TABLE
AS
RETURN
(
SELECT D.IDDETALLE,D.IDPEDIDO,D.IDPRODUCTO,LP.IDCATEGORIA,LP.CATEGORIA,LP.NOMBRE,LP.PRECIO,
	   LP.STOCK,LP.DESCRIPCION,LP.IDIMAGEN,LP.ARCHNOMB,D.CANTIDAD,D.SUBTOTAL,LP.GUARNICION FROM Pedidos P 
INNER JOIN Mesas M ON P.IDMESA = M.IDMESA
INNER JOIN DetallesPedido D ON P.IDPEDIDO = D.IDPEDIDO
INNER JOIN vw_ListaProductos LP ON D.IDPRODUCTO = LP.IDPRODUCTO
WHERE 
	P.IDMESA = @IDMESA 
	AND P.ESTADO = 'EN PROCESO' 
	AND M.ESTADO = 'OCUPADA'
	AND LP.ESTADO = '1'
);
GO

-- Tabla para obtener el pedido pendiente para la mesa indicada
CREATE FUNCTION fn_ObtenerPedido(@IDMESA BIGINT)
RETURNS TABLE
AS
RETURN
(
	SELECT P.IDPEDIDO,M.IDMESA,M.IDSALON,M.ESTADO AS ESTADOMESA,M.HABILITADA, P.ESTADO AS ESTADOPEDIDO FROM Pedidos P
	INNER JOIN Mesas M ON P.IDMESA = M.IDMESA
	WHERE 
		P.IDMESA = @IDMESA
		AND M.ESTADO = 'OCUPADA'
		AND P.ESTADO = 'EN PROCESO'
);
GO

-- Generar un pediodo nuevo
CREATE PROCEDURE sp_GenerarPedido(
	@pIDUSER BIGINT,
	@pIDMESA BIGINT
)AS
BEGIN
	INSERT Pedidos(IDMESA,IDUSUARIO,ESTADO)
	VALUES (@pIDMESA,@pIDUSER,'EN PROCESO')

	UPDATE Mesas SET ESTADO = 'OCUPADA' WHERE IDMESA = @pIDMESA
END
GO

CREATE PROCEDURE sp_CompletarPedido(
	@pIDPEDIDO BIGINT
)AS
BEGIN
	DECLARE @IDMESA BIGINT 
	SET @IDMESA = (SELECT IDMESA FROM Pedidos WHERE IDPEDIDO = @pIDPEDIDO)

	UPDATE Pedidos SET ESTADO = 'COMPLETADO' WHERE IDPEDIDO = @pIDPEDIDO
	UPDATE Mesas SET ESTADO = 'PENDIENTE' WHERE IDMESA = @IDMESA
END
GO

CREATE PROCEDURE sp_AgregarVenta(
	@pIDPEDIDO BIGINT,
	@pIDUSUARIO BIGINT,
	@pTOTAL MONEY
)AS
BEGIN
	INSERT Ventas (IDPEDIDO,IDUSUARIO,TOTAL)
	VALUES (@pIDPEDIDO,@pIDUSUARIO,@pTOTAL)
END
GO


-- Agregar un producto a un detalle de pedido
CREATE PROCEDURE sp_AgregarProdAlPedido(
	@pIDPRODUCTO BIGINT,
	@pIDPEDIDO BIGINT,
	@pCANTIDAD INT
)AS
BEGIN
	
	DECLARE @SubTotal money

	SET @SubTotal = (SELECT PRECIO*@pCANTIDAD FROM Productos WHERE IDPRODUCTO = @pIDPRODUCTO)

	INSERT DetallesPedido (IDPEDIDO,IDPRODUCTO,CANTIDAD,SUBTOTAL)
	VALUES (@pIDPEDIDO,@pIDPRODUCTO,@pCANTIDAD,@SubTotal)
END
GO



--	Datos 

EXEC sp_AgregarProd '1','Cafe solo','1500','120','Cafe de maquina solo','0'
EXEC sp_AgregarProd '1','Cafe con leche','2000','120','Cafe de maquina con leche','0'
EXEC sp_AgregarProd '1','Latte Clasico','2500','80','20% Cafe y 80% leche','0'
EXEC sp_AgregarProd '1','Capuchino Clasico','2500','60','Cafe, leche embulsionada con canela y cacao','0'
EXEC sp_AgregarProd '1','Te negro ','1500','120','Te negro ingles clasico','0'

EXEC sp_AgregarProdEntr 'Tequeños','11000','20','6 unidades de de masa de harina de trigo frita, rellena de queso blanco','0'
EXEC sp_AgregarProdEntr 'Papas Bravas','12300','27','papas fritas con salsa chedar, verdeo y panceta','0'
EXEC sp_AgregarProdEntr 'Chicken Fingers','14200','18','Tiras de pollo frito con salsas aioli i bbq','0'
EXEC sp_AgregarProdEntr 'Empanada Frita','5000','44','Empanadas de Carne cortada a cuchillo fritas','1'
EXEC sp_AgregarProdEntr 'Bocaditos de Espinaca','3000','64','5 unidades de bocaditos de espinaca y muzzarella','1'

EXEC sp_AgregarProd '3','Milanesa de carne','8200','60','Milanesa de Carne sola con guarnicion','1'
EXEC sp_AgregarProd '3','Milanesa de Pollo','7200','80','Milanesa de Pollo sola con guarnicion','1'
EXEC sp_AgregarProd '3','Milanesa Napolitana','10500','80','Milanesa de carne o pollo a la napolitana con guarnicion','1'
EXEC sp_AgregarProd '3','Ravioles de ricota','11000','60','Ravioles de Ricota con salsa a eleccion','0'
EXEC sp_AgregarProd '3','Hamburguesa completa','9000','33','Hamburguesa con jamon, queso, lechuga, tomate y huevo con guarnicion','1'

EXEC sp_AgregarProdPost 'Flan casero','4100','25','Flan casero solo','1','1'
EXEC sp_AgregarProdPost 'Flan sing gluten','5100','10','Flan sin gluten','1','0'
EXEC sp_AgregarProdPost 'Budin de Pan','4100','25','Budin casero solo','1','1'
EXEC sp_AgregarProdPost 'Ensalada de Frutas','5000','10','Ensalada de frutas de estacion sin agregados','0','0'
EXEC sp_AgregarProdPost 'Helado tricolor','4200','20','Helado de chocolate, frutilla y vainilla','1','1'

EXEC sp_AgregarProdBeb 'Agua','1000','100','Agua mineral sin gas','0','500'
EXEC sp_AgregarProdBeb 'Agua c/gas','1000','100','Agua mineral con gas','0','500'
EXEC sp_AgregarProdBeb 'Coca Cola','3000','80','Coca-Cola Orignial','0','500'
EXEC sp_AgregarProdBeb 'Cerveza Corona','3000','40','Cerveza Corona Orignial','1','500'
EXEC sp_AgregarProdBeb 'Limonada Clasica','5500','20','Jugo de limon, agua, menta, jemgibre y azucar','0','1500'

EXEC sp_AgregarProdExtra '6','Papas Fritas','0','1200','Papas Fritas caseras'
EXEC sp_AgregarProdExtra '6','Pure de Papas','0','1200','Pure de papa'
EXEC sp_AgregarProdExtra '6','Ensalada','0','1200','Ensalada 3 ingredientes'
EXEC sp_AgregarProdExtra '6','Papas al Horno','0','1200','Papas al Horno'


EXEC sp_AgregarProdExtra '7','Entera','0','1200','Leche Entera'
EXEC sp_AgregarProdExtra '7','Descremada','1000','1200','Leche Descremada'
EXEC sp_AgregarProdExtra '7','Almendras','2000','1200','Leche de Almendras'
EXEC sp_AgregarProdExtra '7','Coco','2000','1200','Leche de Coco'

EXEC sp_AgregarProdExtra '8','Mediano','0','1200','Taza mediana, tamaño normal'
EXEC sp_AgregarProdExtra '8','Grande','1000','1200','Taza Grande'

