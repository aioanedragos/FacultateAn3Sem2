using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Tema1
{
    public partial class Form1 : Form
    {
        Dictionary<string, int[]> suma_randuri = new Dictionary<string, int[]>();
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
                string cheie = "";
                for(int j=0;j<m;j++)
                {
                    if(m1[i,j]==1)
                    {
                        cheie +=j+"+";
                    }
                }

                if(cheie!="")
                {
                    string[] randuri = cheie.Substring(0,cheie.Length-2).Split('+');
                    int[] randint = new int[randuri.Length];

                    int lk = 0;
                    foreach(string pl in randuri)
                    {
                        if(pl!="")
                            randint[lk++] = Int32.Parse(pl);
                    }


                    if (suma_randuri.ContainsKey(cheie))
                    {
                        for (int p = 0; p < n; p++)
                            ci[i, p] = suma_randuri[cheie][p];
                    }
                    else
                    {
                        int[] x = new int[n];
                        for (int p = 0; p < randint.Length; p++)
                        {
                            int rand = randint[p];
                            for (int k = 0; k < n; k++)
                            {
                                ci[i, k] = sum(ci[i, k], m2[rand, k]);
                                x[k] = ci[i, k];
                            }
                        }
                        suma_randuri.Add(cheie, x);

                    }
                }
                else
                {
                    for(int t=0;t<n;t++)
                    {
                        ci[i, t] = 0;
                    }
                }
                
            }
            return ci;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
            ex3_res.Text = "";
            var filePath1 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\Facultate\\CN\\Tema1\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                    ex3_res.Text = "File 1: " + filePath1;
                }
            }

            var filePath2 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\Facultate\\CN\\Tema1\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath2 = openFileDialog.FileName;
                    ex3_res.Text += "File 2: " + filePath2;
                }
            }

            string sA = System.IO.File.ReadAllText(filePath1);
            string sB = System.IO.File.ReadAllText(filePath2);

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }

            int[,] A = new int[n/2, n/2];
            int[,] B = new int[n/2, n/2];

            double[,] _A = new double[n / 2 , n / 2];
            int i = 0, j = 0;
            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    A[i, j] = int.Parse(col.Trim());
                    _A[i, j] = A[i, j];
                    j++;
                }
                i++;
            }

            double[,] _B = new double[n / 2 , n / 2];
            i = 0;
            j = 0;
            foreach (var row in sB.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    B[i, j] = int.Parse(col.Trim());
                    _B[i, j] = B[i, j];
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

                suma_randuri.Clear();

                Console.WriteLine("A_intermediar " + (k+1).ToString());

                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < m; j++)
                    {
                        Console.Write(interA[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("B_intermediar " + (k + 1).ToString());
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        Console.Write(interB[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("C_intermediar " + (k + 1).ToString());
                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        Console.Write(interC[i, j].ToString() + " ");
                    }
                    Console.WriteLine();
                }
            }

            Matrix<double> A_check = DenseMatrix.OfArray(_A);
            Matrix<double> B_check = DenseMatrix.OfArray(_B);

            var C_check = A_check.Multiply(B_check);

            ex3_res.Text += "\n";   
            matrixA.Text = "Matricea A:\n";
            matrixB.Text = "Matricea B:\n";

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    ex3_res.Text += C[i, j].ToString() + " ";
                    matrixA.Text += A[i, j].ToString() + " ";
                    matrixB.Text += B[i, j].ToString() + " ";
                }
                matrixA.Text += '\n';
                matrixB.Text += '\n';
                ex3_res.Text += '\n';
            }

            ex3_res.Text += "\n";

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (C_check[i, j] >= 1)
                        ex3_res.Text += "1 ";
                    else
                        ex3_res.Text += "0 ";
                }
                ex3_res.Text += '\n';
            }
        }

        private void ex1_res_Click(object sender, EventArgs e)
        {

        }

        private void ex2_res_Click(object sender, EventArgs e)
        {

        }

        private void ex3_res_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
