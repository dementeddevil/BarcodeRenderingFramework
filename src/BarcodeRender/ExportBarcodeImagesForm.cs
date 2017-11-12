using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarcodeRender
{
	public partial class ExportBarcodeImagesForm : Form
	{
		public ExportBarcodeImagesForm ()
		{
			InitializeComponent ();
		}

		public string RootFolder
		{
			get
			{
				return rootFolder.Text;
			}
			set
			{
				rootFolder.Text = value;
			}
		}

		public bool OverwriteFiles
		{
			get
			{
				return overwriteExistingFiles.Checked;
			}
			set
			{
				overwriteExistingFiles.Checked = value;
			}
		}

		public bool FlattenHierarchy
		{
			get
			{
				return flattenHiararchy.Checked;
			}
			set
			{
				flattenHiararchy.Checked = value;
			}
		}

		private void browseButton_Click (object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (rootFolder.Text.Trim ()))
			{
				exportRootFolderBrowser.SelectedPath = rootFolder.Text;
			}
			else
			{
				exportRootFolderBrowser.RootFolder = Environment.SpecialFolder.Personal;
			}
			if (exportRootFolderBrowser.ShowDialog () == DialogResult.OK)
			{
				rootFolder.Text = exportRootFolderBrowser.SelectedPath;
			}
		}
	}
}