using CoreEscuela.Entidades;
using Etapa5.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreEscuela.App
{
   public class Reporteador
   {
      Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
      public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
      {
         _diccionario = dicObsEsc ?? throw new ArgumentException(nameof(dicObsEsc));
      }

      public IEnumerable<Evaluación> GetListaEvaluaciones()
      {
         return _diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> list) ? list.Cast<Evaluación>() : new List<Evaluación>();
      }

      public IEnumerable<string> GetListaAsignatura()
      {
         return GetListaAsignatura(out var dummy);
      }

      public IEnumerable<string> GetListaAsignatura(out IEnumerable<Evaluación> listEvaluaciones)
      {
         listEvaluaciones = GetListaEvaluaciones();
         return listEvaluaciones.Where(x => x.Nota >= 3.0f).Select(x => x.Asignatura.Nombre).Distinct().ToList();
      }

      public Dictionary<string, IEnumerable<Evaluación>> GetDicEvalXAsig()
      {
         var dicRes = new Dictionary<string, IEnumerable<Evaluación>>();
         var listaAsig = GetListaAsignatura(out var listaEval);

         foreach (var asig in listaAsig)
         {
            var evalAsig = listaEval.Where(x => x.Asignatura.Nombre == asig);
            dicRes.Add(asig, evalAsig);
         }

         return dicRes;
      }

      public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnoPorAsignatura()
      {
         var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
         var dicEvalXAsig = GetDicEvalXAsig();

         foreach (var asigConEval in dicEvalXAsig)
         {
            var promAlum = asigConEval.Value.GroupBy(x => new { x.Alumno.UniqueId, x.Alumno.Nombre })
                                         .Select(x => new AlumnoPromedio {
                                            AlumnoId = x.Key.UniqueId,
                                            AlumnoNombre = x.Key.Nombre,
                                            Promedio = x.Average(eval => eval.Nota)
                                         });
            rta.Add(asigConEval.Key, promAlum);
         }

         return rta;
      }

      public IEnumerable<object> GetMejoresPromxAsig(string asignatura, int cantidad = 5)
      {
         var promAlumxAsig = GetPromedioAlumnoPorAsignatura();
         var resp = promAlumxAsig.TryGetValue(asignatura, out var dic) ? dic.OrderByDescending(x => x.Promedio).Take(cantidad) : null;
         return resp;
      }
   }
}
