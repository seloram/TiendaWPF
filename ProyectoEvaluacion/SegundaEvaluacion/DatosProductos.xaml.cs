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
using System.Collections.ObjectModel;
using CapaEntidades;
using CapaNegocio;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for DatosProductos.xaml
    /// </summary>
    public partial class DatosProductos : UserControl
    {
        private List<articulo> art;
        private articulo Art { get; set; }
        public int Index { get; set; }
        private List<articulo> listaP;
        private CollectionViewSource listaFiltrada = new CollectionViewSource();
        private List<string> descripciones;
        private negocio neg;
        private List<tv> listaTv;
        private List<camara> listaCamara;
        private List<objetivo> listaObjetivo;
        private List<memoria> listaMemoria;
        private List<tipoarticulo> tipo;

        public DatosProductos(negocio n)
        {
            Index = 0;
            InitializeComponent();
            listaFiltrada = (System.Windows.Data.CollectionViewSource)this.Resources["listaProductos"];
            listaP = new List<articulo>();
            neg = n;
            art = neg.articulos();
            listaTv = neg.leerTv();
            listaObjetivo = neg.leerObjetivos();
            listaMemoria = neg.leerMemoria();
            descripciones = new List<string>();
            tipo = neg.tipoArticulos();
            Art = new articulo();
            listaCamara = neg.leerCamaras();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {           
            foreach (articulo u in art)
            {
                listaP.Add(u);
            }
            resultado.Visibility = Visibility.Hidden;
            foreach(articulo a in listaP)
            {
                foreach(tipoarticulo t in tipo)
                {
                    if (a.TipoArticuloID == t.TipoArticuloID.ToString())
                    {
                        a.TipoArticuloID = t.Descripcion;
                    }                    
                }             
            }
            listaFiltrada.Source = listaP;
            
        }

        private void Filtrar(object sender, FilterEventArgs e)
        {
            articulo arti = (articulo)e.Item;

            if (arti != null)
            {
                if (arti.Nombre.Contains(filtrarNombre2.Text)
                    && arti.TipoArticuloID.Contains(filtrarTipo2.Text))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void nombreProducto(object sender, KeyEventArgs e)
        {
            
        }

        private void filtrarTipo_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void listaArt_GotFocus(object sender, RoutedEventArgs e)
        {

           
        }

        private camara datosCamara(string id)
        {
            camara camaraAux = new camara();
            foreach(camara c in listaCamara)
            {
                if (id == c.CamaraID)
                {
                    camaraAux = c;
                }
            }
            return camaraAux;
        }

        private tv datosTV(string id)
        {
            tv tele = new tv();
            foreach(tv t in listaTv)
            {
                if (id == t.TvID)
                {
                    tele = t;
                }
            }
            return tele;
        }
        private memoria datosMem(string id)
        {
            memoria mem = new memoria();
            foreach (memoria m in listaMemoria)
            {
                if (id == m.MemoriaID)
                {
                    mem = m;
                }
            }
            return mem;
        }

        private objetivo datosObj(string id)
        {
            objetivo obj = new objetivo();
            foreach (objetivo o in listaObjetivo)
            {
                if (id == o.ObjetivoID)
                {
                    obj = o;
                }
            }
            return obj;
        }

        private void listaArt_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void listaArt_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void listaArt_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (resultado.Visibility == Visibility.Visible)
            {
                resultado.Visibility = Visibility.Collapsed;
                aceptar.Visibility = Visibility.Visible;
            }
            Index = e.Row.GetIndex();            
        }

        private void listaArt_GotFocus(object sender, MouseEventArgs e)
        {

        }

        private void listaArt_GotFocus_1(object sender, RoutedEventArgs e)
        {

        }

        private void listaArt_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void listaArt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (resultado.Visibility == Visibility.Visible)
            {
                resultado.Visibility = Visibility.Collapsed;
                aceptar.Visibility = Visibility.Visible;
            }
            try
            {
                var articulo = listaArt.SelectedCells[0].Item;

                switch (((articulo)articulo).TipoArticuloID)
                {
                    case "Camara":
                        datosArt.Children.Clear();
                        camara camaraAux = datosCamara(((articulo)articulo).ArticuloID);
                        if (((articulo)articulo).Nombre != null)
                        {
                            TextBox nombre = new TextBox();
                            nombre.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            nombre.Margin = new Thickness(10);
                            nombre.Text = ((articulo)articulo).Nombre;
                            nombre.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(nombre);
                        }

                        if (((articulo)articulo).MarcaID != null)
                        {
                            TextBox marca = new TextBox();
                            marca.Margin = new Thickness(10);
                            marca.Text = ((articulo)articulo).MarcaID;
                            marca.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            marca.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(marca);
                        }

                        if (((articulo)articulo).Pvp != null)
                        {
                            TextBox pvp = new TextBox();
                            pvp.Margin = new Thickness(10);
                            pvp.Text = ((articulo)articulo).Pvp;
                            pvp.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pvp.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pvp);
                        }

                        if (camaraAux.Resolucion != null)
                        {
                            TextBox resolucion = new TextBox();
                            resolucion.Margin = new Thickness(10);
                            resolucion.Text = camaraAux.Resolucion;
                            resolucion.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            resolucion.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(resolucion);
                        }

                        if (camaraAux.Sensor != null)
                        {
                            TextBox sensor = new TextBox();
                            sensor.Margin = new Thickness(10);
                            sensor.Text = camaraAux.Sensor;
                            sensor.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            sensor.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(sensor);
                        }

                        if (camaraAux.Tipo != null)
                        {
                            TextBox tipo = new TextBox();
                            tipo.Margin = new Thickness(10);
                            tipo.Text = camaraAux.Tipo;
                            tipo.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            tipo.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(tipo);
                        }

                        if (camaraAux.Factor != null)
                        {
                            TextBox factor = new TextBox();
                            factor.Margin = new Thickness(10);
                            factor.Text = camaraAux.Tipo;
                            factor.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            factor.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(factor);
                        }

                        if (camaraAux.Objetivo != null)
                        {
                            TextBox objetivo = new TextBox();
                            objetivo.Margin = new Thickness(10);
                            objetivo.Text = camaraAux.Objetivo;
                            objetivo.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            objetivo.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(objetivo);
                        }

                        if (camaraAux.Pantalla != null)
                        {
                            TextBox pantalla = new TextBox();
                            pantalla.Margin = new Thickness(10);
                            pantalla.Text = camaraAux.Pantalla;
                            pantalla.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pantalla.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pantalla);
                        }
                        break;
                    case "TV":
                        datosArt.Children.Clear();
                        tv tvAux = datosTV(((articulo)articulo).ArticuloID);
                        if (((articulo)articulo).Nombre != null)
                        {
                            TextBox nombre = new TextBox();
                            nombre.Margin = new Thickness(10);
                            nombre.Text = ((articulo)articulo).Nombre;
                            nombre.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            nombre.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(nombre);
                        }

                        if (((articulo)articulo).MarcaID != null)
                        {
                            TextBox marca = new TextBox();
                            marca.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            marca.Margin = new Thickness(10);
                            marca.Text = ((articulo)articulo).MarcaID;
                            marca.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(marca);
                        }

                        if (((articulo)articulo).Pvp != null)
                        {
                            TextBox pvp = new TextBox();
                            pvp.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pvp.Margin = new Thickness(10);
                            pvp.Text = ((articulo)articulo).Pvp;
                            pvp.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pvp);
                        }

                        if (tvAux.Hdreadyfullhd != null)
                        {
                            TextBox hdready = new TextBox();
                            hdready.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            hdready.Margin = new Thickness(10);
                            hdready.Text = tvAux.Hdreadyfullhd;
                            hdready.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(hdready);
                        }

                        if (tvAux.Panel != null)
                        {
                            TextBox panel = new TextBox();
                            panel.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            panel.Margin = new Thickness(10);
                            panel.Text = tvAux.Panel;
                            panel.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(panel);
                        }

                        if (tvAux.Pantalla != null)
                        {
                            TextBox pantalla = new TextBox();
                            pantalla.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pantalla.Margin = new Thickness(10);
                            pantalla.Text = tvAux.Pantalla;
                            pantalla.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pantalla);
                        }

                        if (tvAux.Resolucion != null)
                        {
                            TextBox resolucion = new TextBox();
                            resolucion.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            resolucion.Margin = new Thickness(10);
                            resolucion.Text = tvAux.Resolucion;
                            resolucion.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(resolucion);
                        }

                        if (tvAux.Tdt != null)
                        {
                            TextBox tdt = new TextBox();
                            tdt.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            tdt.Margin = new Thickness(10);
                            tdt.Text = tvAux.Tdt;
                            tdt.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(tdt);
                        }
                        break;
                    case "Memoria":
                        datosArt.Children.Clear();
                        memoria memAux = datosMem(((articulo)articulo).ArticuloID);
                        if (((articulo)articulo).Nombre != null)
                        {
                            TextBox nombre = new TextBox();
                            nombre.Margin = new Thickness(10);
                            nombre.Text = ((articulo)articulo).Nombre;
                            nombre.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            nombre.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(nombre);
                        }

                        if (((articulo)articulo).MarcaID != null)
                        {
                            TextBox marca = new TextBox();
                            marca.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            marca.Margin = new Thickness(10);
                            marca.Text = ((articulo)articulo).MarcaID;
                            marca.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(marca);
                        }

                        if (((articulo)articulo).Pvp != null)
                        {
                            TextBox pvp = new TextBox();
                            pvp.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pvp.Margin = new Thickness(10);
                            pvp.Text = ((articulo)articulo).Pvp;
                            pvp.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pvp);
                        }

                        if (memAux.Tipo != null)
                        {
                            TextBox tipo = new TextBox();
                            tipo.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            tipo.Margin = new Thickness(10);
                            tipo.Text = memAux.Tipo;
                            tipo.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(tipo);
                        }                        
                        break;
                    case "Objetivo":
                        datosArt.Children.Clear();
                        objetivo objAux = datosObj(((articulo)articulo).ArticuloID);
                        if (((articulo)articulo).Nombre != null)
                        {
                            TextBox nombre = new TextBox();
                            nombre.Margin = new Thickness(10);
                            nombre.Text = ((articulo)articulo).Nombre;
                            nombre.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            nombre.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(nombre);
                        }

                        if (((articulo)articulo).MarcaID != null)
                        {
                            TextBox marca = new TextBox();
                            marca.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            marca.Margin = new Thickness(10);
                            marca.Text = ((articulo)articulo).MarcaID;
                            marca.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(marca);
                        }

                        if (((articulo)articulo).Pvp != null)
                        {
                            TextBox pvp = new TextBox();
                            pvp.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            pvp.Margin = new Thickness(10);
                            pvp.Text = ((articulo)articulo).Pvp;
                            pvp.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(pvp);
                        }

                        if (objAux.Tipo != null)
                        {
                            TextBox tipo = new TextBox();
                            tipo.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            tipo.Margin = new Thickness(10);
                            tipo.Text = objAux.Tipo;
                            tipo.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(tipo);
                        }
                        if (objAux.Montura != null)
                        {
                            TextBox montura = new TextBox();
                            montura.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            montura.Margin = new Thickness(10);
                            montura.Text = objAux.Montura;
                            montura.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(montura);
                        }
                        if (objAux.Focal != null)
                        {
                            TextBox focal = new TextBox();
                            focal.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            focal.Margin = new Thickness(10);
                            focal.Text = objAux.Focal;
                            focal.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(focal);
                        }
                        if (objAux.Apertura != null)
                        {
                            TextBox apertura = new TextBox();
                            apertura.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            apertura.Margin = new Thickness(10);
                            apertura.Text = objAux.Apertura;
                            apertura.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(apertura);
                        }
                        if (objAux.Especiales != null)
                        {
                            TextBox especiales = new TextBox();
                            especiales.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                            especiales.Margin = new Thickness(10);
                            especiales.Text = objAux.Especiales;
                            especiales.HorizontalAlignment = HorizontalAlignment.Left;
                            datosArt.Children.Add(especiales);
                        }
                        break;
                }
            }
            catch
            {

            }
        }

        private void listaArt_CurrentCellChanged_1(object sender, EventArgs e)
        {
            
        }

        private void listaArt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void listaArt_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void listaArt_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void listaArt_TargetUpdated(object sender, DataTransferEventArgs e)
        {
           
        }

        private void filtrarNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            filtrarNombre.Text = "";
        }

        private void filtrarNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarNombre.Text == "")
            {
                filtrarNombreCapa.Visibility = Visibility.Visible;
            }            
        }

        private void filtrarTipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarTipo.Text == "")
            {
                filtrarTipoCapa.Visibility = Visibility.Visible;
            }            
        }

        private void filtrarTipo_GotFocus(object sender, RoutedEventArgs e)
        {
            filtrarTipo.Text = "";
        }

        private void filtrarTipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filtrarTipo.Text != "")
            {
                listaFiltrada.Filter += new FilterEventHandler(Filtrar);
            }
        }

        private void filtrarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filtrarNombre.Text != "")
            {
                listaFiltrada.Filter += new FilterEventHandler(Filtrar);
            }
           
        }

        private void filtrarNombreCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarNombreCapa_MouseEnter(object sender, MouseEventArgs e)
        {

            filtrarNombreCapa.Visibility = Visibility.Collapsed;
            filtrarNombre2.Focus();
        }

        private void filtrarTipoCapa_MouseEnter(object sender, MouseEventArgs e)
        {
            filtrarTipoCapa.Visibility = Visibility.Collapsed;
            filtrarTipo2.Focus();
        }

        private void filtrarNombre2_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);      
        }

        private void filtrarTipo2_TextChanged(object sender, TextChangedEventArgs e)
        { 
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void filtrarTipo2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarTipo2.Text == "")
            {
                filtrarTipoCapa.Visibility = Visibility.Visible;
            }
        }

        private void filtrarNombre2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarNombre2.Text == "")
            {
                filtrarNombreCapa.Visibility = Visibility.Visible;
            }
        }

        private void filtrarTipoCapa_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void filtrarNombreCapa_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            aceptar.Visibility = Visibility.Collapsed;
           // var articulo = listaArt.SelectedCells[0].Item;
            var b = listaArt.Items[Index];
            foreach (tipoarticulo a in tipo)
            {
                if (((articulo)b).TipoArticuloID == a.Descripcion)
                {
                    Art.TipoArticuloID = a.TipoArticuloID.ToString();
                }
            }

            Art.ArticuloID = ((articulo)b).ArticuloID;
            Art.Blob = ((articulo)b).Blob;
            Art.Especificaciones = ((articulo)b).Especificaciones;
            Art.MarcaID = ((articulo)b).MarcaID;
            Art.Nombre = ((articulo)b).Nombre;
            Art.Pvp = ((articulo)b).Pvp;
            //Art.TipoArticuloID = ((articulo)articulo).TipoArticuloID;
            Art.UrlImagen = ((articulo)b).UrlImagen;

            if (neg.modificarArticulo(Art))
            {
                resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                resultado.Text = "Articulo modificado correctamente";
                aceptar.Visibility = Visibility.Collapsed;
                resultado.Visibility = Visibility.Visible;
                resultado.Focus();
            }
            else
            {
                resultado.SetResourceReference(Control.StyleProperty, "textError");
                resultado.Text = "Error al modificar el articulo";
                aceptar.Visibility = Visibility.Collapsed;
                resultado.Visibility = Visibility.Visible;
                resultado.Focus();
            }
        }

        private void resultado_SelectionChanged(object sender, RoutedEventArgs e)
        {
           
        }

        private void resultado_TouchLeave(object sender, TouchEventArgs e)
        {
            
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (resultado.Visibility == Visibility.Visible)
            //{
            //    resultado.Visibility = Visibility.Collapsed;
            //    aceptar.Visibility = Visibility.Visible;
            //}
        }

        private void listaArt_MouseEnter_1(object sender, MouseEventArgs e)
        {
           
        }

        private void datosArt_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (resultado.Visibility == Visibility.Visible)
            //{
            //    resultado.Visibility = Visibility.Collapsed;
            //    aceptar.Visibility = Visibility.Visible;
            //}
        }

        private void filtrarNombre2_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (resultado.Visibility == Visibility.Visible)
            //{
            //    resultado.Visibility = Visibility.Collapsed;
            //    aceptar.Visibility = Visibility.Visible;
            //}
        }

        private void filtrarTipo2_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (resultado.Visibility == Visibility.Visible)
            //{
            //    resultado.Visibility = Visibility.Collapsed;
            //    aceptar.Visibility = Visibility.Visible;
            //}
        }

        private void resultado_LostFocus(object sender, RoutedEventArgs e)
        {
            resultado.Visibility = Visibility.Collapsed;
            aceptar.Visibility = Visibility.Visible;
        }
    }
}
