namespace Almacen_El_Dorado
{
    partial class FrmMovimientos
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
            this.gbTipoMovimiento = new System.Windows.Forms.GroupBox();
            this.rbSalida = new System.Windows.Forms.RadioButton();
            this.rbEntrada = new System.Windows.Forms.RadioButton();
            this.gbDatosMovimiento = new System.Windows.Forms.GroupBox();
            this.btnLimpiarMov = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.gbHistorial = new System.Windows.Forms.GroupBox();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            this.gbTipoMovimiento.SuspendLayout();
            this.gbDatosMovimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.gbHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTipoMovimiento
            // 
            this.gbTipoMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.gbTipoMovimiento.Controls.Add(this.rbSalida);
            this.gbTipoMovimiento.Controls.Add(this.rbEntrada);
            this.gbTipoMovimiento.Font = new System.Drawing.Font("Monotype Corsiva", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTipoMovimiento.Location = new System.Drawing.Point(12, 9);
            this.gbTipoMovimiento.Name = "gbTipoMovimiento";
            this.gbTipoMovimiento.Size = new System.Drawing.Size(219, 144);
            this.gbTipoMovimiento.TabIndex = 0;
            this.gbTipoMovimiento.TabStop = false;
            this.gbTipoMovimiento.Text = "Tipo de MOvimiento";
            this.gbTipoMovimiento.Enter += new System.EventHandler(this.gbTipoMovimiento_Enter);
            // 
            // rbSalida
            // 
            this.rbSalida.AutoSize = true;
            this.rbSalida.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbSalida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbSalida.Font = new System.Drawing.Font("Monotype Corsiva", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSalida.Location = new System.Drawing.Point(6, 87);
            this.rbSalida.Name = "rbSalida";
            this.rbSalida.Size = new System.Drawing.Size(105, 38);
            this.rbSalida.TabIndex = 1;
            this.rbSalida.TabStop = true;
            this.rbSalida.Text = "Salida";
            this.rbSalida.UseVisualStyleBackColor = false;
            this.rbSalida.CheckedChanged += new System.EventHandler(this.rbSalida_CheckedChanged);
            // 
            // rbEntrada
            // 
            this.rbEntrada.AutoSize = true;
            this.rbEntrada.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbEntrada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbEntrada.Font = new System.Drawing.Font("Monotype Corsiva", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEntrada.Location = new System.Drawing.Point(6, 33);
            this.rbEntrada.Name = "rbEntrada";
            this.rbEntrada.Size = new System.Drawing.Size(126, 38);
            this.rbEntrada.TabIndex = 0;
            this.rbEntrada.TabStop = true;
            this.rbEntrada.Text = "Entrada";
            this.rbEntrada.UseVisualStyleBackColor = false;
            this.rbEntrada.CheckedChanged += new System.EventHandler(this.rbEntrada_CheckedChanged);
            // 
            // gbDatosMovimiento
            // 
            this.gbDatosMovimiento.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbDatosMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.gbDatosMovimiento.Controls.Add(this.btnLimpiarMov);
            this.gbDatosMovimiento.Controls.Add(this.btnRegistrar);
            this.gbDatosMovimiento.Controls.Add(this.lblStockActual);
            this.gbDatosMovimiento.Controls.Add(this.nudCantidad);
            this.gbDatosMovimiento.Controls.Add(this.lblCantidad);
            this.gbDatosMovimiento.Controls.Add(this.cmbProducto);
            this.gbDatosMovimiento.Controls.Add(this.lblProducto);
            this.gbDatosMovimiento.Font = new System.Drawing.Font("Monotype Corsiva", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosMovimiento.Location = new System.Drawing.Point(328, 23);
            this.gbDatosMovimiento.Name = "gbDatosMovimiento";
            this.gbDatosMovimiento.Size = new System.Drawing.Size(549, 269);
            this.gbDatosMovimiento.TabIndex = 1;
            this.gbDatosMovimiento.TabStop = false;
            this.gbDatosMovimiento.Text = "Datos del Movimiento";
            this.gbDatosMovimiento.Enter += new System.EventHandler(this.gbDatosMovimiento_Enter);
            // 
            // btnLimpiarMov
            // 
            this.btnLimpiarMov.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnLimpiarMov.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarMov.Location = new System.Drawing.Point(215, 230);
            this.btnLimpiarMov.Name = "btnLimpiarMov";
            this.btnLimpiarMov.Size = new System.Drawing.Size(111, 33);
            this.btnLimpiarMov.TabIndex = 6;
            this.btnLimpiarMov.Text = "Limpiar";
            this.btnLimpiarMov.UseVisualStyleBackColor = false;
            this.btnLimpiarMov.Click += new System.EventHandler(this.btnLimpiarMov_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Location = new System.Drawing.Point(192, 189);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(157, 35);
            this.btnRegistrar.TabIndex = 5;
            this.btnRegistrar.Text = "Registrar Movimiento";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblStockActual.Location = new System.Drawing.Point(6, 150);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(132, 24);
            this.lblStockActual.TabIndex = 4;
            this.lblStockActual.Text = "Stock Actual:--";
            this.lblStockActual.Click += new System.EventHandler(this.lblStockActual_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.BackColor = System.Drawing.Color.LemonChiffon;
            this.nudCantidad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nudCantidad.Location = new System.Drawing.Point(215, 73);
            this.nudCantidad.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(230, 30);
            this.nudCantidad.TabIndex = 3;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblCantidad.Location = new System.Drawing.Point(50, 72);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(88, 24);
            this.lblCantidad.TabIndex = 2;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // cmbProducto
            // 
            this.cmbProducto.BackColor = System.Drawing.Color.LemonChiffon;
            this.cmbProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(215, 26);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(328, 32);
            this.cmbProducto.TabIndex = 1;
            this.cmbProducto.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged);
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblProducto.Location = new System.Drawing.Point(50, 33);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(88, 24);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
            // 
            // gbHistorial
            // 
            this.gbHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHistorial.BackColor = System.Drawing.Color.Transparent;
            this.gbHistorial.Controls.Add(this.dgvMovimientos);
            this.gbHistorial.Location = new System.Drawing.Point(131, 298);
            this.gbHistorial.Name = "gbHistorial";
            this.gbHistorial.Size = new System.Drawing.Size(857, 375);
            this.gbHistorial.TabIndex = 2;
            this.gbHistorial.TabStop = false;
            this.gbHistorial.Text = "Historial de Movimientos";
            this.gbHistorial.Enter += new System.EventHandler(this.gbHistorial_Enter);
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Location = new System.Drawing.Point(14, 21);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.RowHeadersWidth = 51;
            this.dgvMovimientos.RowTemplate.Height = 24;
            this.dgvMovimientos.Size = new System.Drawing.Size(829, 337);
            this.dgvMovimientos.TabIndex = 0;
            this.dgvMovimientos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovimientos_CellContentClick);
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.Image = global::Almacen_El_Dorado.Properties.Resources.VolverN1;
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(939, 31);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(95, 35);
            this.btnVolver.TabIndex = 7;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FrmMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Almacen_El_Dorado.Properties.Resources.fondodesert1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1076, 702);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.gbHistorial);
            this.Controls.Add(this.gbDatosMovimiento);
            this.Controls.Add(this.gbTipoMovimiento);
            this.DoubleBuffered = true;
            this.Name = "FrmMovimientos";
            this.Text = "FrmMovimientos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMovimientos_Load);
            this.gbTipoMovimiento.ResumeLayout(false);
            this.gbTipoMovimiento.PerformLayout();
            this.gbDatosMovimiento.ResumeLayout(false);
            this.gbDatosMovimiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.gbHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTipoMovimiento;
        private System.Windows.Forms.RadioButton rbEntrada;
        private System.Windows.Forms.RadioButton rbSalida;
        private System.Windows.Forms.GroupBox gbDatosMovimiento;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Button btnLimpiarMov;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox gbHistorial;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.Button btnVolver;
    }
}