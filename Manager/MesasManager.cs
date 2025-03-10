﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Manager
{
    public class MesasManager
    {
        public List<Mesa> ObtenerTodos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesa> lista = new List<Mesa>();

            try
            {
                datos.SetearConsulta("SELECT IDSALON,IDMESA,ESTADO,HABILITADA from Mesas ORDER BY IDMESA");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mesa = new Mesa();

                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];

                    lista.Add(mesa);
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

        public List<Mesa> ObtenerMesasPorSalon(int idSalon)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesa> lista = new List<Mesa>();

            try
            {
                datos.SetearConsulta("SELECT IDMESA,IDSALON,ESTADO,HABILITADA,ASIGNADOS FROM vw_ObtenerMesas WHERE IDSALON = @IDSALON ORDER BY IDMESA");
                datos.SetearParametro("@IDSALON",idSalon);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mesa = new Mesa();

                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];
                    mesa.EmplAsignados = (int)datos.Lector["ASIGNADOS"];

                    lista.Add(mesa);
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

        public List<Mesa> ObtenerMesasPorSalon(int idSalon, long idEmpleado)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesa> lista = new List<Mesa>();

            try
            {
                datos.SetearConsulta("SELECT IDMESA,IDSALON,HABILITADA,ESTADO FROM fn_ListaMesasPorEmpleado(@IDEMPLEADO,@IDSALON)");
                datos.SetearParametro("@IDEMPLEADO",idEmpleado);
                datos.SetearParametro("@IDSALON", idSalon);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mesa = new Mesa();

                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];

                    lista.Add(mesa);
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

        public Mesa ObtenerMesasPorID(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            Mesa mesa = new Mesa();

            try
            {
                datos.SetearConsulta("SELECT IDSALON,IDMESA,ESTADO,HABILITADA FROM Mesas WHERE IDMESA = @IDMESA");
                datos.SetearParametro("@IDMESA", idMesa);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];
                }

                return mesa;
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

        public List<Salon> ObtenerListaSalonesHabilitados() 
        {
            AccesoDatos datos = new AccesoDatos();
            List<Salon> lista = new List<Salon>();
            try
            {
                datos.SetearConsulta("SELECT IDSALON,NOMBRE,ESTADO FROM Salones WHERE ESTADO = '1'");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Salon salon = new Salon();

                    salon.IdSalon = (long)datos.Lector["IDSALON"];
                    salon.Nombre = (string)datos.Lector["NOMBRE"];
                    salon.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(salon);
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

        public List<Salon> ObtenerListaSalonesPorID(long IdEmpleado)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Salon> lista = new List<Salon>();
            try
            {
                datos.SetearConsulta("SELECT IDSALON,NOMBRE,ESTADO FROM fn_ListaSalonesPorId(@IDEMPLEADO)");
                datos.SetearParametro("@IDEMPLEADO",IdEmpleado);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Salon salon = new Salon();

                    salon.IdSalon = (long)datos.Lector["IDSALON"];
                    salon.Nombre = (string)datos.Lector["NOMBRE"];
                    salon.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(salon);
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

        public List<Salon> ObtenerListaTodosSalones()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Salon> lista = new List<Salon>();
            try
            {
                datos.SetearConsulta("SELECT IDSALON,NOMBRE,ESTADO FROM Salones");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Salon salon = new Salon();

                    salon.IdSalon = (long)datos.Lector["IDSALON"];
                    salon.Nombre = (string)datos.Lector["NOMBRE"];
                    salon.Estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(salon);
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

        public void ActualizarEstadoSalon (long idSalon, bool estado) 
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.SetearConsulta("EXEC sp_ActualizarSalones @IDSALON,@ESTADO");
                datos.SetearParametro("@IDSALON", idSalon);
                datos.SetearParametro("@ESTADO", estado);
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

        public void ActualizarEstadoMesa(int idMesa, bool estado) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_ModificarEstadoMesa @IDMESA, @ESTADO");
                datos.SetearParametro("@IDMESA",idMesa);
                datos.SetearParametro("@ESTADO",estado);
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

        public void AsignarMesa(long idEmpl, long idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AsignarMesa @IDEMPLEADO,@IDMESA");
                datos.SetearParametro("@IDEMPLEADO", idEmpl);
                datos.SetearParametro("@IDMESA", idMesa);
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

        public void desasignarEmpleadoMesa(long idEmpl, long idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE Mesas_X_Empleados WHERE IDMESA = @IDMESA AND IDEMPLEADO = @IDEMPLEADO");
                datos.SetearParametro("@IDMESA", idMesa); 
                datos.SetearParametro("@IDEMPLEADO",idEmpl);
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

        public void LiberarTodasLasMesas()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE Mesas_X_Empleados");
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

        public void LiberarMesa(long mesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE Mesas_X_Empleados WHERE IDMESA = @IDMESA");
                datos.SetearParametro("@IDMESA", mesa);
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

        public List<int> CantidadMesasPorEstado()
        {
            AccesoDatos datos = new AccesoDatos();
            List<int> lista = new List<int>();

            try
            {
                datos.SetearConsulta("SELECT*FROM vw_CantidadMesasPorEstado\r\nORDER BY \r\n    CASE ESTADO \r\n        WHEN 'DISPONIBLE' THEN 1\r\n        WHEN 'OCUPADA' THEN 2\r\n        WHEN 'PENDIENTE' THEN 3\r\n    END;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector["CANTIDAD"];

                    lista.Add(cantidad);
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
