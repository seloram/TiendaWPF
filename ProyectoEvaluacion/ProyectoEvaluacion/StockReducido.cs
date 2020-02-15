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
    public partial class StockReducido : Form
    {
        private List<stock> listarStock;
        private List<articulo> listarArticulo;
        private List<tipoarticulo> listaTipo;
        private negocio neg;
        private FormularioPrincipal formu;

        public StockReducido(negocio n, FormularioPrincipal f, List<stock> s, List<articulo> a, List<tipoarticulo> t)
        {
            InitializeComponent();
            listarStock = s;
            listarArticulo = a;
            listaTipo = t;
            formu = f;
            neg = n;
        }

        private void StockReducido_Load(object sender, EventArgs e)
        {
            reportViewer1.Dock = DockStyle.Fill;

            reportViewer1.LocalReport.ReportEmbeddedResource = "ProyectoEvaluacion.Report2.rdlc";
            this.Controls.Add(reportViewer1);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Stock", listarStock));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Articulos", listarArticulo));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TipoArticulo", listaTipo));


            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
