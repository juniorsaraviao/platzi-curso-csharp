using System;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (s,e) => Printer.Beep(2000,1000,1);
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            
            var reporteador = new Reporteador(engine.GetDiccionarioObjectos());
            var evaList = reporteador.GetListaEvaluaciones();
            var asigList = reporteador.GetListaAsignatura();
            var evalXAsig = reporteador.GetDicEvalXAsig();
            var promXAsigList = reporteador.GetPromedioAlumnoPorAsignatura();
            var mejoresPromedios = reporteador.GetMejoresPromxAsig("Matemáticas");

            Printer.WriteTitle("Captura de una Evaluación por consola");
            var newEval = new Evaluación();
            string nombre, notaString;
            float nota;

            WriteLine("Ingrese el nombre de la evaluación");
            Printer.PresioneEnter();
            nombre = ReadLine();

            if (string.IsNullOrEmpty(nombre))
            {
               Printer.WriteTitle("El valor del nombre no puede ser vacío");
               WriteLine("Saliendo del programa");
            }
            else
            {
               newEval.Nombre = nombre.ToLower();
               WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PresioneEnter();
            notaString = ReadLine();

            if (string.IsNullOrEmpty(notaString))
            {
               Printer.WriteTitle("El valor de la nota no puede ser vacío");
               WriteLine("Saliendo del programa");
            }
            else
            {
               try
               {
                  newEval.Nota = float.Parse(notaString);
                  if (newEval.Nota < 0 || newEval.Nota > 5)
                  {
                     throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                  }
                  WriteLine("La nota de la evaluación ha sido ingresada correctamente");
               }
               catch (ArgumentOutOfRangeException ex)
               {
                  Printer.WriteTitle(ex.Message);
                  WriteLine("Saliendo del programa");
               }
               catch (Exception)
               {
                  Printer.WriteTitle("El valor de la nota no es número válido");
                  WriteLine("Saliendo del programa");
               }
               finally
               {
                  Printer.WriteTitle("FINALLY");
                  Printer.Beep(2500, 500, 3);
               }
            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIÓ");
        }
        
        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
