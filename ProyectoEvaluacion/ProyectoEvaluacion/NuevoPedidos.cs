using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocio;

namespace ProyectoEvaluacion
{
    public partial class NuevoPedido : Form
    {
        private List<pedido> pedidos;
        private List<articulo> articulos;
        private List<usuario> usuarios;
        private List<linped> lineaPedidos;
        private List<tipoarticulo> tipoArticulos;
        private negocio neg;
        private DataSet ds;
        private DataView dv;
        private DataTable dt;
        public string SelectedRow { get; set; }
        private articulo Art { get; set; }
        private tipoarticulo Tipo { get; set; }
        FormularioPrincipal formu;
        private usuario Usu { get; set; }


        public NuevoPedido(negocio n, FormularioPrincipal f, usuario u)
        {
            Usu = u;
            formu = f;
            dt = new DataTable();
            ds = new DataSet();
            pedidos = new List<pedido>();
            articulos = new List<articulo>();
            usuarios = new List<usuario>();
            lineaPedidos = new List<linped>();
            tipoArticulos = new List<tipoarticulo>();
            neg = n;
            InitializeComponent();
        }

        private void NuevoPedido_Load(object sender, EventArgs e)
        {
            detFechPedido.Text = DateTime.Today.ToString();

            tipoArticulos = neg.tipoArticulos();
            articulos = neg.articulos();

            foreach (tipoarticulo t in tipoArticulos)
            {
                buscarTipo.Items.Add(t.Descripcion);
            }
            buscarTipo.Items.Add("Ninguno");

            dt.Columns.Add("Id");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("PvP");
            dt.Columns.Add("MarcaID");
            dt.Columns.Add("TipoArticulo");

            foreach (articulo p in articulos)
            {
                DataRow row = dt.NewRow();
                row["Id"] = p.ArticuloID;
                row["Nombre"] = p.Nombre;
                row["PvP"] = p.Pvp;
                row["MarcaID"] = p.MarcaID;
                foreach (tipoarticulo t in tipoArticulos)
                {
                    if (p.TipoArticuloID == t.TipoArticuloID.ToString())
                    {
                        row["TipoArticulo"] = t.Descripcion;
                    }
                }

                dt.Rows.Add(row);
            }

            dv = new DataView(dt);
            dataGridView3.DataSource = dv;
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView3.Columns[1].Width = 210;
            dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView3.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dv[0].Delete();

            if (Usu != null)
            {
                detUsuarioID.Text = Usu.UsuarioID.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != "")
            {
                Filtros();
            }
            else
            {
                dv.RowFilter = String.Format("Nombre like '%{0}%'", buscarTipo.Text);
            }

        }

        private void Filtros()
        {            
            dv.RowFilter = String.Format("Nombre like '%{0}%' AND TipoArticulo like '%{1}%'"
                , buscarNombre.Text, buscarTipo.SelectedItem);
        }

        private void buscarTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != null)
            {
                Filtros();
            }
            else
            {
                if (buscarTipo.Text != "Ninguno")
                {
                    dv.RowFilter = String.Format("TipoArticulo like '%{0}%'", "");
                }
            }
        }

        private void detUsuarioID_TextChanged(object sender, EventArgs e)
        {

        }

        private void detUsuarioID_DoubleClick(object sender, EventArgs e)
        {
            FormularioBusqueda busquedaUsu = new FormularioBusqueda(neg, 'b', formu, this);
            busquedaUsu.MdiParent = formu;
            busquedaUsu.WindowState = FormWindowState.Maximized;
            busquedaUsu.Show();
            this.Close();
        }
    }
}
