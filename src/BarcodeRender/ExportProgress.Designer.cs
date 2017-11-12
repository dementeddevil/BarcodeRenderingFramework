namespace BarcodeRender
{
	partial class ExportProgress
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
			this.exportOperation = new System.Windows.Forms.Label ();
			this.progressBar = new System.Windows.Forms.ProgressBar ();
			this.operationDetail = new System.Windows.Forms.Label ();
			this.cancelButton = new System.Windows.Forms.Button ();
			this.exportWorker = new System.ComponentModel.BackgroundWorker ();
			this.SuspendLayout ();
			// 
			// exportOperation
			// 
			this.exportOperation.AutoSize = true;
			this.exportOperation.Location = new System.Drawing.Point (12, 9);
			this.exportOperation.Name = "exportOperation";
			this.exportOperation.Size = new System.Drawing.Size (92, 13);
			this.exportOperation.TabIndex = 0;
			this.exportOperation.Text = "(Export Operation)";
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point (13, 26);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size (359, 23);
			this.progressBar.TabIndex = 1;
			// 
			// operationDetail
			// 
			this.operationDetail.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.operationDetail.Location = new System.Drawing.Point (15, 56);
			this.operationDetail.Name = "operationDetail";
			this.operationDetail.Size = new System.Drawing.Size (266, 49);
			this.operationDetail.TabIndex = 2;
			this.operationDetail.Text = "(Operation Detail)";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point (297, 82);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size (75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler (this.cancelButton_Click);
			// 
			// exportWorker
			// 
			this.exportWorker.WorkerReportsProgress = true;
			this.exportWorker.WorkerSupportsCancellation = true;
			this.exportWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (this.exportWorker_DoWork);
			this.exportWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (this.exportWorker_RunWorkerCompleted);
			// 
			// ExportProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (384, 117);
			this.Controls.Add (this.cancelButton);
			this.Controls.Add (this.operationDetail);
			this.Controls.Add (this.progressBar);
			this.Controls.Add (this.exportOperation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ExportProgress";
			this.ShowInTaskbar = false;
			this.Text = "Exporting Images";
			this.TopMost = true;
			this.ResumeLayout (false);
			this.PerformLayout ();

		}

		#endregion

		private System.Windows.Forms.Label exportOperation;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label operationDetail;
		private System.Windows.Forms.Button cancelButton;
		private System.ComponentModel.BackgroundWorker exportWorker;
	}
}