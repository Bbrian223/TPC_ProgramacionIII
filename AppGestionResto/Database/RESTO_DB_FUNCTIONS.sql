USE RESTO_DB
GO

/*		Usuarios		*/

CREATE VIEW vw_ListaEmpleados
AS
SELECT E.IDEMPLEADO,E.IDUSUARIO,E.NOMBRE,E.APELLIDO,E.DOCUMENTO,E.FECHA_NACIMIENTO,
	   E.FECHA_INGRESO,D.IDDIRECCION,D.CALLE,D.NUM_DIRECCION,D.LOCALIDAD,D.COD_POSTAL,
	   E.EMAIL,E.TELEFONO,U.IDROL,Img.NOMBRE AS 'ARCHIMAGEN',U.ESTADO 
FROM EMPLEADOS E
INNER JOIN Usuarios U ON E.IDUSUARIO = U.IDUSUARIO
INNER JOIN Direcciones D ON E.IDDIRECCION = D.IDDIRECCION
INNER JOIN ImagenesPerfil Img ON E.IDUSUARIO = Img.IDUSUARIO
GO

-- Agregar Empleado
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
	@pIdRol INT,
	@pNombreUser VARCHAR(100)
	)AS
BEGIN 
	
	DECLARE @IdUsuario BIGINT
	DECLARE @IdDireccion BIGINT

	IF EXISTS (SELECT 1 FROM Empleados WHERE DOCUMENTO = @pDNI)
	BEGIN
		RETURN
	END

	INSERT Usuarios (NOMBRE,CONTRASENIA,IDROL)
	VALUES (@pNombreUser,@pDni,@pIdRol)

	INSERT Direcciones (CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL)
	VALUES (@pCalle,@pNumDir,@pLocalidad,@pCodPos)

	SELECT @IdDireccion = IDENT_CURRENT('Direcciones')
	SELECT @IdUsuario = IDENT_CURRENT('Usuarios')

	INSERT INTO Empleados(IDUSUARIO,IDDIRECCION,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,EMAIL,TELEFONO)
	VALUES (@IdUsuario,@IdDireccion,@pNombre,@pApellido,@pDni,@pFechaNac,@pEmail,@pTelefono)

	INSERT INTO ImagenesPerfil(IDUSUARIO, NOMBRE)
	VALUES (@IdUsuario, 'Usuario-' + CONVERT(NVARCHAR(50), @IdUsuario));	
END
GO

-- Editar Usuario
CREATE PROCEDURE  sp_EditarUsuarios(
	@pIDUSUARIO BIGINT,
	@pCALLE VARCHAR(50),
	@pNUMDIR VARCHAR(4),
	@pLOCALIDAD VARCHAR(100),
	@pCODPOSTAL VARCHAR(5),
	@pEMAIL VARCHAR(100),
	@pTELEFONO VARCHAR(15),
	@pIDROL INT
	)AS
BEGIN
	
	DECLARE @IDDIRECCION BIGINT;

	SET @IDDIRECCION = (SELECT IDDIRECCION FROM Empleados WHERE IDUSUARIO = @pIDUSUARIO)

	UPDATE Direcciones SET CALLE = @pCALLE WHERE IDDIRECCION = @IDDIRECCION
	UPDATE Direcciones SET NUM_DIRECCION = @pNUMDIR WHERE IDDIRECCION = @IDDIRECCION 
	UPDATE Direcciones SET LOCALIDAD = @pLOCALIDAD WHERE IDDIRECCION = @IDDIRECCION 
	UPDATE Direcciones SET COD_POSTAL=@pCODPOSTAL WHERE IDDIRECCION = @IDDIRECCION
	
	UPDATE Empleados SET EMAIL=@pEMAIL WHERE IDUSUARIO = @pIDUSUARIO
	UPDATE Empleados SET TELEFONO=@pTELEFONO WHERE IDUSUARIO = @pIDUSUARIO
	

	UPDATE Usuarios SET IDROL = @pIDROL WHERE IDUSUARIO = @pIDUSUARIO
END
GO
-- cambiar contrasenia
CREATE PROCEDURE sp_CambiarPass(
	@pIdUsuario BIGINT,
	@pActualPass VARCHAR(50),
	@pNuevaPass VARCHAR(50)
)AS
BEGIN

	DECLARE @PASS VARCHAR(50)

	SET @PASS = (SELECT CONTRASENIA FROM Usuarios WHERE IDUSUARIO = @pIdUsuario)

	IF(@PASS = @pActualPass)
	BEGIN
		UPDATE Usuarios SET CONTRASENIA = @pNuevaPass WHERE IDUSUARIO = @pIdUsuario
	END

