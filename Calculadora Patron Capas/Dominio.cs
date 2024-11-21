using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Calculadora_Patron_Capas
{
    public class Dominio
    {
        private readonly IPersistencia<Operaciones> _persistencia;
        string valores_bin = "";
        private List<double> memoria = new List<double>();
        private const int MemoriaDisponible = 10;

        public Dominio()
        {
            _persistencia = new Persistencia();
        }
        public void GuardarMemoria(double Num)
        {
            if (memoria.Count >= MemoriaDisponible)
            {
                memoria.RemoveAt(0); // Eliminar el primer número (el más antiguo)
            }
            memoria.Add(Num);
            string memoriaTexto = string.Join(", ", memoria);

            var op = new Operaciones
            {
                Num1 = Num,
                Operacion = "M+",
                Resultado = memoriaTexto
            };
            _persistencia.AgregarOperaciones(op);
        }
        public double Avg()
        {
            if (memoria.Count == 0)
            {
                return 0;
            }
            double promedio = memoria.Average();
            string memoriaTexto = string.Join(", ", memoria);
            var op = new Operaciones
            {
                Num1 = promedio,
                Operacion = "Avg",
                Resultado = memoriaTexto
            };
            _persistencia.AgregarOperaciones(op);
            return promedio;
        }
        public bool Primo(double num)
        {
            bool esPrimo = EsrPrimo(num);

            var op = new Operaciones
            {
                Num1 = num,
                Operacion = "Primo",
                Resultado = esPrimo.ToString()
            };

            _persistencia.AgregarOperaciones(op);
            return esPrimo;
        }
        private bool EsrPrimo(double num)
        {
            if (num < 2 || num % 1 != 0) return false;

            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0) return false;
            }
            return true;
        }
        public double Calcular(double num1, double num2, string operacion)
        {
            double resultado;

            switch (operacion)
            {
                case "+":
                    resultado = num1 + num2;
                    break;
                case "-":
                    resultado = num1 - num2;
                    break;
                case "X":
                    resultado = num1 * num2;
                    break;
                case "*":
                    resultado = num1 * num2;
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        resultado = -1;
                        throw new DivideByZeroException("No se puede dividir entre cero.");
                    }
                    resultado = num1 / num2;
                    break;
                case "÷":
                    if (num2 == 0)
                    {
                        resultado = -1;
                        throw new DivideByZeroException("No se puede dividir entre cero.");
                    }
                    resultado = num1 / num2;
                    break;
                default:
                    throw new InvalidOperationException("Operación no soportada.");
            }

            // Guardar la operación en el historial
            var op = new Operaciones
            {
                Num1 = num1,
                Num2 = num2,
                Operacion = operacion,
                Resultado = resultado.ToString()
            };

            _persistencia.AgregarOperaciones(op);
            return resultado;
        }
        

        public string Binario(string dec)
        {
            valores_bin = "";
            int n = int.Parse(dec.ToString());
            string binario = "";
            int l;
            if (n != 1)
            {
                for (l = n; l != 0 && l != 1; l = l / 2)
                {
                    binario = (l % 2) + binario;
                }
                if (l == 0)
                {
                    valores_bin += "0";
                }
                else
                {
                    binario = 1 + binario;
                    valores_bin += binario;
                }
            }
            else
            {
                valores_bin += "1";
            }
            var op = new Operaciones
            {
                Num1 = n,
                Num2 = null,
                Operacion = "Binario",
                Resultado = valores_bin
            };
            _persistencia.AgregarOperaciones(op);
            return valores_bin;
        }
        public void AgregarOperaciones(Operaciones operacion)
        {
            _persistencia.AgregarOperaciones(operacion);
        }
        public string ObtenerHistorialComoTexto()
        {
            string rutaArchivo = @"C:\Users\sofia\source\repos\Calculadora Patron Capas\Calculadora Patron Capas\Bitacora.txt";

            if (!File.Exists(rutaArchivo))
            {
                return "El archivo de historial no existe.";
            }

            // Leer todo el archivo y devolver su contenido como texto.
            return File.ReadAllText(rutaArchivo);
        }
    }
}
