using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Tema2
{
    public partial class Form1 : Form
    {
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
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
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

            int[,] A = new int[n / 2, n / 2];
            double[,] _A = new double[n / 2, n / 2];

            int i = 0, j = 0;
            foreach (var row in sA.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    A[i, j] = int.Parse(col.Trim());
                    _A[i, j] = (double)A[i, j];
                    j++;
                }
                i++;
            }

            n = n / 2;

            double det = determinant(_A, n);
            if(det==0)
            {
                label1.Text = "Impossible!";
                return;
            }

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

            Console.WriteLine("A:");
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(A[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("U:");

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(U[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("L:");

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(L[i, j].ToString() + " ");
                }
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
    }
}
