using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ProyectoFinal_DSW1.Models;
using ProyectoFinal_DSW1.DAO;

namespace ProyectoFinal_DSW1.Controllers
{
    public class AlumnoController : Controller
    {
        AlumnoDAO Alum = new AlumnoDAO();
        ProfesorDAO Profe = new ProfesorDAO();
        CursoDAO Cur = new CursoDAO();
        MatriculaDAO Mat = new MatriculaDAO();

        Alumno InicioSesion()
        {
            if (Session["login"] == null)
            {
                return null;
            }
            else
            {
                return (Session["login"] as Alumno);
            }
        }

        public ActionResult CloseSession()
        {
            Session["login"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
             return View();
        }        

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            Alumno reg = Alum.validaLog(user, pass);
            if (reg == null)
            {
                ViewBag.mensaje = "Usuario o clave incorrecta";
                return View();
            }
            else {
                Session["login"] = reg;
                return RedirectToAction("Principal");
            }        
        }        

        public ActionResult Registro(string mensaje = null)
        {
            if (mensaje != null)
            {
                ViewBag.mensaje = mensaje;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Registro(Alumno reg)
        {
            ViewBag.mensaje = Alum.registro(reg);
            return View(new Alumno());
        }

        public ActionResult Principal()
        {
            return View();
        }

        public ActionResult Actualiza()
        {
            Alumno reg = InicioSesion();
            Alum.Buscar(reg.idalumno);
            return View(reg);
        }

        [HttpPost]
        public ActionResult Actualiza(Alumno reg)
        {
            ViewBag.mensaje = Alum.actualiza(reg);
            Alumno a = InicioSesion();
            Alum.Buscar(a.idalumno);
            return View(reg);
        }

        public ActionResult Profesores()
        {
            return View(Profe.listado());
        }

        public ActionResult Cursos()
        {
            return View(Cur.listado());
        }

        public ActionResult Matricula(int id)
        {
            Curso c = Cur.Buscar(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Matricula(Curso reg)
        {
            Alumno a = InicioSesion();
            int idalum = a.idalumno;

            Matricula m = new Matricula();
            m.fechmat = DateTime.Now.ToString("MM-dd-yyy");
            m.idalumno = idalum;
            m.idcurso = reg.idcurso;
            m.idhorario = reg.idhorario;

            Matricula x = Mat.validaMat(idalum, reg.idcurso);
            if (x == null)
            {
                ViewBag.mensaje = Mat.registro(m);
                Curso c = Cur.Buscar(reg.idcurso);
                return View(c);
            }
            else
            {
                ViewBag.mensaje = "Ud. ya se encuentra matriculado en este curso";
                Curso c = Cur.Buscar(reg.idcurso);
                return View(c);
            }            
        }

        public ActionResult Consolidado()
        {
            Alumno a = InicioSesion();
            ViewBag.cod = a.idalumno;
            ViewBag.nombre = a.nomalum+" "+a.apealum;

            return View(Mat.listado(a.idalumno));
        }

    }
}