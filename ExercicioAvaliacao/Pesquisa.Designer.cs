namespace ExercicioAvaliacao
{
    partial class Pesquisa
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
            this.dgwPesquisa = new System.Windows.Forms.DataGridView();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPesquisa)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwPesquisa
            // 
            this.dgwPesquisa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPesquisa.Location = new System.Drawing.Point(27, 100);
            this.dgwPesquisa.Name = "dgwPesquisa";
            this.dgwPesquisa.Size = new System.Drawing.Size(725, 317);
            this.dgwPesquisa.TabIndex = 0;
            this.dgwPesquisa.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwPesquisa_CellDoubleClick);
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.Location = new System.Drawing.Point(179, 38);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(417, 20);
            this.txtPesquisar.TabIndex = 1;
            this.txtPesquisar.TextChanged += new System.EventHandler(this.txtPesquisar_TextChanged);
            // 
            // Pesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtPesquisar);
            this.Controls.Add(this.dgwPesquisa);
            this.Name = "Pesquisa";
            this.Text = "Pesquisa";
            ((System.ComponentModel.ISupportInitialize)(this.dgwPesquisa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwPesquisa;
        private System.Windows.Forms.TextBox txtPesquisar;
    }
}