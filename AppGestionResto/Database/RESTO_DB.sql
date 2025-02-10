CREATE DATABASE RESTO_DB
GO
USE RESTO_DB
GO

SET LANGUAGE Spanish;
GO

/*	ROLES USUARIO	*/
CREATE TABLE Roles(
	IDROL INT IDENTITY(1,1),
	NOMBRE VARCHAR(50) NOT NULL,

	PRIMARY KEY(IDROL)
)
GO

/*	TABLA USUARIOS	*/
CREATE TABLE Usuarios(
	IDUSUARIO BIGINT IDENTITY(1,1),
	NOMBRE VARCHAR(50) NOT NULL,
	CONTRASENIA VARCHAR(50) NOT NULL,
	ESTADO BIT DEFAULT 1,
	IDROL INT,

	PRIMARY KEY(IDUSUARIO),
	FOREIGN KEY(IDROL) REFERENCES Roles(IDROL)
)
GO

/*	TABLA DIRECCIONES	*/
CREATE TABLE Direcciones(
	IDDIRECCION BIGINT IDENTITY(1,1),
	CALLE VARCHAR(50) NOT NULL,
	NUM_DIRECCION VARCHAR(4) NOT NULL,
	LOCALIDAD VARCHAR(100) NOT NULL,
	COD_POSTAL VARCHAR(5) NOT NULL,

	PRIMARY KEY(IDDIRECCION),
)
GO

/*	Tabla de Empleados	*/
CREATE TABLE Empleados(
	IDEMPLEADO BIGINT IDENTITY(1,1),
	IDUSUARIO BIGINT,
	IDDIRECCION BIGINT,
	NOMBRE VARCHAR(100) NOT NULL,
	APELLIDO VARCHAR(100) NOT NULL,
	DOCUMENTO VARCHAR(10) NOT NULL,
	FECHA_NACIMIENTO DATE NOT NULL,
	FECHA_INGRESO DATE DEFAULT GETDATE(),
	EMAIL VARCHAR(100),
	TELEFONO VARCHAR(15),

	PRIMARY KEY(IDEMPLEADO),
	FOREIGN KEY(IDUSUARIO) REFERENCES Usuarios(IDUSUARIO),
	FOREIGN KEY(IDDIRECCION) REFERENCES Direcciones(IDDIRECCION)
)

/*	TABLA DE SALONES	*/
CREATE TABLE Salones(
	IDSALON BIGINT IDENTITY(1,1),
	NOMBRE VARCHAR(50) NOT NULL,
	ESTADO BIT NOT NULL DEFAULT 1,

	PRIMARY KEY(IDSALON)
)



/*	TABLA MESAS	*/
CREATE TABLE Mesas(
	IDSALON BIGINT,
	IDMESA BIGINT IDENTITY(1,1),
	ESTADO VARCHAR(100) NOT NULL,
	IDUSUARIO BIGINT NULL,
	HABILITADA BIT DEFAULT 1,

	PRIMARY KEY(IDMESA),
	FOREIGN KEY(IDSALON) REFERENCES Salones(IDSALON),
	CONSTRAINT CHK_ESTADO CHECK(ESTADO IN ('DISPONIBLE','OCUPADA','PENDIENTE'))
)
GO

/*	Categorias de producto*/
CREATE TABLE Categorias(
	IDCATEGORIA BIGINT IDENTITY(1,1),
	NOMBRE VARCHAR(50) NOT NULL,

	PRIMARY KEY(IDCATEGORIA)
)
GO

/*	TABLA PRODUCTOS	*/
CREATE TABLE Productos(
	IDPRODUCTO BIGINT IDENTITY(1,1),
	IDCATEGORIA BIGINT,
	NOMBRE VARCHAR(100) NOT NULL,
	PRECIO MONEY NOT NULL,
	STOCK INT NOT NULL,
	DESCRIPCION TEXT NOT NULL,
	ESTADO BIT DEFAULT 1,
	
	PRIMARY KEY(IDPRODUCTO),
)
GO


CREATE TABLE Entradas(
	IDPRODUCTO BIGINT,
	INDIVIDUAL BIT NOT NULL DEFAULT 0
)
GO


CREATE TABLE Postres(
	IDPRODUCTO BIGINT,
	GLUTEN BIT DEFAULT 0,
	AZUCAR BIT DEFAULT 0
)
GO

CREATE TABLE Bebidas(
	IDPRODUCTO BIGINT,
	ALCOHOL BIT DEFAULT 0,
	VOLUMEN INT NOT NULL,
)
GO

/*	TABLA de IMAGENES	*/

CREATE TABLE Imagenes(
	IDIMAGEN BIGINT IDENTITY(1,1),
	IDPRODUCTO BIGINT,
	NOMBRE VARCHAR(255),
)
GO

/*	TABLA PEDIDOS	*/
CREATE TABLE Pedidos(
	IDPEDIDO BIGINT IDENTITY(1,1),
	IDMESA BIGINT,
	IDUSUARIO BIGINT,
	ESTADO VARCHAR(100),
	
	PRIMARY KEY(IDPEDIDO),
	FOREIGN KEY(IDMESA) REFERENCES Mesas(IDMESA),
	FOREIGN KEY(IDUSUARIO) REFERENCES Usuarios(IDUSUARIO)
)
GO

