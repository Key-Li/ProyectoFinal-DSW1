using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ProyectoFinal_DSW1.Models;

namespace ProyectoFinal_DSW1.DAO
{
    public class MatriculaDAO
    {
        ConexionDAO cn = new ConexionDAO();

        public string registro(Matricula reg)
        {
            string mensaje = "";            
            try
            {
                ConexionDAO cn = new ConexionDAO();
                SqlCommand cmd = new SqlCommand("SP_MatricualRegistro", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@fechmat", reg.fechmat);
                cmd.Parameters.AddWithValue("@idalumno", reg.idalumno);
                cmd.Parameters.AddWithValue("@idcurso", reg.idcurso);
                cmd.Parameters.AddWithValue("@idhorario", reg.idhorario);
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    mensaje = "Usted se ha matriculado en el curso: "+reg.idcurso;
                }
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.getcn.Close(); }
            return mensaje;
        }

        public Matricula validaMat(int idalum = 0, int idcur = 0)
        {
            Matricula reg = null;
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("SP_ValidarMatricula", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@idalumno", idalum);
                cmd.Parameters.AddWithValue("@idcurso", idcur);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Matricula();
                    reg.idmat = dr.GetInt32(0);
                    reg.fechmat = dr.GetString(1);
                    reg.idalumno = dr.GetInt32(2);
                    reg.idcurso = dr.GetInt32(3);
                    reg.idhorario = dr.GetInt32(4);                    
                }
                dr.Close(); cn.getcn.Close();
            }
            return reg;
        }

        public IEnumerable<Consolidado> listado(int idalum = 0)
        {
            List<Consolidado> temporal = new List<Consolidado>();
            cn = new ConexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("SP_MatriculaList", cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.getcn.Open();
                cmd.Parameters.AddWithValue("@idalumno", idalum);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Consolidado()
                    {
                        idmat = dr.GetInt32(0),
                        fechmat = dr.GetString(1),
                        nomcurso = dr.GetString(2),
                        nombre = dr.GetString(3),
                        deshorario = dr.GetString(4)
                    });
                }
                dr.Close(); cn.getcn.Close();
            }
            return temporal;
        }
    }
}