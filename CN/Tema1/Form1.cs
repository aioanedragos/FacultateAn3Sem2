using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public double precizie()
        {
            int m = 0;
            decimal dec = new decimal(-1 * m);
            double d = (double)dec;
            double u = Math.Pow(10, d);
            while (u > 0 && u + 1 != 1)
            {
                m++;
                d = -1 * m;
                u = Math.Pow(10, d);
            }

            return u;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ex1_res.Text = precizie().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x = 1.0;
            double y = precizie();
            double z = precizie();

            double first_sum = (x + y) + z;
            double second_sum = x + (y + z);

            if (first_sum != second_sum)
            {
                ex2_res.Text = "Adunare neasociativa pt. x=" + x.ToString() + ", y=" + y.ToString() + ", z=" + z.ToString() + ";\n";
            }

            double first_mul = (x * y) * z;
            double second_mul = x * (y * z);

            while(true)
            {
                if (first_mul != second_mul)
                {
                    ex2_res.Text += "Inmultire neasociativa pt. x=" + x.ToString() + ", y=" + y.ToString() + ", z=" + z.ToString();
                    break;
                }
                else
                {
                    var rand = new Random();
                    x = rand.NextDouble()*(1.0 - 0.9) + 0.9;
                    first_mul = (x * y) * z;
                    second_mul = x * (y * z);
                }
            }

        }
        public int sum(int a,int b)
        {
            if (a == 0 && b == 0)
                return 0;
            else
            if (a == 0 && b == 1)
                return 1;
            else
                if (a == 1 && b == 0)
                return 1;
            else 
                return 1;
        }

        public int[,] adunare(int[,] m1,int[,] m2,int n,int m)
        {
            int[,] ci = new int[n, n];
            for(int s=0;s<n;s++)
                for (int t = 0; t < n; t++)
                    ci[s, t] = 0;
           
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<m;j++)
                {
                    if(m1[i,j]==1)
                    {
                        for(int k=0;k<n;k++)
                        {
                            ci[i, k] = sum(ci[i, k], m2[j, k]);
                        }
                    }
                }
            }
            return ci;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sA = System.IO.File.ReadAllText(@"E:\Facultate\CN\Tema1\matrixA.txt");
            string sB = System.IO.File.ReadAllText(@"E:\Facultate\CN\Tema1\matrixB.txt");

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }

            int[,] A = new int[n/2, n/2];
            int[,] B = new int[n/2, n/2];

            int i = 0, j = 0;
            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    A[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }

            i = 0;
            j = 0;
            foreach (var row in sB.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    B[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }
            
            n = n / 2;

            int m = (int)Math.Log((double)n, 2);

            int p = 0;
            if (n % m != 0) 
            {
                p = n / m + 1;
            }
            else
            {
                p = n / m;
            }

            int[,] C = new int[n, n];

            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    C[i, j] = 0;

            
            for(int k=0;k<p;k++)
            {
                int[,] interA = new int[n, m];
                int[,] interB = new int[m, n];
                int[,] interC = new int[n, n];

                for (i = 0; i < n; i++)
                {
                    for (j = k*m; j < k*m+m; j++)
                    {
                        if (j > n - 1)
                            interA[i, j%m] = 0;
                        else
                            interA[i, j%m] = A[i, j];
                    }
                }

                for (i = k*m; i < k*m+m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        if (i > n - 1)
                            interB[i%m, j] = 0;
                        else
                            interB[i%m, j] = B[i, j];
                    }
                }

                interC = adunare(interA, interB, n, m);

                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                        C[i, j] = sum(interC[i, j], C[i, j]);
            }

            ex3_res.Text = "";
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                    ex3_res.Text += C[i, j].ToString() + " ";
                ex3_res.Text += '\n';
            }
                


        }
    }
}
