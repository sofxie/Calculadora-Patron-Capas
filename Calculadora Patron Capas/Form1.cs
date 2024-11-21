using System;
using System.IO;
using System.Windows.Forms;

namespace Calculadora_Patron_Capas
{
    public partial class Calculadora : Form
    {
        Historial form2;
        private readonly IAplicacion<Operaciones> _aplicacion;
        public Calculadora()
        {
            InitializeComponent();
            _aplicacion = new Aplicacion();
            form2 = new Historial(_aplicacion);
            // Boton de numero de 0-9
            button13.Click += BotonNumero_Click;
            button10.Click += BotonNumero_Click;
            button11.Click += BotonNumero_Click;
            button12.Click += BotonNumero_Click;
            button7.Click += BotonNumero_Click;
            button8.Click += BotonNumero_Click;
            button9.Click += BotonNumero_Click;
            button3.Click += BotonNumero_Click;
            button6.Click += BotonNumero_Click;
            button5.Click += BotonNumero_Click;

            button14.Click += BotonNumero_Click;

            // Boton de Operaciones
            buttonDividir.Click += BotonOperacion_Click;
            buttonMultiplicar.Click += BotonOperacion_Click;
            buttonRestar.Click += BotonOperacion_Click;
            buttonSumar.Click += BotonOperacion_Click;
        }
        private void BotonOperacion_Click(object sender, EventArgs e)
        {
            bool Binario = _aplicacion.CambioBinary();
            var boton = sender as Button;
            if (boton != null && textBox.Text != "ERROR" && textBox.Text != "True" && textBox.Text != "False" && !Binario)
            {
                _aplicacion.AgregarOperacion(boton.Text);
                textBox.Text = _aplicacion.EntradaActual();
            }
        }
        private void BotonNumero_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null && textBox.Text != "ERROR" && textBox.Text != "True" && textBox.Text != "False")
            {
                // Delegar a la capa de aplicación
                _aplicacion.AgregarDigito(boton.Text);
                textBox.Text = _aplicacion.EntradaActual();
            }
        }
        private void buttonIgual_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.Calcular();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.Clear();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el archivo existe antes de intentar leerlo.
                string rutaArchivo = @"C:\Users\sofia\source\repos\Calculadora Patron Capas\Calculadora Patron Capas\Bitacora.txt";

                // Instanciar form2 si no está ya instanciado.
                if (form2 == null || form2.IsDisposed)
                {
                    form2 = new Historial(_aplicacion);  // Pasando la aplicación al formulario.
                }

                // Limpiar el ListBox antes de agregar los nuevos elementos.
                form2.listBox1.Items.Clear();

                // Leer el archivo línea por línea y agregarlo al ListBox.
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                    while (!lector.EndOfStream)
                    {
                        form2.listBox1.Items.Add(lector.ReadLine());
                    }
                }

                // Mostrar el formulario con el historial.
                form2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void Button10_Click(object sender, EventArgs e)
        {
        }
        private void button21_Click(object sender, EventArgs e)
        {
        }
        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.Primo();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.Binario();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }

        private void textBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void buttonSumar_Click(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e) // Memoria
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.Memoria();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }

        private void buttonMultiplicar_Click(object sender, EventArgs e)
        {

        }

        private void buttonDividir_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.AvgMemoria();
                textBox.Text = _aplicacion.EntradaActual();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
