using LiteDB;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LiteDB_Winforms_App_Robel
{
    public partial class Form1 : Form
    {
        // Ruta a tu base de datos
        private string dbPath = @"MiBaseDeDatos.db";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var coleccion = db.GetCollection<Producto>("productos");
                var productos = coleccion.FindAll().ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = productos;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            var producto = new Producto
            {
                Nombre = txtNombre.Text,
                Precio = precio
            };

            using (var db = new LiteDatabase(dbPath))
            {
                var coleccion = db.GetCollection<Producto>("productos");
                coleccion.Insert(producto);
            }

            MostrarDatos();
            txtNombre.Clear();
            txtPrecio.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
