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
using CapaEntidades;
using CapaNegocio;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for ComprobarStock.xaml
    /// </summary>
    public partial class ComprobarStock : UserControl
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int cantidad;
            if (!int.TryParse(limite.Text, out cantidad))
            {
                if (resultado != null)
                {
                    if (limite.Text != "" && limite.Text != "Establezca el límite mínimo")
                    {
                        resultado.SetResourceReference(Control.StyleProperty, "textError");
                        resultado.Text = "Introduzca números exclusivamente";
                        resultado.Visibility = Visibility.Visible;
                        resultado.Focus();
                    }
                }

            }
            else
            {
                if (!CalcularStock(Convert.ToInt32(limite.Text)))
                {
                    resultado.SetResourceReference(Control.StyleProperty, "textError");
                    resultado.Text = "No hay productos por debajo de ese límite";
                    resultado.Visibility = Visibility.Visible;
                    resultado.Focus();
                }
                else
                {
                    Stock stock = new Stock(neg, formu, stocks, articulos, tipo);
                    formu.panelPrincipal.Children.Clear();
                    formu.panelPrincipal.Children.Add(stock);
                }

                
            }
            
        }

        public bool CalcularStock(int cantidad)
        {
            foreach (stock s in listaStock)
            {
                if (s.Disponible < cantidad)
                {
                    stocks.Add(s);
                    foreach (articulo a in listaArticulo)
                    {
                        if (s.ArticuloID == a.ArticuloID)
                        {
                            articulos.Add(a);
                            foreach (tipoarticulo t in tipoArti)
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
            if (tipo.Count == 0)
            {
                return false;
            }
            return true;
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            listaArticulo = neg.articulos();
            listaStock = neg.leerStock();
            tipoArti = neg.tipoArticulos();
        }

        private void limite_GotFocus(object sender, RoutedEventArgs e)
        {
            limite.Text = "";
        }

        private void limite_LostFocus(object sender, RoutedEventArgs e)
        {
            if (limite.Text == "")
            {
                limite.Text = "Establezca el límite mínimo";
            }
        }

        private void limite_TextChanged(object sender, TextChangedEventArgs e)
        {
                      
        }

        private void resultado_LostFocus(object sender, RoutedEventArgs e)
        {
            resultado.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            resultado.Text = "Establezca el límite mínimo";
            resultado.Visibility = Visibility.Hidden;
        }
    }
}
