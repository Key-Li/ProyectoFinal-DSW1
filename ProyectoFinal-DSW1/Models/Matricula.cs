using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_DSW1.Models
{
    public class Matricula
    {
        public int idmat { get; set; }
        public int idalumno { get; set; }
        public int idcurso { get; set; }
        public int idhorario { get; set; }
        public String fechmat { get; set; }
    }
}