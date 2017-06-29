using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public class ServicioTipoDocumento
    {
        public static List<TiposDocumentos> listatipoDocumentos()
        {
            CineContext db = new CineContext();
            List<TiposDocumentos> lista = (from TiposDocumentos in db.TiposDocumentos
                                           select TiposDocumentos).ToList();
            return lista;
        }
    }
}