END
GO

-- Restablecer contrasenia
CREATE PROCEDURE sp_RestablecerPass(
	@pIDUSUARIO BIGINT
)AS
BEGIN
	DECLARE @DNI VARCHAR(10);

	SET @DNI = (SELECT DOCUMENTO FROM Empleados WHERE IDUSUARIO = @pIDUSUARIO)

	UPDATE Usuarios SET CONTRASENIA = @DNI WHERE IDUSUARIO = @pIDUSUARIO

END
GO

-- funcion para vertificar si el dni ingresado ya existe en la db
CREATE FUNCTION fn_VerificarExisteUsuario(@DNI VARCHAR(50))
RETURNS BIT
AS
BEGIN
	DECLARE @EXISTE BIT = 0;

	IF EXISTS (SELECT 1 FROM Empleados WHERE DOCUMENTO = @DNI)
	BEGIN
		SET @EXISTE = 1;
	END

	RETURN @EXISTE
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

CREATE VIEW vw_ListaAdicionales
AS
	SELECT*FROM vw_ListaProductos 
	WHERE IDCATEGORIA = '6'
		  OR IDCATEGORIA = '7'
		  OR IDCATEGORIA = '8'
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

-- Editar Productos
CREATE PROCEDURE sp_EditarProducto(
	@pIDPRODUCTO BIGINT,
	@pPRECIO MONEY,
	@pSTOCK INT,
	@pDESCRIPCION NVARCHAR(MAX),
	@pNOMBRE VARCHAR(100)
)AS
BEGIN

	UPDATE Productos SET PRECIO = @pPRECIO WHERE IDPRODUCTO = @pIDPRODUCTO
	UPDATE Productos SET STOCK = @pSTOCK WHERE IDPRODUCTO = @pIDPRODUCTO
	UPDATE Productos SET DESCRIPCION = @pDESCRIPCION WHERE IDPRODUCTO = @pIDPRODUCTO
	UPDATE Productos SET NOMBRE = @pNOMBRE WHERE IDPRODUCTO = @pIDPRODUCTO
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

-- Lista de pedidos
CREATE VIEW vw_ListaPedidos
AS
SELECT P.IDPEDIDO,M.IDSALON,P.IDMESA,P.IDUSUARIO,P.ESTADO FROM Pedidos P 
INNER JOIN Mesas M ON P.IDMESA = M.IDMESA
GO

-- Lista de Ventas
CREATE VIEW vw_ListaVentas
AS
SELECT V.IDVENTA,V.IDPEDIDO,M.IDSALON,P.IDMESA,V.IDUSUARIO,V.FECHA_HORA AS FECHA,P.ESTADO,V.TOTAL FROM Ventas V
INNER JOIN Pedidos P ON V.IDPEDIDO = P.IDPEDIDO
INNER JOIN Mesas M ON P.IDMESA = M.IDMESA
GO

-- Lista de pedidos por empleado
CREATE VIEW vw_ListaPedidosPorEmpleado
AS
SELECT E.IDEMPLEADO, E.NOMBRE, E.APELLIDO, COALESCE(COUNT(P.IDPEDIDO), 0) AS CANTIDAD
FROM Empleados E
LEFT JOIN Pedidos P ON P.IDUSUARIO = E.IDUSUARIO
LEFT JOIN Ventas V ON P.IDPEDIDO = V.IDPEDIDO
LEFT JOIN Usuarios U ON P.IDUSUARIO = U.IDUSUARIO
WHERE CAST(GETDATE() AS DATE) = CAST(V.FECHA_HORA AS DATE) OR P.IDPEDIDO IS NULL
GROUP BY E.IDEMPLEADO, E.NOMBRE, E.APELLIDO 
GO

-- Lista de total de venta de la ultima semana
CREATE VIEW vw_VentaSemana
AS
WITH DiasSemana AS (
    SELECT 'Sunday' AS DIA
    UNION ALL SELECT 'Monday'
    UNION ALL SELECT 'Tuesday'
    UNION ALL SELECT 'Wednesday'
    UNION ALL SELECT 'Thursday'
    UNION ALL SELECT 'Friday'
    UNION ALL SELECT 'Saturday'
)
SELECT 
    ds.DIA, 
    ISNULL(SUM(v.TOTAL), 0) AS TOTAL
