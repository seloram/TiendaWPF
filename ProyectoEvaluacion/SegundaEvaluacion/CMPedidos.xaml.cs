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
using System.Data;
using Microsoft.VisualC.StlClr;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for CMPedidos.xaml
    /// </summary>
    public partial class CMPedidos : UserControl
    {   
        private List<articulo> art;
        private articulo Art { get; set; }
        private List<articulo> listaP;
        private List<articulo> listaSModifArt;
        private int index;
        private int nLinea;
        private bool creado;
        private CollectionViewSource listaFiltrada;
        private string nombreArticulo;
        private int Total { get; set; }
        private int Iva { get; set; }
        private int TotalF { get; set; }
        private List<lineaCompleta> listaLineaCompleta;
        private List<string> descripciones;
        private negocio neg;
        private List<tv> listaTv;
        private List<tipoarticulo> listaTipos;
        public  usuario Usu { get; set; }
        private List<camara> listaCamara;
        private List<objetivo> listaObjetivo;
        private char modo;
        private List<memoria> mem;
        private FormularioPrincipal formu;
        private List<linped> nuevaListaLinPed;        
        private List<memoria> listaMemoria;
        private List<tipoarticulo> tipo;
        private List<detallePedido> det;
        private int PedidoId { get; set; }
        private List<pedido> listaPedidos;
        private List<linped> lineaPed;

        public CMPedidos(FormularioPrincipal f, negocio n, usuario u, int idPedido, char m)
        {
            InitializeComponent();
            neg = n;
            listaFiltrada = (System.Windows.Data.CollectionViewSource)this.Resources["listaProductos"];           
            listaP = new List<articulo>();
            PedidoId = idPedido;
            creado = false;
            modo = m;
            formu = f;
            listaMemoria = neg.leerMemoria();
            listaTv = neg.leerTv();
            nLinea = 0;
            listaSModifArt = neg.articulos();
            listaCamara = neg.leerCamaras();
            listaLineaCompleta = new List<lineaCompleta>();
            nuevaListaLinPed = new List<linped>();
            listaTipos = neg.tipoArticulos();
            listaObjetivo = neg.leerObjetivos();
            gridLineaPed.DataContext = nuevaListaLinPed;
            index = 0;
            Usu = u;            
            listaPedidos = neg.pedidos();
            lineaPed = neg.lineaPedidos();
            art = neg.articulos();
            Art = new articulo();
            tipo = neg.tipoArticulos();
           
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

    

            foreach (articulo u in art)
            {
                listaP.Add(u);
            }

            resultado.Visibility = Visibility.Hidden;
            foreach (articulo a in listaP)
            {
                foreach (tipoarticulo t in tipo)
                {
                    if (a.TipoArticuloID == t.TipoArticuloID.ToString())
                    {
                        a.TipoArticuloID = t.Descripcion;
                    }
                }
            }

            listaFiltrada.Source = listaP;

            if (Usu != null)
            {
                buscarUsuario.Text = Usu.Nombre;
                apellidoUsu.Text = Usu.Apellidos;
            }
            else
                Usu = new usuario();

            if (modo == 'm')
            {
                buscarUsuario.Text = Usu.Nombre;
                apellidoUsu.Visibility = Visibility.Visible;
                apellidoUsu.Text = Usu.Apellidos;
                informarDatagrid(PedidoId.ToString());

                //foreach(linped l in lineaPed)
                //{
                //    if (l.PedidoID == PedidoId)
                //    {

                //    }
                //}
                //gridLineaPed.DataContext
            }
        }

        private void nombreProducto(object sender, KeyEventArgs e)
        {
            
        }

        private void filtrarTipo_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Filtrar(object sender, FilterEventArgs e)
        {
            articulo arti = (articulo)e.Item;

            if (arti != null)
            {
                if (((arti.Nombre.Contains(filtrarNombre.Text)) || (filtrarNombre.Text == "Nombre"))
                    && ((arti.TipoArticuloID.Contains(filtrarTipo.Text) || (filtrarTipo.Text == "Tipo"))))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void resultado_LostFocus(object sender, RoutedEventArgs e)
        {
            resultado.Visibility = Visibility.Hidden;
        }

        private void buscarUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (modo != 'm')
            {
                listaLineaCompleta.Clear();
                gridLineaPed.DataContext = "";
                gridLineaPed.DataContext = listaLineaCompleta;
                recalcular();
                total.Text = "Total sin Iva:................." + Total.ToString();
                iva.Text = "Iva:................................." + Iva.ToString();
                totalFactura.Text = "Total Factura:..............." + TotalF.ToString();

                ModificacionUsuario modificar = new ModificacionUsuario(this, formu, neg, 'b');
                formu.panelPrincipal.Children.Add(modificar);
                modificar.titulo.Content = "BUSQUEDA DE USUARIO";
                Visibility = Visibility.Collapsed;
            }            
        }
        public void informarDatagrid(string i)
        {
            int importeArt = 0, lin = 0, cantidad = 0;
            string nombre = null;
            string pvp = "", marcaID = "", tipoArtiDesc = "", tipoArtiID = "", nombreEspecif = "", idArt="";
            string id = null;

            id = i;
            foreach (linped l in lineaPed)
            {
                if (id == l.PedidoID.ToString())
                {
                    lin = l.Linea;
                    importeArt = l.Importe;
                    cantidad = l.Cantidad;
                    idArt = l.ArticuloID;


                    foreach (articulo a in listaSModifArt)
                    {
                        if (a.ArticuloID == idArt)
                        {
                            //idArt = a.ArticuloID;
                            nombreArticulo = a.Nombre;
                            tipoArtiID = a.TipoArticuloID;
                        }
                    }

                    foreach (articulo a in listaP)
                    {
                        if (a.ArticuloID == idArt)
                        {
                            nombre = a.Nombre;
                            pvp = a.Pvp;
                            marcaID = a.MarcaID;
                        }
                    }

                    foreach (tipoarticulo t in listaTipos)
                    {
                        if (t.TipoArticuloID.ToString() == tipoArtiID)
                        {
                            tipoArtiDesc = t.Descripcion;
                            //   tipoArtiID = t.TipoArticuloID.ToString();
                            switch (tipoArtiID)
                            {
                                case "1":
                                    foreach (tv tv in listaTv)
                                    {
                                        if (tv.TvID == idArt)
                                        {
                                            nombreEspecif = tv.Panel;
                                        }
                                    }
                                    break;
                                case "2":
                                    foreach (memoria m in listaMemoria)
                                    {
                                        if (m.MemoriaID == idArt)
                                        {
                                            nombreEspecif = m.Tipo;
                                        }
                                    }
                                    break;
                                case "3":
                                    foreach (camara c in listaCamara)
                                    {
                                        if (c.CamaraID == idArt)
                                        {
                                            nombreEspecif = c.Tipo;
                                        }
                                    }
                                    break;
                                case "4":
                                    foreach (objetivo o in listaObjetivo)
                                    {
                                        if (o.ObjetivoID == idArt)
                                        {
                                            nombreEspecif = o.Tipo;
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                    lineaCompleta linea = new lineaCompleta(lin, idArt, importeArt, cantidad, nombre, pvp, marcaID, tipoArtiID
                        , nombreEspecif, tipoArtiDesc);


                    //linped l = new linped(listaPedidos[listaPedidos.Count - 1].PedidoID + 1, nLinea,
                    //    id, Convert.ToInt32(((articulo)artAux).Pvp), 1);

                    listaLineaCompleta.Add(linea);

                    //nuevaListaLinPed.Add(linea);
                    //gridLineaPed.DataContext = "";
                    //gridLineaPed.DataContext = nuevaListaLinPed;
                }
            }
            gridLineaPed.DataContext = "";
            gridLineaPed.DataContext = listaLineaCompleta;
            recalcular();
            total.Text = "Total sin Iva:................." + Total.ToString();
            iva.Text = "Iva:................................." + Iva.ToString();
            totalFactura.Text = "Total Factura:..............." + TotalF.ToString();

        }


        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            //if (modo != 'm')
            //{
            //    nLinea += 1;
            //}
            //else
            //{
            //    if (listaLineaCompleta.Count == 0)
            //    {
            //        nLinea = 1;
            //    }else
            //        nLinea = listaLineaCompleta[listaLineaCompleta.Count - 1].Linea + 1;
            //}
                
        
            //    int importeArt = 0;
            //    string nombre = null;
            //    string pvp = "", marcaID = "", tipoArtiDesc = "", tipoArtiID = "", nombreEspecif = "";
            //    string id = null;

            //    try
            //    {

            //        var artAux = gridArticulos.SelectedCells[0].Item;

            //        id = ((articulo)artAux).ArticuloID;

            //        foreach (articulo a in listaSModifArt)
            //        {
            //            if (a.ArticuloID == id)
            //            {
            //                id = a.ArticuloID;
            //                nombreArticulo = a.Nombre;
            //                tipoArtiID = a.TipoArticuloID;
            //            }
            //        }

            //        foreach (articulo a in listaP)
            //        {
            //            if (a.ArticuloID == id)
            //            {
            //                nombre = a.Nombre;
            //                pvp = a.Pvp;
            //                marcaID = a.MarcaID;
            //            }
            //        }

            //        foreach (tipoarticulo t in listaTipos)
            //        {
            //            if (t.TipoArticuloID.ToString() == tipoArtiID)
            //            {
            //                tipoArtiDesc = t.Descripcion;
            //                //   tipoArtiID = t.TipoArticuloID.ToString();
            //                switch (tipoArtiID)
            //                {
            //                    case "1":
            //                        foreach (tv tv in listaTv)
            //                        {
            //                            if (tv.TvID == id)
            //                            {
            //                                nombreEspecif = tv.Panel;
            //                            }
            //                        }
            //                        break;
            //                    case "2":
            //                        foreach (memoria m in listaMemoria)
            //                        {
            //                            if (m.MemoriaID == id)
            //                            {
            //                                nombreEspecif = m.Tipo;
            //                            }
            //                        }
            //                        break;
            //                    case "3":
            //                        foreach (camara c in listaCamara)
            //                        {
            //                            if (c.CamaraID == id)
            //                            {
            //                                nombreEspecif = c.Tipo;
            //                            }
            //                        }
            //                        break;
            //                    case "4":
            //                        foreach (objetivo o in listaObjetivo)
            //                        {
            //                            if (o.ObjetivoID == id)
            //                            {
            //                                nombreEspecif = o.Tipo;
            //                            }
            //                        }
            //                        break;
            //                }
            //            }
            //        }
            //        importeArt = Convert.ToInt32(pvp) * 1;
            //        lineaCompleta linea = new lineaCompleta(nLinea, id, importeArt, 1, nombre, pvp, marcaID, tipoArtiID
            //            , nombreEspecif, tipoArtiDesc);


            //        linped l = new linped(listaPedidos[listaPedidos.Count - 1].PedidoID + 1, nLinea,
            //            id, Convert.ToInt32(((articulo)artAux).Pvp), 1);

            //        listaLineaCompleta.Add(linea);
            //        //nuevaListaLinPed.Add(linea);
            //        //gridLineaPed.DataContext = "";
            //        //gridLineaPed.DataContext = nuevaListaLinPed;

            //        gridLineaPed.DataContext = "";
            //        gridLineaPed.DataContext = listaLineaCompleta;
            //        recalcular();
            //        total.Text = "Total sin Iva:................." + Total.ToString();
            //        iva.Text = "Iva:................................." + Iva.ToString();
            //        totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //    }
            //    catch
            //    {

            //    }
            //if (modo == 'm')
            //{
            //   // nLinea = listaLineaCompleta[listaLineaCompleta.Count-1].Linea + 1;
            //    try
            //    {

            //        var artAux = gridArticulos.SelectedCells[0].Item;
            //        linped nuevaLinea = new linped(PedidoId, nLinea, id, importeArt, 1);
            //        if (neg.insertarLinPed(nuevaLinea))
            //        {

            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
            //            resultado.Text = "Linea de pedido creado";
            //            resultado.Focus();
            //        }
            //        else
            //        {

            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textError");
            //            resultado.Text = "Error al crear la línea";
            //            resultado.Focus();
            //        }
                  
            //    }
            //    catch { }
            //}            
            
        }

        private void informarLista(List<lineaCompleta> l)
        {
            
        }

        private void gridLineaPed_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //((linped)lin).Importe=((linped)lin).Importe*((linped)lin).Cantidad;
            //int a = ((linped)lin).Cantidad;
            index = e.Row.GetIndex();
            //           int c=e.Column.GetCellContent.
            //var a = gridLineaPed.SelectedCells[0].Column.GetCellContent(0);

            //var s = gridLineaPed.SelectedCells[0].Item;

            ////int num = ((linped)num2).Cantidad;
            
            //var dg = new DataGridCellInfo(gridLineaPed.Items[0], gridLineaPed.Columns[3]).Item;
            //string cantidad = ((linped)dg).Cantidad.ToString();
            //nuevaListaLinPed[f].Cantidad = Convert.ToInt32(cantidad);

            //var b = gridLineaPed.CurrentCell.Item;
            //int z = ((linped)b).Cantidad;           
           
            //recalcular();
            //total.Text = "Total sin Iva:................." + Total.ToString();
            //iva.Text = "Iva:................................." + Iva.ToString();
            //totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
        }

        private void gridLineaPed_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var fila = gridLineaPed.SelectedCells[0].Item;
            int c = ((linped)fila).Cantidad;

            var dg = new DataGridCellInfo(gridLineaPed.Items[0], gridLineaPed.Columns[3]).Item;
            int cantidad = ((linped)dg).Cantidad;
            int precio = Convert.ToInt32(((lineaCompleta)dg).Pvp);
            nuevaListaLinPed[index].Cantidad = Convert.ToInt32(cantidad);
            nuevaListaLinPed[index].Importe = cantidad * precio;
            recalcular();
            total.Text = "Total sin Iva:................." + Total.ToString();
            iva.Text = "Iva:................................." + Iva.ToString();
            totalFactura.Text = "Total Factura:..............." + TotalF.ToString();


            //DataRowView view = (DataRowView)gridLineaPed.SelectedItem;
            //int index = gridLineaPed.CurrentCell.Column.DisplayIndex;
            //if (view != null)
            //{
            //    string cellvalue = view.Row.ItemArray[index].ToString();
            //}
            //var dg = new DataGridCellInfo(gridLineaPed.Items[0], gridLineaPed.Columns[3]).Item;
            //string a = ((linped)dg).Cantidad.ToString();

            //recalcular();
            //total.Text = "Total sin Iva:................." + Total.ToString();
            //iva.Text = "Iva:................................." + Iva.ToString();
            //totalFactura.Text = "Total Factura:..............." + TotalF.ToString();        
        }

        private void recalcular()
        {
            int total=0, iva = 0, totalf = 0;

            Total= neg.FacturaWPF(listaLineaCompleta, total, out iva, out totalf);
       
            Iva = iva;
            TotalF = totalf;
        }

        private void gridLineaPed_CurrentCellChanged(object sender, EventArgs e)
        {
            var fila = gridLineaPed.SelectedCells[0].Item;
            //try
            //{
            //    var lin = gridLineaPed.SelectedCells[0].Item;

            //    int imp = 0;
            //    for (int i = 0; i < art.Count; i++)
            //    {
            //        if (art[i].ArticuloID == ((linped)lin).ArticuloID)
            //        {
            //            imp = Convert.ToInt32(art[i].Pvp) * ((linped)lin).Cantidad;
            //            for (int j = 0; j < nuevaListaLinPed.Count; j++)
            //            {
            //                if ((nuevaListaLinPed[0].PedidoID == ((linped)lin).PedidoID)
            //                    && (nuevaListaLinPed[0].ArticuloID == ((linped)lin).ArticuloID))
            //                {
            //                    nuevaListaLinPed[0].Importe = imp;
            //                }
            //            }
            //        }
            //    }

            //    gridLineaPed.DataContext = "";
            //    gridLineaPed.DataContext = nuevaListaLinPed;
            //}
            //catch
            //{

            //}
        }

        private void gridLineaPed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void gridLineaPed_TextInput(object sender, TextCompositionEventArgs e)
        {   
        }

        private void gridLineaPed_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void filtrarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listaFiltrada != null)
            {
                listaFiltrada.Filter += new FilterEventHandler(Filtrar);
            }
            
        }

        private void mascaraNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            //mascaraNombre = new TextBox();
            mascaraNombre.Visibility = Visibility.Collapsed;
            filtrarNombre.Focus();
        }

        private void filtrarTipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaFiltrada.Filter += new FilterEventHandler(Filtrar);
        }

        private void mascarabre_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void mascaraTipo_TextChanged(object sender, RoutedEventArgs e)
        {
            mascaraTipo.Visibility = Visibility.Collapsed;
            filtrarTipo.Focus();
        }

        private void gridLineaPed_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            index = e.Row.GetIndex();
        }

        private void gridLineaPed_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        {

            //try
            //{
            //    var fila = gridLineaPed.SelectedCells[0].Item;
            //    //int c = ((lineaCompleta)fila).Cantidad;
            //    //gridLineaPed.SelectedIndex = index;
            //    //int celda = ((DataGrid)sender).SelectedIndex;


            //    var dg = new DataGridCellInfo(gridLineaPed.Items[index], gridLineaPed.Columns[3]).Item;
            //    ////string cantidad = ((lineaCompleta)dg).Cantidad.ToString();
            //    ////lineaCompleta[index].Cantidad = Convert.ToInt32(cantidad);

            //    int cantidad = ((lineaCompleta)dg).Cantidad;
            //    int precio = Convert.ToInt32(((lineaCompleta)dg).Pvp);
            //    listaLineaCompleta[index].Cantidad = cantidad;
            //    listaLineaCompleta[index].Importe = cantidad * precio;
            //    //listaLineaCompleta[index].Cantidad = ((lineaCompleta)celda).Cantidad;
            //    //listaLineaCompleta[index].Importe = ((lineaCompleta)sender).Cantidad * Convert.ToInt32(((lineaCompleta)sender).Pvp);

            //    recalcular();
            //    total.Text = "Total sin Iva:................." + Total.ToString();
            //    iva.Text = "Iva:................................." + Iva.ToString();
            //    totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //}
            //catch
            //{

            //}

        }

        private void gridLineaPed_CurrentCellChanged_1(object sender, EventArgs e)
        {
            
            //var fila = gridLineaPed.SelectedCells[0].Item;
            
        }

        private void gridLineaPed_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            //try
            //{
            //    int da = ((DataGrid)sender).SelectedIndex;
            //    if (modo == 'm')
            //    {
            //        linped l = new linped(PedidoId, ((lineaCompleta)listaLineaCompleta[da]).Linea,
            //            ((lineaCompleta)listaLineaCompleta[da]).ArticuloID, ((lineaCompleta)listaLineaCompleta[da]).Importe,
            //            ((lineaCompleta)listaLineaCompleta[da]).Cantidad);
            //        if (neg.eliminarLinPed(l))
            //        {
            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
            //            resultado.Text = "Linea de pedido eliminada";
            //            resultado.Focus();
            //        }
            //        else
            //        {

            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textError");
            //            resultado.Text = "Error al eliminar la línea";
            //            resultado.Focus();
            //        }
            //    }
            //    listaLineaCompleta.Remove(listaLineaCompleta[da]);
            //    gridLineaPed.DataContext = "";
            //    gridLineaPed.DataContext = listaLineaCompleta;
            //    recalcular();
            //    total.Text = "Total sin Iva:................." + Total.ToString();
            //    iva.Text = "Iva:................................." + Iva.ToString();
            //    totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //}
            //catch
            //{

            //}      
        }

        private void aceptar_Click(object sender, RoutedEventArgs e)
        {    
                if (modo != 'm')
                {
                    bool pedido = false, lineas = false;
                    int pedidoId = listaPedidos[listaPedidos.Count - 1].PedidoID + 1;
                    //DatePicker.DisplayDateProperty.ToString("yyyy-M-dd hh:mm:ss")
                    string fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");

                    pedido p = new pedido();
                    p.Fecha = fecha;
                    if (Usu.UsuarioID != 0)
                    {
                        p = new pedido(pedidoId, Usu.UsuarioID, fecha);

                        if (neg.insertarPedido(p))
                        {
                            pedido = true;
                            //resultado.Visibility = Visibility.Visible;
                            //resultado.SetResourceReference(Control.StyleProperty, "textAceptar");
                            //resultado.Text = "Pedido creado";
                            //resultado.Focus();
                            foreach (lineaCompleta l in listaLineaCompleta)
                            {
                                linped lin = new linped(pedidoId, l.Linea, l.ArticuloID, l.Importe, l.Cantidad);
                                if (neg.insertarLinPed(lin))
                                {
                                    lineas = true;
                                    //resultado.Visibility = Visibility.Visible;
                                    //resultado.SetResourceReference(Control.StyleProperty, "textAceptar");
                                    //resultado.Text = "Linea de pedido creado";
                                    //resultado.Focus();
                                }
                                else
                                {
                                    lineas = false;
                                    //resultado.Visibility = Visibility.Visible;
                                    //resultado.SetResourceReference(Control.StyleProperty, "textError");
                                    //resultado.Text = "Error al crear la línea";
                                    //resultado.Focus();
                                }
                            }
                        }
                        else
                        {
                            //pedido = false;
                            //resultado.Visibility = Visibility.Visible;
                            //resultado.SetResourceReference(Control.StyleProperty, "textError");
                            //resultado.Text = "Error al crear el pedido";
                            //resultado.Focus();
                        }
                        if (pedido)
                        {
                            resultado.Visibility = Visibility.Visible;
                            resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                            resultado.Text = "Pedido creado";
                            resultado.Focus();
                            aceptar.IsEnabled = false;
                            gridLineaPed.IsEnabled = false;
                        }
                        else
                        {
                            pedido = false;
                            resultado.Visibility = Visibility.Visible;
                            resultado.SetResourceReference(Control.StyleProperty, "textError");
                            resultado.Text = "Error al crear el pedido";
                            resultado.Focus();
                        }
                    }
                    else
                    {
                        resultado.Visibility = Visibility.Visible;
                        resultado.SetResourceReference(Control.StyleProperty, "textError");
                        resultado.Text = "Usuario no informado";
                        resultado.Focus();
                    }
               
                }
                else
                {
                    foreach (lineaCompleta l in listaLineaCompleta)
                    {
                        //foreach(linped linpedi in lineaPed)
                        //{
                        //    if (PedidoId==linpedi.Linea && l.Linea != linpedi.Linea)
                        //    {
                        //        linped nuevaLinea = new linped(PedidoId, l.Linea, l.ArticuloID, l.Importe, l.Cantidad);
                        //        if (neg.insertarLinPed(nuevaLinea))
                        //        {

                        //            //resultado.Visibility = Visibility.Visible;
                        //            //resultado.SetResourceReference(Control.StyleProperty, "textAceptar");
                        //            //resultado.Text = "Linea de pedido creado";
                        //            //resultado.Focus();
                        //        }
                        //        else
                        //        {

                        //            //resultado.Visibility = Visibility.Visible;
                        //            //resultado.SetResourceReference(Control.StyleProperty, "textError");
                        //            //resultado.Text = "Error al crear la línea";
                        //            //resultado.Focus();
                        //        }
                        //    }
                        //}
                        linped lin = new linped(PedidoId, l.Linea, l.ArticuloID, l.Importe, l.Cantidad);
                        if (neg.modificarLinped(lin))
                        {
                            resultado.Visibility = Visibility.Visible;
                            resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                            resultado.Text = "Pedido modificado";
                            resultado.Focus();
                        }
                        else
                        {
                            resultado.Visibility = Visibility.Visible;
                            resultado.SetResourceReference(Control.StyleProperty, "textError");
                            resultado.Text = "Error al modificar el pedido";
                            resultado.Focus();
                        }
                    }
                }            
        }

        private void gridLineaPed_KeyDown_1(object sender, KeyEventArgs e)
        {
           
        }

        private void gridLineaPed_KeyUp(object sender, KeyEventArgs e)
        {
            //int cantidad = 0;
            //var dg = new DataGridCellInfo(gridLineaPed.Items[index], gridLineaPed.Columns[3]).Item;
            //bool correcto = int.TryParse(((lineaCompleta)dg).Cantidad.ToString(), out cantidad) ? true : false;
            //if (!correcto)
            //{
            //    resultado.Visibility = Visibility.Visible;
            //    resultado.SetResourceReference(Control.StyleProperty, "textError");
            //    resultado.Text = "Solo se admiten números";
            //    resultado.Focus();
            //}
        }

        private void eliminar_click(object sender, RoutedEventArgs e)
        {
            //int i = gridLineaPed.SelectedIndex;

            //try
            //{
            //    int da = gridLineaPed.SelectedIndex;
            //    if (modo == 'm')
            //    {
            //        linped l = new linped(PedidoId, ((lineaCompleta)listaLineaCompleta[da]).Linea,
            //            ((lineaCompleta)listaLineaCompleta[da]).ArticuloID, ((lineaCompleta)listaLineaCompleta[da]).Importe,
            //            ((lineaCompleta)listaLineaCompleta[da]).Cantidad);
            //        if (neg.eliminarLinPed(l))
            //        {
            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
            //            resultado.Text = "Linea de pedido eliminada";
            //            resultado.Focus();
            //        }
            //        else
            //        {

            //            resultado.Visibility = Visibility.Visible;
            //            resultado.SetResourceReference(Control.StyleProperty, "textError");
            //            resultado.Text = "Error al eliminar la línea";
            //            resultado.Focus();
            //        }
            //    }
            //    listaLineaCompleta.Remove(listaLineaCompleta[da]);
            //    gridLineaPed.DataContext = "";
            //    gridLineaPed.DataContext = listaLineaCompleta;
            //    recalcular();
            //    total.Text = "Total sin Iva:................." + Total.ToString();
            //    iva.Text = "Iva:................................." + Iva.ToString();
            //    totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //}
            //catch
            //{

            //}
        }


        private void gridLineaPed_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Return)
            //{
            //    try
            //    {
            //        var fila = gridLineaPed.SelectedCells[0].Item;
            //        //int c = ((lineaCompleta)fila).Cantidad;
            //        //gridLineaPed.SelectedIndex = index;
            //        //int celda = ((DataGrid)sender).SelectedIndex;


            //        var dg = new DataGridCellInfo(gridLineaPed.Items[index], gridLineaPed.Columns[3]).Item;
            //        ////string cantidad = ((lineaCompleta)dg).Cantidad.ToString();
            //        ////lineaCompleta[index].Cantidad = Convert.ToInt32(cantidad);

            //        int cantidad = ((lineaCompleta)dg).Cantidad;
            //        int precio = Convert.ToInt32(((lineaCompleta)dg).Pvp);
            //        listaLineaCompleta[index].Cantidad = cantidad;
            //        listaLineaCompleta[index].Importe = cantidad * precio;
            //        //listaLineaCompleta[index].Cantidad = ((lineaCompleta)celda).Cantidad;
            //        //listaLineaCompleta[index].Importe = ((lineaCompleta)sender).Cantidad * Convert.ToInt32(((lineaCompleta)sender).Pvp);

            //        recalcular();
            //        total.Text = "Total sin Iva:................." + Total.ToString();
            //        iva.Text = "Iva:................................." + Iva.ToString();
            //        totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        private void gridLineaPed_CurrentCellChanged_2(object sender, EventArgs e)
        {
            try
            {
                var fila = gridLineaPed.SelectedCells[0].Item;
                //int c = ((lineaCompleta)fila).Cantidad;
                //gridLineaPed.SelectedIndex = index;
                //int celda = ((DataGrid)sender).SelectedIndex;


                var dg = new DataGridCellInfo(gridLineaPed.Items[index], gridLineaPed.Columns[3]).Item;
                ////string cantidad = ((lineaCompleta)dg).Cantidad.ToString();
                ////lineaCompleta[index].Cantidad = Convert.ToInt32(cantidad);

                int cantidad = ((lineaCompleta)dg).Cantidad;
                int precio = Convert.ToInt32(((lineaCompleta)dg).Pvp);
                listaLineaCompleta[index].Cantidad = cantidad;
                listaLineaCompleta[index].Importe = cantidad * precio;
                //listaLineaCompleta[index].Cantidad = ((lineaCompleta)celda).Cantidad;
                //listaLineaCompleta[index].Importe = ((lineaCompleta)sender).Cantidad * Convert.ToInt32(((lineaCompleta)sender).Pvp);
               
               
                recalcular();
                total.Text = "Total sin Iva:................." + Total.ToString();
                iva.Text = "Iva:................................." + Iva.ToString();
                totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
                gridLineaPed.DataContext = "";
                gridLineaPed.DataContext = listaLineaCompleta;
            }
            catch
            {

            }
        }

        private void gridLineaPed_LostFocus(object sender, RoutedEventArgs e)
        {
         
            //try
            //{
            //    var fila = gridLineaPed.SelectedCells[0].Item;
            //    //int c = ((lineaCompleta)fila).Cantidad;
            //    //gridLineaPed.SelectedIndex = index;
            //    //int celda = ((DataGrid)sender).SelectedIndex;


            //    var dg = new DataGridCellInfo(gridLineaPed.Items[index], gridLineaPed.Columns[3]).Item;
            //    ////string cantidad = ((lineaCompleta)dg).Cantidad.ToString();
            //    ////lineaCompleta[index].Cantidad = Convert.ToInt32(cantidad);

            //    int cantidad = ((lineaCompleta)dg).Cantidad;
            //    int precio = Convert.ToInt32(((lineaCompleta)dg).Pvp);
            //    listaLineaCompleta[index].Cantidad = cantidad;
            //    listaLineaCompleta[index].Importe = cantidad * precio;
            //    //listaLineaCompleta[index].Cantidad = ((lineaCompleta)celda).Cantidad;
            //    //listaLineaCompleta[index].Importe = ((lineaCompleta)sender).Cantidad * Convert.ToInt32(((lineaCompleta)sender).Pvp);

            //    recalcular();
            //    total.Text = "Total sin Iva:................." + Total.ToString();
            //    iva.Text = "Iva:................................." + Iva.ToString();
            //    totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            //}
            //catch
            //{

            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridLineaPed.DataContext = "";
            gridLineaPed.DataContext = listaLineaCompleta;
        }

        private void gridLineaPed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int i = gridLineaPed.SelectedIndex;

            try
            {
                int da = gridLineaPed.SelectedIndex;
                if (modo == 'm')
                {
                    linped l = new linped(PedidoId, ((lineaCompleta)listaLineaCompleta[da]).Linea,
                        ((lineaCompleta)listaLineaCompleta[da]).ArticuloID, ((lineaCompleta)listaLineaCompleta[da]).Importe,
                        ((lineaCompleta)listaLineaCompleta[da]).Cantidad);
                    if (neg.eliminarLinPed(l))
                    {
                        resultado.Visibility = Visibility.Visible;
                        resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                        resultado.Text = "Linea de pedido eliminada";
                        resultado.Focus();
                    }
                    else
                    {

                        resultado.Visibility = Visibility.Visible;
                        resultado.SetResourceReference(Control.StyleProperty, "textError");
                        resultado.Text = "Error al eliminar la línea";
                        resultado.Focus();
                    }
                }
                listaLineaCompleta.Remove(listaLineaCompleta[da]);
                gridLineaPed.DataContext = "";
                gridLineaPed.DataContext = listaLineaCompleta;
                recalcular();
                total.Text = "Total sin Iva:................." + Total.ToString();
                iva.Text = "Iva:................................." + Iva.ToString();
                totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            }
            catch
            {

            }
        }

        private void gridArticulos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (modo != 'm')
            {
                nLinea += 1;
            }
            else
            {
                if (listaLineaCompleta.Count == 0)
                {
                    nLinea = 1;
                }
                else
                    nLinea = listaLineaCompleta[listaLineaCompleta.Count - 1].Linea + 1;
            }


            int importeArt = 0;
            string nombre = null;
            string pvp = "", marcaID = "", tipoArtiDesc = "", tipoArtiID = "", nombreEspecif = "";
            string id = null;

            try
            {

                var artAux = gridArticulos.SelectedCells[0].Item;

                id = ((articulo)artAux).ArticuloID;

                foreach (articulo a in listaSModifArt)
                {
                    if (a.ArticuloID == id)
                    {
                        id = a.ArticuloID;
                        nombreArticulo = a.Nombre;
                        tipoArtiID = a.TipoArticuloID;
                    }
                }

                foreach (articulo a in listaP)
                {
                    if (a.ArticuloID == id)
                    {
                        nombre = a.Nombre;
                        pvp = a.Pvp;
                        marcaID = a.MarcaID;
                    }
                }

                foreach (tipoarticulo t in listaTipos)
                {
                    if (t.TipoArticuloID.ToString() == tipoArtiID)
                    {
                        tipoArtiDesc = t.Descripcion;
                        //   tipoArtiID = t.TipoArticuloID.ToString();
                        switch (tipoArtiID)
                        {
                            case "1":
                                foreach (tv tv in listaTv)
                                {
                                    if (tv.TvID == id)
                                    {
                                        nombreEspecif = tv.Panel;
                                    }
                                }
                                break;
                            case "2":
                                foreach (memoria m in listaMemoria)
                                {
                                    if (m.MemoriaID == id)
                                    {
                                        nombreEspecif = m.Tipo;
                                    }
                                }
                                break;
                            case "3":
                                foreach (camara c in listaCamara)
                                {
                                    if (c.CamaraID == id)
                                    {
                                        nombreEspecif = c.Tipo;
                                    }
                                }
                                break;
                            case "4":
                                foreach (objetivo o in listaObjetivo)
                                {
                                    if (o.ObjetivoID == id)
                                    {
                                        nombreEspecif = o.Tipo;
                                    }
                                }
                                break;
                        }
                    }
                }
                importeArt = Convert.ToInt32(pvp) * 1;
                lineaCompleta linea = new lineaCompleta(nLinea, id, importeArt, 1, nombre, pvp, marcaID, tipoArtiID
                    , nombreEspecif, tipoArtiDesc);


                linped l = new linped(listaPedidos[listaPedidos.Count - 1].PedidoID + 1, nLinea,
                    id, Convert.ToInt32(((articulo)artAux).Pvp), 1);

                listaLineaCompleta.Add(linea);
                //nuevaListaLinPed.Add(linea);
                //gridLineaPed.DataContext = "";
                //gridLineaPed.DataContext = nuevaListaLinPed;

                gridLineaPed.DataContext = "";
                gridLineaPed.DataContext = listaLineaCompleta;
                recalcular();
                total.Text = "Total sin Iva:................." + Total.ToString();
                iva.Text = "Iva:................................." + Iva.ToString();
                totalFactura.Text = "Total Factura:..............." + TotalF.ToString();
            }
            catch
            {

            }
            if (modo == 'm')
            {
                // nLinea = listaLineaCompleta[listaLineaCompleta.Count-1].Linea + 1;
                try
                {

                    var artAux = gridArticulos.SelectedCells[0].Item;
                    linped nuevaLinea = new linped(PedidoId, nLinea, id, importeArt, 1);
                    if (neg.insertarLinPed(nuevaLinea))
                    {

                        resultado.Visibility = Visibility.Visible;
                        resultado.SetResourceReference(Control.StyleProperty, "textCorrecto");
                        resultado.Text = "Linea de pedido creado";
                        resultado.Focus();
                    }
                    else
                    {

                        resultado.Visibility = Visibility.Visible;
                        resultado.SetResourceReference(Control.StyleProperty, "textError");
                        resultado.Text = "Error al crear la línea";
                        resultado.Focus();
                    }

                }
                catch { }
            }
        }

        private void filtrarNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarNombre.Text == "")
            {
                mascaraNombre.Visibility = Visibility.Visible;
            }
        }

        private void filtrarTipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (filtrarTipo.Text == "")
            {
                mascaraTipo.Visibility = Visibility.Visible;
            }
        }
    }
}
