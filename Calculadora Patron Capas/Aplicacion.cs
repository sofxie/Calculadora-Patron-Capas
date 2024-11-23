using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_Patron_Capas
{
    public class Aplicacion : IAplicacion<Operaciones>
    {
        private string _entradaactual = "";
        private double _num1;
        private string _operacion;
        private double _num2;
        private double _result;
        private double _memoryNum;
        bool IsBinary = false;
        private Historial _form2;

        private readonly Dominio _dominio;

        public Aplicacion()
        {
            _dominio = new Dominio();
        }
        public void AgregarDigito(string digit)
        {
            if (digit == "." && _entradaactual.Contains("."))
            {
                return;
            }
            _entradaactual += digit;
        }
        public void Memoria()
        {
            bool Binario = CambioBinary();
            if (_entradaactual == "False" || _entradaactual == "True" || Binario)
            {
                _entradaactual = "Dato Inválido";
            }
            else
            {
                _memoryNum = Convert.ToDouble(_entradaactual);
                _dominio.GuardarMemoria(_memoryNum);
                _entradaactual = "";
            }
        }
        public double AvgMemoria()
        {
            double avg = _dominio.Avg();
            _entradaactual = avg.ToString();
            return avg;
        }
        public string Binario()
        {
            IsBinary = true;
            string num =_entradaactual.ToString();
            string binary = _dominio.Binario(num);
            _entradaactual = binary.ToString();
            return binary;
        }
        public void AgregarOperacion(string operacion)
        {
            if (!string.IsNullOrEmpty(_entradaactual))
            {
                _num1 = Convert.ToDouble(_entradaactual);
                _operacion = operacion;
                _entradaactual = "";
            }
        }
        public double Calcular()
        {
            if (string.IsNullOrEmpty(_entradaactual) || string.IsNullOrEmpty(_operacion))
            {
                double num = Convert.ToDouble(_entradaactual);
                var operacion = new Operaciones
                {
                    Num1 = num,
                    Num2 = null, // No se usó un segundo número.
                    Operacion = null, // No hubo operación.
                    Resultado = num.ToString()
                };
                _dominio.AgregarOperaciones(operacion);
                _entradaactual = num.ToString();
                return num; // Retorna el número directamente.
                
            }
            else
            {
                _num2 = Convert.ToDouble(_entradaactual);
                Console.WriteLine(_num1);
                _result = _dominio.Calcular(_num1, _num2, _operacion);
                if (_result == -1)
                {
                    _entradaactual = "Error";
                }
                _entradaactual = _result.ToString();
                return _result;
            }
        }
        public void Clear()
        {
            IsBinary = false;
            _entradaactual = "";
            _num1 = 0;
            _operacion = null;
        }
        public string EntradaActual()
        {
            return _entradaactual;
        }
        public bool CambioBinary()
        {
            return IsBinary;
        }
        public List<string> ObtenerHistorialComoLista()
        {
            string rutaArchivo = @"C:\Users\sofia\source\repos\Calculadora Patron Capas\Calculadora Patron Capas\Bitacora.txt";

            if (!File.Exists(rutaArchivo))
            {
                return new List<string> { "El archivo de historial no existe." };
            }

            // Leer todas las líneas del archivo y devolverlas como lista.
            return File.ReadAllLines(rutaArchivo).ToList();
        }
        public bool Primo()
        {
            double num = Convert.ToDouble(_entradaactual);
            bool esPrimo = _dominio.Primo(num);
            _entradaactual = esPrimo.ToString();
            return esPrimo;
        }
    }
}