FROM DiasSemana ds
LEFT JOIN Ventas v 
    ON DATENAME(WEEKDAY, v.FECHA_HORA) = ds.DIA
    AND v.FECHA_HORA >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) -- �ltimos 7 d�as
    AND v.FECHA_HORA < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))  -- Hasta hoy
GROUP BY ds.DIA
GO

-- TOTAL RECAUDADO DEL DIA ACTUAL
CREATE VIEW vs_TotalRecaudadoDia
AS
SELECT SUM(TOTAL) AS TOTAL FROM Ventas V
INNER JOIN Pedidos P ON V.IDPEDIDO = P.IDPEDIDO
WHERE CAST(FECHA_HORA AS DATE) = CAST(GETDATE() AS DATE)
	  AND P.ESTADO = 'COMPLETADO'
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
	DECLARE @IDPEDIDO BIGINT
	
	INSERT Pedidos(IDMESA,IDUSUARIO,ESTADO,FECHA)
	VALUES (@pIDMESA,@pIDUSER,'EN PROCESO',GETDATE())
	
	SELECT @IDPEDIDO = IDENT_CURRENT('Pedidos')
	
	INSERT Ventas (IDPEDIDO,IDUSUARIO,TOTAL)
	VALUES (@IDPEDIDO,@pIDUSER,'0')


	UPDATE Mesas SET ESTADO = 'OCUPADA' WHERE IDMESA = @pIDMESA
END
GO

-- Cerrar Pedido
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

-- Cancelar Pedido
CREATE PROCEDURE sp_CancelarPedido(
	@pIDPEDIDO BIGINT
)AS
BEGIN
	DECLARE @IDMESA BIGINT 
	SET @IDMESA = (SELECT IDMESA FROM Pedidos WHERE IDPEDIDO = @pIDPEDIDO)

	UPDATE Pedidos SET ESTADO = 'CANCELADO' WHERE IDPEDIDO = @pIDPEDIDO
	UPDATE Mesas SET ESTADO = 'PENDIENTE' WHERE IDMESA = @IDMESA
END
GO

-- Funcion para obtener una lista de productos ligada a un pedido/venta
CREATE FUNCTION fn_ObtenerProductoPedido(@IDVENTA BIGINT)
RETURNS TABLE
AS
RETURN
(	
	SELECT DP.IDPRODUCTO, P.IDCATEGORIA, C.NOMBRE AS CATEGNOMBRE, P.NOMBRE, P.PRECIO,DP.CANTIDAD,DP.ACLARACIONES,DP.SUBTOTAL FROM DetallesPedido DP
	INNER JOIN Productos P ON DP.IDPRODUCTO = P.IDPRODUCTO
	INNER JOIN Ventas V ON DP.IDPEDIDO = V.IDPEDIDO
	INNER JOIN Categorias C ON P.IDCATEGORIA = C.IDCATEGORIA
	WHERE V.IDVENTA = @IDVENTA
);
GO

-- Agregar a la tabla de ventas
CREATE PROCEDURE sp_AgregarVenta(
	@pIDPEDIDO BIGINT,
	@pIDUSUARIO BIGINT,
	@pTOTAL MONEY
)AS
BEGIN
	UPDATE Ventas SET TOTAL = @pTOTAL WHERE IDPEDIDO = @pIDPEDIDO AND IDUSUARIO = @pIDUSUARIO
END
GO


-- Agregar un producto a un detalle de pedido
CREATE PROCEDURE sp_AgregarProdAlPedido(
	@pIDPRODUCTO BIGINT,
	@pIDPEDIDO BIGINT,
	@pCANTIDAD INT,
	@pACLARACIONES TEXT
)AS
BEGIN
	
	DECLARE @SubTotal money
	DECLARE @nuevoStock int

	SET @SubTotal = (SELECT PRECIO*@pCANTIDAD FROM Productos WHERE IDPRODUCTO = @pIDPRODUCTO)
	SET @nuevoStock	= (SELECT STOCK-@pCANTIDAD FROM Productos WHERE IDPRODUCTO = @pIDPRODUCTO)


	INSERT DetallesPedido (IDPEDIDO,IDPRODUCTO,CANTIDAD,SUBTOTAL,ACLARACIONES)
	VALUES (@pIDPEDIDO,@pIDPRODUCTO,@pCANTIDAD,@SubTotal,@pACLARACIONES)

	UPDATE Productos SET STOCK = @nuevoStock WHERE IDPRODUCTO = @pIDPRODUCTO
END
GO

