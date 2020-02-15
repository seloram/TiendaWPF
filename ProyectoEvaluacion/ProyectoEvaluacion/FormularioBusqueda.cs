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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

namespace ProyectoEvaluacion
{
    public partial class FormularioBusqueda : Form
    {
        private List<usuario> usuarios;
        private List<provincia> provincias;
        private List<localidad> localidades;
        private List<pedido> pedidos;
        private DataTable tabla;
        private DataSet ds;
        private DataView dv;
        private negocio neg;        
        private usuario Usu { get; set; }
        private provincia Prov { get; set; }
        private char Modo { get; set; }
        FormularioPrincipal formu;
        NuevoPedido nuevo;

        public int SelectedRow { get; set; }

        public FormularioBusqueda(negocio n, char modo, FormularioPrincipal f, NuevoPedido nue)
        {
            formu = f;
            nuevo = nue;
            Modo = modo;
            tabla = new DataTable();
            ds = new DataSet();
            usuarios = new List<usuario>();
            provincias = new List<provincia>();
            localidades = new List<localidad>();
            pedidos = new List<pedido>();
            neg = n;
            InitializeComponent();
        }

        private void FormularioBusqueda_Load(object sender, EventArgs e)
        {            
            if (Modo == 'e')
            {
                button1.Visible = false;
            }

            if (Modo == 'p')
            {
                button1.Visible = false;
            }

            statusStrip1.Visible = false;

            usuarios = neg.usuarios();
            provincias = neg.provincias();
            localidades = neg.localidades();
            pedidos = neg.pedidos();

            tabla.Columns.Add("Id");
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Apellidos");
            tabla.Columns.Add("Dni");
            tabla.Columns.Add("Email");            

            foreach (usuario u in usuarios)
            {
                DataRow row = tabla.NewRow();
                row["Id"] = u.UsuarioID;
                row["nombre"] = u.Nombre;
                row["Apellidos"] = u.Apellidos;
                row["Dni"] = u.Dni;
                row["Email"] = u.Email;
                tabla.Rows.Add(row);
            }

            dv = new DataView(tabla);
            dataGridView1.DataSource = dv;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //dataGridView1.Columns[1].Width= ;
            dv[0].Delete();

            if (Modo == 'b')
            {
                button1.Text = "Seleccionar";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != "")
            {
                Filtros();
            }
            else
                dv.RowFilter = String.Format("Nombre like '%{0}%'", nombre.Text);
        }

