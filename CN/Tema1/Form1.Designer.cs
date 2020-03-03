namespace Tema1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ex1_res = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ex2_res = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.ex3_res = new System.Windows.Forms.Label();
            this.matrixA = new System.Windows.Forms.Label();
            this.matrixB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exercitul 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ex1_res
            // 
            this.ex1_res.AutoSize = true;
            this.ex1_res.Location = new System.Drawing.Point(173, 41);
            this.ex1_res.Name = "ex1_res";
            this.ex1_res.Size = new System.Drawing.Size(0, 17);
            this.ex1_res.TabIndex = 1;
            this.ex1_res.Click += new System.EventHandler(this.ex1_res_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 101);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 41);
            this.button2.TabIndex = 2;
            this.button2.Text = "Exercitiul 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ex2_res
            // 
            this.ex2_res.AutoSize = true;
            this.ex2_res.Location = new System.Drawing.Point(170, 109);
            this.ex2_res.Name = "ex2_res";
            this.ex2_res.Size = new System.Drawing.Size(0, 17);
            this.ex2_res.TabIndex = 3;
            this.ex2_res.Click += new System.EventHandler(this.ex2_res_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(40, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 45);
            this.button3.TabIndex = 4;
            this.button3.Text = "Exercitiul 3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ex3_res
            // 
            this.ex3_res.AutoSize = true;
            this.ex3_res.Location = new System.Drawing.Point(433, 187);
            this.ex3_res.Name = "ex3_res";
            this.ex3_res.Size = new System.Drawing.Size(0, 17);
            this.ex3_res.TabIndex = 5;
            this.ex3_res.Click += new System.EventHandler(this.ex3_res_Click);
            // 
            // matrixA
            // 
            this.matrixA.AutoSize = true;
            this.matrixA.Location = new System.Drawing.Point(184, 182);
            this.matrixA.Name = "matrixA";
            this.matrixA.Size = new System.Drawing.Size(0, 17);
            this.matrixA.TabIndex = 6;
            // 
            // matrixB
            // 
            this.matrixB.AutoSize = true;
            this.matrixB.Location = new System.Drawing.Point(196, 314);
            this.matrixB.Name = "matrixB";
            this.matrixB.Size = new System.Drawing.Size(0, 17);
            this.matrixB.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 523);
            this.Controls.Add(this.matrixB);
            this.Controls.Add(this.matrixA);
            this.Controls.Add(this.ex3_res);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ex2_res);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ex1_res);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ex1_res;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label ex2_res;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label ex3_res;
        private System.Windows.Forms.Label matrixA;
        private System.Windows.Forms.Label matrixB;
    }
}

