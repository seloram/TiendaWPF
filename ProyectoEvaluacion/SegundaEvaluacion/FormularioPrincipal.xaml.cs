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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CapaNegocio;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for FormularioPrincipal.xaml
    /// </summary>
    public partial class FormularioPrincipal : Window
    {
        private FormularioPrincipal formu;
        private string Ruta { get; set; }
        private MainWindow main;
        private negocio neg;
        DoubleAnimation animacionAnchoBoton;
        DoubleAnimation animacionAltoBoton;
        DoubleAnimation animacionAnchoTexto;
    

        public FormularioPrincipal(MainWindow m, negocio n)
        {
            InitializeComponent();
            neg = n;
            main = m;
            animacionAnchoBoton = new DoubleAnimation();
            animacionAltoBoton = new DoubleAnimation();
            animacionAnchoTexto = new DoubleAnimation();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            main.Visibility = Visibility.Visible;
            main.datoUsu.Text = "";
            main.pass.Password = "";
            main.intentos = 3;
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //Insertar insertar = new Insertar(neg);
            //insertar.HorizontalAlignment = HorizontalAlignment.Center;
            //insertar.VerticalAlignment = VerticalAlignment.Center;

            //panel1.Children.Clear();
            //panel1.Children.Add(insertar);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.datoUsu.Text = "Usuario";
            main.password.Visibility = Visibility.Visible;
            main.pass.Password = "";
            main.datoUsu.Focus();
            main.Visibility = Visibility.Visible;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            altaUsuario.Width = 0;
            altaUsuario.Height = 0;
            consultaUsu.Width = 0;
            consultaUsu.Height = 0;
            pedidosNuevo.Width = 0;
            pedidosNuevo.Height = 0;
            pedidosConsulta.Width = 0;
            pedidosConsulta.Height = 0;
            informesFactura.Width = 0;
            informesFactura.Height = 0;
            informesStock.Width = 0;
            informesStock.Height = 0;
            Ruta = System.IO.Directory.GetCurrentDirectory() +
                "\\..\\..\\..\\";

            animacionAnchoBoton = new DoubleAnimation();
            animacionAnchoBoton.To = 250;
            animacionAnchoBoton.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            Storyboard storyAncho = new Storyboard();
            storyAncho.Children.Add(animacionAnchoBoton);

            animacionAltoBoton = new DoubleAnimation();
            animacionAltoBoton.To = 70;
            animacionAltoBoton.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            Storyboard storyAlto = new Storyboard();
            storyAlto.Children.Add(animacionAltoBoton);


        }
        
        private void usuarios_MouseEnter(object sender, MouseEventArgs e)
        {
            altaUsuImag.Source = new BitmapImage(new Uri(Ruta + "altaUsuario2.jfif", UriKind.Absolute));
            consulUsuImag.Source = new BitmapImage(new Uri(Ruta + "usuarios.jfif", UriKind.Absolute));
            
        }

        private void usuarios_MouseLeave(object sender, MouseEventArgs e)
        {
           
        }

        private void altaUsuario_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void altaUsuario_MouseLeave(object sender, MouseEventArgs e)
        {
       
        }

        private void consultaUsu_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void consultaUsu_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void usuario_Click(object sender, RoutedEventArgs e)
        {
            //Insertar insertar = new Insertar(neg);
            //insertar.HorizontalAlignment = HorizontalAlignment.Center;
            //insertar.VerticalAlignment = VerticalAlignment.Center;
            //panelPrincipal.Children.Clear();
            //panelPrincipal.Children.Add(insertar);
        }

        private void insertarUsuario(object sender, RoutedEventArgs e)
        {
            Insertar insertar = new Insertar(neg,null,'i',null,this);
            insertar.HorizontalAlignment = HorizontalAlignment.Center;
            insertar.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(insertar);
        }

        private void consultaUsuario(object sender, RoutedEventArgs e)
        {
            ModificacionUsuario modificarUsusario = new ModificacionUsuario(null, this, neg, 'x');
            modificarUsusario.HorizontalAlignment = HorizontalAlignment.Center;
            modificarUsusario.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(modificarUsusario);
        }

        private void mostrarProductos(object sender, RoutedEventArgs e)
        {
            DatosProductos productos = new DatosProductos(neg);
            productos.HorizontalAlignment = HorizontalAlignment.Center;
            productos.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(productos);
        }

        private void pedidosConsulta_Click(object sender, RoutedEventArgs e)
        {
            BusquedaPedido bPedidos = new BusquedaPedido(neg, this);
            bPedidos.HorizontalAlignment = HorizontalAlignment.Center;
            bPedidos.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(bPedidos);
        }

        private void pedidosNuevo_Click(object sender, RoutedEventArgs e)
        {
            CMPedidos cmpedidos = new CMPedidos(this, neg, null, 0, 'z');
            cmpedidos.HorizontalAlignment = HorizontalAlignment.Center;
            cmpedidos.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(cmpedidos);
        }

        private void informes_Click(object sender, RoutedEventArgs e)
        {
            BusquedaPedido bPedidos = new BusquedaPedido(neg, this);
            bPedidos.HorizontalAlignment = HorizontalAlignment.Center;
            bPedidos.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(bPedidos);
        }

        private void informesStock_Click(object sender, RoutedEventArgs e)
        {
            ComprobarStock c = new ComprobarStock(neg, this);
            c.HorizontalAlignment = HorizontalAlignment.Center;
            c.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(c);
        }

        private void estadisticas_click(object sender, RoutedEventArgs e)
        {
            Estadisticas es = new Estadisticas(neg, this);
            es.HorizontalAlignment = HorizontalAlignment.Center;
            es.VerticalAlignment = VerticalAlignment.Center;
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(es);
        }
        /*
private void estadisticas_MouseEnter(object sender, MouseEventArgs e)
{
DoubleAnimation animacion = new DoubleAnimation();
animacion.From = 200;
animacion.To = 250;
animacion.Duration = new Duration(TimeSpan.FromMilliseconds(700));
Storyboard story = new Storyboard();
story.Children.Add(animacion);
((Button)sender).BeginAnimation(WidthProperty, animacion);
}

private void pedidosConsulta_MouseEnter(object sender, MouseEventArgs e)
{
DoubleAnimation animacion = new DoubleAnimation();
animacion.From = 200;
animacion.To = 250;
animacion.Duration = new Duration(TimeSpan.FromMilliseconds(700));
Storyboard story = new Storyboard();
story.Children.Add(animacion);
((Button)sender).BeginAnimation(WidthProperty, animacion);
informes.BeginAnimation(WidthProperty, animacion);
}

private void pedidosConsultaClick(object sender, RoutedEventArgs e)
{
DoubleAnimation animacion = new DoubleAnimation();
animacion.From = 200;
animacion.To = 250;
animacion.Duration = new Duration(TimeSpan.FromMilliseconds(700));
Storyboard story = new Storyboard();
story.Children.Add(animacion);
((Button)sender).BeginAnimation(WidthProperty, animacion);
}

private void pedidos_MouseEnter(object sender, MouseEventArgs e)
{
/* DoubleAnimation animacionAncho = new DoubleAnimation();
animacionAncho.To = 250;
animacionAncho.Duration = new Duration(TimeSpan.FromMilliseconds(500));
Storyboard storyAncho = new Storyboard();
storyAncho.Children.Add(animacionAncho);
((Button)sender).BeginAnimation(WidthProperty, animacionAncho);

DoubleAnimation animacionAlto = new DoubleAnimation();
animacionAlto.To = 70;
animacionAlto.Duration = new Duration(TimeSpan.FromMilliseconds(500));
Storyboard storyAlto = new Storyboard();
storyAlto.Children.Add(animacionAlto);
animacionAncho.To = 250;
((Button)sender).BeginAnimation(WidthProperty, animacionAncho);
animacionAncho.To = 110;
t_pedidos.BeginAnimation(WidthProperty, animacionAncho);
animacionAncho.To = 250;
animacionAncho.BeginTime = TimeSpan.FromMilliseconds(400);
animacionAlto.BeginTime = TimeSpan.FromMilliseconds(400);
animacionAlto.To = 70;           
pedidosConsulta.BeginAnimation(HeightProperty, animacionAlto);
pedidosConsulta.BeginAnimation(WidthProperty, animacionAncho);
animacionAncho.BeginTime = TimeSpan.FromMilliseconds(200);
animacionAlto.BeginTime = TimeSpan.FromMilliseconds(200);
pedidosNuevo.BeginAnimation(HeightProperty, animacionAlto);
pedidosNuevo.BeginAnimation(WidthProperty, animacionAncho);
}

private void pedidos_MouseLeave(object sender, MouseEventArgs e)
{           
animacionAncho.To = 200;
((Button)sender).BeginAnimation(WidthProperty, animacionAncho);
animacionAncho.BeginTime = TimeSpan.FromMilliseconds(900);
animacionAlto.BeginTime = TimeSpan.FromMilliseconds(900);
animacionAlto.To = 0;
animacionAncho.To = 0;
pedidosConsulta.BeginAnimation(HeightProperty, animacionAlto);
pedidosConsulta.BeginAnimation(WidthProperty, animacionAncho);
animacionAncho.BeginTime = TimeSpan.FromMilliseconds(700);
animacionAlto.BeginTime = TimeSpan.FromMilliseconds(700);
pedidosNuevo.BeginAnimation(HeightProperty, animacionAlto);
pedidosNuevo.BeginAnimation(WidthProperty, animacionAncho);
}*/
    }
}
