using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models
{
    public abstract class ConversorDeHoras
    {
        public static int HoraASegundos(int hora)
        {
            //Toma la hora de la cartelera y lo convierto a string. 
            string hora_texto = hora.ToString();
            //Ahora que es un string podemos separar de dos en dos (Horas y minutos).            
            int hora_en_segundos = Int32.Parse(hora_texto.Substring(0, 2)) * 3600;
            int minutos_en_segundos = Int32.Parse(hora_texto.Substring(2, 2)) * 60;            
            //Formato de persistencia en la bd.
            hora_en_segundos = (hora_en_segundos + minutos_en_segundos);
            return hora_en_segundos;
        }
    }
}