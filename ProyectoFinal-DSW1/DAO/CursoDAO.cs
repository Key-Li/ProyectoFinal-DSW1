using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinal_DSW1.Models;

namespace ProyectoFinal_DSW1.DAO
{
    public class CursoDAO
    {
        ConexionDAO cn = new ConexionDAO();
        public IEnumerable<Curso> listado()
        {
            List<Curso> temporal = new List<Curso>();
            cn = new ConexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec SP_CursoList", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Curso()
                    {
                        idcurso = dr.GetInt32(0),
                        nomcurso = dr.GetString(1),
                        idhorario = dr.GetInt32(2),
                        deshorario= dr.GetString(3),
                        idprof = dr.GetInt32(4),
                        nombre = dr.GetString(5),
                    });
                }
                dr.Close(); cn.getcn.Close();
            }
            return temporal;
        }

        public Curso Buscar(int id)
        {
            return listado().Where(c => c.idcurso == id).FirstOrDefault();
        }
    }
}