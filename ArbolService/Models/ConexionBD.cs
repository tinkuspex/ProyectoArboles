using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArbolService.Models
{
    public class ConexionBD
    {
        public SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString);
            return con;
        }
    }
}