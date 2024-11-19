using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public interface IAplicacion<Operaciones>
    {
        void AgregarDigito(string digit);
        void AgregarOperacion(string operacion);
        double Calcular();
        void Clear();
        string EntradaActual();
        bool Primo();
        IEnumerable<Operaciones> ObtenerHistorial();
    }
}
