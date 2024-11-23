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

            try
            {
                // Intentar convertir la entrada a double
                if (double.TryParse(dec, out double numero))
                {
                    // Verificar si el número es negativo
                    if (numero < 0)
                    {
                        return "ERROR: No se pueden convertir números negativos a binario.";
                    }

                    // Separar parte entera y fraccionaria
                    int parteEntera = (int)Math.Floor(numero);
                    double parteFraccionaria = numero - parteEntera;

                    // Convertir parte entera a binario
                    string binarioEntero = "";
                    if (parteEntera == 0)
                    {
                        binarioEntero = "0";
                    }
                    else
                    {
                        for (int l = parteEntera; l > 0; l /= 2)
                        {
                            binarioEntero = (l % 2) + binarioEntero;
                        }
                    }

                    // Convertir parte fraccionaria a binario
                    string binarioFraccionario = "";
                    int limiteDigitos = 10; // Limitar a 10 dígitos para evitar bucles infinitos
                    while (parteFraccionaria > 0 && binarioFraccionario.Length < limiteDigitos)
                    {
                        parteFraccionaria *= 2;
                        if (parteFraccionaria >= 1)
                        {
                            binarioFraccionario += "1";
                            parteFraccionaria -= 1;
                        }
                        else
                        {
                            binarioFraccionario += "0";
                        }
                    }

                    // Combinar parte entera y fraccionaria
                    valores_bin = binarioFraccionario.Length > 0
                        ? $"{binarioEntero}.{binarioFraccionario}"
                        : binarioEntero;

                    // Guardar en el historial
                    var op = new Operaciones
                    {
                        Num1 = numero,
                        Num2 = null,
                        Operacion = "Binario",
                        Resultado = valores_bin
                    };
                    _persistencia.AgregarOperaciones(op);

                    return valores_bin;
                }
                else
                {
                    return "ERROR: Entrada no válida.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
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
