namespace CapaPresentacion
{
    partial class frmClientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtindice = new System.Windows.Forms.TextBox();
            this.clean = new FontAwesome.Sharp.IconButton();
            this.btnbuscar = new FontAwesome.Sharp.IconButton();
            this.txtbusqueda = new System.Windows.Forms.TextBox();
            this.cbobusqueda = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btneliminar = new FontAwesome.Sharp.IconButton();
            this.btnlimpiar = new FontAwesome.Sharp.IconButton();
            this.btnguardar = new FontAwesome.Sharp.IconButton();
            this.dgvdata = new System.Windows.Forms.DataGridView();
            this.cboestado = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtnoext = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.txtnombrecompleto = new System.Windows.Forms.TextBox();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtrfc = new System.Windows.Forms.TextBox();
            this.lblrazonsocial = new System.Windows.Forms.Label();
            this.txtcalle = new System.Windows.Forms.TextBox();
            this.labelrfc = new System.Windows.Forms.Label();
            this.txtnoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmunicipio = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcolonia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtpoblacion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtestado = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboregimenfiscal = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnseleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colonia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Calle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoInt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Municipio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Poblacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regimen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtindice
            // 
            this.txtindice.Location = new System.Drawing.Point(183, 6);
            this.txtindice.Margin = new System.Windows.Forms.Padding(4);
            this.txtindice.Name = "txtindice";
            this.txtindice.Size = new System.Drawing.Size(33, 22);
            this.txtindice.TabIndex = 53;
            this.txtindice.Text = "0";
            this.txtindice.Visible = false;
            // 
            // clean
            // 
            this.clean.BackColor = System.Drawing.Color.White;
            this.clean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clean.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.clean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clean.ForeColor = System.Drawing.Color.White;
            this.clean.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.clean.IconColor = System.Drawing.Color.Black;
            this.clean.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.clean.IconSize = 16;
            this.clean.Location = new System.Drawing.Point(804, 4);
            this.clean.Margin = new System.Windows.Forms.Padding(4);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(36, 28);
            this.clean.TabIndex = 52;
            this.clean.UseVisualStyleBackColor = false;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.Color.White;
            this.btnbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscar.ForeColor = System.Drawing.Color.White;
            this.btnbuscar.IconChar = FontAwesome.Sharp.IconChar.SearchLocation;
            this.btnbuscar.IconColor = System.Drawing.Color.Black;
            this.btnbuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscar.IconSize = 16;
            this.btnbuscar.Location = new System.Drawing.Point(760, 4);
            this.btnbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(36, 28);
            this.btnbuscar.TabIndex = 51;
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // txtbusqueda
            // 
            this.txtbusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtbusqueda.Location = new System.Drawing.Point(538, 4);
            this.txtbusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.txtbusqueda.Name = "txtbusqueda";
            this.txtbusqueda.Size = new System.Drawing.Size(170, 22);
            this.txtbusqueda.TabIndex = 50;
            // 
            // cbobusqueda
            // 
            this.cbobusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbobusqueda.FormattingEnabled = true;
            this.cbobusqueda.Location = new System.Drawing.Point(360, 4);
            this.cbobusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.cbobusqueda.Name = "cbobusqueda";
            this.cbobusqueda.Size = new System.Drawing.Size(170, 24);
            this.cbobusqueda.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(271, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 48;
            this.label10.Text = "Buscar por:";
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(225, 7);
            this.txtid.Margin = new System.Windows.Forms.Padding(4);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(33, 22);
            this.txtid.TabIndex = 47;
            this.txtid.Text = "0";
            this.txtid.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(259, 42);
            this.label9.TabIndex = 46;
            this.label9.Text = "Lista de Clientes:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btneliminar
            // 
            this.btneliminar.BackColor = System.Drawing.Color.IndianRed;
            this.btneliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btneliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneliminar.ForeColor = System.Drawing.Color.White;
            this.btneliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.btneliminar.IconColor = System.Drawing.Color.White;
            this.btneliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btneliminar.IconSize = 16;
            this.btneliminar.Location = new System.Drawing.Point(9, 676);
            this.btneliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(410, 28);
            this.btneliminar.TabIndex = 44;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btneliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btneliminar.UseVisualStyleBackColor = false;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // btnlimpiar
            // 
            this.btnlimpiar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnlimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnlimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlimpiar.ForeColor = System.Drawing.Color.White;
            this.btnlimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnlimpiar.IconColor = System.Drawing.Color.White;
            this.btnlimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlimpiar.IconSize = 16;
            this.btnlimpiar.Location = new System.Drawing.Point(9, 636);
            this.btnlimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnlimpiar.Name = "btnlimpiar";
            this.btnlimpiar.Size = new System.Drawing.Size(410, 28);
            this.btnlimpiar.TabIndex = 43;
            this.btnlimpiar.Text = "Limpiar";
            this.btnlimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnlimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlimpiar.UseVisualStyleBackColor = false;
            this.btnlimpiar.Click += new System.EventHandler(this.btnlimpiar_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnguardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnguardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnguardar.ForeColor = System.Drawing.Color.White;
            this.btnguardar.IconChar = FontAwesome.Sharp.IconChar.SdCard;
            this.btnguardar.IconColor = System.Drawing.Color.White;
            this.btnguardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnguardar.IconSize = 16;
            this.btnguardar.Location = new System.Drawing.Point(9, 600);
            this.btnguardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(410, 28);
            this.btnguardar.TabIndex = 42;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnguardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // dgvdata
            // 
            this.dgvdata.AllowUserToAddRows = false;
            this.dgvdata.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnseleccionar,
            this.Id,
            this.Codigo,
            this.NombreCompleto,
            this.RFC,
            this.Correo,
            this.EstadoValor,
            this.Estado,
            this.CP,
            this.Colonia,
            this.Calle,
            this.NoExt,
            this.NoInt,
            this.Municipio,
            this.Poblacion,
            this.Edo,
            this.Regimen});
            this.dgvdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvdata.Location = new System.Drawing.Point(4, 4);
            this.dgvdata.Margin = new System.Windows.Forms.Padding(4);
            this.dgvdata.MultiSelect = false;
            this.dgvdata.Name = "dgvdata";
            this.dgvdata.ReadOnly = true;
            this.dgvdata.RowHeadersWidth = 51;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvdata.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvdata.RowTemplate.Height = 28;
            this.dgvdata.Size = new System.Drawing.Size(885, 670);
            this.dgvdata.TabIndex = 45;
            this.dgvdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdata_CellContentClick);
            this.dgvdata.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvdata_CellPainting);
            // 
            // cboestado
            // 
            this.cboestado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboestado.FormattingEnabled = true;
            this.cboestado.Location = new System.Drawing.Point(108, 494);
            this.cboestado.Margin = new System.Windows.Forms.Padding(4);
            this.cboestado.Name = "cboestado";
            this.cboestado.Size = new System.Drawing.Size(311, 24);
            this.cboestado.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 497);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 40;
            this.label8.Text = "Estatus";
            // 
            // txtnoext
            // 
            this.txtnoext.Location = new System.Drawing.Point(97, 233);
            this.txtnoext.Margin = new System.Windows.Forms.Padding(4);
            this.txtnoext.Name = "txtnoext";
            this.txtnoext.Size = new System.Drawing.Size(322, 22);
            this.txtnoext.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 236);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "NoExt:";
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(84, 194);
            this.txtcorreo.Margin = new System.Windows.Forms.Padding(4);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(335, 22);
            this.txtcorreo.TabIndex = 33;
            // 
            // txtnombrecompleto
            // 
            this.txtnombrecompleto.Location = new System.Drawing.Point(152, 86);
            this.txtnombrecompleto.Margin = new System.Windows.Forms.Padding(4);
            this.txtnombrecompleto.Name = "txtnombrecompleto";
            this.txtnombrecompleto.Size = new System.Drawing.Size(267, 22);
            this.txtnombrecompleto.TabIndex = 32;
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(135, 40);
            this.txtcodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Size = new System.Drawing.Size(284, 22);
            this.txtcodigo.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 200);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Correo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Nombre Completo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Código:";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(431, 721);
            this.label11.TabIndex = 27;
            this.label11.Text = "Detalle Cliente";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbobusqueda, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtbusqueda, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnbuscar, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.clean, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(431, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 43);
            this.tableLayoutPanel1.TabIndex = 54;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dgvdata, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(431, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(893, 678);
            this.tableLayoutPanel2.TabIndex = 55;
            // 
            // txtrfc
            // 
            this.txtrfc.Location = new System.Drawing.Point(123, 119);
            this.txtrfc.Margin = new System.Windows.Forms.Padding(4);
            this.txtrfc.Name = "txtrfc";
            this.txtrfc.Size = new System.Drawing.Size(296, 22);
            this.txtrfc.TabIndex = 57;
            // 
            // lblrazonsocial
            // 
            this.lblrazonsocial.AutoSize = true;
            this.lblrazonsocial.BackColor = System.Drawing.Color.White;
            this.lblrazonsocial.Location = new System.Drawing.Point(3, 122);
            this.lblrazonsocial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblrazonsocial.Name = "lblrazonsocial";
            this.lblrazonsocial.Size = new System.Drawing.Size(37, 16);
            this.lblrazonsocial.TabIndex = 56;
            this.lblrazonsocial.Text = "RFC:";
            // 
            // txtcalle
            // 
            this.txtcalle.Location = new System.Drawing.Point(70, 155);
            this.txtcalle.Margin = new System.Windows.Forms.Padding(4);
            this.txtcalle.Name = "txtcalle";
            this.txtcalle.Size = new System.Drawing.Size(349, 22);
            this.txtcalle.TabIndex = 59;
            // 
            // labelrfc
            // 
            this.labelrfc.AutoSize = true;
            this.labelrfc.BackColor = System.Drawing.Color.White;
            this.labelrfc.Location = new System.Drawing.Point(3, 161);
            this.labelrfc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelrfc.Name = "labelrfc";
            this.labelrfc.Size = new System.Drawing.Size(41, 16);
            this.labelrfc.TabIndex = 58;
            this.labelrfc.Text = "Calle:";
            // 
            // txtnoint
            // 
            this.txtnoint.Location = new System.Drawing.Point(63, 263);
            this.txtnoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtnoint.Name = "txtnoint";
            this.txtnoint.Size = new System.Drawing.Size(356, 22);
            this.txtnoint.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 269);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "NoInt:";
            // 
            // txtmunicipio
            // 
            this.txtmunicipio.Location = new System.Drawing.Point(97, 298);
            this.txtmunicipio.Margin = new System.Windows.Forms.Padding(4);
            this.txtmunicipio.Name = "txtmunicipio";
            this.txtmunicipio.Size = new System.Drawing.Size(322, 22);
            this.txtmunicipio.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 304);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 62;
            this.label6.Text = "Municipio:";
            // 
            // txtcolonia
            // 
            this.txtcolonia.Location = new System.Drawing.Point(97, 330);
            this.txtcolonia.Margin = new System.Windows.Forms.Padding(4);
            this.txtcolonia.Name = "txtcolonia";
            this.txtcolonia.Size = new System.Drawing.Size(322, 22);
            this.txtcolonia.TabIndex = 65;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 336);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 64;
            this.label7.Text = "Colonia:";
            // 
            // txtcp
            // 
            this.txtcp.Location = new System.Drawing.Point(106, 366);
            this.txtcp.Margin = new System.Windows.Forms.Padding(4);
            this.txtcp.Name = "txtcp";
            this.txtcp.Size = new System.Drawing.Size(313, 22);
            this.txtcp.TabIndex = 67;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 372);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 16);
            this.label12.TabIndex = 66;
            this.label12.Text = "Codigo Postal:";
            // 
            // txtpoblacion
            // 
            this.txtpoblacion.Location = new System.Drawing.Point(97, 410);
            this.txtpoblacion.Margin = new System.Windows.Forms.Padding(4);
            this.txtpoblacion.Name = "txtpoblacion";
            this.txtpoblacion.Size = new System.Drawing.Size(322, 22);
            this.txtpoblacion.TabIndex = 69;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(6, 410);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 16);
            this.label13.TabIndex = 68;
            this.label13.Text = "Poblacion:";
            // 
            // txtestado
            // 
            this.txtestado.Location = new System.Drawing.Point(97, 449);
            this.txtestado.Margin = new System.Windows.Forms.Padding(4);
            this.txtestado.Name = "txtestado";
            this.txtestado.Size = new System.Drawing.Size(322, 22);
            this.txtestado.TabIndex = 71;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(6, 455);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 16);
            this.label14.TabIndex = 70;
            this.label14.Text = "Estado:";
            // 
            // cboregimenfiscal
            // 
            this.cboregimenfiscal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboregimenfiscal.FormattingEnabled = true;
            this.cboregimenfiscal.Location = new System.Drawing.Point(9, 552);
            this.cboregimenfiscal.Margin = new System.Windows.Forms.Padding(4);
            this.cboregimenfiscal.Name = "cboregimenfiscal";
            this.cboregimenfiscal.Size = new System.Drawing.Size(410, 24);
            this.cboregimenfiscal.TabIndex = 73;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(6, 530);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 16);
            this.label15.TabIndex = 72;
            this.label15.Text = "Regimen Fiscal:";
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.HeaderText = "";
            this.btnseleccionar.MinimumWidth = 6;
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.ReadOnly = true;
            this.btnseleccionar.Width = 30;
            // 
            // Id
            // 
            this.Id.HeaderText = "IdUsuario";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 125;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.MinimumWidth = 6;
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 150;
            // 
            // NombreCompleto
            // 
            this.NombreCompleto.HeaderText = "Nombre Completo";
            this.NombreCompleto.MinimumWidth = 6;
            this.NombreCompleto.Name = "NombreCompleto";
            this.NombreCompleto.ReadOnly = true;
            this.NombreCompleto.Width = 180;
            // 
            // RFC
            // 
            this.RFC.HeaderText = "RFC";
            this.RFC.MinimumWidth = 6;
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 125;
            // 
            // Correo
            // 
            this.Correo.HeaderText = "Correo";
            this.Correo.MinimumWidth = 6;
            this.Correo.Name = "Correo";
            this.Correo.ReadOnly = true;
            this.Correo.Width = 150;
            // 
            // EstadoValor
            // 
            this.EstadoValor.HeaderText = "EstadoValor";
            this.EstadoValor.MinimumWidth = 6;
            this.EstadoValor.Name = "EstadoValor";
            this.EstadoValor.ReadOnly = true;
            this.EstadoValor.Visible = false;
            this.EstadoValor.Width = 125;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 125;
            // 
            // CP
            // 
            this.CP.HeaderText = "CP";
            this.CP.MinimumWidth = 6;
            this.CP.Name = "CP";
            this.CP.ReadOnly = true;
            this.CP.Width = 125;
            // 
            // Colonia
            // 
            this.Colonia.HeaderText = "Colonia";
            this.Colonia.MinimumWidth = 6;
            this.Colonia.Name = "Colonia";
            this.Colonia.ReadOnly = true;
            this.Colonia.Width = 125;
            // 
            // Calle
            // 
            this.Calle.HeaderText = "Calle";
            this.Calle.MinimumWidth = 6;
            this.Calle.Name = "Calle";
            this.Calle.ReadOnly = true;
            this.Calle.Width = 125;
            // 
            // NoExt
            // 
            this.NoExt.HeaderText = "NoExt";
            this.NoExt.MinimumWidth = 6;
            this.NoExt.Name = "NoExt";
            this.NoExt.ReadOnly = true;
            this.NoExt.Width = 125;
            // 
            // NoInt
            // 
            this.NoInt.HeaderText = "NoInt";
            this.NoInt.MinimumWidth = 6;
            this.NoInt.Name = "NoInt";
            this.NoInt.ReadOnly = true;
            this.NoInt.Width = 125;
            // 
            // Municipio
            // 
            this.Municipio.HeaderText = "Municipio";
            this.Municipio.MinimumWidth = 6;
            this.Municipio.Name = "Municipio";
            this.Municipio.ReadOnly = true;
            this.Municipio.Width = 125;
            // 
            // Poblacion
            // 
            this.Poblacion.HeaderText = "Población";
            this.Poblacion.MinimumWidth = 6;
            this.Poblacion.Name = "Poblacion";
            this.Poblacion.ReadOnly = true;
            this.Poblacion.Width = 125;
            // 
            // Edo
            // 
            this.Edo.HeaderText = "Estado";
            this.Edo.MinimumWidth = 6;
            this.Edo.Name = "Edo";
            this.Edo.ReadOnly = true;
            this.Edo.Width = 125;
            // 
            // Regimen
            // 
            this.Regimen.HeaderText = "Regimen Fiscal";
            this.Regimen.MinimumWidth = 6;
            this.Regimen.Name = "Regimen";
            this.Regimen.ReadOnly = true;
            this.Regimen.Width = 125;
            // 
            // frmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 721);
            this.Controls.Add(this.cboregimenfiscal);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtestado);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtpoblacion);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtcp);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtcolonia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtmunicipio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtnoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcalle);
            this.Controls.Add(this.labelrfc);
            this.Controls.Add(this.txtrfc);
            this.Controls.Add(this.lblrazonsocial);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtindice);
            this.Controls.Add(this.txtid);
            this.Controls.Add(this.btneliminar);
            this.Controls.Add(this.btnlimpiar);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.cboestado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtnoext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.txtnombrecompleto);
            this.Controls.Add(this.txtcodigo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmClientes";
            this.Text = "frmClientes";
            this.Load += new System.EventHandler(this.frmClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtindice;
        private FontAwesome.Sharp.IconButton clean;
        private FontAwesome.Sharp.IconButton btnbuscar;
        private System.Windows.Forms.TextBox txtbusqueda;
        private System.Windows.Forms.ComboBox cbobusqueda;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton btneliminar;
        private FontAwesome.Sharp.IconButton btnlimpiar;
        private FontAwesome.Sharp.IconButton btnguardar;
        private System.Windows.Forms.DataGridView dgvdata;
        private System.Windows.Forms.ComboBox cboestado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtnoext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.TextBox txtnombrecompleto;
        private System.Windows.Forms.TextBox txtcodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtrfc;
        private System.Windows.Forms.Label lblrazonsocial;
        private System.Windows.Forms.TextBox txtcalle;
        private System.Windows.Forms.Label labelrfc;
        private System.Windows.Forms.TextBox txtnoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtmunicipio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcolonia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtcp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtpoblacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtestado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboregimenfiscal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewButtonColumn btnseleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Correo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colonia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Calle;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoInt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Municipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Poblacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Edo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regimen;
    }
}