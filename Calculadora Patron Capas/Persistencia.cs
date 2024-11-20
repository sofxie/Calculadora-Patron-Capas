using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Calculadora_Patron_Capas
{
    public class Persistencia : IPersistencia<Operaciones>
    {
        public void AgregarOperaciones(Operaciones operacion)
        {
            string opera = operacion.ToString();
            using (StreamWriter escribir = new StreamWriter(@"C:\Users\sofia\source\repos\Calculadora Patron Capas\Calculadora Patron Capas\Bitacora.txt", append: true))
            {
                if(operacion.Operacion == "Primo" || operacion.Operacion == "Binario")
                {
                    opera = $"{operacion.Num1}  {operacion.Operacion}  {operacion.Resultado}";
                }

                escribir.WriteLine(opera);
            }
        }
        public IEnumerable<Operaciones> ObtenerHistorial()
        {
            var operaciones = new List<Operaciones>();
            foreach (var linea in File.ReadAllLines(@"C:\Users\sofia\source\repos\Calculadora Patron Capas\Calculadora Patron Capas\Bitacora.txt"))
            {
                var partes = linea.Split(' '); 
                operaciones.Add(new Operaciones
                {
                    Num1 = double.Parse(partes[0]),
                    Operacion = partes[1],
                    Num2 = double.Parse(partes[2]),
                    Resultado = partes[4]
                });
            }
            return operaciones;
        }
    }
}