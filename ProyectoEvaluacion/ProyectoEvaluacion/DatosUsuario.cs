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
    public partial class datosUsuario : Form
    {
        //private login padre;
        private List<usuario> usuarios;
        //private negocio neg;

        private List<usuario> ListaUsuarios { get; set; }

        // public datosUsuario(login p, negocio neg)
        public datosUsuario(negocio neg)
        {            
            usuarios = new List<usuario>();
            //padre = p;          
            
            InitializeComponent();
            Rellenar(neg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //padre.Show();            
            this.Close();
        }

        private void datosUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void datosUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void Rellenar(negocio neg)
        {
            ListaUsuarios = neg.usuarios();
            
            foreach(usuario b in ListaUsuarios)
            {
                dataGridView1.Rows.Add(b.Nombre);                
            }  

            /*
            textBox1.Text = usu.Dni;
            textBox2.Text = usu.Calle;
            textBox3.Text = usu.Telefono;
            textBox4.Text = usu.Email;
            textBox5.Text = usu.Date;*/
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = e.RowIndex;
            textBox1.Text = ListaUsuarios[n].Dni;
            textBox2.Text = ListaUsuarios[n].Calle;
            textBox3.Text = ListaUsuarios[n].Telefono;
            textBox4.Text = ListaUsuarios[n].Email;
            textBox5.Text = ListaUsuarios[n].Date.ToString();
        }
    }
}
