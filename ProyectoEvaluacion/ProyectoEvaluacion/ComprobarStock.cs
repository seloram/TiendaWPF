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
    public partial class ComprobarStock : Form
    {
        private List<stock> listaStock;
        private List<articulo> listaArticulo;
        private negocio neg;
        private FormularioPrincipal formu;
        private List<stock> stocks;
        private List<articulo> articulos;
        private List<tipoarticulo> tipoArti;
        private List<tipoarticulo> tipo;

        public ComprobarStock(negocio n, FormularioPrincipal f)
        {
            InitializeComponent();
            neg = n;
            formu = f;
            listaStock = new List<stock>();
            listaArticulo = new List<articulo>();
            stocks = new List<stock>();
            articulos = new List<articulo>();
            tipoArti = new List<tipoarticulo>();
            tipo = new List<tipoarticulo>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            neg.CalcularStock(Convert.ToInt32(numericUpDown1.Value), listaStock, listaArticulo, stocks, articulos, tipoArti, tipo);
            formu.IsMdiContainer = false;
            formu.IsMdiContainer = true;

            StockReducido stock = new StockReducido(neg, formu, stocks, articulos, tipo);
            stock.MdiParent = formu;
            stock.WindowState = FormWindowState.Normal;
            stock.Show();
        }

        public void CalcularStock(int cantidad)
        {
            foreach(stock s in listaStock)
            {
                if (s.Disponible < cantidad)
                {
                    stocks.Add(s);
                    foreach(articulo a in listaArticulo)
                    {
                        if (s.ArticuloID == a.ArticuloID)
                        {
                            articulos.Add(a);
                            foreach(tipoarticulo t in tipoArti)
                            {
                                if (t.TipoArticuloID.ToString() == a.TipoArticuloID)
                                {
                                    tipo.Add(t);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ComprobarStock_Load(object sender, EventArgs e)
        {
            listaArticulo = neg.articulos();
            listaStock = neg.leerStock();
            tipoArti = neg.tipoArticulos();
        }
    }
}