/*	TABLE DETALLES PEDIDO	*/
CREATE TABLE DetallesPedido(
	IDDETALLE BIGINT IDENTITY(1,1),
	IDPEDIDO BIGINT,
	IDPRODUCTO BIGINT,
	CANTIDAD INT NOT NULL,
	SUBTOTAL MONEY NOT NULL,

	PRIMARY KEY(IDDETALLE),
	FOREIGN KEY(IDPEDIDO) REFERENCES Pedidos(IDPEDIDO),
	FOREIGN KEY(IDPRODUCTO) REFERENCES Productos(IDPRODUCTO)
)
GO

/*	TABLA VENTAS	*/
CREATE TABLE Ventas(
	IDVENTA BIGINT IDENTITY(1,1),
	IDPEDIDO BIGINT,
	IDUSUARIO BIGINT,
	FECHA_HORA DATETIME NOT NULL DEFAULT GETDATE(),
	TOTAL MONEY NOT NULL,

	PRIMARY KEY(IDVENTA),
	FOREIGN KEY(IDPEDIDO) REFERENCES Pedidos(IDPEDIDO),
	FOREIGN KEY(IDUSUARIO) REFERENCES Usuarios(IDUSUARIO)
)
GO



/******* Inputs *******/

/*	Roles de usuario --> ampliable	*/
INSERT INTO Roles(NOMBRE)
VALUES ('GERENTE'),('MOZO')

/*	Usuarios	*/
INSERT INTO Usuarios(NOMBRE,CONTRASENIA,IDROL)
VALUES ('Master','Master',1)
INSERT INTO Usuarios(NOMBRE,CONTRASENIA,IDROL)
VALUES ('Test','Test',2)

INSERT INTO Direcciones(CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL)
VALUES 
('Martin Rodriguez','2061','Villa Adelina','1607'),
('Entre Rios','4089','Munro','1605')

INSERT INTO Empleados(IDUSUARIO,IDDIRECCION,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,EMAIL,TELEFONO)
VALUES
(1,1,'Brian','Barrera','42832435','2000/08/02','Master@gmail.com','1167624662'),
(2,2,'Doris','Gomez','94256338','1969/04/24','Test@gmial.com','1138833931')

/******* Productos *******/

INSERT INTO Categorias(NOMBRE)
VALUES ('CAFETERIA'),('ENTRADAS'),('COMIDAS'),('POSTRES'),('BEBIDAS')

EXEC sp_AgregarProd '1','Cafe solo','1500','120','Cafe de maquina solo'
EXEC sp_AgregarProd '1','Cafe con leche','2000','120','Cafe de maquina con leche'
EXEC sp_AgregarProd '1','Latte Clasico','2500','80','20% Cafe y 80% leche'
EXEC sp_AgregarProd '1','Capuchino Clasico','2500','60','Cafe, leche embulsionada con canela y cacao'
EXEC sp_AgregarProd '1','Te negro ','1500','120','Te negro ingles clasico'

EXEC sp_AgregarProdEntr 'Tequeños','11000','20','6 unidades de de masa de harina de trigo frita, rellena de queso blanco','0'
EXEC sp_AgregarProdEntr 'Papas Bravas','12300','27','papas fritas con salsa chedar, verdeo y panceta','0'
EXEC sp_AgregarProdEntr 'Chicken Fingers','14200','18','Tiras de pollo frito con salsas aioli i bbq','0'
EXEC sp_AgregarProdEntr 'Empanada Frita','5000','44','Empanadas de Carne cortada a cuchillo fritas','1'
EXEC sp_AgregarProdEntr 'Bocaditos de Espinaca','3000','64','5 unidades de bocaditos de espinaca y muzzarella','1'

EXEC sp_AgregarProd '3','Milanesa de carne','8200','60','Milanesa de Carne sola con guarnicion'
EXEC sp_AgregarProd '3','Milanesa de Pollo','7200','80','Milanesa de Pollo sola con guarnicion'
EXEC sp_AgregarProd '3','Milanesa Napolitana','10500','80','Milanesa de carne o pollo a la napolitana con guarnicion'
EXEC sp_AgregarProd '3','Ravioles de ricota','11000','60','Ravioles de Ricota con salsa a eleccion'
EXEC sp_AgregarProd '3','Hamburguesa completa','9000','33','Hamburguesa con jamon, queso, lechuga, tomate y huevo con guarnicion'

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

/******* Salones y Mesas *******/

INSERT INTO Salones(NOMBRE)
VALUES ('SALON 1'),('SALON 2'),('SALON 3'),('SALON 4'),('SALON 5')

INSERT INTO Mesas (IDSALON, ESTADO)
SELECT 
    ((ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1) / 24) + 1 AS IDSALON, 
    'DISPONIBLE'
FROM master.dbo.spt_values
WHERE TYPE = 'P'
AND NUMBER BETWEEN 1 AND 120;
