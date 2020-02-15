using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocio;
using Microsoft.Reporting.WinForms;

namespace ProyectoEvaluacion
{
    public partial class Factura : Form
    {
        private List<pedido> pedidos;
        private List<linped> lineaPed;
        private List<usuario> usuario;
        private List<articulo> articulos;
        private negocio neg;
        private FormularioPrincipal formu;
        public Factura(negocio n,FormularioPrincipal f,List<pedido> p, List<linped> l, List<usuario> u, List<articulo> a)
        {
            pedidos = p;
            lineaPed = l;
            neg = n;
            formu = f;
            usuario = u;
            articulos = a;
            InitializeComponent();
        }

        private void Factura_Load(object sender, EventArgs e)
        {            
            reportViewer1.Dock = DockStyle.Fill;

            reportViewer1.LocalReport.ReportEmbeddedResource = "ProyectoEvaluacion.Report1.rdlc";
            this.Controls.Add(reportViewer1);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", pedidos));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", lineaPed));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", usuario));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", articulos));

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
