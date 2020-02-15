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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapaEntidades;
using CapaNegocio;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private negocio neg;
        private List<usuario> usuarios;
        public int intentos;

        public MainWindow()
        {
            InitializeComponent();
            intentos = 3;
            usuarios = new List<usuario>();
            neg = new negocio();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (neg.ComprobarLogin(datoUsu.Text, pass.Password, usuarios))
            {
                FormularioPrincipal formPrincipal = new FormularioPrincipal(this, neg);
                formPrincipal.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                aceptar.Visibility = Visibility.Collapsed;
                error.Visibility = Visibility.Visible;
                errorIntentos.Visibility = Visibility.Visible;                
                intentos -= 1;
                errorIntentos.Content = "Acceso denegado....intentos restantes:  " + intentos.ToString();
                if (intentos < 1)
                {
                    this.Close();
                }
            } 
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usuarios = neg.usuarios();
       
        }

        private void datoUsu_TextChanged(object sender, TextChangedEventArgs e)
        {
         
        }

        private void pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
                
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Collapsed;
            pass.Password = "";
            pass.Focus();
            
        }

        private void pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pass.Password == "")
            {
                password.Visibility = Visibility.Visible;
            }
        }

        private void datoUsu_KeyDown(object sender, KeyEventArgs e)
        {
            aceptar.Visibility = Visibility.Visible;
            error.Visibility = Visibility.Collapsed;
            errorIntentos.Visibility = Visibility.Collapsed;
            password.Visibility = Visibility.Visible;
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
                        
        }

        private void datoUsu_GotFocus(object sender, RoutedEventArgs e)
        {
            datoUsu.Text = "";
        }

        private void pass_GotFocus(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Collapsed;
            pass.Password = "";
            
            aceptar.Visibility = Visibility.Visible;
            error.Visibility = Visibility.Collapsed;
            errorIntentos.Visibility = Visibility.Collapsed;
        }

        private void datoUsu_LostFocus(object sender, RoutedEventArgs e)
        {
            if (datoUsu.Text == "")
            {
                datoUsu.Text = "Usuario";
            }            
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
