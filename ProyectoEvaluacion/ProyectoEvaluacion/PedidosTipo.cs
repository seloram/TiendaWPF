using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProyectoEvaluacion
{
    public partial class PedidosTipo : Form
    {
        private List<pedido> listaPedidos;
        private List<tipoarticulo> listaTipoArt;
        private List<articulo> listaArticulos;
        private List<linped> listaLineaPed;
        public PedidosTipo(negocio neg, FormularioPrincipal f)
        {
            listaPedidos = neg.pedidos();
            listaTipoArt = neg.tipoArticulos();
            listaArticulos = neg.articulos();
            listaLineaPed = neg.lineaPedidos();

            InitializeComponent();
        }

        private void PedidosTipo_Load(object sender, EventArgs e)
        {
            List<articulo> miLista = listaArticulos.OrderBy(o => o.TipoArticuloID).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            var art = from pe in listaPedidos
                      join lin in listaLineaPed on pe.PedidoID equals lin.PedidoID
                      where DateTime.Parse(pe.Fecha).Month.ToString() == dateTimePicker1.Value.Month.ToString() /*t_mes.Text*/ &&
                      DateTime.Parse(pe.Fecha).Year.ToString() == dateTimePicker1.Value.Year.ToString() /*t_anyo.Text*/
                      select new { lin.ArticuloID, lin.Cantidad };

            IEnumerator i = art.GetEnumerator();

            var cant2 = (from a in art
                        join b in listaArticulos on a.ArticuloID equals b.ArticuloID
                        group a by b.TipoArticuloID into g
                        select new { key= g.Key, cantidad = g.Sum(x => x.Cantidad)}).ToList();

            IEnumerator listcant = cant2.GetEnumerator();

            int j = 0;
            string nombre = "";
            foreach (var a in cant2)
            {
                foreach(tipoarticulo t in listaTipoArt)
                {
                    if (a.key == t.TipoArticuloID.ToString())
                    {
                        nombre = t.Descripcion;
                    }
                }
                chart1.Series[0].Points.Add(a.cantidad);
                chart1.Series[0].Points[j].LegendText = "Tipo:" + nombre;
                chart1.Series[0].Points[j].IsValueShownAsLabel = true;
                j++;
            }
        }
    }
}
