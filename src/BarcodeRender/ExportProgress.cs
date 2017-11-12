using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BarcodeRender
{
	public partial class ExportProgress : Form
	{
		private bool _cancelled;
		private bool _finished;

		public ExportProgress ()
		{
			InitializeComponent ();
		}

		private class ExportWorkerState
		{
			public SymbologyTestPlan TestPlan;
			public string Folder;
			public bool OverwriteFiles;
			public bool FlattenHierarchy;
			public int MaxBarHeight;
		}

		public static void Start (SymbologyTestPlan testPlan,
			string rootFolder, bool overwriteFiles,
			bool flattenHierarchy)
		{
			Start (testPlan, rootFolder, overwriteFiles,
				flattenHierarchy, 30);
		}

		public static void Start (SymbologyTestPlan testPlan, 
			string rootFolder, bool overwriteFiles, 
			bool flattenHierarchy, int maxBarHeight)
		{
			ExportWorkerState state = new ExportWorkerState ();
			state.TestPlan = testPlan;
			state.Folder = rootFolder;
			state.OverwriteFiles = overwriteFiles;
			state.FlattenHierarchy = flattenHierarchy;
			state.MaxBarHeight = maxBarHeight;

			new Thread (new ParameterizedThreadStart (WorkerThread)).Start (state);
		}

		private static void WorkerThread (object state)
		{
			// Start our background worker
			ExportProgress progressForm = new ExportProgress ();
			progressForm.exportWorker.RunWorkerAsync (state);

			// Run our message loop
			Application.Run (progressForm);

			// Ensure the worker has cancelled
			progressForm.exportWorker.CancelAsync ();
		}

		private void exportWorker_DoWork (object sender, DoWorkEventArgs e)
		{
			ExportWorkerState state = (ExportWorkerState) e.Argument;
			state.TestPlan.ExportImages (state.Folder, state.OverwriteFiles,
				state.FlattenHierarchy, state.MaxBarHeight,
				delegate (int done, int total, string operation, string detail)
				{
					// Check for cancellation
					if (e.Cancel)
					{
						return false;
					}

					// Report progress change to worker
					if (total > 0)
					{
						if (done < total)
						{
							exportWorker.ReportProgress ((int) (((float) done / (float) total) * 100.0f));
						}
						else
						{
							exportWorker.ReportProgress (100);
						}
					}

					// Update the progress
					return UpdateProgress (done, total, operation, detail);
				});
			_finished = true;
		}

		private void exportWorker_ProgressChanged (object sender, ProgressChangedEventArgs e)
		{
		}

		public bool UpdateProgress (int done, int total, string operation, string detail)
		{
			// Issue callback on UI thread as required.
			if (InvokeRequired)
			{
				ExportProgressHandler handler = new ExportProgressHandler(UpdateProgress);
				return (bool)Invoke(handler, done, total, operation, detail);
			}

			if (IsDisposed || _cancelled)
			{
				if (!IsDisposed)
				{
					Close ();
				}
				return false;
			}

			// Update progress bar
			progressBar.Minimum = 0;
			progressBar.Maximum = total;
			progressBar.Value = done;
			if (!string.IsNullOrEmpty (operation))
			{
				exportOperation.Text = string.Format ("{0}...", operation);
			}
			if (!string.IsNullOrEmpty (detail))
			{
				operationDetail.Text = detail;
			}
			return true;
		}

		private void cancelButton_Click (object sender, EventArgs e)
		{
			if (!_finished)
			{
				Hide ();
			}
			else
			{
				Close ();
			}
			_cancelled = true;
		}

		private void exportWorker_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)
		{
			Close ();
		}
	}
}