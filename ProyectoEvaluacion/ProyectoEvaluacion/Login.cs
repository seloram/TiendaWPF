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

namespace ProyectoEvaluacion
{
    public partial class login : Form
    {
        private negocio negocio;
      //  private usuario usu;

        public login()
        {            
            InitializeComponent();
            
            negocio = new negocio();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((textBox1.Text == "admin") && (textBox2.Text == "1234"))
            {
                statusStrip1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Acceso denegado";
                FormularioPrincipal formPrincipal = new FormularioPrincipal(this, negocio);
                this.Visible = false;
                formPrincipal.Show();
            }
            else
            {
                statusStrip1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Acceso denegado";
                //MessageBox.Show("Acceso denegado");
            }

         //   datosUsuario datosUsuario = new datosUsuario(this, negocio);

            // datosUsuario.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            if (Application.OpenForms.Count == 1)
            {
                this.Close();
            }*/
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {
            statusStrip1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
