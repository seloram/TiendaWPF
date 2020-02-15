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
// Necesarios para el paquete de informes
using Syncfusion.Windows.Reports;
using Syncfusion.Windows.Reports.Viewer;

namespace SegundaEvaluacion
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        private List<stock> listarStock;
        private List<articulo> listarArticulo;
        private List<tipoarticulo> listaTipo;
        private negocio neg;
        private FormularioPrincipal formu;
        private ReportViewer reportViewer1;
        public Stock(negocio n, FormularioPrincipal f, List<stock> s, List<articulo> a, List<tipoarticulo> t)
        {
            InitializeComponent();
            listarStock = s;
            listarArticulo = a;
            listaTipo = t;
            formu = f;
            neg = n;

            string ruta = System.IO.Directory.GetCurrentDirectory() +
              "\\..\\..\\R_Stock.rdlc";
            reportViewer1 = new ReportViewer();
            reportViewer1.ReportPath = ruta;
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            reportViewer1.DataSources.Clear();
            reportViewer1.DataSources.Add((new ReportDataSource("DataSet1", listarStock)));
            reportViewer1.DataSources.Add((new ReportDataSource("DataSet2", listarArticulo)));
            reportViewer1.DataSources.Add((new ReportDataSource("DataSet3", listaTipo)));

            reportViewer1.RefreshReport();
            gridPrincipal.Children.Add(reportViewer1);
        }
    }
}
