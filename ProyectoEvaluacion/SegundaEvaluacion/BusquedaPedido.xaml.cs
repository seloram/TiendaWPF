using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BusquedaPedido.xaml
    /// </summary>
    public partial class BusquedaPedido : UserControl
    {
        private negocio neg;
        private List<usuario> listaUsuarios;
        private List<articulo> articulos;
        private List<pedido> listaPedidos;
        private List<linped> listaLinea;
        private List<linped> listaLineaAux;
        private usuario usu;
        private int PedidoId { get; set; }
        private FormularioPrincipal formu;
        private List<detallePedido> listaDetalles;
        private ObservableCollection<listaCompletaPedidos> usuPedidos;
        private CollectionViewSource listaFiltrada = new CollectionViewSource();

        public BusquedaPedido(negocio n, FormularioPrincipal f)
        {
            InitializeComponent();
            neg = n;
            formu = f;
            usu = new usuario();
            listaUsuarios = neg.usuarios();
            articulos = neg.articulos();
            listaDetalles = new List<detallePedido>();
            listaLinea = neg.lineaPedidos();
            listaLineaAux = new List<linped>();
            listaPedidos = neg.pedidos();
            usuPedidos = new ObservableCollection<listaCompletaPedidos>();
            listaFiltrada = (System.Windows.Data.CollectionViewSource)this.Resources["listaPedidos"];          
            informarListaComletaPedidos();
        }

        public void informarListaComletaPedidos()
        {
            int usuarioID=0, pedidoID=0;
            string nombre = "", apellidos = "", fecha = "";
            foreach(pedido p in listaPedidos)
            {                
                pedidoID = p.PedidoID;
                fecha = p.Fecha;
                usuarioID = p.UsuarioID;
                foreach(usuario u in listaUsuarios)
                {
                    if (p.UsuarioID == u.UsuarioID)
                    {
                        nombre = u.Nombre;
                        apellidos = u.Apellidos;
                        usuarioID = u.UsuarioID;
                    }
                }
                listaCompletaPedidos l = new listaCompletaPedidos(usuarioID, nombre, apellidos, pedidoID, fecha);
                usuPedidos.Add(l);
            }            
        }

        private void data_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int idUsuario = 0, idPedido = 0, importe=0, cantidad=0, linea=0;
            string idart = "", nombreArt="";
            listaDetalles.Clear();
            listaLineaAux.Clear();
            try
            {
                var pedidoAux = pedidos.SelectedCells[0].Item;
                idUsuario = ((listaCompletaPedidos)pedidoAux).UsuarioID;
                idPedido = ((listaCompletaPedidos)pedidoAux).PedidoID;
                PedidoId = idPedido;
                foreach(linped lin in listaLinea)
                {                    
                    if (lin.PedidoID == idPedido)
                    {
                        idart = lin.ArticuloID;
                        foreach(articulo a in articulos)
                        {
                            if (idart == a.ArticuloID)
                            {
                                nombreArt = a.Nombre;
                            }                                           
                        }
                        importe = lin.Importe;
                        cantidad = lin.Cantidad;
                        linea = lin.Linea;
                        detallePedido det = new detallePedido(nombreArt, linea, importe, cantidad, lin.ArticuloID, idPedido);

                        listaDetalles.Add(det);
                    }                    
                }

                lineas.DataContext = "";
                lineas.DataContext = listaDetalles;
            }
            catch { }
        }

        private void resultado_LostFocus(object sender, RoutedEventArgs e)
        {
            resultado.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listaFiltrada.Source = usuPedidos;
            resultado.Visibility = Visibility.Collapsed;
        }

        private void Filtrar(object sender, FilterEventArgs e)
        {
            listaCompletaPedidos pedido = (listaCompletaPedidos)e.Item;
            //string fecha = "";
            //try
            //{
            //    fecha = date.SelectedDate.Value.ToString("d");
            //}
            //catch { }

            if (pedido != null)
            {
                if ((pedido.Nombre.Contains(buscarUsuario.Text)
                    //&& (pedido.Fecha.Contains(buscarFecha.Text)))  )      
                    && (pedido.Fecha.Contains(date.SelectedDate.ToString()))))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void buscarUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void buscarFecha_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void buscarUsuarioCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            buscarUsuarioCapa.Visibility = Visibility.Collapsed;
            buscarUsuario.Focus();
        }

        private void buscarUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (buscarUsuario.Text == "")
            {
                buscarUsuarioCapa.Visibility = Visibility.Visible;
            }
        }

        private void buscarFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            if (date.Text == "")
            {
                buscarFechaCapa.Visibility = Visibility.Visible;
            }
        }

        private void buscarFechaCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            buscarFechaCapa.Visibility = Visibility.Collapsed;
            date.Visibility = Visibility.Visible;
            date.Focus();
            //buscarFecha.Focus();
            //date.Visibility = Visibility.Visible;
            //date.Focus();
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //string fecha = "";
            //try { fecha = date.SelectedDate.Value.ToString("d"); } catch { }
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
            
            //buscarFechaCapa.Visibility = Visibility.Visible;
            //buscarFechaCapa.Text = buscarFechaCapa.Text;
            //date.Visibility = Visibility.Collapsed;                     
        }

        private void date_LostFocus(object sender, RoutedEventArgs e)
        {
            if (date.Text == "")
            {
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void kk_TextChanged(object sender, TextChangedEventArgs e)
        {
            //listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void buscarFechaCapa_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void buscarUsuarioCapa_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (buscarUsuario.Text == "")
            //{
            //    buscarUsuarioCapa.Visibility = Visibility.Visible;
            //}
        }

        private void buscarFecha_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void buscarFecha_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (buscarFecha.Text == "")
            {
                buscarFechaCapa.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var pedido = pedidos.SelectedCells[0].Item;
            int index = pedidos.SelectedIndex;
            foreach(detallePedido det in listaDetalles)
            {
                linped l = new linped(det.PedidoID, det.Linea, det.ArticuloID, det.Importe, det.Cantidad);
               
                if (!neg.eliminarLinPed(l))
                {
                    resultado.Visibility = Visibility.Visible;
                    resultado.SetResourceReference(Control.StyleProperty, "textError");
                    resultado.Text = "Error al borrar la linea";
                    resultado.Focus();                
                }
            }

            usuPedidos.Remove(((listaCompletaPedidos)pedidos.SelectedCells[0].Item));
            pedido p = new pedido(((listaCompletaPedidos)pedido).PedidoID, ((listaCompletaPedidos)pedido).UsuarioID, 
                                ((listaCompletaPedidos)pedido).Fecha);

            if (neg.eliminarPedido(p.PedidoID.ToString()))
            {
                resultado.Visibility = Visibility.Visible;
                resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                resultado.Text = "Pedido borrado correctamente";
                resultado.Focus();
            }
            else
            {
                resultado.Visibility = Visibility.Visible;
                resultado.SetResourceReference(Control.StyleProperty, "textError");
                resultado.Text = "Error al borrar pedido";
                resultado.Focus();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (lineas.Items.Count != 0)
            {
                usuario usu = new usuario();
                try
                {
                    var pedidoAux = pedidos.SelectedCells[0].Item;
                    foreach (usuario u in listaUsuarios)
                    {
                        if (((listaCompletaPedidos)pedidoAux).UsuarioID == u.UsuarioID)
                        {
                            usu = u;
                        }
                    }
                    CMPedidos cmP = new CMPedidos(formu, neg, usu, ((listaCompletaPedidos)pedidoAux).PedidoID, 'm');
                    cmP.HorizontalAlignment = HorizontalAlignment.Center;
                    cmP.VerticalAlignment = VerticalAlignment.Center;
                    formu.panelPrincipal.Children.Add(cmP);
                    formu.panelPrincipal.Children.Remove(this);
                }
                catch
                {

                }
            }
            else
            {
                resultado.Visibility = Visibility.Visible;
                resultado.SetResourceReference(Control.StyleProperty, "textError");
                resultado.Text = "Selecciona un pedido primero";
                resultado.Focus();
            }
                      
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            List<listaCompletaPedidos> l_Pedidos= new List<listaCompletaPedidos>();
            foreach(listaCompletaPedidos l in usuPedidos)
            {
                if (PedidoId == l.PedidoID)
                {
                    l_Pedidos.Add(l);
                }              
            }          
           
            Factura f = new Factura(neg, formu, l_Pedidos, listaDetalles);
            f.HorizontalAlignment = HorizontalAlignment.Center;
            f.VerticalAlignment = VerticalAlignment.Center;
            
            formu.panelPrincipal.Children.Clear();
            formu.panelPrincipal.Children.Add(f);
        }

        private void date_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void date_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //date.Text = "";
        }

        private void date_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void date_MouseEnter(object sender, MouseEventArgs e)
        {
            //date.Text = "";
        }

        private void date_MouseLeave(object sender, MouseEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
            if (date.Text == "" || date.Text == "Seleccione una fecha")
            {
                buscarFechaCapa.Text = "Fecha*";
                date.Text = "Seleccione una fecha";
                date.SelectedDate = null;
            }
            else
                buscarFechaCapa.Text = date.Text;
            
            buscarFechaCapa.Visibility = Visibility.Visible;
            date.Visibility = Visibility.Collapsed;
          
        }

        private void date_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void date_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            
        }
    }
}
