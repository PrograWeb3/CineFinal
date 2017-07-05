using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioUsuarios
    {

        static public Usuarios buscarUsuario(Usuarios usuario)
        {

            CineContext db = new CineContext();
            var LogUsuario = (from Usuarios u in db.Usuarios
                              where u.NombreUsuario == usuario.NombreUsuario && u.Password == usuario.Password
                              select u).FirstOrDefault();

            return LogUsuario;
        }
    }
}