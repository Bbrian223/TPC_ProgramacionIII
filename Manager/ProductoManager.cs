using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class ProductoManager
    {
        public List<Producto> ObtnerTodos() 
        { 
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto> ();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO,GUARNICION  from vw_ListaProductos WHERE IDCATEGORIA < 6");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (datos.Lector["DESCRIPCION"] is DBNull) ? string.Empty : (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];
                    prod.Guarnicion = (bool)datos.Lector["GUARNICION"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                datos.CerrarConeccion();
            }

           
        }

        public List<Producto> ObtnerGuarniciones()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO  from vw_ListaGuarniciones");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (datos.Lector["DESCRIPCION"] is DBNull) ? string.Empty : (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }


        }

        public List<Producto> ObtnerTipoLeche()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO  from vw_ListaLeches");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (datos.Lector["DESCRIPCION"] is DBNull) ? string.Empty : (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }


        }

        public List<Producto> ObtnerTamanioTaza()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO  from vw_ListaTazas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (datos.Lector["DESCRIPCION"] is DBNull) ? string.Empty : (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }


        }

        public List<Producto> ObtenerAdicionales()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO,GUARNICION FROM vw_ListaAdicionales ORDER BY IDCATEGORIA ASC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();

                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];
                    prod.Guarnicion = (bool)datos.Lector["GUARNICION"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }


        public List<Producto> ObtenerProductosConBajoStock(int min)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO,GUARNICION FROM vw_ListaProductos WHERE IDCATEGORIA <= '5' AND STOCK <= @MIN");
                datos.SetearParametro("@MIN",min);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (datos.Lector["DESCRIPCION"] is DBNull) ? string.Empty : (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];
                    prod.Guarnicion = (bool)datos.Lector["GUARNICION"];

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }
        public void AgregarProd(Producto prod) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProd @IdCategoria,@Nombre,@Precio,@Stock,@Descripcion,@GUARNICION");
                datos.SetearParametro("@IdCategoria", prod.Categoria.IdCategoria);
                datos.SetearParametro("@Nombre", prod.Nombre);
                datos.SetearParametro("@Precio", prod.Precio);
                datos.SetearParametro("@Stock", prod.stock);
                datos.SetearParametro("@Descripcion", prod.Descripcion);
                datos.SetearParametro("@GUARNICION",prod.Guarnicion);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void AgregarEntrada(Producto prod, bool Indiv) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProdEntr @Nombre,@Precio,@Stock,@Descripcion,@Individual");
                datos.SetearParametro("@Nombre", prod.Nombre);
                datos.SetearParametro("@Precio", prod.Precio);
                datos.SetearParametro("@Stock", prod.stock);
                datos.SetearParametro("@Descripcion", prod.Descripcion);
                datos.SetearParametro("@Individual",Indiv);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void AgregarPostre(Producto prod, bool azucar, bool gluten)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProdPost @Nombre,@Precio,@Stock,@Descripcion,@Azucar,@Gluten");
                datos.SetearParametro("@Nombre", prod.Nombre);
                datos.SetearParametro("@Precio", prod.Precio);
                datos.SetearParametro("@Stock", prod.stock);
                datos.SetearParametro("@Descripcion", prod.Descripcion);
                datos.SetearParametro("@Azucar", azucar);
                datos.SetearParametro("@Gluten",gluten);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void AgregarBebida(Producto prod, int vol, bool alcohol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProdBeb @Nombre,@Precio,@Stock,@Descripcion,@Alcohol,@Volumen");
                datos.SetearParametro("@Nombre", prod.Nombre);
                datos.SetearParametro("@Precio", prod.Precio);
                datos.SetearParametro("@Stock", prod.stock);
                datos.SetearParametro("@Descripcion", prod.Descripcion);
                datos.SetearParametro("@Alcohol", alcohol);
                datos.SetearParametro("@Volumen", vol);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void Baja(long idProd) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Productos SET ESTADO = '0' WHERE IDPRODUCTO = @IdProducto");
                datos.SetearParametro("@IdProducto",idProd);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public long UltimoId()
        {
            AccesoDatos datos = new AccesoDatos();
            long id = -1;

            try
            {
                datos.SetearConsulta("SELECT MAX(IDPRODUCTO) AS ID FROM Productos;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    id = (long)datos.Lector["ID"];
                }

                return id;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public Producto Obtener(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            Producto prod = new Producto();

            try
            {
                datos.SetearConsulta("SELECT IDPRODUCTO,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,IDIMAGEN,ARCHNOMB,ESTADO,GUARNICION  from vw_ListaProductos WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    prod.Imagen.IdImagen = (long)datos.Lector["IDIMAGEN"];
                    prod.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    prod.Estado = (bool)datos.Lector["ESTADO"];
                    prod.Guarnicion = (bool)datos.Lector["GUARNICION"];
                }

                return prod;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public int ObtenerStock(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            int stock = 0;

            try
            {
                datos.SetearConsulta("SELECT STOCK FROM Productos WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    stock = (int)datos.Lector["STOCK"];
                }

                return stock;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public bool Guarnicion(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            bool guarnicion = false;

            try
            {
                datos.SetearConsulta("SELECT GUARNICION FROM Productos WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    guarnicion = (bool)datos.Lector["GUARNICION"];
                }

                return guarnicion;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public bool EntradaIndividual(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            bool individual = false;

            try
            {
                datos.SetearConsulta("SELECT INDIVIDUAL FROM Entradas WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    individual = (bool)datos.Lector["INDIVIDUAL"];
                }

                return individual;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public Entradas ObtenerEntrada(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            Entradas entrada = new Entradas();     // podria obtenerlo llamando al metodo obtenerProducto

            try
            {
                datos.SetearConsulta("SELECT INDIVIDUAL FROM Entradas WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    entrada.Individual = (bool)datos.Lector["INDIVIDUAL"];
                }

                return entrada;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public Postres ObtenerPostre(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            Postres postre = new Postres();     // podria obtenerlo llamando al metodo obtenerProducto

            try
            {
                datos.SetearConsulta("SELECT GLUTEN,AZUCAR FROM Postres WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    postre.ContieneGluten = (bool)datos.Lector["GLUTEN"];
                    postre.ContieneAzucar = (bool)datos.Lector["AZUCAR"];
                }

                return postre;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public Bebidas ObtenerBebida(long idProd)
        {
            AccesoDatos datos = new AccesoDatos();
            Bebidas bebida = new Bebidas();     // podria obtenerlo llamando al metodo obtenerProducto

            try
            {
                datos.SetearConsulta("SELECT ALCOHOL,VOLUMEN FROM Bebidas WHERE IDPRODUCTO = @IDPRODUCTO");
                datos.SetearParametro("@IDPRODUCTO", idProd);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    bebida.Alcohol = (bool)datos.Lector["ALCOHOL"];
                    bebida.Volumen = (int)datos.Lector["VOLUMEN"];
                }

                return bebida;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void Editar(Producto prod)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_EditarProducto @IDPRODUCTO,@PRECIO,@STOCK,@DESCRIPCION,@NOMBRE");
                datos.SetearParametro("@IDPRODUCTO",prod.IdProducto);
                datos.SetearParametro("@NOMBRE", prod.Nombre);
                datos.SetearParametro("@PRECIO", prod.Precio);
                datos.SetearParametro("@STOCK", prod.stock);
                datos.SetearParametro("@DESCRIPCION", prod.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

    }
}
