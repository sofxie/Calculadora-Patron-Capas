using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Calculadora_Patron_Capas
{
    public partial class Historial : Form
    {
        private readonly IAplicacion<Operaciones> _aplicacion;
        public Historial(IAplicacion<Operaciones> aplicacion)
        {
            InitializeComponent();
            _aplicacion = aplicacion;
        }
        private void Historial_Load(object sender, EventArgs e)
        {
            try
            {
                // Obtener el historial como una lista de cadenas.
                var historial = _aplicacion.ObtenerHistorialComoLista();

                // Asignar las líneas al ListBox.
                listBox1.Items.Clear();
                listBox1.Items.AddRange(historial.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
