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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.questionNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.correctNumber = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.questionStatement = new System.Windows.Forms.Label();
            this.options = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "LINGUApp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Teal;
            this.label3.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(356, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pregunta:";
            // 
            // questionNumber
            // 
            this.questionNumber.AutoSize = true;
            this.questionNumber.BackColor = System.Drawing.Color.Teal;
            this.questionNumber.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionNumber.ForeColor = System.Drawing.SystemColors.Window;
            this.questionNumber.Location = new System.Drawing.Point(460, 5);
            this.questionNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.questionNumber.Name = "questionNumber";
            this.questionNumber.Size = new System.Drawing.Size(22, 23);
            this.questionNumber.TabIndex = 10;
            this.questionNumber.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Teal;
            this.label2.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(356, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Correctas:";
            // 
            // correctNumber
            // 
            this.correctNumber.AutoSize = true;
            this.correctNumber.BackColor = System.Drawing.Color.Teal;
            this.correctNumber.Font = new System.Drawing.Font("Verdana", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correctNumber.ForeColor = System.Drawing.SystemColors.Window;
            this.correctNumber.Location = new System.Drawing.Point(460, 28);
            this.correctNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.correctNumber.Name = "correctNumber";
            this.correctNumber.Size = new System.Drawing.Size(22, 23);
            this.correctNumber.TabIndex = 12;
            this.correctNumber.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = global::AprenderIdiomaApp.Properties.Resources.globe;
            this.pictureBox1.Location = new System.Drawing.Point(105, 112);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(325, 286);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(284, 278);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Teal;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(492, 57);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // questionStatement
            // 
            this.questionStatement.BackColor = System.Drawing.Color.Teal;
            this.questionStatement.Dock = System.Windows.Forms.DockStyle.Top;
            this.questionStatement.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionStatement.ForeColor = System.Drawing.SystemColors.Window;
            this.questionStatement.Location = new System.Drawing.Point(0, 57);
            this.questionStatement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.questionStatement.Name = "questionStatement";
            this.questionStatement.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.questionStatement.Size = new System.Drawing.Size(492, 46);
            this.questionStatement.TabIndex = 5;
            this.questionStatement.Text = "Choose a question topic";
            this.questionStatement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // options
            // 
            this.options.BackColor = System.Drawing.Color.CadetBlue;
            this.options.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.options.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.options.ForeColor = System.Drawing.SystemColors.Window;
            this.options.Location = new System.Drawing.Point(0, 411);
            this.options.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.options.Name = "options";
            this.options.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.options.Size = new System.Drawing.Size(492, 46);
            this.options.TabIndex = 13;
            this.options.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(492, 457);
            this.Controls.Add(this.options);
            this.Controls.Add(this.questionStatement);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.correctNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.questionNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.MaximumSize = new System.Drawing.Size(508, 539);
            this.MinimumSize = new System.Drawing.Size(508, 496);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label questionNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label correctNumber;
        private System.Windows.Forms.Label questionStatement;
        private System.Windows.Forms.Label options;
    }
}

