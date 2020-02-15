using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocio;
using System.Text.RegularExpressions;

namespace ProyectoEvaluacion
{
    public partial class ModificarUsuario : Form
    {

        private int SelectedRow { get; set; }
        private usuario Usu { get; set; }
        private negocio Neg { get; set; }
        private FormularioBusqueda formu;
        private char Modo { get; set; }
        private FormularioPrincipal fp;

        
        public ModificarUsuario(usuario usu, negocio neg, char modo, FormularioPrincipal f)
        {
            fp = f;
            Usu = new usuario();
            Neg = neg;
            //formu = f;
            Modo = modo;
            InitializeComponent();
            informar();
        }
        public void informar()
        {
            if (Modo == 'm')
            {
                detalleEmail.ReadOnly = false;
                detalleNombre.ReadOnly = false;
                detallePassWord.ReadOnly = false;
                detalleApe.ReadOnly = false;
                detalleDNI.ReadOnly = false;
                detalleTel.ReadOnly = false;
                detalleCalle.ReadOnly = false;
                detalleCP.ReadOnly = false;
                detalleNacido.ReadOnly = false;

                detalleEmail.Text = Usu.Email;
                detalleNombre.Text = Usu.Nombre;
                detallePassWord.Text = Usu.Password;
                detalleApe.Text = Usu.Apellidos;
                detalleDNI.Text = Usu.Dni;
                detalleTel.Text = Usu.Telefono;
                detalleCalle.Text = Usu.Calle;
                detalleCP.Text = Usu.Codpos;
                detalleNacido.Text = Usu.Date;
                label2.Text = "MODIFICACIÓN";
                button1.Text = "Modificar";
            }
            else
            {
                if(Modo == 'i')
                {
                    detalleEmail.ReadOnly = false;
                    detalleNombre.ReadOnly = false;
                    detallePassWord.ReadOnly = false;
                    detalleApe.ReadOnly = false;
                    detalleDNI.ReadOnly = false;
                    detalleTel.ReadOnly = false;
                    detalleCalle.ReadOnly = false;
                    detalleCP.ReadOnly = false;
                    detalleNacido.ReadOnly = false;
                    label2.Text = "INSERCIÓN";
                    button1.Text = "Crear";
                }
            }
        }

        public string codifica_MD5(string pas)
        {
            int i;
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pas));

            pas = "";
            for (i = 0; i < data.Length; i++)
            {
                pas = pas + data[i];
            }

            return pas;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string password = codifica_MD5(detallePassWord.Text);

            if (Modo == 'm')
            {
                //if (Neg.modificar(Usu.UsuarioID, detalleEmail.Text, detalleNombre.Text, 
                //    password, detalleApe.Text, detalleDNI2.Text, detalleTel.Text,
                //    detalleCalle.Text, "", detalleCP.Text, Usu.PuebloID, Usu.ProvinciaID, detalleNacido.Text))
                if(Neg.modificar(Usu))
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Usuario modificado";
                }
                else
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Error al modificar el usuario";
                }
            }
            else
                if (Modo == 'i')
            {
                if (Neg.insertar(detalleEmail.Text, detalleNombre.Text,
                    password, detalleApe.Text, detalleDNI.Text, detalleTel.Text,
                    detalleCalle.Text, "", detalleCP.Text, "2331", "46", detalleNacido.Text))
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Usuario creado";
                }
                else
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Error al crear usuario";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            //if (Modo == 'm')
            //{
            //    formu.Visible = true;
            //    formu.WindowState = FormWindowState.Maximized;
            //}

            //this.Close();
        }

        private void detallePassWord_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ModificarUsuario_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ModificarUsuario_Load(object sender, EventArgs e)
        {
            statusStrip1.Visible = false;
        }

        private void detalleEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarEmail(detalleEmail.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleEmail, "El formato del email no está correcto");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleNombre_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarNom(detalleNombre.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleNombre, "El campo no puede estar vacío");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleApe_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarApe(detalleApe.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleApe, "El campo no puede estar vacío");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleDNI2_Validating(object sender, CancelEventArgs e)
        {

        }

        private void detalleDNI_Validating(object sender, CancelEventArgs e)
        {
            string mensaje = Usu.ComprobarDni(detalleDNI.Text.ToString());
            if (mensaje != "")
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleDNI, mensaje);
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleDNI_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(detalleDNI, "");
            StringBuilder cadena;
            if (detalleDNI.Text.Length > 0)
            {
                char primeraLetra = Convert.ToChar(detalleDNI.Text[0]);
                switch (primeraLetra)
                {
                    case 'X':
                        cadena = new StringBuilder(detalleDNI.Text);
                        cadena.Replace('X', '0');
                        detalleDNI.Text = cadena.ToString();
                        detalleDNI.Select(detalleDNI.Text.Length, 0);
                        break;
                    case 'Y':
                        cadena = new StringBuilder(detalleDNI.Text);
                        cadena.Replace('Y', '1');
                        detalleDNI.Text = cadena.ToString();
                        detalleDNI.Select(detalleDNI.Text.Length, 0);
                        break;
                    case 'Z':
                        cadena = new StringBuilder(detalleDNI.Text);
                        cadena.Replace('Z', '2');
                        detalleDNI.Text = cadena.ToString();
                        detalleDNI.Select(detalleDNI.Text.Length, 0);
                        break;
                }
            }
        }

        private void detalleCP_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarCP(detalleCP.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleCP, "El campo no tiene el formato correcto");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleTel_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarTel(detalleTel.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleTel, "El campo no tiene el formato correcto");
            }else
            {
                errorProvider1.Clear();
            }
        }
    }
}
