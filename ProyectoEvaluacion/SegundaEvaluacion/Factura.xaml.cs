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
using System.IO;
using Microsoft.Win32;
using CapaNegocio;
// Necesarios para el paquete de informes
using Syncfusion.Windows.Reports;
using Syncfusion.Windows.Reports.Viewer;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for Factura.xaml
    /// </summary>
    public partial class Factura : UserControl
    {
        private List<listaCompletaPedidos> pedidos;
        private List<detallePedido> lineaPed;

        private List<usuario> usuario;
        private List<articulo> articulos;
        private negocio neg;
        private FormularioPrincipal formu;
        private ReportViewer reportViewer1;
        public Factura(negocio n, FormularioPrincipal f, List<listaCompletaPedidos> p, List<detallePedido> l)
        {            
            InitializeComponent();
            pedidos = p;
            lineaPed = l;
            neg = n;
            formu = f;
     

            string ruta = System.IO.Directory.GetCurrentDirectory() +
                          "\\..\\..\\R_Factura.rdlc";
            reportViewer1 = new ReportViewer();
            reportViewer1.ReportPath = ruta;
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            reportViewer1.DataSources.Clear();
            reportViewer1.DataSources.Add((new ReportDataSource("DataSet1", pedidos)));
            reportViewer1.DataSources.Add((new ReportDataSource("DataSet2", lineaPed)));

            reportViewer1.RefreshReport();            
            gridPrincipal.Children.Add(reportViewer1);
        }
    }
}
