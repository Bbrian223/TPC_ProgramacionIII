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

    }
}
