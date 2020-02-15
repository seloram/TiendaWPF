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


using System.Data;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for Insertar.xaml
    /// </summary>
    public partial class Insertar : UserControl
    {
        private bool b_dni, b_nombre, b_telefono, b_direc, b_provi, b_cp, b_apell, b_email, b_local, b_date, b_pass;
        private List<provincia> pro;
        private List<localidad> loc;
        LinearGradientBrush fondoCorrecto;
        private string ProvUsuario { get; set; }
        private string LocalUsuario { get; set; }
        private negocio neg;
        private ModificacionUsuario modificarU;
        private string Ruta { get; set; }
        bool insertado;
        private char Modo { get; set; }
        private FormularioPrincipal formu;
        private usuario Usu { get; set; }
        public Insertar(negocio n, usuario usu, char modo, ModificacionUsuario m, FormularioPrincipal f)
        {
            InitializeComponent();
          
            b_dni = false;
            b_pass = false;
            b_nombre = false;
            b_telefono = false;
            b_direc = false;
            b_provi = false;
            b_cp = false;
            b_apell = false;
            b_email = false;
            b_local = false;
            b_date = false;
            
            pro = n.provincias();
            loc = n.localidades();
            modificarU = m;
            formu = f;
            Modo = modo;
            if (usu == null)
            {
                Usu = new usuario();
            }
            else
            {
                Usu = usu;
            }            
            neg = n;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Color startColor = new Color();
            Color endColor = new Color();
            Point startPoint = new Point();
            Point endPoint = new Point();
            startPoint.X = 0.5;
            startPoint.Y = 0;
            endPoint.X = 0.52;
            endPoint.Y = 1;
            fondoCorrecto = new LinearGradientBrush(Colors.White, endColor, startPoint, endPoint);
            insertado = false;
            /* paisCombo.DataContext = neg.provincias();*/
            paisCombo.ItemsSource = pro;
            Ruta = System.IO.Directory.GetCurrentDirectory() +
                "\\..\\..\\..\\";
            mensajeError.SetResourceReference(Control.StyleProperty, "textError");
            mensajeError.Visibility = Visibility.Hidden;

            if (Modo == 'm')
            {
                informar();
            }            
            //ListBox1.DataContext = neg.provincias();
        }

        private void informar()
        {
            t_nombre.Text = Usu.Nombre;
            t_apellidos.Text = Usu.Apellidos;
            t_dni.Text = Usu.Dni;
            t_direccion.Text = Usu.Calle;
            pass.Password = Usu.Password;
            t_telefono.Text = Usu.Telefono;
              
            foreach(provincia p in pro)
            {
                if (p.ProvinciaID == Usu.ProvinciaID)
                {
                    paisCombo.Text = p.Nombre;
                }
                foreach (localidad l in loc)
                {
                    if ((l.LocalidadID == Usu.PuebloID)&&(Usu.ProvinciaID==l.ProvinciaID))
                    {
                        localCombo.Text = l.Nombre;
                    }
                }
            }
            //t_provincia.Visibility = Visibility.Collapsed;
            //t_localidad.Visibility = Visibility.Collapsed;

            codigo_postal.Text = Usu.Codpos;
            t_email.Text = Usu.Email;
            t_date.Text = Usu.Nacido;
        }

        //private void comboPro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    List<provincia> c = new List<provincia>();
        //    c.Add((provincia)comboPro.SelectedItem);
        //    string name = c[0].Nombre;

        //    pass.Text = name;


        //    string nombre = "";
        //    for (int i = 0; i < loc.Count; i++)
        //    {
        //        if (c[0].ProvinciaID == loc[i].ProvinciaID)
        //        {
        //            localCombo.Items.Add(loc[i].Nombre);
        //        }
        //    }

        //}

        private void paisCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<provincia> c = new List<provincia>();
            c.Add((provincia)paisCombo.SelectedItem);
            string name = c[0].Nombre;
            List<localidad> l = new List<localidad>();
            localCombo.ItemsSource = null;
            localCombo.Items.Clear();
            localCombo.ItemsSource = "";

            for (int i = 0; i < loc.Count; i++)
            {
                if (c[0].ProvinciaID == loc[i].ProvinciaID)
                {
                    //localCombo.Items.Add(loc[i].Nombre);
                    l.Add(loc[i]);
                }
            }
            localCombo.ItemsSource = l;
        }

        private void nombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_nombre.Text.Length == 0)
            {
                t_nombre.Text = "Nombre*";
                //mensajeError.Text = "Nombre no introducido";
                //b_nombre = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //    b_nombre = false;
        }

        //private void calle_Copy_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    t_provincia.Visibility = Visibility.Collapsed;
        //}

        private void paisCombo_MouseLeave(object sender, MouseEventArgs e)
        {
         //   t_provincia.Visibility = Visibility.Visible;
            
            //if (paisCombo.Text.Length == 0)
            //{
            //    paisCombo.Text = "Provincia*";
            //    mensajeError.Visibility = Visibility.Visible;
            //    mensajeError.Focus();
            //    mensajeError.Text = "Provincia no introducido";
            //}
            //else
            //    t_provincia.Text = paisCombo.Text;
        }

        //private void localidad_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    t_localidad.Visibility = Visibility.Collapsed;
        //}

        //private void localCombo_MouseLeave(object sender, MouseEventArgs e)
        //{

        //    t_localidad.Visibility = Visibility.Visible;
        //    if (localCombo.Text.Length == 0)
        //    {
        //        t_localidad.Text = "Localidad*";
        //    }
        //    else
        //        t_localidad.Text = localCombo.Text;
        //}

        private void t_nombre_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_telefono_MouseLeave(object sender, MouseEventArgs e)
        {
            if (t_telefono.Text.Length == 0)
            {
                t_telefono.Text = "Teléfono*";
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
                //mensajeError.Text = "Telefono no introducido";
            }
        }

        private void t_nombre_GotFocus(object sender, RoutedEventArgs e)
        {
            t_nombre.Text = "";
        }

        private void t_apellidos_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_apellidos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_apellidos.Text.Length == 0)
            {
                t_apellidos.Text = "Apellidos*";
                //mensajeError.Text = "Apellidos no introducidos";
                //b_apell = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //    b_apell = false;
        }

        private void t_telefono_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_telefono.Text.Length == 0)
            {
                t_telefono.Text = "Telefono*";
                //mensajeError.Text = "Telefono no introducido";
                //b_telefono = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //{
            //    b_telefono = false;
            //    if (!Usu.ComprobarTel(t_telefono.Text))
            //    {
            //        mensajeError.Text = "El Telefono no tiene el formato correcto";
            //        mensajeError.Visibility = Visibility.Visible;
            //        b_telefono = true;
            //        mensajeError.Focus();
            //    }
            //    else
            //        b_telefono = false;
            //}

        }

        private void t_telefono_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_direccion_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_direccion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_direccion.Text.Length == 0 || t_direccion.Text == "Dirección*")
            {
                t_direccion.Text = "Dirección*";
                //mensajeError.Text = "Dirección no introducido";
                //b_direc = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //    b_direc = false;
        }

        private void t_provincia_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_provincia_LostFocus(object sender, RoutedEventArgs e)
        {
            //t_provincia.Visibility = Visibility.Visible;
            //if (t_provincia.Text.Length == 0)
            //{
            //    t_provincia.Text = "Provincia*";
            //    mensajeError.Text = "Provincia no introducido";
            //    mensajeError.Visibility = Visibility.Visible;
            //    mensajeError.Focus();
            //}
        }

        private void t_telefono_GotFocus(object sender, RoutedEventArgs e)
        {
            t_telefono.Text = "";
        }

        private void t_direccion_GotFocus(object sender, RoutedEventArgs e)
        {
            t_direccion.Text = "";
        }

        private void t_provincia_MouseEnter(object sender, MouseEventArgs e)
        {
            //t_provincia.Visibility = Visibility.Collapsed;
        }

        private void paisCombo_MouseLeave_1(object sender, MouseEventArgs e)
        {
            //t_provincia.Visibility = Visibility.Visible;
            //if (paisCombo.Text.Length == 0)
            //{
            //    t_provincia.Text = "provincia*";
            //}
            //else
            //    t_provincia.Text = paisCombo.Text;
        }

        private void t_apellidos_GotFocus(object sender, RoutedEventArgs e)
        {
            t_apellidos.Text = "";
        }

        private void t_email_GotFocus(object sender, RoutedEventArgs e)
        {
            t_email.Text = "";
        }

        private void codigo_postal_GotFocus(object sender, RoutedEventArgs e)
        {
            codigo_postal.Text = "";
        }

        private void codigo_postal_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void codigo_postal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (codigo_postal.Text.Length == 0)
            {
                codigo_postal.Text = "CódigoPostal*";
                //mensajeError.Text = "Código Postal no introducido";
                //b_cp = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //{
            //    b_cp = false;
            //    if (!Usu.ComprobarCP(codigo_postal.Text))
            //    {
            //        mensajeError.Text = "Formato incorrecto de Código Postal";
            //        mensajeError.Visibility = Visibility.Visible;
            //        b_cp = true;
            //        mensajeError.Focus();
            //    }
            //    else
            //        b_cp = false;

            //}
        }

        private void t_email_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_email.Text.Length == 0)
            {
                t_email.Text = "Email*";
                //mensajeError.Text = "Email no introducido";
                //b_email = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //{
            //    b_email = false;
            //    if (!Usu.ComprobarEmail(t_email.Text))
            //    {
            //        mensajeError.Text = "Formato incorrecto de email";
            //        b_email = true;
            //        mensajeError.Visibility = Visibility.Visible;
            //        mensajeError.Focus();
            //    }
            //    else
            //        b_email = false;
            //}            
        }

        private void t_dni_GotFocus(object sender, RoutedEventArgs e)
        {
            t_dni.Text = "";
        }

        private void t_dni_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_dni_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_dni.Text.Length == 0)
            {
                t_dni.Text = "Dni*";
                //mensajeError.Text = "Dni no introducido";
                //b_dni = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //{
            //    b_dni = false;
            //    string mensaje = Usu.ComprobarDni(t_dni.Text);
            //    if (mensaje != "")
            //    {
            //        mensajeError.Text = mensaje;
            //        b_dni = true;
            //        mensajeError.Visibility = Visibility.Visible;
            //        mensajeError.Focus();
            //    }
            //    else
            //        b_dni = false;
            //}
        }

        //private void t_localidad_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    t_localidad.Text = "";
        //    t_localidad.Visibility = Visibility.Collapsed;
        //}

        private void t_localidad_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        //private void t_localidad_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    t_localidad.Visibility = Visibility.Visible;
        //    if (t_localidad.Text.Length == 0)
        //    {
        //        t_localidad.Text = "Localidad*";
        //        mensajeError.Text = "Localidad no introducido";
        //        mensajeError.Visibility = Visibility.Visible;
        //        mensajeError.Focus();
        //    }
        //}

        private void t_date_GotFocus(object sender, RoutedEventArgs e)
        {
            t_date.Text = "";
        }

        private void t_date_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void t_date_LostFocus(object sender, RoutedEventArgs e)
        {
            if (t_date.Text == "")
            {
                t_date.Text = "Fecha de nacimiento*";
                //mensajeError.Text = "Fecha de nacimiento no introducido";
                //b_date = true;
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
            }
            //else
            //    b_date = false;
        }

        private void localCombo_GotFocus(object sender, RoutedEventArgs e)
        {
            //mensajeError.Visibility = Visibility.Hidden;
        }

        //private void paissCombo_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    mensajeError.Visibility = Visibility.Hidden;
        //}

        private void paisCombo_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void t_provincia_GotFocus(object sender, RoutedEventArgs e)
        {
            //t_provincia.Text = "";
            //t_provincia.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Password == "" || pass.Password == "password*")
            {
                //pass.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Password no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");             
                mensajeError.Visibility = Visibility.Visible;
                b_pass = true;
                t_pass.Focus();
                return;
            }
            else
            {
                //pass.Background = fondoCorrecto;
                b_pass = false;
            }             


            if (t_nombre.Text == "" || t_nombre.Text == "Nombre*")
            {
                //t_nombre.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Nombre no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_nombre = true;
                t_nombre.Focus();
                return;
            }
            else
            {
                //t_nombre.Background = fondoCorrecto;
                b_nombre = false;
            }
                

            if (t_telefono.Text == "" || t_telefono.Text == "Teléfono*")
            {
                //t_telefono.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Teléfono no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_telefono = true;
                t_telefono.Focus();
                return;
            }
            else
            {
                b_telefono = false;
                if (!Usu.ComprobarTel(t_telefono.Text))
                {
                    //t_telefono.Background = new SolidColorBrush(Colors.IndianRed);
                    mensajeError.Text = "El Telefono no tiene el formato correcto";
                    mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                    mensajeError.Visibility = Visibility.Visible;
                    b_telefono = true;
                    t_telefono.Focus();
                    return;
                }
                else
                {
                    //t_telefono.Background = fondoCorrecto;
                    b_telefono = false;
                }
                    
            }
                

            if (t_direccion.Text == "" || t_direccion.Text == "Dirección*")
            {
                //t_direccion.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Dirección no introducida";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_direc = true;
                t_direccion.Focus();
                return;
            }
            else
            {
                //t_direccion.Background = fondoCorrecto;
                b_direc = false;
            }
                

            if (paisCombo.Text == "" || paisCombo.Text == "Provincia*")
            {
                //paisCombo.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Provincia no introducida";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_provi = true;
                paisCombo.Focus();
                return;
            }
            else
            {
                //paisCombo.Background = fondoCorrecto;
                b_provi = false;
            }
                

            if (codigo_postal.Text.Length == 0 || codigo_postal.Text == "CódigoPostal*")
            {
                //codigo_postal.Background = new SolidColorBrush(Colors.IndianRed);
                codigo_postal.Text = "CódigoPostal*";
                mensajeError.Text = "Código Postal no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_cp = true;
                codigo_postal.Focus();
                return;
            }
                
            else
            {
                b_cp = false;
                if (!Usu.ComprobarCP(codigo_postal.Text))
                {
                    //codigo_postal.Background = new SolidColorBrush(Colors.IndianRed);
                    mensajeError.Text = "Formato incorrecto de Código Postal";
                    mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                    mensajeError.Visibility = Visibility.Visible;
                    b_cp = true;
                    codigo_postal.Focus();
                    return;
                }
                else
                {
                    //codigo_postal.Background = fondoCorrecto;
                    b_cp = false;
                }
                    
            }

            if (t_apellidos.Text == "" || t_apellidos.Text == "Apellidos*")
            {
                //t_apellidos.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Apellidos no introducidos";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_apell = true;
                t_apellidos.Focus();
                return;
            }
            else
            {
                //t_apellidos.Background = fondoCorrecto;
                b_apell = false;
            }
                


            if (t_email.Text == "" || t_email.Text == "email*")
            {
                //t_email.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Email no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_email = true;
                t_email.Focus();
                return;
            }else
            {
                b_email = false;
                if (!Usu.ComprobarEmail(t_email.Text))
                {
                    //t_email.Background = new SolidColorBrush(Colors.IndianRed);
                    mensajeError.Text = "Formato incorrecto de email";
                    b_email = true;
                    mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                    mensajeError.Visibility = Visibility.Visible;
                    b_email = true;
                    t_email.Focus();
                    return;
                }
                else
                {
                    //t_email.Background = fondoCorrecto;
                    b_email = false;
                }
                    
            }


            if (t_dni.Text.Length == 0 || t_dni.Text == "Dni*")
            {       
                //t_dni.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Dni no introducido";             
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_dni = true;
                t_dni.Focus();
                return;
            }
            else
            {
                b_dni = false;
                string mensaje = Usu.ComprobarDni(t_dni.Text);
                if (mensaje != "")
                {
                    //t_dni.Background = new SolidColorBrush(Colors.IndianRed);
                    mensajeError.Text = mensaje;
                    mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                    mensajeError.Visibility = Visibility.Visible;
                    b_dni = true;
                    t_dni.Focus();
                    return;
                }
                else
                {
                    //t_dni.Background = fondoCorrecto;
                    b_dni = false;
                }
                    
            }

            if (localCombo.Text == "" || localCombo.Text == "Localidad*")
            {
                //localCombo.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Localidad no introducida";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_local = true;
                //var textbox = (TextBox)localCombo.Template.FindName("togglesbotones", localCombo);
                //if (textbox != null)
                //{
                //    var parent = (Border)textbox.Parent;
                //    parent.Background = fondoCorrecto;
                //}
                //localCombo.Background = fondoCorrecto;
                localCombo.Focus();
                return;
            }
            else
            {
                
                //localCombo.Background = fondoCorrecto;
                b_local = false;
            }

            

            if (t_date.Text == "" || t_date.Text == "Fecha de nacimiento*")
            {
                //t_date.Background = new SolidColorBrush(Colors.IndianRed);
                mensajeError.Text = "Fecha de nacimiento no introducido";
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Visible;
                b_date = true;
                t_date.Focus();
                return;
            }
            else
            {
                //t_date.Background = fondoCorrecto;
                b_date = false;
            }
                
            
            if ((!b_dni) && (!b_nombre) && (!b_telefono) && (!b_direc) && (!b_provi) && (!b_cp) && (!b_apell) 
                && (!b_email) && (!b_local) && (!b_date) && (!b_pass))
            {
                try
                {
                    string password = neg.Codifica_MD5(t_pass.Text);
                    var fecha = Convert.ToDateTime(t_date.Text).ToString("yyyy-MM-dd");

                    Usu.Nombre = t_nombre.Text;
                    Usu.Apellidos = t_apellidos.Text;
                    Usu.Dni = t_dni.Text;
                    Usu.Email = t_email.Text;
                    if (password != "password")
                    {
                        Usu.Password = password;
                    }

                    Usu.Codpos = codigo_postal.Text;
                    Usu.Nacido = fecha;
                    Usu.ProvinciaID = ((provincia)paisCombo.SelectedItem).ProvinciaID;

                    foreach (localidad l in loc)
                    {
                        if ((Usu.ProvinciaID == l.ProvinciaID) && (localCombo.Text == l.Nombre))
                        {
                            Usu.PuebloID = l.LocalidadID;
                        }
                    }
                    //    string kk = ((localidad)localCombo.SelectedItem).ToString();
                    //    Usu.PuebloID = ((localidad)localCombo.SelectedItem).LocalidadID;
                    Usu.Telefono = t_telefono.Text;
                    Usu.Calle = t_direccion.Text;
                    Usu.Calle2 = Usu.Calle2;

                    if (Modo == 'm')
                    {
                        if (neg.modificar(Usu))
                        {
                            insertado = true;
                            modificarU.Visibility = Visibility.Visible;
                            modificarU.resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                            modificarU.resultado.Text = "Usuario modificado";
                            modificarU.renovar();
                            modificarU.resultado.Visibility = Visibility.Visible;
                            modificarU.resultado.Focus();
                            formu.panelPrincipal.Children.Remove(this);

                        }
                        else
                        {
                            modificarU.resultado.SetResourceReference(Control.StyleProperty, "textError");
                            mensajeError.Text = "Error al modificar el usuario";
                            mensajeError.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        if (neg.insertar(t_email.Text, password, t_nombre.Text,
                        t_apellidos.Text, t_dni.Text, t_telefono.Text,
                        t_direccion.Text, null, codigo_postal.Text, /*t_localidad.Text*/
                        ((localidad)localCombo.SelectedItem).LocalidadID.ToString(), ((provincia)paisCombo.SelectedItem).ProvinciaID/*t_provincia.Text*/,
                        /*t_date.Text.ToString()*/ fecha.ToString()))

                        //if (neg.insertar("sssse@gmail.com", password, "sergio",
                        //    "ffefef", "52778467E", "653830429",
                        //    "aañelfi", null, "03006", /*t_localidad.Text*/
                        //    "0149", "03", "2020-01-01"))

                        {
                            insertado = true;
                            mensajeError.SetResourceReference(Control.StyleProperty, "textCorrecto");
                            mensajeError.Text = "Usuario introducido correctamente";
                            mensajeError.Visibility = Visibility.Visible;
                            mensajeError.Focus();
                        }
                        else
                        {
                            mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                            mensajeError.Text = "Error al insertar el usuario";
                            mensajeError.Visibility = Visibility.Visible;
                            mensajeError.Focus();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void t_pass_MouseEnter(object sender, MouseEventArgs e)
        {
            t_pass.Visibility = Visibility.Collapsed;
        }

        private void t_dni_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringBuilder cadena;
            if (t_dni.Text.Length > 0)
            {
                char primeraLetra = Convert.ToChar(t_dni.Text[0]);
                switch (primeraLetra)
                {
                    case 'X':
                        cadena = new StringBuilder(t_dni.Text);
                        cadena.Replace('X', '0');
                        t_dni.Text = cadena.ToString();
                        t_dni.Select(t_dni.Text.Length, 0);
                        break;
                    case 'Y':
                        cadena = new StringBuilder(t_dni.Text);
                        cadena.Replace('Y', '1');
                        t_dni.Text = cadena.ToString();
                        t_dni.Select(t_dni.Text.Length, 0);
                        break;
                    case 'Z':
                        cadena = new StringBuilder(t_dni.Text);
                        cadena.Replace('Z', '2');
                        t_dni.Text = cadena.ToString();
                        t_dni.Select(t_dni.Text.Length, 0);
                        break;
                }
            }
        }

        private void mensajeError_LostFocus(object sender, RoutedEventArgs e)
        {
            if (insertado)
            {
                mensajeError.SetResourceReference(Control.StyleProperty, "textError");
                mensajeError.Visibility = Visibility.Hidden;
                insertado = false;
            }            
        }

        private void t_date_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void paisCombo_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
          
        }

        private void t_pass_GotFocus(object sender, RoutedEventArgs e)
        {
            t_pass.Visibility = Visibility.Collapsed;
            pass.Focus();
        }

        private void t_pass_MouseLeave(object sender, MouseEventArgs e)
        {
          
        }

        private void pass_MouseLeave(object sender, MouseEventArgs e)
        {
            if (pass.Password.Length == 0)
            {
                t_pass.Visibility = Visibility.Visible;
            }
        }

        private void localCombo_LostFocus(object sender, RoutedEventArgs e)
        {
            //t_localidad.Visibility = Visibility.Visible;
            if (localCombo.Text.Length == 0)
            {
                localCombo.Text = "Localidad*";
                //mensajeError.Visibility = Visibility.Visible;
                //mensajeError.Focus();
                //mensajeError.Text = "Localidad no introducida";
            }
            //else
            //    t_localidad.Text = localCombo.Text;
        }

        private void paisCombo_LostFocus(object sender, RoutedEventArgs e)
        {
            //t_provincia.Visibility = Visibility.Visible;

            if (paisCombo.Text.Length == 0)
            {
                paisCombo.Text = "Provincia*";
                //mensajeError.Visibility = Visibility.Visible;
                //b_provi = true;
                //mensajeError.Focus();
                //mensajeError.Text = "Provincia no introducido";
            }
            //else
            //    b_provi = false;
            //else
            //    t_provincia.Text = paisCombo.Text;
        }

        private void pass_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (pass.Password.Length == 0)
            {
                t_pass.Visibility = Visibility.Visible;
                //    mensajeError.Text = "Password no introducido";
                //    b_pass = true;
                //    mensajeError.Visibility = Visibility.Visible;
                //    mensajeError.Focus();
            }
            //else
            //    b_pass = false;
        }

        private void t_pass_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void pass_KeyDown(object sender, KeyEventArgs e)
        {
            mensajeError.Visibility = Visibility.Hidden;
        }

        private void localCombo_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (localCombo.Text.Length == 0 || localCombo.Text==null)
            {
                localCombo.Text = "Localidad*";
                //mensajeError.Visibility = Visibility.Visible;
                //b_local = true;
                //mensajeError.Focus();
                //mensajeError.Text = "Localidad no introducida";
            }
            //else
            //    b_local = false;
        }

        private void localCombo_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void paisCombo_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (paisCombo.Text.Length == 0)
        //    {                
        //        mensajeError.Text = "Provincia no introducida";
        //        mensajeError.Visibility = Visibility.Visible;
        //        mensajeError.Focus();
        //    }
        //}

        //private void localCombo_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (localCombo.Text.Length == 0)
        //    {
        //        mensajeError.Text = "Localidad no introducida";
        //        mensajeError.Visibility = Visibility.Visible;
        //        mensajeError.Focus();
        //    }
        //}
    }
}
