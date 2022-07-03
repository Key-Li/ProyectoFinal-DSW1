using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoFinal_DSW1.DAO
{
    public class ConexionDAO
    {
        SqlConnection cn = new SqlConnection(
    ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        public SqlConnection getcn
        {
            get { return cn; }
        }
    }
}