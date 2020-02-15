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
    public partial class FormularioPrincipal : Form
    {
        
        private login log;
        private negocio neg;

        public FormularioPrincipal(login l, negocio n)
        {
            log = l;
            neg = n;
            InitializeComponent();            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioBusqueda busqueda = new FormularioBusqueda(neg, 'm', this, null);
            busqueda.MdiParent = this;
            busqueda.Show();            

            //datosUsuario datosUsuario = new datosUsuario(neg);
            //datosUsuario.MdiParent = this;
            //datosUsuario.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea salir?","Cerrando aplicación...",MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) 
                == System.Windows.Forms.DialogResult.Yes)
            {
                log.Visible = true;
                this.Close();
            }                   
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ModificarUsuario modificar = new ModificarUsuario(null, neg, 'i', null);
            modificar.MdiParent = this;
            modificar.WindowState = FormWindowState.Maximized;            
            modificar.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioBusqueda eliminar = new FormularioBusqueda(neg, 'e', null, null);
            eliminar.MdiParent = this;
            eliminar.WindowState = FormWindowState.Maximized;
            eliminar.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosProductos productos = new DatosProductos(neg);
            productos.MdiParent = this;
            productos.WindowState = FormWindowState.Maximized;
            productos.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoPedido nuevoP = new NuevoPedido(neg, this, null);
            nuevoP.MdiParent = this;
            nuevoP.WindowState = FormWindowState.Maximized;
            nuevoP.Show();
        }
    }
}
