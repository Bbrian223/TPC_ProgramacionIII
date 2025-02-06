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
                datos.SetearConsulta("select IDPRODUCTO,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,ESTADO  from vw_ListaProductos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto();
                    prod.IdProducto = (long)datos.Lector["IDPRODUCTO"];
                    prod.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    prod.Nombre = (string)datos.Lector["NOMBRE"];
                    prod.Precio = (decimal)datos.Lector["PRECIO"];
                    prod.stock = (int)datos.Lector["STOCK"];
                    prod.Descripcion = (string)datos.Lector["DESCRIPCION"];
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

        public void AgregarProd(Producto prod) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProd @IdCategoria,@Nombre,@Precio,@Stock,@Descripcion");
                datos.SetearParametro("@IdCategoria", prod.Categoria.IdCategoria);
                datos.SetearParametro("@Nombre", prod.Nombre);
                datos.SetearParametro("@Precio", prod.Precio);
                datos.SetearParametro("@Stock", prod.stock);
                datos.SetearParametro("@Descripcion", prod.Descripcion);
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
    }
}
