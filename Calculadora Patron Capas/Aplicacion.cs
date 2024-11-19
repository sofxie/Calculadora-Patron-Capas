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

        private readonly IPersistencia<Operaciones> _persistencia;
        private readonly Dominio _dominio;

        public Aplicacion(IPersistencia<Operaciones> persistencia)
        {
            _persistencia = persistencia;
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
                _entradaactual = _entradaactual;
                return -1;
            }
            else
            {
                _num2 = Convert.ToDouble(_entradaactual);
                Console.WriteLine(_num1);
                _result = _dominio.Calcular(_num1, _num2, _operacion);

                // Guardar en el historial
                var operacion = new Operaciones
                {
                    Num1 = _num1,
                    Num2 = _num2,
                    Operacion = _operacion,
                    Resultado = _result
                };
                _persistencia.AgregarOperaciones(operacion);

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
        public IEnumerable<Operaciones> ObtenerHistorial() => _persistencia.ObtenerHistorial();
        public bool Primo()
        {
            double d = Convert.ToDouble(_entradaactual);
            if (d == (int)d)
            {
                int num = (int)d;

                bool esPrimo = _dominio.Primo(num);
                _entradaactual = esPrimo.ToString();
                return esPrimo;
            }
            else
            {
                _entradaactual = "False";
                return false;
            }
        }
    }
}
