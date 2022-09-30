using ConsumoAPI.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace aplicacion.Datos
{
    public class Conexion
    {
        public string cadenaSQL = string.Empty;
        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }
        public string getCadenaSQL() {
            return cadenaSQL;
        }

        public SqlConnection getSQLConexion()
        {
            return new SqlConnection(cadenaSQL);
        }

        public SqlCommand Procedure(string procedimiento, bool parms, List<TbParametros> parametros, SqlConnection conex)
        {
            SqlCommand cmd = new SqlCommand(procedimiento, conex);
            if (parms)
            {
                foreach (TbParametros p in parametros)
                {

                    int numericValue;
                    bool isNumber = int.TryParse(p.valor, out numericValue);
                    if (isNumber)
                    {
                        var pm = new SqlParameter(p.columna, numericValue);
                        cmd.Parameters.Add(pm);
                    }
                    else
                    {
                        var pm = new SqlParameter(p.columna, p.valor);
                        cmd.Parameters.Add(pm);
                    }
                }
            }
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;

        }

    }
}
