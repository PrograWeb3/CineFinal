using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioHoras
    {
        public static List<Horas> solapamientoHorasReservas(int IdVersiones , int IdPelicula , int IdSedes)
        {

            CineContext db = new CineContext();
            var ObtenerCartelera = ServicioCarteleras.BuscarCarteleraReserva(IdVersiones, IdPelicula, IdSedes);

            List<Horas> lista = new List<Horas>();

            int hora_real;
            int minutos_real;

            string fin_funcion = "";
            int fin_funcion_en_segundos = 0;

            int horaInicio = ObtenerCartelera.HoraInicio;
            int horaPelicula = ObtenerCartelera.Peliculas.Duracion;

            for (int i = 1; i <= 7; i++)
            {

                //Si es primera iteración toma la hora de inicio de la cartelera.
                if (i == 1)
                {
                    hora_real = (horaInicio) / 3600;
                    minutos_real = (horaInicio % 3600) / 60;
                    fin_funcion_en_segundos = ((horaPelicula) * 60) + horaInicio;
                }
                else
                {
                    hora_real = fin_funcion_en_segundos / 3600;
                    minutos_real = (fin_funcion_en_segundos % 3600) / 60;
                    fin_funcion_en_segundos += ((horaPelicula) * 60) + 1800; // El fin de la funcion en segundos.  
                    if (fin_funcion_en_segundos > 86400)
                    {
                        fin_funcion_en_segundos = fin_funcion_en_segundos - 86400;
                    }
                }

                string minutos_real_texto; // Para añadir un cero a la izquierda si fuera necesario.
                string hora_formato_original;

                if (minutos_real < 10)
                {
                    minutos_real_texto = minutos_real.ToString();
                    minutos_real_texto = "0" + minutos_real;
                    hora_formato_original = String.Format("{0}:{1}", hora_real, minutos_real_texto);
                }
                else
                {
                    hora_formato_original = String.Format("{0}:{1}", hora_real, minutos_real);
                }

                //Calculo el final de la funcion mediante la duracion de la pelicula.            
                hora_real = fin_funcion_en_segundos / 3600;
                minutos_real = (fin_funcion_en_segundos % 3600) / 60;
                if (minutos_real < 10)
                {
                    minutos_real_texto = minutos_real.ToString();
                    minutos_real_texto = "0" + minutos_real;
                    fin_funcion = String.Format("{0}:{1}", hora_real, minutos_real_texto);
                }
                else
                {
                    fin_funcion = String.Format("{0}:{1}", hora_real, minutos_real);
                }

                fin_funcion_en_segundos += 1800; //Le agrego la media hora de intervalo.

                if (fin_funcion_en_segundos > 86400)
                {
                    fin_funcion_en_segundos = fin_funcion_en_segundos - 86400;
                }


                lista.Add(new Horas() { IdHora = hora_formato_original, Hora = hora_formato_original });
            }



            return lista;
        }
    }
}