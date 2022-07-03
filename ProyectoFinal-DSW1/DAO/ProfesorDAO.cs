using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinal_DSW1.Models;

namespace ProyectoFinal_DSW1.DAO
{
    public class ProfesorDAO
    {
        ConexionDAO cn = new ConexionDAO();
        public IEnumerable<Profesor> listado()
        {
            List<Profesor> temporal = new List<Profesor>();
            cn = new ConexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec SP_ProfesorList", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Profesor()
                    {
                        idprof = dr.GetInt32(0),
                        nomprof = dr.GetString(1),
                        apeprof = dr.GetString(2),
                        espeprof = dr.GetString(3),
                        dniprof = dr.GetString(4),
                        celprof = dr.GetString(5)
                    });
                }
                dr.Close(); cn.getcn.Close();
            }
            return temporal;
        }
    }
}