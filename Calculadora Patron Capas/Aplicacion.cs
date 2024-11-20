using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Patron_Capas
{
    public class Aplicacion : IAplicacion<Operaciones>
    {
        private string _entradaactual = "";
        private double _num1;
        private string _operacion;
        private double _num2;
        private double _result;

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
        public void EliminarDigito()
        {
            if (!string.IsNullOrEmpty(_entradaactual))
            {
                _entradaactual = _entradaactual.Remove(_entradaactual.Length - 1);
            }
        }
        public string Binario()
        {
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
            _entradaactual = "";
            _num1 = 0;
            _operacion = null;
        }
        public string EntradaActual()
        {
            return _entradaactual;
        }
        public void ObtenerHistorial() => _dominio.ObtenerHistorial();
        public bool Primo()
        {
            double num = Convert.ToDouble(_entradaactual);
            bool esPrimo = _dominio.Primo(num);
            _entradaactual = esPrimo.ToString();
            return esPrimo;
        }
    }
}
