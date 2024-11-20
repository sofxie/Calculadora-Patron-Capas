using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public class Operaciones
    {
        public double Num1 { get; set; }
        public string Operacion { get; set; }
        public double? Num2 { get; set; }
        public string Resultado { get; set; }

        public override string ToString()
        {
            return $"{Num1} {Operacion} {Num2} = {Resultado}";
        }
    }
}
