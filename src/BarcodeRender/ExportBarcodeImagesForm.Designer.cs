namespace BarcodeRender
{
	partial class ExportBarcodeImagesForm
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
			if (disposing && (components != null))
			{
				components.Dispose ();
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
			this.exportRootFolderBrowser = new System.Windows.Forms.FolderBrowserDialog ();
			this.label1 = new System.Windows.Forms.Label ();
			this.rootFolder = new System.Windows.Forms.TextBox ();
			this.browseButton = new System.Windows.Forms.Button ();
			this.overwriteExistingFiles = new System.Windows.Forms.CheckBox ();
			this.flattenHiararchy = new System.Windows.Forms.CheckBox ();
			this.okButton = new System.Windows.Forms.Button ();
			this.cancelButton = new System.Windows.Forms.Button ();
			this.SuspendLayout ();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point (13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size (65, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Root Folder:";
			// 
			// rootFolder
			// 
			this.rootFolder.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rootFolder.Location = new System.Drawing.Point (13, 30);
			this.rootFolder.Name = "rootFolder";
			this.rootFolder.Size = new System.Drawing.Size (322, 20);
			this.rootFolder.TabIndex = 1;
			// 
			// browseButton
			// 
			this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.browseButton.Location = new System.Drawing.Point (341, 28);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size (31, 23);
			this.browseButton.TabIndex = 2;
			this.browseButton.Text = "...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler (this.browseButton_Click);
			// 
			// overwriteExistingFiles
			// 
			this.overwriteExistingFiles.AutoSize = true;
			this.overwriteExistingFiles.Checked = true;
			this.overwriteExistingFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.overwriteExistingFiles.Location = new System.Drawing.Point (13, 57);
			this.overwriteExistingFiles.Name = "overwriteExistingFiles";
			this.overwriteExistingFiles.Size = new System.Drawing.Size (134, 17);
			this.overwriteExistingFiles.TabIndex = 3;
			this.overwriteExistingFiles.Text = "Overwrite Existing Files";
			this.overwriteExistingFiles.UseVisualStyleBackColor = true;
			// 
			// flattenHiararchy
			// 
			this.flattenHiararchy.AutoSize = true;
			this.flattenHiararchy.Location = new System.Drawing.Point (166, 57);
			this.flattenHiararchy.Name = "flattenHiararchy";
			this.flattenHiararchy.Size = new System.Drawing.Size (138, 17);
			this.flattenHiararchy.TabIndex = 3;
			this.flattenHiararchy.Text = "Flatten Folder Hierarchy";
			this.flattenHiararchy.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point (216, 82);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size (75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.CausesValidation = false;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point (297, 82);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size (75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// ExportBarcodeImagesForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size (384, 117);
			this.Controls.Add (this.cancelButton);
			this.Controls.Add (this.okButton);
			this.Controls.Add (this.flattenHiararchy);
			this.Controls.Add (this.overwriteExistingFiles);
			this.Controls.Add (this.browseButton);
			this.Controls.Add (this.rootFolder);
			this.Controls.Add (this.label1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size (600, 153);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size (327, 153);
			this.Name = "ExportBarcodeImagesForm";
			this.Text = "Export Barcode Images";
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog exportRootFolderBrowser;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox rootFolder;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.CheckBox overwriteExistingFiles;
		private System.Windows.Forms.CheckBox flattenHiararchy;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}