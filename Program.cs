using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad: 10);
            ImpimirCursosEscuela(engine.Escuela);
            var listaObjetos = engine.GetObjetosEscuela();
            
            Printer.DrawLine(10);
            Printer.WriteTitle("Pruebas de polimorfismo");

            var studentTest = new Alumno { Nombre = "Claire Underwood" };
            
            ObjetoEscuelaBase ob = studentTest;

            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {studentTest.Nombre}");
            WriteLine($"Alumno: {studentTest.UniqueId}");
            WriteLine($"Alumno: {studentTest.GetType()}");

            Printer.WriteTitle("ObjetoEscuelaBase");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");
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