        private void apellidos_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != null)
            {
                Filtros();
            }
            else
                dv.RowFilter = String.Format("Apellidos like '%{0}%'", apellidos.Text);
        }

        private void email_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != "")
            {
                Filtros();
            }
            else
                dv.RowFilter = String.Format("Email like '%{0}%'", email.Text);
        }

        private void dni_TextChanged(object sender, EventArgs e)
        {
            if (dv.RowFilter != "")
            {
                Filtros();
            }
            else
                dv.RowFilter = String.Format("dni like '%{0}%'", dni.Text);
        }

        public void Filtros()
        {
            dv.RowFilter = String.Format("Nombre like '%{0}%' AND Apellidos like " +
            "'%{1}%' AND Email like '%{2}%' AND Dni like '%{3}%'",
            nombre.Text, apellidos.Text, email.Text, dni.Text);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private string ListaProvincias(string id)
        {
            string ciudadUsuario = "";
            foreach(provincia p in provincias)
            {
                comboProv.Items.Add(p.Nombre);
                if (id == p.ProvinciaID)
                {
                    ciudadUsuario = p.Nombre;
                }      
            }
            return ciudadUsuario;
        }

        private string ListaLocalidades(string id)
        {
            string local = "";
            string prov = "";
            if (Usu.ProvinciaID != "")
            {
                detalleNacido.Text = "no encontrado";
                string provinc = Usu.ProvinciaID;
                foreach (localidad l in localidades)
                {
                    if (l.ProvinciaID.CompareTo(Usu.ProvinciaID)==0)
                    {
                        detalleNacido.Text = "encontrado";
                        foreach (provincia a in provincias)
                        {
                            if (l.ProvinciaID == a.ProvinciaID)
                            {
                                prov = a.Nombre;
                                comboLocal.Items.Add(l.Nombre + "/" + prov);
                            }
                        }
                        //comboLocal.Items.Add(l.Nombre+"/"+prov);
                        if (id == l.LocalidadID)
                        {
                            local = l.Nombre + "/" + prov;
                        }
                    }
                }
            }            
            return local;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            int n = e.RowIndex+1;
            SelectedRow=n;
            statusStrip1.Visible = false;
            SelectedRow = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            foreach (usuario u in usuarios)
            {
                if (u.UsuarioID == SelectedRow)
                {
                    Usu = u;
                    detalleNombre.Text = u.Nombre;                    
                    detalleApe.Text = u.Apellidos;
                    detalleEmail.Text = u.Email;
                    detalleDNI.Text = u.Dni;
                    detalleTel.Text = u.Telefono;
                    detalleCalle.Text = u.Calle;
                    detalleCalle2.Text = u.Calle2;
                    detalleProvID.Text = u.ProvinciaID;
                    comboProv.SelectedItem = ListaProvincias(u.ProvinciaID);
                    comboLocal.SelectedItem = ListaLocalidades(u.PuebloID);
                    detallePuebloID.Text = u.PuebloID;                    
                    detalleCP.Text = u.Codpos;


                    //detalleNacido.Text = u.Date;
                    //IDictionary diccionario = new Dictionary<int, string>();
                    //List<string> p = new List<string>();
                    //var h = new Dictionary<int, localidad>();
                    //IEnumerable<localidad> listaordenada = localidades.OrderBy(x => x.LocalidadID);
                    //int cont = 0;
                    //var enumerator = diccionario.GetEnumerator();

                    //while (enumerator.MoveNext())
                    //{
                    //    var pair = enumerator.Current;
                    //    comboProv.Items.Add(pair.ToString());
                    //}

                    //foreach (localidad l in localidades)
                    //{
                    //    cont++;
                    //    h.Add(cont, l);
                    //}

                    //var lista = (from d in localidades
                    //             where d.ProvinciaID == u.ProvinciaID
                    //             select d.Nombre).ToList();

                    //foreach(string nom in lista)
                    //{
                    //    comboLocal.Items.Add(nom);
                    //}

                }
            }

            if (Modo == 'e')
            {
                if (ComprobarPedido()) 
                {
                    if (MessageBox.Show("¿Está seguro de querer eliminar al usuario?", "Eliminar usuario...", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (usuario u in usuarios)
                        {
                            if (u.UsuarioID == SelectedRow)
                            {
                                if (neg.eliminar(u.UsuarioID.ToString()))
                                {
                                    statusStrip1.Visible = true;
                                    toolStripStatusLabel1.Visible = true;
                                    toolStripStatusLabel1.Text = "Usuario eliminado";
                                }
                                else
                                {
                                    statusStrip1.Visible = true;
                                    toolStripStatusLabel1.Visible = true;
                                    toolStripStatusLabel1.Text = "Error al eliminar el usuario";
                                }
                            }
                        }
                    }
                }
                else
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Usuario con pedidos";
                }
            }
        }

        private bool ComprobarPedido()
        {
            foreach(pedido p in pedidos)
            {
                if (p.UsuarioID == Usu.UsuarioID)
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Modificar")
            {
                dataGridView1.Enabled = false;
                statusStrip1.Visible = false;
                button1.Text = "Aceptar";
                detalleEmail.ReadOnly = false;
                detalleNombre.ReadOnly = false;
                detalleApe.ReadOnly = false;
                comboProv.Enabled = true;
                comboLocal.Enabled = true;
                detalleDNI.ReadOnly = false;
                detalleTel.ReadOnly = false;
                detalleCalle.ReadOnly = false;
                detalleCP.ReadOnly = false;
                detalleNacido.ReadOnly = false;
            }
            else
            {
                if (button1.Text == "Aceptar")
                {

                    dataGridView1.Enabled = true;
                    button1.Text = "Modificar";
                    detalleEmail.ReadOnly = true;
                    detalleNombre.ReadOnly = true;
                    detalleApe.ReadOnly = true;
                    comboProv.Enabled = false;
                    comboLocal.Enabled = false;
                    detalleDNI.ReadOnly = true;
                    detalleTel.ReadOnly = true;
                    detalleCalle.ReadOnly = true;
                    detalleCP.ReadOnly = true;
                    detalleNacido.ReadOnly = true;
                    Usu = new usuario(Usu.UsuarioID, detalleEmail.Text, detalleNombre.Text, Usu.Password, 
                        detalleApe.Text, detalleDNI.Text, detalleTel.Text, detalleCalle.Text, detalleCalle2.Text, 
                        detalleCP.Text, Usu.PuebloID, Usu.ProvinciaID, detalleNacido.Text);
                    ModificarUsuario(Usu);
                }
                else
                {
                    if (button1.Text == "Seleccionar")
                    {
                        NuevoPedido nuevoPedido = new NuevoPedido(neg, formu, Usu);
                        nuevoPedido.MdiParent = formu;
                        nuevoPedido.Show();
                        nuevoPedido.WindowState = FormWindowState.Maximized;
                        this.Close();
                    }
                }
            }
        }

        private void ModificarUsuario(usuario usu)
        {
            //if (neg.modificar(usu.UsuarioID, detalleEmail.Text, detalleNombre.Text,
            //usu.Password, detalleApe.Text, detalleDNI.Text, detalleTel.Text,
            //detalleCalle.Text, "", detalleCP.Text, usu.PuebloID, Usu.ProvinciaID, detalleNacido.Text))
            if(neg.modificar(Usu))
            {
                statusStrip1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Usuario modificado";
            }
            else
            {
                statusStrip1.Visible = true;
                toolStripStatusLabel1.Text = "Error al modificar el usuario";
            }
            detalleEmail.ReadOnly = true;
            detalleNombre.ReadOnly = true;            
            detalleApe.ReadOnly = true;
            detalleDNI.ReadOnly = true;
            detalleTel.ReadOnly = true;
            comboProv.Enabled = false;
            comboLocal.Enabled = false;
            detalleCalle.ReadOnly = true;
            detalleCalle2.ReadOnly = true;
            detalleCP.ReadOnly = true;
            detalleNacido.ReadOnly = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }   

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void detalleNombre_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarNom(detalleNombre.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleNombre, "El campo no puede estar vacío");
            }else
            {
                errorProvider1.Clear();
            }
        }

        private void detalleNombre_TextChanged(object sender, EventArgs e)
        {

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
        private void detalleDNI_Validating(object sender, CancelEventArgs e)
        {
            string mensaje = Usu.ComprobarDni(detalleDNI.Text.ToString());
            if (mensaje!="")
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleDNI, mensaje);
            }else
            {
                errorProvider1.Clear();
            }
        }


        //private void detalleDNI_Validating(object sender, CancelEventArgs e)
        //{
        //    switch (detalleDNI.Text.Length)
        //    {
        //        case 0:
        //            e.Cancel = true;
        //            errorProvider1.SetError(detalleDNI, "El campo no puede estar vacío");
        //            break;
        //        default:
        //            char letra = ' ';
        //            errorProvider1.SetError(detalleDNI, "");
        //            if (detalleDNI.Text.Length > 0)
        //            {
        //                letra = detalleDNI.Text[0];
        //            }

        //            errorProvider1.SetError(detalleDNI, "");

        //            ComprobarLetra(e, letra);
        //            break;
        //    }         
        //}

        //public void ComprobarLetra(CancelEventArgs e, char letra)
        //{
        //    switch (letra)
        //    {
        //        case ' ':
        //            break;
        //        default:
        //            letra = LetraNIF();
        //            if (letra == '0')
        //            {
        //                e.Cancel = true;
        //                errorProvider1.SetError(detalleDNI, "");
        //                errorProvider1.SetError(detalleDNI, "El formato no es correcto");
        //            }
        //            if (letra == ' ')
        //            {
        //                e.Cancel = true;
        //                errorProvider1.SetError(detalleDNI, "");
        //                errorProvider1.SetError(detalleDNI, "El DNI debe contener 9 dígitos.");
        //            }
        //            else
        //            {
        //                if (letra != detalleDNI.Text.ToString()[8])
        //                {
        //                    e.Cancel = true;
        //                    errorProvider1.SetError(detalleDNI, "");
        //                    errorProvider1.SetError(detalleDNI, "El DNI no está correcto");
        //                }
        //            }
        //        break;
        //    }            
        //}

        //public char LetraNIF()
        //{
        //    char l = ' ';
        //    string numero="";
        //    if (detalleDNI.Text.Length == 9) {                
        //        numero = detalleDNI.Text.ToString().Substring(0, 8);
        //        try
        //        {
        //            int num = Int32.Parse(numero);
        //        }
        //        catch
        //        {
        //            return '0';
        //        }
        //    }           

        //    const string CORRESPONDENCIA = "TRWAGMYFPDXBNJZSQVHLCKE";
        //    Match match = new Regex(@"\b(\d{8})\b").Match(numero);
        //    if (match.Success)
        //        l = CORRESPONDENCIA[int.Parse(numero) % 23];
        //    else
        //    {

        //    }            
        //    return l;
        //}

        //public char LetraNIE()
        //{
        //    char l = ' ';
        //    string numero = "";

        //    if (detalleDNI.Text.Length > 0)
        //    {
        //        if (detalleDNI.Text.Length == 9)
        //        {
        //            numero = detalleDNI.Text.ToString().Substring(0, 8);
        //        }
        //        char primeraLetra = Convert.ToChar(detalleDNI.Text[0]);

        //        const string CORRESPONDENCIA = "TRWAGMYFPDXBNJZSQVHLCKE";
        //        Match match = new Regex(@"\b(\d{8})\b").Match(numero);
        //        if (match.Success)
        //            l = CORRESPONDENCIA[int.Parse(numero) % 23];
        //        else
        //        {

        //        }
        //    }
        //    return l;
        //}


        private void detalleEmail_TextChanged(object sender, EventArgs e)
        {

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

        private void detalleCP_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
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

        private void detalleEmail_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void detalleEmail_Leave(object sender, EventArgs e)
        {

        }

        private void detalleDNI_Leave(object sender, EventArgs e)
        {

        }

        private void detalleDNI_TextChanged(object sender, EventArgs e)
        {

        }

        private void detalleDNI_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void detalleDNI_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void detalleDNI_KeyPress(object sender, KeyPressEventArgs e)
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

        private void detalleDNI_Validated(object sender, EventArgs e)
        {

        }

        private void comboProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(provincia i in provincias)
            {
                if (comboProv.SelectedItem.ToString() == i.Nombre)
                {
                    Usu.ProvinciaID = i.ProvinciaID;
                }
            }
            detalleProvID.Text = Usu.ProvinciaID;
            comboLocal.Items.Clear();
            comboLocal.SelectedItem = "";
            comboLocal.Text = "";
            comboLocal.SelectedItem = ListaLocalidades(Usu.PuebloID);
        }

        private void comboLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] st = comboLocal.SelectedItem.ToString().Split('/');            

            foreach(localidad l in localidades)
            {

                if (st[0] == l.Nombre)
                {
                    Usu.PuebloID = l.LocalidadID;
                }
            }
            detallePuebloID.Text = Usu.PuebloID;

        }

        private void comboProv_Click(object sender, EventArgs e)
        {

        }

        private void comboLocal_Click(object sender, EventArgs e)
        {

            //comboLocal.SelectedItem = ListaLocalidades(Usu.PuebloID);
        }

        private void detalleTel_Validating(object sender, CancelEventArgs e)
        {
            if (!Usu.ComprobarTel(detalleTel.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(detalleTel, "El campo no tiene el formato correcto");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
