using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapaNegocio;
using CapaEntidades;
using LiveCharts;
using LiveCharts.Wpf;
using CapaNegocio;
using CapaEntidades;
using System.Collections;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for Estadisticas.xaml
    /// </summary>
    public partial class Estadisticas : UserControl
    {
        private List<pedido> listaPedidos;
        private List<tipoarticulo> listaTipoArt;
        private List<articulo> listaArticulos;
        private List<linped> listaLineaPed;

        public Estadisticas(negocio neg, FormularioPrincipal f)
        {
            listaPedidos = neg.pedidos();
            listaTipoArt = neg.tipoArticulos();
            listaArticulos = neg.articulos();
            listaLineaPed = neg.lineaPedidos();
            InitializeComponent();
        }

        private void Calcular_Click(object sender, RoutedEventArgs e)
        {
            //chart1.Series[0].Points.Clear();
            SeriesCollection serie1 = new SeriesCollection();
            List<pedido> myLista = listaPedidos.OrderByDescending(o => Convert.ToDateTime(o.Fecha)).ToList();
            List<articulo> miListaPastel = listaArticulos.OrderBy(o => o.TipoArticuloID).ToList();
            int cont = 0;
            string fecha = DateTime.MinValue.ToShortDateString();
            bool mayor = false;
            int x = 0;
            string[] a = new[] { "d", "ddd" };
            List<string> res = new List<string>();
            serie1.Add(new ColumnSeries
            {
                Title = "Pedidos realizados",
                Values = new ChartValues<int> { }
            });
            chart1.Series = serie1;
            int y = 1;
            g2_eje_x.Labels = new List<string>();
            //chart1.Series[0].Title =  "Pedidos por día";
            for (int f = 0; f < myLista.Count; f++)
            {                
                if ((DateTime.Parse(myLista[f].Fecha).Month.ToString() == pedidosDia.SelectedDate.Value.Month.ToString()
                    /*t_mes.Text*/) && (DateTime.Parse(myLista[f].Fecha).Year.ToString() == pedidosDia.SelectedDate.Value.Year.ToString() /*t_anyo.Text*/))
                {
                    fecha = DateTime.Parse(myLista[f].Fecha).ToShortDateString();
                    cont++;
                    if (f < 68)
                    {
                        while (DateTime.Parse(myLista[f].Fecha).ToShortDateString() == DateTime.Parse(myLista[f + 1].Fecha).ToShortDateString())
                        {
                            cont++;
                            f++;
                        }
                    }
                    
                    chart1.Series[chart1.Series.CurrentSeriesIndex].Values.Add(cont);                    
                    g2_eje_x.Labels.Add("Dia:" + DateTime.Parse(myLista[f].Fecha).Day.ToString());
                    
                    
                    //res.Add(cont);
                    //chart1.Series[0].Points.Add(cont);
                    //chart1.Series[0].Points[x].Label = "Dia:" + DateTime.Parse(myLista[i].Fecha).Day.ToString();
                    //chart1.Series[0].Points[x].IsValueShownAsLabel = true;
                    x++;
                    y++;
                    cont = 0;
                }
            }

            SeriesCollection seriePastel = new SeriesCollection();


            
            var art = from pe in listaPedidos
                      join lin in listaLineaPed on pe.PedidoID equals lin.PedidoID
                      where DateTime.Parse(pe.Fecha).Month.ToString() == pedidosDia.SelectedDate.Value.Month.ToString() /*t_mes.Text*/ &&
                      DateTime.Parse(pe.Fecha).Year.ToString() == pedidosDia.SelectedDate.Value.Year.ToString() /*t_anyo.Text*/
                      select new { lin.ArticuloID, lin.Cantidad };

            IEnumerator i = art.GetEnumerator();

            var cant2 = (from ab in art
                         join b in listaArticulos on ab.ArticuloID equals b.ArticuloID
                         group ab by b.TipoArticuloID into g
                         select new { key = g.Key, cantidad = g.Sum(xy => xy.Cantidad) }).ToList();

            IEnumerator listcant = cant2.GetEnumerator();

            int j = 0;
            string nombre = "";
            foreach (var b in cant2)
            {
                foreach (tipoarticulo t in listaTipoArt)
                {
                    if (b.key == t.TipoArticuloID.ToString())
                    {
                        nombre = t.Descripcion;
                    }
                }
                seriePastel.Add(new PieSeries
                {
                    Title = "Tipo:" + nombre,
                    Values = new ChartValues<double> {b.cantidad  },
                    //Stroke = System.Windows.Media.Brushes.HotPink,
                    //Fill = System.Windows.Media.Brushes.HotPink
                });
                pastel.Series = seriePastel;
                j++;
            }




        }


    }
}
