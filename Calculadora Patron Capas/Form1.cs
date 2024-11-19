using System;
using System.Windows.Forms;

namespace Calculadora_Patron_Capas
{
    public partial class Calculadora : Form
    {
        private readonly IAplicacion<Operaciones> _aplicacion;
        public Calculadora()
        {
            InitializeComponent();
            var _dominio = new Persistencia();
            _aplicacion = new Aplicacion(_dominio);
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
            var boton = sender as Button;
            if (boton != null && textBox.Text != "ERROR" && textBox.Text != "True" && textBox.Text != "False")
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
            var boton = sender as Button;
            if (boton != null)
            {
                _aplicacion.ObtenerHistorial();
                textBox.Text = _aplicacion.EntradaActual();
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
    }
}
