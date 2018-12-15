using System.Drawing;

namespace AprenderIdiomaApp
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.questionStatement = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.questionNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.correctNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // questionStatement
            // 
            this.questionStatement.AutoSize = true;
            this.questionStatement.BackColor = System.Drawing.Color.Salmon;
            this.questionStatement.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionStatement.ForeColor = System.Drawing.SystemColors.Window;
            this.questionStatement.Location = new System.Drawing.Point(223, 173);
            this.questionStatement.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.questionStatement.Name = "questionStatement";
            this.questionStatement.Padding = new System.Windows.Forms.Padding(20);
            this.questionStatement.Size = new System.Drawing.Size(552, 89);
            this.questionStatement.TabIndex = 5;
            this.questionStatement.Text = "Choose a question topic jljljl";
            this.questionStatement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AprenderIdiomaApp.Properties.Resources._2;
            this.pictureBox1.Location = new System.Drawing.Point(265, 295);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.IndianRed;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1008, 110);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.IndianRed;
            this.label1.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 45);
            this.label1.TabIndex = 8;
            this.label1.Text = "LinguApp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.IndianRed;
            this.label3.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(711, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 45);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pregunta:";
            // 
            // questionNumber
            // 
            this.questionNumber.AutoSize = true;
            this.questionNumber.BackColor = System.Drawing.Color.IndianRed;
            this.questionNumber.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionNumber.ForeColor = System.Drawing.SystemColors.Window;
            this.questionNumber.Location = new System.Drawing.Point(920, 9);
            this.questionNumber.Name = "questionNumber";
            this.questionNumber.Size = new System.Drawing.Size(44, 45);
            this.questionNumber.TabIndex = 10;
            this.questionNumber.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.IndianRed;
            this.label2.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(711, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 45);
            this.label2.TabIndex = 11;
            this.label2.Text = "Correctas:";
            // 
            // correctNumber
            // 
            this.correctNumber.AutoSize = true;
            this.correctNumber.BackColor = System.Drawing.Color.IndianRed;
            this.correctNumber.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correctNumber.ForeColor = System.Drawing.SystemColors.Window;
            this.correctNumber.Location = new System.Drawing.Point(920, 54);
            this.correctNumber.Name = "correctNumber";
            this.correctNumber.Size = new System.Drawing.Size(44, 45);
            this.correctNumber.TabIndex = 12;
            this.correctNumber.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1008, 919);
            this.Controls.Add(this.correctNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.questionNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.questionStatement);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label questionStatement;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label questionNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label correctNumber;
    }
}

