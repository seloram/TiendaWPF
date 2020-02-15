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
    public partial class DatosProductos : Form
    {
        private List<articulo> articulos;
        private List<tipoarticulo> tipoarticulo;
        private List<tv> tv;
        private List<camara> camara;
        private List<objetivo> objetivo;
        private List<memoria> memoria;
        private negocio neg;
        private DataSet ds;
        private DataView dv;
        private DataTable dt;
        public string SelectedRow { get; set; }
        private articulo Art { get; set; }
        private tipoarticulo Tipo { get; set; }

        public DatosProductos(negocio n)
        {
            dt = new DataTable();
            ds = new DataSet();
            articulos = new List<articulo>();
            tipoarticulo = new List<tipoarticulo>();
            tv = new List<tv>();
            camara = new List<camara>();
            objetivo = new List<objetivo>();
            memoria = new List<memoria>();
            neg = n;
            InitializeComponent();
        }

        private void DatosProductos_Load(object sender, EventArgs e)
        {
            statusStrip1.Visible = false;

            articulos = neg.articulos();
            tipoarticulo = neg.tipoArticulos();

            foreach(tipoarticulo t in tipoarticulo)
            {
                detalleTipo.Items.Add(t.Descripcion);
            }
            detalleTipo.Items.Add("Ninguno");

            dt.Columns.Add("Id");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("PvP");
            dt.Columns.Add("MarcaID");
            dt.Columns.Add("TipoArticulo");

            foreach(articulo p in articulos)
            {
                DataRow row = dt.NewRow();
                row["Id"] = p.ArticuloID;
                row["Nombre"] = p.Nombre;
                row["PvP"] = p.Pvp;
                row["MarcaID"] = p.MarcaID;
                foreach(tipoarticulo t in tipoarticulo)
                {
                    if (p.TipoArticuloID == t.TipoArticuloID.ToString())
                    {
                        row["TipoArticulo"] = t.Descripcion;
                    }
                }

                dt.Rows.Add(row);
            }

            dv = new DataView(dt);
            dataGridView1.DataSource = dv;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dv[0].Delete();


            //dv[0].Delete();
            //dataGridView1.Columns[4].Visible = false;

            //dv[4].Delete();

            //articulos = neg.articulos();
            //dt.Columns.Add("Nombre");
            //dt.Columns.Add("PvP");
            //dt.Columns.Add("MarcaID");
            //dt.Columns.Add("urlimagen");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //SelectedRow = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            //foreach(articulo a in articulos)
            //{
            //    if (a.ArticuloID == SelectedRow)
            //    {
            //        Art = a;
            //        switch (a.TipoArticuloID)
            //        {
            //            case "1":

            //                CreacionTv(Art);
            //                panelComponentes.Controls
            //        }
            //    }
            //}
            //panelComponentes.Controls
        }

        public void CreacionTv(articulo Art)
        {
            //Label nombre = new Label();
            //TextBox pvp = new TextBox();
            //ComboBox marca, panel, pantalla, resolucion, hdreadyfullhd, tdt;

            //BuscarTipo(Art.TipoArticuloID);

            //nombre.Text = Art.Nombre;
            //pvp.Text = Art.Pvp;

            //panelComponentes.Controls.Add(nombre);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != "")
            {
                Filtros();
            }
            else
            {
                dv.RowFilter = String.Format("Nombre like '%{0}%'", detalleNombre.Text);
            }
        }

        private void Filtros()
        {
            dv.RowFilter = String.Format("Nombre like '%{0}%' AND TipoArticulo like '%{1}%'"
                , detalleNombre.Text, detalleTipo.SelectedItem);
        }

        private void detalleTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != null)
            {
                Filtros();
            }
            else
            {
                if (detalleTipo.Text != "Ninguno")
                {
                    dv.RowFilter = String.Format("TipoArticuloID like '%{0}%'", detalleTipo.SelectedItem);
                }

            }
        }
    }
}