-- Eliminar producto de un pedido activo -- hace la devolucion a stock
CREATE PROCEDURE sp_EliminarProdDePedido(
	@pIDDETALLE BIGINT
	)AS
BEGIN
	
	DECLARE @IDPRODUCTO BIGINT
	DECLARE @CANTIDAD INT
	DECLARE @STOCK INT

	SET @IDPRODUCTO = (SELECT IDPRODUCTO FROM DetallesPedido WHERE IDDETALLE = @pIDDETALLE)
	SET @CANTIDAD = (SELECT CANTIDAD FROM DetallesPedido WHERE IDDETALLE = @pIDDETALLE)
	SET @STOCK = (SELECT STOCK FROM Productos WHERE IDPRODUCTO = @IDPRODUCTO)

	UPDATE Productos SET STOCK = (@STOCK+@CANTIDAD) WHERE IDPRODUCTO = @IDPRODUCTO
	
	DELETE DetallesPedido WHERE IDDETALLE = @pIDDETALLE

END
GO


/*	Mesas	*/

-- Vista de salones con mesas ocupadas
CREATE VIEW vw_SalonesOcupados
AS
SELECT DISTINCT S.IDSALON FROM Mesas M
INNER JOIN Salones S ON M.IDSALON = S.IDSALON
WHERE M.ESTADO != 'DISPONIBLE'
GO

-- Cantidad de mesas por estado
CREATE VIEW vw_CantidadMesasPorEstado
AS
WITH Estados AS (
    SELECT 'DISPONIBLE' AS ESTADO
    UNION ALL SELECT 'OCUPADA'
    UNION ALL SELECT 'PENDIENTE'
)
SELECT 
    e.ESTADO, 
    ISNULL(COUNT(m.ESTADO), 0) AS CANTIDAD
FROM Estados e
LEFT JOIN Mesas m 
    ON e.ESTADO = m.ESTADO 
    AND m.HABILITADA = '1' 
GROUP BY e.ESTADO
GO

-- OBTENER CANTIDAD DE MESAS POR ESTADO
CREATE VIEW vw_PedidosPorEstado
AS
WITH Estados AS (
    SELECT 'COMPLETADO' AS ESTADO
    UNION ALL SELECT 'EN PROCESO'
    UNION ALL SELECT 'CANCELADO'
)
SELECT 
    e.ESTADO, 
    ISNULL(COUNT(p.ESTADO), 0) AS CANTIDAD
FROM Estados e
LEFT JOIN Pedidos p 
    ON e.ESTADO = p.ESTADO 
    AND CAST(p.FECHA AS DATE) = CAST(GETDATE() AS DATE) 
GROUP BY e.ESTADO
GO


-- Tabla de salones habilitados para cada empleado
CREATE FUNCTION fn_ListaSalonesPorId(
	@IDEMPLEADO BIGINT
)
RETURNS TABLE
AS
RETURN
(
	SELECT DISTINCT M.IDSALON, S.NOMBRE, S.ESTADO FROM Mesas_X_Empleados ME
	INNER JOIN Mesas M ON ME.IDMESA = M.IDMESA
	INNER JOIN Salones S ON M.IDSALON = S.IDSALON
	WHERE 
		ME.IDEMPLEADO = @IDEMPLEADO
		AND S.ESTADO = '1'
);
GO

-- Lista de mesas por salon e id empleado
CREATE FUNCTION fn_ListaMesasPorEmpleado(
	@IDEMPLEADO BIGINT,
	@IDSALON BIGINT
)
RETURNS TABLE
AS
RETURN
(
	SELECT DISTINCT ME.IDMESA,M.IDSALON,M.ESTADO,M.HABILITADA FROM Mesas_X_Empleados ME
	INNER JOIN Mesas M ON ME.IDMESA = M.IDMESA
	INNER JOIN Salones S ON M.IDSALON = M.IDSALON
	WHERE ME.IDEMPLEADO = @IDEMPLEADO
		  AND M.IDSALON = @IDSALON
		  AND S.ESTADO = '1'
);
GO

-- Vista de mesas
CREATE VIEW vw_ObtenerMesas
AS
SELECT M.IDMESA,M.IDSALON,M.ESTADO,M.HABILITADA,COUNT(MXE.IDEMPLEADO) AS ASIGNADOS FROM Mesas M
LEFT JOIN Mesas_X_Empleados MXE ON M.IDMESA = MXE.IDMESA
GROUP BY M.IDMESA, M.IDSALON, M.ESTADO, M.HABILITADA
GO

