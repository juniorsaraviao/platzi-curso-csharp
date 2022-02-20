﻿using System;
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
