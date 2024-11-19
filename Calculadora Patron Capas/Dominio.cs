using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public class Dominio
    {
        public bool Primo(int num)
        {
            int num1 = num;
            int num2 = num-1;
            if (num1 <= 1)
            {
                return false;
            }
            while (num2 > 1)
            {
                if (num1%num2 == 0)
                {
                    return false;
                }
                num2--;
            }
            return true;
        }
        public double Calcular(double num1, double num2, string operacion)
        {
            switch (operacion)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "X":
                    return num1 * num2;
                case "÷":
                    if (num2 == 0)
                        throw new DivideByZeroException("No se puede dividir entre cero.");
                    return num1 / num2;
                default:
                    throw new InvalidOperationException("Operación no soportada.");
            }
        }
    }
}
