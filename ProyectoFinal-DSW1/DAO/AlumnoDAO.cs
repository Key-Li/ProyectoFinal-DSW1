using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinal_DSW1.Models;

namespace ProyectoFinal_DSW1.DAO
{
    public class AlumnoDAO
    {
        ConexionDAO cn = new ConexionDAO();
        public Alumno validaLog(string user = null, string pass = null)
        {
            Alumno reg = null;
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("SP_ValidarLogueoAlu", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@usu", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Alumno();
                    reg.idalumno = dr.GetInt32(0);
                    reg.nomalum = dr.GetString(1);
                    reg.apealum = dr.GetString(2);
                    reg.dnialum = dr.GetString(3);
                    reg.fechalum = dr.GetString(4);
                    reg.celalum = dr.GetString(5);
                    reg.usualum = dr.GetString(6);
                    reg.passalum = dr.GetString(7);
                }
                dr.Close(); cn.getcn.Close();
            }
            return reg;
        }
        public string registro(Alumno reg)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AlumnoRegistro", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@nomalum", reg.nomalum);
                cmd.Parameters.AddWithValue("@apealum", reg.apealum);
                cmd.Parameters.AddWithValue("@dnialum", reg.dnialum);
                cmd.Parameters.AddWithValue("@fechalum", reg.fechalum);
                cmd.Parameters.AddWithValue("@celalum", reg.celalum);
                cmd.Parameters.AddWithValue("@usualum", reg.usualum);
                cmd.Parameters.AddWithValue("@passalum", reg.passalum);
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    mensaje = "Se registró al alumno " + reg.nomalum;
                }
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.getcn.Close(); }
            return mensaje;
        }

        public IEnumerable<Alumno> listado()
        {
            List<Alumno> temporal = new List<Alumno>();
            cn = new ConexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec SP_AlumnoList", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Alumno()
                    {
                        idalumno = dr.GetInt32(0),
                        nomalum = dr.GetString(1),
                        apealum = dr.GetString(2),
                        dnialum = dr.GetString(3),
                        fechalum = dr.GetString(4),
                        celalum = dr.GetString(5),
                        usualum = dr.GetString(6),
                        passalum = dr.GetString(7)
                    });
                }
                dr.Close(); cn.getcn.Close();
            }
            return temporal;
        }

        public Alumno Buscar(int id)
        {
            return listado().Where(c => c.idalumno == id).FirstOrDefault();
        }

        public string actualiza(Alumno reg)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SP_AlumnoActualiza", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@idalumno", reg.idalumno);
                cmd.Parameters.AddWithValue("@nomalum", reg.nomalum);
                cmd.Parameters.AddWithValue("@apealum", reg.apealum);
                cmd.Parameters.AddWithValue("@dnialum", reg.dnialum);
                cmd.Parameters.AddWithValue("@fechalum", reg.fechalum);
                cmd.Parameters.AddWithValue("@celalum", reg.celalum);
                cmd.Parameters.AddWithValue("@usualum", reg.usualum);
                cmd.Parameters.AddWithValue("@passalum", reg.passalum);
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    mensaje = "Se modificaron los datos del alumno " + reg.nomalum;
                }
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.getcn.Close(); }
            return mensaje;
        }
    }
}
