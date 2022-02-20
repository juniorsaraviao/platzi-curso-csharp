using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using Etapa5.Entidades;

namespace CoreEscuela.App
{
    // sealed: not inheritance but we can create instances
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        public List<ObjetoEscuelaBase> GetObjetosEscuela()
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            listaObj.AddRange(Escuela.Cursos);

            foreach (var curso in Escuela.Cursos)
            {
                listaObj.AddRange(curso.Asignaturas);
                listaObj.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listaObj.AddRange(alumno.Evaluaciones);
                }
            }

            return listaObj;
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprimirEval = false) 
        {
            foreach (var objDic in dic)
            {      
               Printer.WriteTitle(objDic.Key.ToString());
               foreach (var val in objDic.Value)
               {
                  switch (objDic.Key)
                  {
                     case LlaveDiccionario.Evaluacion:
                        if(imprimirEval)
                           Console.WriteLine(val);
                        break;
                     case LlaveDiccionario.Escuela:
                        Console.WriteLine($"Escuela: {val.Nombre}");
                        break;
                     case LlaveDiccionario.Alumno:
                        Console.WriteLine($"Alumno: {val.Nombre}");
                        break;
                     case LlaveDiccionario.Curso:
                        var curtmp = val as Curso;
                        if (curtmp != null)
                        {
                           var count = ((Curso)val).Alumnos.Count;
                           Console.WriteLine($"Curso: {val.Nombre} Cantidad de Alumnos: {count}");
                        }                        
                        break;
                     default:
                        Console.WriteLine(val);
                        break;
                  }
               }
            }
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjectos() 
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();
            diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos);
            
            var evaluaciones = new List<Evaluación>();
            var tempAsignaturas = new List<Asignatura>();
            var tempAlumnos = new List<Alumno>();
            foreach (var curso in Escuela.Cursos) 
            {
               tempAsignaturas.AddRange(curso.Asignaturas);
               tempAlumnos.AddRange(curso.Alumnos);
               
               foreach(var alum in curso.Alumnos) 
               {
                  evaluaciones.AddRange(alum.Evaluaciones);
               }               
            }
            diccionario.Add(LlaveDiccionario.Asignatura, tempAsignaturas);
            diccionario.Add(LlaveDiccionario.Alumno, tempAlumnos);
            diccionario.Add(LlaveDiccionario.Evaluacion, evaluaciones);
            return diccionario;
        } 

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( bool incluirEvaluaciones = true, 
                                                          bool traerAlumnos = true,
                                                          bool traerAsignaturas = true,
                                                          bool traerCursos = true)
        {
            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy, 
                                     incluirEvaluaciones, traerAlumnos, traerAsignaturas, traerCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( out int conteoEvaluaciones,
                                                          bool incluirEvaluaciones = true, 
                                                          bool traerAlumnos = true,
                                                          bool traerAsignaturas = true,
                                                          bool traerCursos = true)
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy, 
                                     incluirEvaluaciones, traerAlumnos, traerAsignaturas, traerCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( out int conteoEvaluaciones,
                                                          out int conteoCursos,
                                                          bool incluirEvaluaciones = true, 
                                                          bool traerAlumnos = true,
                                                          bool traerAsignaturas = true,
                                                          bool traerCursos = true)
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out conteoCursos, 
                                     incluirEvaluaciones, traerAlumnos, traerAsignaturas, traerCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( out int conteoEvaluaciones,
                                                          out int conteoCursos,
                                                          out int conteoAsignaturas,
                                                          bool incluirEvaluaciones = true, 
                                                          bool traerAlumnos = true,
                                                          bool traerAsignaturas = true,
                                                          bool traerCursos = true)
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out conteoAsignaturas, out conteoCursos, 
                                     incluirEvaluaciones, traerAlumnos, traerAsignaturas, traerCursos);
        }
         
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( out int conteoEvaluaciones,
                                                          out int conteoAlumnos,
                                                          out int conteoAsignaturas,
                                                          out int conteoCursos,
                                                          bool incluirEvaluaciones = true, 
                                                          bool traerAlumnos = true,
                                                          bool traerAsignaturas = true,
                                                          bool traerCursos = true )
        {
            conteoEvaluaciones = 0;
            conteoAlumnos = 0;
            conteoAsignaturas = 0;
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);            

            if (traerCursos)
               listaObj.AddRange(Escuela.Cursos);
            
            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;
                if (traerAsignaturas)
                {
                    listaObj.AddRange(curso.Asignaturas);                
                }
                
                if (traerAlumnos)
                {
                    listaObj.AddRange(curso.Alumnos);                   
                }

                if (incluirEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();
        }

        #region Metodos de Carga

        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)Math.Round(5 * rnd.NextDouble(), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

        }        

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }

        #endregion
    }
}