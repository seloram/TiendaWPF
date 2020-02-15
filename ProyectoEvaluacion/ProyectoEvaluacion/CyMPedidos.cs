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
    public partial class CyMPedidos : Form
    {
        private List<pedido> pedidos;
        private List<linped> lineaPedidos;
        private List<usuario> usuarios;
        private List<articulo> articulos;
        private List<tipoarticulo> tipoArticulos;
        private negocio neg;
        private DataSet ds;
        private DataView dv;
        private DataView dv2;
        private DataTable dt;
        private DataTable dt2;
        public string SelectedRow { get; set; }
        public int LineaSeleccionada { get; set; }
        private usuario Usu { get; set; }
        private linped Linea { get; set; }
        FormularioPrincipal formu;
        private int NumPedido { get; set; }
        private string FechPedido { get; set; }
        private char Modo { get; set; }

        public CyMPedidos(negocio n, FormularioPrincipal f)
        {            
            formu = f;
            Linea = new linped();
            dt = new DataTable();
            dt2 = new DataTable();
            ds = new DataSet();
            articulos = new List<articulo>();
            tipoArticulos = new List<tipoarticulo>();
            pedidos = new List<pedido>();
            lineaPedidos = new List<linped>();
            usuarios = new List<usuario>();
            neg = n;
            InitializeComponent();
        }

        private void CyMPedidos_Load(object sender, EventArgs e)
        {
            error.Visible = false;
            articulos = neg.articulos();
            pedidos = neg.pedidos();
            usuarios = neg.usuarios();
            lineaPedidos = neg.lineaPedidos();           

            dt.Columns.Add("IdPedido");
            dt.Columns.Add("UsuarioID");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellidos");
            dt.Columns.Add("Fecha");

            foreach (pedido p in pedidos)
            {
                DataRow row = dt.NewRow();
                row["IdPedido"] = p.PedidoID;
                row["UsuarioID"] = p.UsuarioID;
                foreach (usuario u in usuarios)
                {
                    if (u.UsuarioID == p.UsuarioID)
                    {
                        row["Nombre"] = u.Nombre;
                        row["Apellidos"] = u.Apellidos;
                    }
                }
                row["Fecha"] = p.Fecha;
                
                dt.Rows.Add(row);
            }
            rellenarData1();
            dt2.Columns.Add("PedidoId");
            dt2.Columns.Add("Linea");
            dt2.Columns.Add("ArticuloID");
            dt2.Columns.Add("Articulo");
            dt2.Columns.Add("Importe");
            dt2.Columns.Add("Cantidad");
            LineaSeleccionada = -1;
        }

        private void rellenarData1()
        {
            dv = new DataView(dt);
            dataGridView1.DataSource = dv;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(87, 108, 168);
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.IndianRed;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(27, 38, 79);
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.IndianRed;
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.White;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LineaSeleccionada = ((DataGridView)sender).CurrentRow.Index;

            var celdaSel = (DataGridView)sender;

            dt2.Clear();
            foreach (linped l in lineaPedidos)
            {
                DataRow row2 = dt2.NewRow();
                NumPedido = Convert.ToInt32(celdaSel.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                FechPedido = celdaSel.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
                if (Convert.ToInt32(celdaSel.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value) == l.PedidoID)
                {
                    row2["PedidoId"] = l.PedidoID;
                    row2["Linea"] = l.Linea;
                    row2["ArticuloID"] = l.ArticuloID;
                    foreach (articulo a in articulos)
                    {
                        if (l.ArticuloID == a.ArticuloID)
                        {
                            row2["Articulo"] = a.Nombre;
                        }
                    }
                    row2["Importe"] = l.Importe;
                    row2["Cantidad"] = l.Cantidad;
                    dt2.Rows.Add(row2);
                    }
                }
            
            dv2 = new DataView(dt2);
            dataGridView2.DataSource = dv2;

            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.Columns[4].ReadOnly = true;
            dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(87, 108, 168);
            dataGridView2.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.IndianRed;
            dataGridView2.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            dataGridView2.RowsDefaultCellStyle.BackColor = Color.FromArgb(27, 38, 79);
            dataGridView2.RowsDefaultCellStyle.SelectionBackColor = Color.IndianRed;
            dataGridView2.RowsDefaultCellStyle.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<pedido> p = new List<pedido>();
            List<linped> l = new List<linped>();
            List<usuario> u = new List<usuario>();
            List<articulo> a = new List<articulo>();
            usuario auxUsu = new usuario();
                        
            if (dataGridView1.SelectedRows.Count != 0)
            {
                pedido k = new pedido(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value),
                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value), dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString());

                p.Add(k);

                foreach(usuario us in usuarios)
                {
                    if (us.UsuarioID == k.UsuarioID)
                    {
                        auxUsu = new usuario(us.Email, us.Password, us.Nombre, us.Apellidos, us.Dni, us.Telefono, us.Calle, us.Calle2, us.Codpos, us.PuebloID, us.ProvinciaID, us.Nacido);
                        u.Add(auxUsu);
                    }
                }

                for (int i = 0; i < lineaPedidos.Count; i++)
                {
                    if (p[0].PedidoID == lineaPedidos[i].PedidoID)
                    {
                        l.Add(lineaPedidos[i]);                        
                    }
                }
                
                foreach(linped linea in l)
                {
                    foreach(articulo art in articulos)
                    {
                        if (linea.ArticuloID == art.ArticuloID)
                        {
                            a.Add(art);
                        }
                    }
                }

                for (int i = 0; i < formu.MdiChildren.Count(); i++)
                {
                    Form f = formu.MdiChildren[i];
                    f.Close();
                }
                formu.IsMdiContainer = false;
                formu.IsMdiContainer = true;

                Factura factura = new Factura(neg, formu, p, l, u, a);
                factura.MdiParent = formu;
                factura.WindowState = FormWindowState.Normal;
                factura.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < formu.MdiChildren.Count(); i++)
            {
                Form f = formu.MdiChildren[i];
                f.Close();
            }
            formu.IsMdiContainer = false;
            formu.IsMdiContainer = true;

            NuevoPedido modificar = new NuevoPedido(neg, formu, Usu, 'm', dt2, NumPedido, FechPedido);
            modificar.MdiParent = formu;
            modificar.WindowState = FormWindowState.Normal;
            modificar.Show();
            this.Close();
        }

        private void b_Usuario_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Visible)
            {
                dv.RowFilter = String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", String.Empty, b_Usuario.Text);
            }else
                dv.RowFilter = String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", dateTimePicker1.Value.ToString("d"), b_Usuario.Text);
        }

        private void b_Fecha_TextChanged(object sender, EventArgs e)
        {
            dv.RowFilter = String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", dateTimePicker1.Value.ToString("d"), b_Usuario.Text);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {            
            dv.RowFilter = String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", dateTimePicker1.Value.ToString("d"), b_Usuario.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            dv.RowFilter=String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", String.Empty, b_Usuario.Text);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            dv.RowFilter = String.Format("Fecha like '%{0}%' and Nombre like '%{1}%'", dateTimePicker1.Value.ToString("d"), b_Usuario.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idPedido = 0;
            if (LineaSeleccionada == -1)
            {
                error.Visible = true;
                error.Text = "Seleccionar primero el pedido";                
            }
            else
            {
                idPedido = Convert.ToInt32( dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                for(int i = 0; i < lineaPedidos.Count; i++)
                {
                    if (idPedido == lineaPedidos[i].PedidoID)
                    {
                        neg.eliminarLinPed(lineaPedidos[i]);
                    }                    
                }
                for(int i = 0; i < pedidos.Count; i++)
                {
                    if (idPedido == pedidos[i].PedidoID)
                    {
                        if (neg.eliminarPedido(idPedido.ToString()))
                        {
                            LineaSeleccionada = -1;
                            error.Visible = true;
                            
                            error.Text = "Pedido borrado correctamente";
                        }
                        else
                        {
                            error.Visible = true;
                            error.Text = "Ha habido un error borrando el pedido";
                        }
                    }
                }
            }
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            error.Visible = false;
        }
    }     
}
