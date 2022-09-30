using aplicacion.Datos;
using aplicacion.Models;
using ConsumoAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace aplicacion.Datos
{
    public class DatosEventos
    {
        public List<EventosModels> Listar(string Nombre_Institucion = "")
        {
            var oListar = new List<EventosModels>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ListarEventos", conexion);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre_Institucion", "");
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        oListar.Add(new EventosModels()
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre_Institucion_Superior = dr["Nombre_Institucion_Superior"].ToString(),
                            Direccion_Institucion = dr["Direccion_Institucion"].ToString(),                            
                            Numero_Alumno = Convert.ToInt32(dr["Numero_Alumno"]),
                            Hora_inicio = dr["Hora_inicio"].ToString(),
                            Valor_Servicio = Convert.ToDouble(dr["Valor_Servicio"])
                        });
                    }
                }
            }
            return oListar;
        }

        public bool Guardar(EventosModels model)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = cn.getSQLConexion())
                {

                    conexion.Open();
                    List<TbParametros> parametros = new List<TbParametros>() {
                       new TbParametros { columna = "Id", valor = "0"},
                       new TbParametros { columna = "Nombre_Institucion", valor = model.Nombre_Institucion_Superior.ToString()},
                       new TbParametros { columna = "Direccion_Institucion", valor = model.Direccion_Institucion.ToString()},
                       new TbParametros { columna = "Numero_Alumno", valor = model.Numero_Alumno.ToString()},
                       new TbParametros { columna = "Hora_inicio", valor = model.Hora_inicio.ToString()},
                       new TbParametros { columna = "Valor_Servicio", valor = model.Valor_Servicio.ToString()}
                    };

                    var procedimiento = "RegistroEvento";
                    var cmd = cn.Procedure(procedimiento, true, parametros, conexion);
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;

            }


            return rpta;
        }

        public bool Editar(EventosModels model)
        {
            bool rpta;
            try
            {

                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    List<TbParametros> parametros = new List<TbParametros>() {
                       new TbParametros { columna = "Id", valor = model.Id.ToString()},
                       new TbParametros { columna = "Nombre_Institucion", valor = model.Nombre_Institucion_Superior.ToString()},
                       new TbParametros { columna = "Direccion_Institucion", valor = model.Direccion_Institucion.ToString()},
                       new TbParametros { columna = "Numero_Alumno", valor = model.Numero_Alumno.ToString()},
                       new TbParametros { columna = "Hora_inicio", valor = model.Hora_inicio.ToString()},
                       new TbParametros { columna = "Valor_Servicio", valor = model.Valor_Servicio.ToString()}
                    };

                    var procedimiento = "RegistroEvento";
                    var cmd = cn.Procedure(procedimiento, true, parametros, conexion);
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Eliminar(int IdEvento)
        {
            bool rpta;
            try
            {

                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("EliminarrEventos", conexion);
                    cmd.Parameters.AddWithValue("IdEvento", IdEvento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
        public EventosModels Obtener(int IdEvento)
        {

            var oEvento = new EventosModels();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerListarEventos", conexion);
                cmd.Parameters.AddWithValue("IdEvento", IdEvento);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEvento.Id = Convert.ToInt32(dr["id"]);
                        oEvento.Nombre_Institucion_Superior = dr["Nombre_Institucion_Superior"].ToString();
                        oEvento.Direccion_Institucion = dr["Direccion_Institucion"].ToString();
                        oEvento.Numero_Alumno = Convert.ToInt32(dr["Numero_Alumno"]);
                        oEvento.Hora_inicio = dr["Hora_inicio"].ToString();
                        oEvento.Valor_Servicio = Convert.ToDouble(dr["Valor_Servicio"]);
                    }
                }
            }
            return oEvento;
        }


    }
}