-- Asignar Mesa a Empleado
CREATE PROCEDURE sp_AsignarMesa(
	@IDEMPLEADO BIGINT,
	@IDMESA BIGINT
)AS
BEGIN
	INSERT INTO Mesas_X_Empleados (IDMESA, IDEMPLEADO)
	SELECT @IDMESA, @IDEMPLEADO
	WHERE NOT EXISTS (
		SELECT 1 FROM Mesas_X_Empleados 
		WHERE IDMESA = @IDMESA AND IDEMPLEADO = @IDEMPLEADO
	);

END
GO

-- Obtener empleados asignados a mesas
CREATE PROCEDURE sp_ListaEmpleadosPorMesa(
	@IDMESA BIGINT
)AS
BEGIN
	SELECT L.IDEMPLEADO,L.IDUSUARIO,L.NOMBRE,L.APELLIDO,L.DOCUMENTO,L.FECHA_NACIMIENTO,L.FECHA_INGRESO,L.IDROL,L.ESTADO FROM vw_ListaEmpleados L
	INNER JOIN Mesas_X_Empleados ME ON L.IDEMPLEADO = ME.IDEMPLEADO
	WHERE ME.IDMESA = @IDMESA
END
GO

-- Actualiza el estado de los salones y las mesas en consecuencia
CREATE PROCEDURE sp_ActualizarSalones(
	@pIDSALON BIGINT,
	@pESTADO BIT
)AS
BEGIN

	UPDATE Salones SET ESTADO = @pESTADO WHERE IDSALON = @pIDSALON
	UPDATE Mesas SET HABILITADA = @pESTADO WHERE IDSALON = @pIDSALON

	IF @pESTADO = '0'
	BEGIN
		-- BAJA DE SALON, MESAS Y PEDIDOS

		UPDATE Pedidos  SET ESTADO = 'CANCELADO'
		WHERE IDPEDIDO IN (
			SELECT P.IDPEDIDO FROM Pedidos P
			INNER JOIN Mesas M ON P.IDMESA = M.IDMESA
			WHERE M.IDSALON = @pIDSALON AND P.ESTADO != 'COMPLETADO'
		);

		UPDATE Mesas SET ESTADO = 'PENDIENTE' WHERE IDSALON = @pIDSALON

	END
	ELSE BEGIN
		UPDATE Mesas SET ESTADO = 'DISPONIBLE' WHERE IDSALON = @pIDSALON
	END
END
GO

/*	Actualiza es estado de la mesa	*/
CREATE PROCEDURE sp_ModificarEstadoMesa(
	@pIDMESA BIGINT,
	@pESTADO BIT
)AS
BEGIN
	UPDATE Mesas SET HABILITADA = @pESTADO WHERE IDMESA = @pIDMESA

	IF @pESTADO = '1'
	BEGIN
		-- ASEGURA QUE EL SALON ESTE HABILITADO
		UPDATE Salones SET ESTADO = '1'
		WHERE IDSALON IN(
			SELECT IDSALON FROM Mesas WHERE IDMESA = @pIDMESA
		);

		UPDATE Mesas SET ESTADO = 'DISPONIBLE' WHERE IDMESA = @pIDMESA
	END
	ELSE BEGIN
		UPDATE Pedidos  SET ESTADO = 'CANCELADO'
		WHERE IDPEDIDO IN (
			SELECT IDPEDIDO FROM Pedidos WHERE IDMESA = @pIDMESA AND ESTADO != 'COMPLETADO'
		);

		UPDATE Mesas SET ESTADO = 'PENDIENTE' WHERE IDMESA = @pIDMESA
	END
END
GO


EXEC sp_AgregarProd '1','Cafe solo','1500','120','Cafe de maquina solo','0'
EXEC sp_AgregarProd '1','Cafe con leche','2000','120','Cafe de maquina con leche','0'
EXEC sp_AgregarProd '1','Latte Clasico','2500','80','20% Cafe y 80% leche','0'
EXEC sp_AgregarProd '1','Capuchino Clasico','2500','60','Cafe, leche embulsionada con canela y cacao','0'
EXEC sp_AgregarProd '1','Te negro ','1500','120','Te negro ingles clasico','0'

EXEC sp_AgregarProdEntr 'Teque�os','11000','20','6 unidades de de masa de harina de trigo frita, rellena de queso blanco','0'
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

EXEC sp_AgregarProdExtra '8','Mediano','0','1200','Taza mediana, tama�o normal'
EXEC sp_AgregarProdExtra '8','Grande','1000','1200','Taza Grande'

