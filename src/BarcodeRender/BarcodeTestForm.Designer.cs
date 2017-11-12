namespace BarcodeRender
{
	partial class BarcodeTestForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose ();
					components = null;
				}
				if (_barcodeFont != null)
				{
					_barcodeFont.Dispose ();
					_barcodeFont = null;
				}
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (BarcodeTestForm));
			this.barcodeDocument = new System.Drawing.Printing.PrintDocument ();
			this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog ();
			this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog ();
			this.printDialog = new System.Windows.Forms.PrintDialog ();
			this.groupBox2 = new System.Windows.Forms.GroupBox ();
			this.testSymbology = new System.Windows.Forms.ComboBox ();
			this.label5 = new System.Windows.Forms.Label ();
			this.scannerResult = new System.Windows.Forms.TextBox ();
			this.label2 = new System.Windows.Forms.Label ();
			this.barcodePanel = new BarcodeRender.BarcodePanel ();
			this.barcodeLabel = new System.Windows.Forms.TextBox ();
			this.label1 = new System.Windows.Forms.Label ();
			this.nextTestButton = new System.Windows.Forms.Button ();
			this.previousTestButton = new System.Windows.Forms.Button ();
			this.mainMenu = new System.Windows.Forms.MenuStrip ();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator ();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator ();
			this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator ();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator ();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator ();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator ();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator ();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.groupBox1 = new System.Windows.Forms.GroupBox ();
			this.testTree = new System.Windows.Forms.TreeView ();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog ();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog ();
			this.exportImageFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog ();
			this.groupBox2.SuspendLayout ();
			this.mainMenu.SuspendLayout ();
			this.groupBox1.SuspendLayout ();
			this.SuspendLayout ();
			// 
			// barcodeDocument
			// 
			this.barcodeDocument.DocumentName = "Barcode Test Document";
			this.barcodeDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler (this.barcodeDocument_PrintPage);
			this.barcodeDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler (this.barcodeDocument_EndPrint);
			this.barcodeDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler (this.barcodeDocument_BeginPrint);
			// 
			// pageSetupDialog
			// 
			this.pageSetupDialog.Document = this.barcodeDocument;
			// 
			// printPreviewDialog
			// 
			this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size (0, 0);
			this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size (0, 0);
			this.printPreviewDialog.ClientSize = new System.Drawing.Size (400, 300);
			this.printPreviewDialog.Document = this.barcodeDocument;
			this.printPreviewDialog.Enabled = true;
			this.printPreviewDialog.Icon = ((System.Drawing.Icon) (resources.GetObject ("printPreviewDialog.Icon")));
			this.printPreviewDialog.Name = "printPreviewDialog";
			this.printPreviewDialog.Visible = false;
			// 
			// printDialog
			// 
			this.printDialog.Document = this.barcodeDocument;
			this.printDialog.UseEXDialog = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add (this.testSymbology);
			this.groupBox2.Controls.Add (this.label5);
			this.groupBox2.Controls.Add (this.scannerResult);
			this.groupBox2.Controls.Add (this.label2);
			this.groupBox2.Controls.Add (this.barcodePanel);
			this.groupBox2.Controls.Add (this.barcodeLabel);
			this.groupBox2.Controls.Add (this.label1);
			this.groupBox2.Controls.Add (this.nextTestButton);
			this.groupBox2.Controls.Add (this.previousTestButton);
			this.groupBox2.Location = new System.Drawing.Point (260, 39);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size (422, 276);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Test Area";
			// 
			// testSymbology
			// 
			this.testSymbology.FormattingEnabled = true;
			this.testSymbology.Items.AddRange (new object[] {
            "Code 39 (No Checksum)",
            "Code 39 (With Checksum)",
            "Code 93 (With Checksum)",
            "Code 128 (With Checksum)",
            "Code 11 (No Checksum)",
            "Code 11 (With Checksum)",
            "Code EAN-13 (With Checksum)",
            "Code EAN-8 (With Checksum)",
            "Code 25 Standard (No Checksum)",
            "Code 25 Standard (With Checksum)",
            "Code 25 Interleaved (No Checksum)",
            "Code 25 Interleaved (With Checksum)",
            "Code PDF417 (With Checksum)"});
			this.testSymbology.Location = new System.Drawing.Point (160, 57);
			this.testSymbology.Name = "testSymbology";
			this.testSymbology.Size = new System.Drawing.Size (191, 21);
			this.testSymbology.TabIndex = 10;
			this.testSymbology.SelectedIndexChanged += new System.EventHandler (this.symbology_SelectedIndexChanged);
			this.testSymbology.Leave += new System.EventHandler (this.symbology_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point (92, 61);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size (61, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Symbology:";
			// 
			// scannerResult
			// 
			this.scannerResult.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.scannerResult.Location = new System.Drawing.Point (160, 236);
			this.scannerResult.Name = "scannerResult";
			this.scannerResult.Size = new System.Drawing.Size (191, 20);
			this.scannerResult.TabIndex = 5;
			this.scannerResult.KeyDown += new System.Windows.Forms.KeyEventHandler (this.scannerResult_KeyDown);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point (70, 239);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size (83, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Scanner Result:";
			// 
			// barcodePanel
			// 
			this.barcodePanel.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.barcodePanel.BackColor = System.Drawing.Color.White;
			this.barcodePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.barcodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.barcodePanel.Location = new System.Drawing.Point (6, 129);
			this.barcodePanel.Name = "barcodePanel";
			this.barcodePanel.Size = new System.Drawing.Size (410, 88);
			this.barcodePanel.Symbology = Zen.Barcode.BarcodeSymbology.Unknown;
			this.barcodePanel.TabIndex = 3;
			// 
			// barcodeLabel
			// 
			this.barcodeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.barcodeLabel.Location = new System.Drawing.Point (160, 92);
			this.barcodeLabel.Name = "barcodeLabel";
			this.barcodeLabel.Size = new System.Drawing.Size (191, 20);
			this.barcodeLabel.TabIndex = 2;
			this.barcodeLabel.TextChanged += new System.EventHandler (this.barcodeLabel_TextChanged);
			this.barcodeLabel.Leave += new System.EventHandler (this.barcodeLabel_Leave);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point (79, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (74, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Barcode Text:";
			// 
			// nextTestButton
			// 
			this.nextTestButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nextTestButton.Location = new System.Drawing.Point (341, 20);
			this.nextTestButton.Name = "nextTestButton";
			this.nextTestButton.Size = new System.Drawing.Size (75, 23);
			this.nextTestButton.TabIndex = 0;
			this.nextTestButton.Text = "Next";
			this.nextTestButton.UseVisualStyleBackColor = true;
			this.nextTestButton.Click += new System.EventHandler (this.nextTestButton_Click);
			// 
			// previousTestButton
			// 
			this.previousTestButton.Location = new System.Drawing.Point (6, 20);
			this.previousTestButton.Name = "previousTestButton";
			this.previousTestButton.Size = new System.Drawing.Size (75, 23);
			this.previousTestButton.TabIndex = 0;
			this.previousTestButton.Text = "Previous";
			this.previousTestButton.UseVisualStyleBackColor = true;
			this.previousTestButton.Click += new System.EventHandler (this.previousTestButton_Click);
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point (0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size (694, 24);
			this.mainMenu.TabIndex = 2;
			this.mainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.pageSetupToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size (37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.newToolStripMenuItem.Text = "&New Test Plan";
			this.newToolStripMenuItem.Click += new System.EventHandler (this.OnNewTestPlan);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.openToolStripMenuItem.Text = "&Open Test Plan";
			this.openToolStripMenuItem.Click += new System.EventHandler (this.OnOpenTestPlan);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size (200, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.saveToolStripMenuItem.Text = "&Save Test Plan";
			this.saveToolStripMenuItem.Click += new System.EventHandler (this.OnSaveTestPlan);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.saveAsToolStripMenuItem.Text = "Save Test Plan &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler (this.OnSaveTestPlanAs);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size (200, 6);
			// 
			// exportImageToolStripMenuItem
			// 
			this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
			this.exportImageToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.exportImageToolStripMenuItem.Text = "Export Barcode I&mages...";
			this.exportImageToolStripMenuItem.Click += new System.EventHandler (this.exportImageToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size (200, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("printToolStripMenuItem.Image")));
			this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.printToolStripMenuItem.Text = "&Print";
			this.printToolStripMenuItem.Click += new System.EventHandler (this.OnPrint);
			// 
			// pageSetupToolStripMenuItem
			// 
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.pageSetupToolStripMenuItem.Text = "Page Set&up";
			this.pageSetupToolStripMenuItem.Click += new System.EventHandler (this.OnPageSetup);
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("printPreviewToolStripMenuItem.Image")));
			this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			this.printPreviewToolStripMenuItem.Click += new System.EventHandler (this.OnPrintPreview);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size (200, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size (203, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator4,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size (39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size (141, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image) (resources.GetObject ("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size (141, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size (144, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size (48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// customizeToolStripMenuItem
			// 
			this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
			this.customizeToolStripMenuItem.Size = new System.Drawing.Size (130, 22);
			this.customizeToolStripMenuItem.Text = "&Customize";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size (130, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size (44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size (122, 22);
			this.contentsToolStripMenuItem.Text = "&Contents";
			// 
			// indexToolStripMenuItem
			// 
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size (122, 22);
			this.indexToolStripMenuItem.Text = "&Index";
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size (122, 22);
			this.searchToolStripMenuItem.Text = "&Search";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size (119, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size (122, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add (this.testTree);
			this.groupBox1.Location = new System.Drawing.Point (12, 39);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size (242, 276);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Test Plan";
			// 
			// testTree
			// 
			this.testTree.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.testTree.HideSelection = false;
			this.testTree.Location = new System.Drawing.Point (7, 20);
			this.testTree.Name = "testTree";
			this.testTree.Size = new System.Drawing.Size (229, 247);
			this.testTree.TabIndex = 0;
			this.testTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler (this.testTree_AfterSelect);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "btp";
			this.openFileDialog.FileName = "mytest";
			this.openFileDialog.Filter = "Barcode Test Plan files|*.btp|All files|*.*";
			this.openFileDialog.Title = "Open Barcode Test Plan";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "btp";
			this.saveFileDialog.Filter = "Barcode Test Plan files|*.btp|All files|*.*";
			this.saveFileDialog.Title = "Save Barcode Test Plan";
			// 
			// exportImageFolderBrowserDialog
			// 
			this.exportImageFolderBrowserDialog.Description = "Select the folder to contain the exported images.";
			this.exportImageFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Personal;
			// 
			// BarcodeTestForm
			// 
			this.ClientSize = new System.Drawing.Size (694, 327);
			this.Controls.Add (this.groupBox1);
			this.Controls.Add (this.groupBox2);
			this.Controls.Add (this.mainMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MainMenuStrip = this.mainMenu;
			this.MaximizeBox = false;
			this.Name = "BarcodeTestForm";
			this.Text = "Printable Barcode Test";
			this.groupBox2.ResumeLayout (false);
			this.groupBox2.PerformLayout ();
			this.mainMenu.ResumeLayout (false);
			this.mainMenu.PerformLayout ();
			this.groupBox1.ResumeLayout (false);
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Drawing.Printing.PrintDocument barcodeDocument;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
		private System.Windows.Forms.PrintDialog printDialog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox scannerResult;
		private System.Windows.Forms.Label label2;
		private BarcodePanel barcodePanel;
		private System.Windows.Forms.TextBox barcodeLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button nextTestButton;
		private System.Windows.Forms.Button previousTestButton;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TreeView testTree;
		private System.Windows.Forms.ComboBox testSymbology;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog exportImageFolderBrowserDialog;
	}
}