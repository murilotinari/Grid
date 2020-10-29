using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable Tbl;
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Tbl = new DataTable();

            Tbl.Columns.Add("Rodada", typeof(double));
            Tbl.Columns.Add("a", typeof(double));
            Tbl.Columns.Add("f(a)", typeof(double));
            Tbl.Columns.Add("b", typeof(double));
            Tbl.Columns.Add("f(b)", typeof(double));
            Tbl.Columns.Add("Xk", typeof(double));
            Tbl.Columns.Add("f(Xk)", typeof(double));
            Tbl.Columns.Add("Precisao", typeof(double));

            DataRow Linha;


            int x = 1;
            double[] valorA = new double[x], valorB = new double[x], funcaoDeA = new double[x], funcaoDeB = new double[x], valorXk = new double[x], funcaoDeXk = new double[x];
            double[] rodadas = new double[x], precisao = new double[x];
            double E = 0.1;
            lblA.Text = "3";
            lblB.Text = "-3";
            

            valorA[0] = double.Parse(lblA.Text);
            valorB[0] = double.Parse(lblB.Text);

            funcaoDeA[0] = Math.Pow(double.Parse(lblA.Text), 2) + double.Parse(lblA.Text) - 7;
            funcaoDeB[0] = Math.Pow(double.Parse(lblB.Text), 2) + double.Parse(lblB.Text) - 7;

            valorXk[0] = (valorA[0] + valorB[0])/2;
            
            funcaoDeXk[0] = Math.Pow(valorXk[0], 2) + valorXk[0] - 7;
            rodadas[0] = 0;
            for (x = 1; x > -1; x++)
            {                         

                precisao[x - 1] = valorXk[x - 1] - valorA[x - 1];

                if (precisao[x - 1] < 0)
                    precisao[x - 1] = - precisao[x - 1];


                if (precisao[x - 1] < E)
                    break;

                Array.Resize(ref valorA, x + 1);
                Array.Resize(ref valorB, x + 1);
                Array.Resize(ref funcaoDeA , x + 1);
                Array.Resize(ref funcaoDeB , x + 1);
                Array.Resize(ref valorXk , x + 1);
                Array.Resize(ref funcaoDeXk , x + 1);
                Array.Resize(ref rodadas, x + 1);
                Array.Resize(ref precisao, x + 1);


                if (funcaoDeXk[x - 1] > 0)
                {
                    valorA[x] = valorXk[x - 1];
                    valorB[x] = valorB[x - 1];
                }
                else if (funcaoDeXk[x - 1] < 0)
                {
                    valorB[x] = valorXk[x - 1];
                    valorA[x] = valorA[x - 1];
                }
                else
                    break;

                funcaoDeA[x] = equacao(valorA[x]);
                funcaoDeB[x] = equacao(valorB[x]);

                valorXk[x] = (valorA[x] + valorB[x]) / 2;
                funcaoDeXk[x] = equacao(valorXk[x]);

                rodadas[x] = x;

                Linha = Tbl.NewRow();

                Linha[0] = Math.Round(rodadas[x - 1], 7);
                Linha["a"] = Math.Round(valorA[x - 1], 7);
                Linha["f(a)"] = Math.Round(funcaoDeA[x - 1], 7);
                Linha["b"] = Math.Round(valorB[x - 1], 7);
                Linha["f(b)"] = Math.Round(funcaoDeB[x - 1], 7);
                Linha["Xk"] = Math.Round(valorXk[x - 1], 7);
                Linha["f(Xk)"] = Math.Round(funcaoDeXk[x - 1], 7);
                Linha["Precisao"] = Math.Round(precisao[x - 1], 7);

                Tbl.Rows.Add(Linha);
            }

                       
            DataGridView.DataSource = Tbl;


        }
        static double equacao(double entrada)
        {
            return (Math.Pow(entrada, 2) + entrada - 7);
        }
    }
}
