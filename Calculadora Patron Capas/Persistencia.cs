using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public class Persistencia : IPersistencia<Operaciones>
    {
        private List<Operaciones> _operacion = new List<Operaciones>();
        public void AgregarOperaciones(Operaciones operacion)
        {
            _operacion.Add(operacion);
        }
        public IEnumerable<Operaciones> ObtenerHistorial() => _operacion;
    }
}
