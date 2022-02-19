using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using Etapa5.Entidades;
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
            //Printer.Beep(10000, cantidad: 10);
            //ImpimirCursosEscuela(engine.Escuela);
            var listaObjetos = engine.GetObjetosEscuela( out int conteoEvaluaciones,
                                                         out int conteoAlumnos,
                                                         out int conteoAsignaturas,
                                                         out int conteoCursos );
            
            var dictmp = engine.GetDiccionarioObjectos();
            engine.ImprimirDiccionario(dictmp, true);
         
            //Dictionary<int, string> diccionario = new Dictionary<int, string>();
            //diccionario.Add(10, "Random");
            //diccionario.Add(23, "Lorem ipsum");

            //foreach(var keyValPair in diccionario)
            //{
            //   WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            //}

            //Printer.WriteTitle("Acceso a diccionario");
            //WriteLine(diccionario[23]);

            //Printer.WriteTitle("Otro diccionario");
            //var dic = new Dictionary<string, string>();
            //dic["Luna"] = "Cuerpo celeste que gira alrededor de la tierra";
            //WriteLine(dic["Luna"]);            
            
            //Printer.DrawLine(10);
            //Printer.WriteTitle("Pruebas de polimorfismo");

            //var studentTest = new Alumno { Nombre = "Claire Underwood" };
            
            //ObjetoEscuelaBase ob = studentTest;

            //Printer.WriteTitle("Alumno");
            //WriteLine($"Alumno: {studentTest.Nombre}");
            //WriteLine($"Alumno: {studentTest.UniqueId}");
            //WriteLine($"Alumno: {studentTest.GetType()}");

            //Printer.WriteTitle("ObjetoEscuelaBase");
            //WriteLine($"Alumno: {ob.Nombre}");
            //WriteLine($"Alumno: {ob.UniqueId}");
            //WriteLine($"Alumno: {ob.GetType()}");

            //var listaIlugar = listaObjetos.Where(x => x is ILugar).ToList();
            //engine.Escuela.LimpiarLugar();
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
