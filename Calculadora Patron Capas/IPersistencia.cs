using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public interface IPersistencia<Operaciones>
    {
        void AgregarOperaciones(Operaciones operacion);
        IEnumerable<Operaciones> ObtenerHistorial();
        string LeerHistorialComoTexto();
    }
}
