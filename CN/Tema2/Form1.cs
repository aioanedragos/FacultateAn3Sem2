using System;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using VisioForge.Shared.Accord.Math;

namespace Tema2
{
    public partial class Form1 : Form
    {
        public double Norma(double[] ceva) {
            double sum = 0;
            for (int i = 0; i < ceva.Length; i++)
                sum += Math.Pow(ceva[i], 2);
            return sum;
        }

        public double Norma_1(double[,] ceva) {
            double norma = 0;
            int n;
            if (ceva.Length % 2 == 1)
                n = ceva.Length / 2 - 1;
            else
                n = ceva.Length / 2;

            Console.WriteLine(ceva.Length.ToString());
            for (int i = 0; i < n; i++) {
                double sum = 0;
                for (int j = 0; j < n; j++) {
                    sum += Math.Abs(ceva[j, i]);
                }
                if (sum > norma)
                    norma = sum;
            }

            return norma;
        }

        public void calculate(double[,] points)
        {
            var distanceArray = new double[points.Length, points.Length];

            for (int i = 0; i < points.Length; i++)
                for (int j = 0; j < points.Length; j++)
                    distanceArray[i, j] = Distance(points[i, 0], points[i, 1], points[j, 0], points[j, 1]);
        }

        public static double Distance(double x1, double y1, double x2, double y2)
        => Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));

        public static Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        public static double determinant(double[,] a, int n)
        {
            int i, j, k;
            double det = 0;
            for (i = 0; i < n - 1; i++)
            {
                for (j = i + 1; j < n; j++)
                {
                    det = a[j, i] / a[i, i];
                    for (k = i; k < n; k++)
                        a[j, k] = a[j, k] - det * a[i, k];
                }
            }
            det = 1;
            for (i = 0; i < n; i++)
                det = det * a[i, i];
            return det;
        }

        public static double[,] GenerateMatrix(int order)
        {
            double[,] matrix = new double[order, order];

            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    matrix[i, j] = rand.Next(-5, 5);
                }
            }
            return matrix;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath1 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\Facultate\\CN\\Tema2\\Tema2";
                openFileDialog.Filter = "txt files (.txt)|.txt|All files (.)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                }
            }

            string sA = System.IO.File.ReadAllText(filePath1);

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }
            double[,] A = new double[n / 2, n / 2];
            double[,] _A = new double[n / 2, n / 2];

            int i = 0, j = 0;
            int max = 0;

            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                int nr = 0;
                foreach (var col in row.Trim().Split(' '))
                {

                    if (col.Trim().Contains('.'))
                        nr++;
                    A[i, j] = double.Parse(col.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    _A[i, j] = (double)A[i, j];
                    j++;
                }
                if (nr > max)
                    max = nr;
                i++;
            }

            if (max != 0)
                n = n - max;

            n = n / 2;
            Console.WriteLine(n.ToString());



            double[,] L = new double[n, n];
            double[,] U = new double[n, n];

            for (i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (j = 0; j < i; j++)
                        sum += (L[i, j] * U[j, k]);
                    U[i, k] = A[i, k] - sum;
                }

                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        L[i, i] = 1;
                    else
                    {
                        double sum = 0;
                        for (j = 0; j < i; j++)
                            sum += (L[k, j] * U[j, i]);
                        L[k, i] = (A[k, i] - sum) / U[i, i];
                    }
                }
            }
            /*Console.WriteLine("A:");
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(A[i, j].ToString() + " ");
                    
                }
                Console.WriteLine();
            }*/
            label1.Text = "Descompunerea LU";


            label2.Text = "Matricea U\n";
            //Console.WriteLine("U:");

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    label2.Text += U[i, j].ToString() + " ";
                    Console.Write(U[i, j].ToString() + " ");
                }
                label2.Text += '\n';
                Console.WriteLine();
            }

            label3.Text = "Matricea L\n";
            //Console.WriteLine("L:");

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    label3.Text += L[i, j].ToString() + " ";
                    Console.Write(L[i, j].ToString() + " ");
                }
                label3.Text += '\n';
                Console.WriteLine();
            }

            //Matrix<double> L_check = DenseMatrix.OfArray(L);
            //Matrix<double> U_check = DenseMatrix.OfArray(L);

            //var C_check = L_check.Multiply(U_check);
            //Console.WriteLine("C:");
            //for (i = 0; i < n; i++)
            //{
            //    for (j = 0; j < n; j++)
            //    {
            //        Console.Write(C_check[i, j].ToString() + " ");
            //    }
            //    Console.WriteLine();
            //}

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var filePath1 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\Facultate\\CN\\Tema2\\Tema2";
                openFileDialog.Filter = "txt files (.txt)|.txt|All files (.)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                }
            }

            string sA = System.IO.File.ReadAllText(filePath1);

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }
            double[,] A = new double[n / 2, n / 2];
            double[,] _A = new double[n / 2, n / 2];

            int i = 0, j = 0;
            int max = 0;

            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                int nr = 0;
                foreach (var col in row.Trim().Split(' '))
                {

                    if (col.Trim().Contains('.'))
                        nr++;
                    A[i, j] = double.Parse(col.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    _A[i, j] = (double)A[i, j];
                    j++;
                }
                if (nr > max)
                    max = nr;
                i++;
            }
            if (max != 0)
                n = n - max;
            n = n / 2;


            double[,] L = new double[n, n];
            double[,] U = new double[n, n];

            for (i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (j = 0; j < i; j++)
                        sum += (L[i, j] * U[j, k]);
                    U[i, k] = (double)A[i, k] - sum;
                }

                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        L[i, i] = 1;
                    else
                    {
                        double sum = 0;
                        for (j = 0; j < i; j++)
                            sum += (L[k, j] * U[j, i]);
                        L[k, i] = ((double)A[k, i] - sum) / U[i, i];
                    }
                }
            }

            double determinantU = 1;
            for (i = 0; i < n; i++)
                determinantU *= U[i, i];
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";


            label1.Text = "Determinanatul matricei cu ajutorul descompunerii Lu";
            label2.Text = determinantU.ToString();
            Console.WriteLine(determinantU.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filePath1 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\Facultate Git\\FacultateAn3Sem2\\CN\\Tema2";
                openFileDialog.Filter = "txt files (.txt)|.txt|All files (.)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                }
            }

            string sA = System.IO.File.ReadAllText(filePath1);

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }

            double[,] A = new double[n / 2, n / 2];
            double[,] _A = new double[n / 2, n / 2];

            int i = 0, j = 0;
            int max = 0;

            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                int nr = 0;
                foreach (var col in row.Trim().Split(' '))
                {

                    if (col.Trim().Contains('.'))
                        nr++;
                    A[i, j] = double.Parse(col.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    _A[i, j] = (double)A[i, j];
                    j++;
                }
                if (nr > max)
                    max = nr;
                i++;
            }
            if (max != 0)
                n = n - max;
            n = n / 2;


            double[,] A_init = new double[n, n];

            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    A_init[i, j] = A[i, j];



            double[,] L = new double[n, n];
            double[,] U = new double[n, n];



            for (i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (j = 0; j < i; j++)
                        sum += (L[i, j] * U[j, k]);
                    U[i, k] = A[i, k] - sum;
                    A[i, k] = A_init[i, k] - sum;
                }

                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        L[i, i] = 1;
                    if (i != k)
                    {
                        double sum = 0;
                        for (j = 0; j < i; j++)
                            sum += (L[k, j] * U[j, i]);
                        L[k, i] = ((double)A[k, i] - sum) / U[i, i];
                        A[k, i] = ((double)A_init[k, i] - sum) / U[i, i];
                    }
                }
            }

            label1.Text = "descompunerea LU intr-o singura matrice\n";

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                    label1.Text += (A[i, j].ToString() + " ");
                label1.Text += '\n';
            }



            //==================================
            var filePath2 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\Facultate Git\\FacultateAn3Sem2\\CN\\Tema2";
                openFileDialog.Filter = "txt files (.txt)|.txt|All files (.)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                }
            }

            string sb = System.IO.File.ReadAllText(filePath1);
            int m;
            m = n;


            double[,] b = new double[m, 1];
            double[,] b_init = new double[m, 1];
            int l = 0;
            foreach (var element in sb.Split('\n'))
            {
                b[l, 0] = (double)int.Parse(element);
                l++;
            }
            for (i = 0; i < m; i++)
                Console.WriteLine(b[i, 0]);

            for (i = 0; i < m; i++)
                b_init[i, 0] = b[i, 0];
            double[,] y = new double[m, 1];

            //y[0, 0] = b[0, 0];
            for (i = 1; i < n; i++)
            {
                double sum = 0;
                for (j = 0; j < n; j++)
                {
                    if (i > j)
                        sum += A[i, j] * b[j, 0];
                    if (i == j)
                    {
                        //y[j, 0] = (b[j, 0] - sum);
                        b[j, 0] = (b[j, 0] - sum);

                    }
                }
            }
            label2.Text = "Matricea Y\n";
            Console.WriteLine("Y:");
            for (i = 0; i < m; i++)
                label2.Text += b[i, 0].ToString() + '\n';
            //Console.WriteLine(b[i, 0]);

            double[,] x = new double[m, 1];

            x[m - 1, 0] = b[m - 1, 0] / A[n - 1, n - 1];


            for (i = n - 2; i >= 0; i--)
            {
                double sum = 0;
                for (j = n - 1; j >= 0; j--)
                {
                    if (i < j)
                        sum += A[i, j] * x[j, 0];
                    else
                        x[j, 0] = (b[j, 0] - sum) / A[i, j];

                }
            }

            Console.WriteLine("X:");
            label3.Text = "Matricea X\n";
            for (i = 0; i < m; i++)
                label3.Text += x[i, 0].ToString() + '\n';
            //Console.WriteLine(x[i, 0]);
            Console.WriteLine("======================");

            double[] vectorRezultate = new double[m];

            for (i = 0; i < m; i++)
            {
                double sum = 0;
                for (j = 0; j < m; j++)
                {
                    sum += A_init[i, j] * x[j, 0];
                }

                vectorRezultate[i] = sum;
            }

            for (i = 0; i < n; i++)
                vectorRezultate[i] -= b_init[i, 0];

            label4.Text = "Norma ||A_init * x_Lu - b_init||2 este " + Norma(vectorRezultate).ToString();
            //Console.WriteLine("Norma este egala cu : " + Norma(vectorRezultate));

            Matrix<double> A_check = DenseMatrix.OfArray(A_init);
            Matrix<double> b_check = DenseMatrix.OfArray(b_init);

            Console.WriteLine("Inversa matricei A este : ");
            A_check = A_check.Inverse();
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                    Console.Write(A_check[i, j].ToString());
                Console.WriteLine();
            }

            var x_lib = A_check.Multiply(b_check);

            Console.WriteLine("Rezultatul ecuatiei Ax=b este:");

            for (i = 0; i < n; i++)
                Console.WriteLine(x_lib[i, 0].ToString());




            for (i = 0; i < n; i++)
                vectorRezultate[i] = x[i, 0] - x_lib[i, 0];

            //Console.WriteLine("Norma ||X_LU - X_LIB||2 este : ");
            label5.Text = "Norma ||X_LU - X_LIB||2 este " + Norma(vectorRezultate).ToString();
            //Console.WriteLine(Norma(vectorRezultate));


            //Console.WriteLine("Ultima norma : ");

            double[] vectorIntermedia = new double[m];

            for (i = 0; i < m; i++)
            {
                double sum = 0;
                for (j = 0; j < m; j++)
                {
                    sum += A_check[i, j] * b_init[j, 0];
                }
                vectorRezultate[i] = sum;
            }

            for (i = 0; i < n; i++)
            {
                vectorRezultate[i] = x[i, 0] - vectorRezultate[i];

            }

            label6.Text = "Norma ||x_LU - A_invers_lib * b_init este " + Norma(vectorRezultate).ToString();

            Console.WriteLine(Norma(vectorRezultate));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var filePath1 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\Facultate Git\\FacultateAn3Sem2\\CN\\Tema2";
                openFileDialog.Filter = "txt files (.txt)|.txt|All files (.)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath1 = openFileDialog.FileName;
                }
            }

            string sA = System.IO.File.ReadAllText(filePath1);

            int n = 0;
            while (sA[n] != '\n')
            {
                n++;
            }

            double[,] A = new double[n / 2, n / 2];
            double[,] _A = new double[n / 2, n / 2];

            int i = 0, j = 0;
            int max = 0;

            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                int nr = 0;
                foreach (var col in row.Trim().Split(' '))
                {

                    if (col.Trim().Contains('.'))
                        nr++;
                    A[i, j] = double.Parse(col.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    _A[i, j] = A[i, j];
                    j++;
                }
                if (nr > max)
                    max = nr;
                i++;
            }
            if (max != 0)
                n = n - max;
            n = n / 2;


            double[,] A_init = new double[n, n];

            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    A_init[i, j] = A[i, j];



            double[,] L = new double[n, n];
            double[,] U = new double[n, n];



            for (i = 0; i < n; i++)
            {
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (j = 0; j < i; j++)
                        sum += (L[i, j] * U[j, k]);
                    U[i, k] = A[i, k] - sum;
                    A[i, k] = A_init[i, k] - sum;
                }

                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        L[i, i] = 1;
                    if (i != k)
                    {
                        double sum = 0;
                        for (j = 0; j < i; j++)
                            sum += (L[k, j] * U[j, i]);
                        L[k, i] = (A[k, i] - sum) / U[i, i];
                        A[k, i] = (A_init[k, i] - sum) / U[i, i];
                    }
                }
            }


            /*for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                    Console.Write(A[i, j].ToString() + " ");
                Console.WriteLine();
            }*/

            double[,] matriceRezultat = new double[n, n];
            double[] e_j = new double[n];

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";

            double[,] matrice_finala_inversa = new double[n, n];

            for (j = 0; j < n; j++)
            {
                //intializare vector e_j
                for (i = 0; i < e_j.Length; i++)
                    if (i == j)
                        e_j[i] = 1;
                    else
                        e_j[i] = 0;
                //calcul coloana invers

                for (int k = 1; k < n; k++)
                {
                    double sum = 0;
                    for (int o = 0; o < n; o++)
                    {
                        if (k > o)
                            sum += A[k, o] * e_j[o];
                        if (k == o)
                        {
                            e_j[o] = (e_j[o] - sum);

                        }
                    }
                }
                //Console.WriteLine("ceva" + e_j.Length.ToString());

                //for (int f = 0; f < e_j.Length; f++)
                //Console.WriteLine(e_j[f].ToString() + " ");

                double[,] x = new double[n, 1];
                x[n - 1, 0] = e_j[n - 1] / A[n - 1, n - 1];

                for (int k = n - 2; k >= 0; k--)
                {
                    double sum = 0;
                    for (int o = n - 1; o >= 0; o--)
                    {
                        if (k < o)
                            sum += A[k, o] * x[o, 0];
                        if (k == o)
                            x[o, 0] = (e_j[o] - sum) / A[k, o];

                    }
                }


                

                label1.Text = "inversul calculat al matricei cu aujtorul descompunerii LU\n";
                for (int f = 0; f < e_j.Length; f++)
                {
                    matrice_finala_inversa[f, j] = x[f, 0];
                    label2.Text += x[f, 0].ToString() + " ";
                }
                //Console.Write(x[f,0] + " ");
                label2.Text += '\n';
                Console.WriteLine();

            }

            //==================================
            Matrix<double> A_check = DenseMatrix.OfArray(A_init);
            A_check = A_check.Inverse();
            double[,] A_final = new double[n, n];
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    A_final[i, j] = matrice_finala_inversa[i, j] - A_check[i, j];
                } 
            }


            var rezultat = Norma_1(A_final);

            label5.Text = rezultat.ToString();



        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
