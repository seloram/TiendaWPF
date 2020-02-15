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
using System.Windows.Forms.DataVisualization.Charting;

namespace ProyectoEvaluacion
{
    public partial class Estadisticas : Form
    {
        private List<pedido> listaPedidos;
        public Estadisticas(negocio neg, FormularioPrincipal f)
        {
            listaPedidos = neg.pedidos();
            InitializeComponent();
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            int cont = 0, cont2 = 0;
            DateTime fecha = DateTime.Parse(listaPedidos[0].Fecha);

            
        }

        public void CalcularEstad()
        {
            chart1.Series[0].Points.Clear();

            List<pedido> myLista = listaPedidos.OrderByDescending(o => Convert.ToDateTime(o.Fecha)).ToList();
            int cont = 0;
            string fecha = DateTime.MinValue.ToShortDateString();
            bool mayor = false;
            int x = 0;

            int y = 1;
            chart1.Series[0].LegendText = "Pedidos por día";
            for (int i = 0; i < myLista.Count; i++)
            {
                
                if ((DateTime.Parse(myLista[i].Fecha).Month.ToString() == dateTimePicker1.Value.Month.ToString()
                    /*t_mes.Text*/) && (DateTime.Parse(myLista[i].Fecha).Year.ToString() == dateTimePicker1.Value.Year.ToString() /*t_anyo.Text*/))
                {
                    fecha = DateTime.Parse(myLista[i].Fecha).ToShortDateString();
                    cont++;
                    if (i < 68)
                    {
                        while (DateTime.Parse(myLista[i].Fecha).ToShortDateString() == DateTime.Parse(myLista[i + 1].Fecha).ToShortDateString())
                        {
                            cont++;
                            i++;
                        }
                    }

                    chart1.Series[0].Points.Add(cont);
                    chart1.Series[0].Points[x].Label = "Dia:" + DateTime.Parse(myLista[i].Fecha).Day.ToString();
                    chart1.Series[0].Points[x].IsValueShownAsLabel = true;
                    x++;
                    y++;
                    cont = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                CalcularEstad();          
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
