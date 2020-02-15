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
using System.Collections.ObjectModel;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for ModificacionUsuario.xaml
    /// </summary>
    public partial class ModificacionUsuario : UserControl
    {
        private ObservableCollection<usuario> listaU;
        private CollectionViewSource listaFiltrada = new CollectionViewSource();
        private List<usuario> usu;
        private List<localidad> loc;
        private List<provincia> provincias;
        private List<pedido> listaPedidos;
        private FormularioPrincipal main;
        private CMPedidos cmpedidos;

        public char modificado;
        private usuario Usu { get; set; }
        private negocio neg;
        public ModificacionUsuario(CMPedidos cm, FormularioPrincipal m, negocio n, char res)
        {
            InitializeComponent();
            neg = n;
            cmpedidos = cm;
            listaPedidos = neg.pedidos();
            modificado = res;
            main = m;
            usu = neg.usuarios();
            loc = neg.localidades();
            provincias = neg.provincias();
            Usu = new usuario();
            listaFiltrada = (System.Windows.Data.CollectionViewSource)this.Resources["listaUsuarios"];
            listaU = new ObservableCollection<usuario>();
        }

        private void ventanaModUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            //data.ItemsSource = "";
            //data.ItemsSource = usu;
            if (modificado == 's')
            {
                resultado.Text = "Usuario modificado correctamente";
                resultado.Visibility = Visibility.Visible;
            }else
                resultado.Visibility = Visibility.Hidden;

          

            foreach (usuario u in usu)
            {
                listaU.Add(u);
            }

            listaFiltrada.Source = listaU;
        }

        private void Filtrar(object sender, FilterEventArgs e)
        {
            usuario usu = (usuario)e.Item;

            if (usu != null)
            {
                if ((usu.Nombre.Contains(filtrarNombre.Text) 
                    && (usu.Dni.Contains(filtrarDni.Text)))
                    && (usu.Apellidos.Contains(filtrarApellidos.Text))
                    && (usu.Email.Contains(filtrarEmail.Text)))               
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void filtrarNombre_KeyDown(object sender, KeyEventArgs e)
        {
            //listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        public void renovar()
        {
        
                    listaU.Clear();
                    
                    usu = neg.usuarios();
                    foreach (usuario u in usu)
                    {
                        listaU.Add(u);
                    }
            datosUsuario.Children.Clear();
       
        }

        private void filtrarDni_KeyDown(object sender, KeyEventArgs e)
        {
            //listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void filtrarApellidos_KeyDown(object sender, KeyEventArgs e)
        {
            //listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void modificar(object sender, RoutedEventArgs e)
        {
            var usuario = data.SelectedCells[0].Item;
            var fecha = Convert.ToDateTime(((usuario)usuario).Nacido).ToString("yyyy-MM-dd");
            Usu.UsuarioID = ((usuario)usuario).UsuarioID;
            Usu.Nombre = ((usuario)usuario).Nombre;
            Usu.Apellidos = ((usuario)usuario).Apellidos;
            Usu.Dni = ((usuario)usuario).Dni;
            Usu.Email = ((usuario)usuario).Email;
            Usu.Password = ((usuario)usuario).Password;
            Usu.Codpos = ((usuario)usuario).Codpos;
            Usu.Nacido = fecha;
            Usu.ProvinciaID = ((usuario)usuario).ProvinciaID;
            Usu.PuebloID = ((usuario)usuario).PuebloID;
            Usu.Telefono = ((usuario)usuario).Telefono;
            Usu.Calle = ((usuario)usuario).Calle;
            Usu.Calle2 = ((usuario)usuario).Calle2;

            Insertar insertar = new Insertar(neg, Usu, 'm', this, main);
            main.panelPrincipal.Children.Add(insertar);
            insertar.titulo.Content = "MODIFICACION DE USUARIO";
            Visibility = Visibility.Collapsed;
            //main.panelPrincipal.Children[0].Visibility = Visibility.Collapsed;

            //if (neg.modificar(Usu))
            //{
            //    resultado.SetResourceReference(Control.StyleProperty, "textAceptar");
            //    resultado.Text = "Usuario modificado correctamente";
            //    resultado.Visibility = Visibility.Visible;
            //    resultado.Focus();
            //}
            //else
            //{
            //    resultado.SetResourceReference(Control.StyleProperty, "textError");
            //    resultado.Text = "Error al modificar el usuario";
            //    resultado.Visibility = Visibility.Visible;
            //    resultado.Focus();
            //}
        }        

        private void resultado_LostFocus(object sender, RoutedEventArgs e)
        {
            resultado.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void eliminar(object sender, RoutedEventArgs e)
        {
            bool encontrado = false;
            try
            {
                var usuario = data.SelectedCells[0].Item;
                Usu.UsuarioID = ((usuario)usuario).UsuarioID;

                foreach(pedido p in listaPedidos)
                {
                    if (p.UsuarioID == Usu.UsuarioID)
                    {
                        encontrado = true;
                    }
                }
                if (!encontrado)
                {
                    if (neg.eliminar(Usu.UsuarioID.ToString()))
                    {
                        resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                        resultado.Text = "Usuario eliminado correctamente";
                        resultado.Visibility = Visibility.Visible;
                        resultado.Focus();
                        for (int i = 0; i < listaU.Count; i++)
                        {
                            if (listaU[i].UsuarioID == Usu.UsuarioID)
                            {
                                listaU.Remove(listaU[i]);
                            }
                        }
                    }
                    else
                    {
                        resultado.SetResourceReference(Control.StyleProperty, "textError");
                        resultado.Text = "Error al eliminar el usuario";
                        resultado.Visibility = Visibility.Visible;
                        resultado.Focus();
                    }
                }
                else
                {
                    resultado.SetResourceReference(Control.StyleProperty, "textError");
                    resultado.Text = "Usuario con pedidos";
                    resultado.Visibility = Visibility.Visible;
                    resultado.Focus();
                }                
            }
            catch { }
            datosUsuario.Children.Clear();
        }

        private void filtrarNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarDni_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarApellidos_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarNombre.Text == "")
            {
                filtrarNombreCapa.Visibility = Visibility.Visible;
            }
        }

        private void filtrarDni_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarDni.Text == "")
            {
                filtrarDniCapa.Visibility = Visibility.Visible;
            }
        }

        private void filtrarApellidos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarApellidos.Text == "")
            {
                filtrarApellidosCapa.Visibility = Visibility.Visible;
            }            
        }

        private void filtrarEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarEmail.Text == "")
            {
                filtrarEmailCapa.Visibility = Visibility.Visible;
            }            
        }

        private void filtrarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);            
        }

        private void filtrarDni_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void filtrarApellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void filtrarEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void filtrarEmail_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void data_GotFocus(object sender, RoutedEventArgs e)
        {
        //    var usuario = data.SelectedCells[0].Item;
        //    datosUsuario.Children.Clear();
        //    if (((usuario)usuario).Telefono != null)
        //    {
        //        TextBox telefono = new TextBox();
        //        telefono.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        telefono.Margin = new Thickness(10);
        //        telefono.Text = ((usuario)usuario).Telefono;
        //        telefono.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(telefono);
        //    }

        //    if (((usuario)usuario).Calle != null)
        //    {
        //        TextBox calle = new TextBox();
        //        calle.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        calle.Margin = new Thickness(10);
        //        calle.Text = ((usuario)usuario).Calle;
        //        calle.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(calle);
        //    }

        //    if (((usuario)usuario).Codpos != null)
        //    {
        //        TextBox codpos = new TextBox();
        //        codpos.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        codpos.Margin = new Thickness(10);
        //        codpos.Text = ((usuario)usuario).Codpos;
        //        codpos.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(codpos);
        //    }

        //    if (((usuario)usuario).Telefono != null)
        //    {
        //        TextBox localidad = new TextBox();
        //        localidad.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        localidad.Margin = new Thickness(10);
        //        string codigoPueblo = ((usuario)usuario).PuebloID;
        //        foreach(localidad l in loc)
        //        {
        //            if (codigoPueblo == l.LocalidadID)
        //            {
        //                localidad.Text = l.Nombre;
        //            }
        //        }
        //        localidad.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(localidad);
        //    }

        //    if (((usuario)usuario).Telefono != null)
        //    {
        //        TextBox t_provincia = new TextBox();
        //        t_provincia.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        t_provincia.Margin = new Thickness(10);
        //        string prov = ((usuario)usuario).ProvinciaID;
        //        foreach(provincia p in provincias)
        //        {
        //            if (prov == p.ProvinciaID)
        //            {
        //                t_provincia.Text = p.Nombre;
        //            }
        //        }
        //        t_provincia.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(t_provincia);
        //    }

        //    if (((usuario)usuario).Nacido != null)
        //    {
        //        TextBox nacido = new TextBox();
        //        nacido.SetResourceReference(StyleProperty, "t_inser_usu_azul");
        //        nacido.Margin = new Thickness(10);
        //        nacido.Text = ((usuario)usuario).Nacido;
        //        nacido.HorizontalAlignment = HorizontalAlignment.Left;
        //        datosUsuario.Children.Add(nacido);
        //    }
        }

        private void data_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            try
                {
                    var usuario = data.SelectedCells[0].Item;
                    datosUsuario.Children.Clear();

                if (modificado == 'b')
                {                  
                    cmpedidos.Visibility = Visibility.Visible;
                    cmpedidos.buscarUsuario.Text = ((usuario)usuario).Nombre;
                    cmpedidos.apellidoUsu.Visibility = Visibility.Visible;
                    cmpedidos.apellidoUsu.Text = ((usuario)usuario).Apellidos;
                    cmpedidos.Usu.UsuarioID = Convert.ToInt32(((usuario)usuario).UsuarioID);
                    main.panelPrincipal.Children.Remove(this);

                }
                if (((usuario)usuario).Telefono != null)
                    {
                        TextBox telefono = new TextBox();
                        telefono.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                        telefono.Margin = new Thickness(10);
                        telefono.Text = ((usuario)usuario).Telefono;
                        telefono.HorizontalAlignment = HorizontalAlignment.Left;
                        datosUsuario.Children.Add(telefono);
                    }

                    if (((usuario)usuario).Calle != null)
                    {
                        TextBox calle = new TextBox();
                        calle.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                        calle.Margin = new Thickness(10);
                        calle.Text = ((usuario)usuario).Calle;
                        calle.HorizontalAlignment = HorizontalAlignment.Left;
                        datosUsuario.Children.Add(calle);
                    }

                    if (((usuario)usuario).Codpos != null)
                    {
                        TextBox codpos = new TextBox();
                        codpos.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                        codpos.Margin = new Thickness(10);
                        codpos.Text = ((usuario)usuario).Codpos;
                        codpos.HorizontalAlignment = HorizontalAlignment.Left;
                        datosUsuario.Children.Add(codpos);
                    }

                    if (((usuario)usuario).ProvinciaID != null)
                    {

                        TextBox t_provincia = new TextBox();
                        t_provincia.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                        t_provincia.Margin = new Thickness(10);
                        string prov = ((usuario)usuario).ProvinciaID;
                        foreach (provincia p in provincias)
                        {
                            if (prov == p.ProvinciaID)
                            {
                                t_provincia.Text = p.Nombre;
                                TextBox localidad = new TextBox();
                                localidad.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                                localidad.Margin = new Thickness(10);
                                string codigoPueblo = ((usuario)usuario).PuebloID;
                                foreach (localidad l in loc)
                                {
                                    if ((codigoPueblo == l.LocalidadID) && (prov == l.ProvinciaID))
                                    {
                                        localidad.Text = l.Nombre;
                                    }
                                }
                                localidad.HorizontalAlignment = HorizontalAlignment.Left;
                                datosUsuario.Children.Add(localidad);
                            }
                        }
                        t_provincia.HorizontalAlignment = HorizontalAlignment.Left;
                        datosUsuario.Children.Add(t_provincia);

                        //ComboBox paisCombo = new ComboBox();
                        //paisCombo.ItemsSource = provincias;
                        //paisCombo.DisplayMemberPath = "Nombre";
                        //paisCombo.SelectedValuePath = "Nombre";
                        //paisCombo.Text = t_provincia.Text;

                        //paisCombo.HorizontalAlignment = HorizontalAlignment.Left;
                        //datosUsuario.Children.Add(paisCombo);
                    }
                    if (((usuario)usuario).Nacido != null)
                    {
                        TextBox nacido = new TextBox();
                        nacido.SetResourceReference(StyleProperty, "t_inser_usu_azul");
                        nacido.Margin = new Thickness(10);
                        nacido.Text = ((usuario)usuario).Nacido;
                        nacido.HorizontalAlignment = HorizontalAlignment.Left;
                        datosUsuario.Children.Add(nacido);
                    }
                }
                catch
                {

                }
        }

        private void filtrarNombreCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            //filtrarNombreCapa.Visibility = Visibility.Collapsed;
            //filtrarNombre.Focus();
        }

        private void filtrarDniCapa_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void filtrarApellidosCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarEmailCapa_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void filtrarNombreCapa_MouseEnter(object sender, MouseEventArgs e)
        {
            filtrarNombreCapa.Visibility = Visibility.Collapsed;
            filtrarNombre.Focus();
        }

        private void filtrarDniCapa_MouseEnter(object sender, MouseEventArgs e)
        {
            filtrarDniCapa.Visibility = Visibility.Collapsed;
            filtrarDni.Focus();
        }

        private void filtrarApellidosCapa_MouseEnter(object sender, MouseEventArgs e)
        {
            filtrarApellidosCapa.Visibility = Visibility.Collapsed;
            filtrarApellidos.Focus();
        }

        private void filtrarEmailCapa_MouseEnter(object sender, MouseEventArgs e)
        {
            filtrarEmailCapa.Visibility = Visibility.Collapsed;
            filtrarEmail.Focus();
        }

        private void data_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void data_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          
        }
    }
}
