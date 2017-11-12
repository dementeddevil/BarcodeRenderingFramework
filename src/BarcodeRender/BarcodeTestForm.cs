using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using Zen.Barcode;

namespace BarcodeRender
{
	/// <summary>
	/// <c>BarcodeTestForm</c> is designed to support the printing and
	/// subsequent testing (by using a barcode scanner) to validate the
	/// barcode algorithms used in the rendering suite.
	/// </summary>
	public partial class BarcodeTestForm : Form
	{
		#region Internal Objects
		/// <summary>
		/// <c>BiIterator</c> implements a bi-directional iterator capable of
		/// iterating forwards or backwards across any IList derived collection.
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <typeparam name="TList"></typeparam>
		private class BiIterator<TElement, TList>
			where TList : IList
		{
			public IEnumerable<TElement> GetEnumerator (bool forward, TList list)
			{
				if (forward)
				{
					for (int index = 0; index < list.Count; ++index)
					{
						yield return (TElement) list[index];
					}
				}
				else
				{
					for (int index = list.Count - 1; index >= 0; --index)
					{
						yield return (TElement) list[index];
					}
				}
			}
		}
		#endregion

		#region Private Fields
		private int _maxBarHeight = 30;
		private int _barcodeColumns = 2;
		private int _barcodesPerPage = 20;
		private int _barcodePadding = 10;

		private IEnumerator<SymbologyTestCase> _testIterator;

		private Font _barcodeFont;
		private Random _random;
		private SymbologyTestPlan _testPlan;
		private string _testPlanPathName;

		private bool _testPlanDirty;
		private bool _testChangesPending;
		private bool _symbologyChanged;
		private bool _barcodeTextChanged;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:BarcodeTestForm"/> class.
		/// </summary>
		public BarcodeTestForm ()
		{
			InitializeComponent ();
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closing"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"></see> that contains the event data.</param>
		protected override void OnClosing (CancelEventArgs e)
		{
			// Allow save of dirty test plan...
			if (!SaveModified ())
			{
				e.Cancel = true;
				return;
			}
			base.OnClosing (e);
		}
		#endregion

		#region Private Properties
		private BarcodeSymbology SelectedSymbology
		{
			get
			{
				BarcodeSymbology current = BarcodeSymbology.Unknown;
				if (testSymbology.SelectedIndex != -1)
				{
					current = (BarcodeSymbology) (testSymbology.SelectedIndex + 1);
				}
				return current;
			}
		}

		private Random RandomGenerator
		{
			get
			{
				if (_random == null)
				{
					_random = new Random (DateTime.Now.Second);
				}
				return _random;
			}
		}
		#endregion

		#region Private Methods
		#region Random Test Case Generation
		/// <summary>
		/// Prepares the form for printing and testing of the symbology.
		/// </summary>
		/// <param name="symbology">Zero-based symbology index.</param>
		private void GenerateRandomTestCases (BarcodeSymbology symbology)
		{
			switch (symbology)
			{
				case BarcodeSymbology.Code39NC:
					GenerateTestDigit (symbology, 5, 10);
					break;
				case BarcodeSymbology.Code39C:
					GenerateTestDigit (symbology, 5, 10);
					break;
				case BarcodeSymbology.Code93:
					GenerateTestDigit (symbology, 5, 10);
					GenerateTestAlpha (symbology, 5, 10, true, false);
					break;
				case BarcodeSymbology.Code128:
					GenerateTestDigit (symbology, 5, 10);
					GenerateTestAlpha (symbology, 5, 10, true, true);
					break;
				case BarcodeSymbology.Code11NC:
				case BarcodeSymbology.Code11C:
				case BarcodeSymbology.Code25StandardNC:
				case BarcodeSymbology.Code25StandardC:
				case BarcodeSymbology.Code25InterleavedNC:
				case BarcodeSymbology.Code25InterleavedC:
					GenerateTestDigit (symbology, 5, 10);
					break;
				case BarcodeSymbology.CodeEan13:
					GenerateTestDigit (symbology, 5, 12);
					break;
				case BarcodeSymbology.CodeEan8:
					GenerateTestDigit (symbology, 5, 7);
					break;
			}
		}

		private void GenerateTestDigit (BarcodeSymbology symbology, int codes,
			int length)
		{
			for (int index = 0; index < codes; ++index)
			{
				StringBuilder builder = new StringBuilder ();
				while (builder.Length < length)
				{
					int value = RandomGenerator.Next (10);
					builder.Append ((char) ('0' + value));
				}
				AddTestCase (symbology, builder.ToString ());
			}
		}

		private void GenerateTestAlpha (BarcodeSymbology symbology, int codes,
			int length, bool allowUpper, bool allowLower)
		{
			for (int index = 0; index < codes; ++index)
			{
				StringBuilder builder = new StringBuilder ();
				while (builder.Length < length)
				{
					int value = RandomGenerator.Next (26);
					bool upperCase = false;
					if (allowUpper && !allowLower)
					{
						upperCase = true;
					}
					else if (allowUpper && allowLower)
					{
						upperCase = (RandomGenerator.Next (10) > 5);
					}
					if (upperCase)
					{
						builder.Append ((char) ('A' + value));
					}
					else
					{
						builder.Append ((char) ('a' + value));
					}
				}
				AddTestCase (symbology, builder.ToString ());
			}
		} 
		#endregion

		#region Misc Forms Support
		private void AddTestCase (BarcodeSymbology symbology, string text)
		{
			if (_testPlan == null)
			{
				_testPlan = new SymbologyTestPlan ();
			}
			_testPlan.AddTestCase (symbology, text);
			SetTestPlanModified ();

			RefreshTree ();
		}

		private void SetTestPlanModified ()
		{
			_testPlanDirty = true;
			UpdateTitle ();
		}

		private void ClearTestPlanModified ()
		{
			_testPlanDirty = false;
			UpdateTitle ();
		}

		private void UpdateTitle ()
		{
			string title = "Barcode Test Plan - ";
			if (!string.IsNullOrEmpty (_testPlanPathName))
			{
				title += Path.GetFileName (_testPlanPathName);
			}
			else
			{
				title += "default.btp";
			}
			if (_testPlanDirty)
			{
				title += " *";
			}
			Text = title;
		}

		private void CommitChanges ()
		{
			if (_testChangesPending)
			{
				AddTestCase (SelectedSymbology, barcodeLabel.Text);
				_testChangesPending = false;
			}
		}

		private void RefreshBarcode ()
		{
			barcodePanel.Text = string.Empty;
			barcodePanel.Symbology = SelectedSymbology;
			barcodePanel.Text = barcodeLabel.Text;
		} 
		#endregion

		#region Printing Support
		private void OnPrint (object sender, EventArgs e)
		{
			printDialog.ShowDialog (this);
		}

		private void OnPageSetup (object sender, EventArgs e)
		{
			pageSetupDialog.ShowDialog (this);
		}

		private void OnPrintPreview (object sender, EventArgs e)
		{
			printPreviewDialog.ShowDialog (this);
		}

		private void barcodeDocument_BeginPrint (object sender, PrintEventArgs e)
		{
			_testIterator = _testPlan.GetTestCases (_maxBarHeight).GetEnumerator ();
			_barcodeFont = new Font ("Verdana", 8);
		}

		private void barcodeDocument_PrintPage (object sender, PrintPageEventArgs e)
		{
			// Determine printable region for each barcode and label
			int numLines = _barcodesPerPage / _barcodeColumns;
			if ((_barcodesPerPage % _barcodeColumns) != 0)
			{
				++numLines;
			}

			// Determine size to allocate for each barcode
			SizeF barcodeArea = new SizeF ();
			barcodeArea.Width =
				(e.MarginBounds.Width / _barcodeColumns);
			barcodeArea.Height =
				(e.MarginBounds.Height / numLines);

			e.HasMorePages = true;
			for (int index = 0; index < _barcodesPerPage; ++index)
			{
				// Fetch the next test
				if (!_testIterator.MoveNext ())
				{
					e.HasMorePages = false;
					break;
				}

				// Determine placement rectangle
				PointF topLeft = new PointF (
					((index % _barcodeColumns) * barcodeArea.Width) + e.MarginBounds.Left,
					((index / _barcodeColumns) * barcodeArea.Height) + e.MarginBounds.Top);
				RectangleF drawRectangle = new RectangleF (
					topLeft, barcodeArea);

				PointF textLocation = new PointF (topLeft.X, topLeft.Y);

				// Render barcode image and label
				using (SymbologyTestCase testCase = _testIterator.Current)
				{
					PointF imageLocation = new PointF (topLeft.X, topLeft.Y);
					imageLocation.X += (drawRectangle.Width - testCase.BarcodeImage.Width) / 2;
					imageLocation.Y += _barcodePadding;
					e.Graphics.DrawImage (testCase.BarcodeImage, imageLocation);

					// Adjust text position
					textLocation.Y += (_barcodePadding * 2) + testCase.BarcodeImage.Height;

					// Determine text area
					string barcodeLabelText = testCase.BarcodeText;
					SizeF textSize = e.Graphics.MeasureString (barcodeLabelText, _barcodeFont);
					textLocation.X += (drawRectangle.Width - textSize.Width) / 2;

					// Draw text
					e.Graphics.DrawString (barcodeLabelText, _barcodeFont,
						SystemBrushes.WindowText, textLocation);
				}
			}
		}

		private void barcodeDocument_EndPrint (object sender, PrintEventArgs e)
		{
			if (_barcodeFont != null)
			{
				_barcodeFont.Dispose ();
				_barcodeFont = null;
			}
			if (_testIterator != null)
			{
				_testIterator.Dispose ();
				_testIterator = null;
			}
		} 
		#endregion

		#region Test Tree Support
		private void RefreshTree ()
		{
			testTree.Nodes.Clear ();
			if (_testPlan != null)
			{
				TreeNode root = new TreeNode ("Symbologies");
				foreach (SymbologyTestGroup group in _testPlan.GroupTests)
				{
					TreeNode groupNode = new TreeNode (group.Symbology.ToString ());
					groupNode.Tag = group;

					foreach (SymbologyTestItem item in group.TestItems)
					{
						TreeNode itemNode = new TreeNode (item.Barcode);
						itemNode.Tag = item;
						groupNode.Nodes.Add (itemNode);
					}

					root.Nodes.Add (groupNode);
				}
				testTree.Nodes.Add (root);
			}
		}

		private bool UpdateSelectedNode (bool nextNode)
		{
			// Ensure we have a valid selection first...
			if (testTree.SelectedNode == null)
			{
				testTree.SelectedNode = testTree.Nodes[0];
			}

			// Recurse to find next (or previous) node and update selection
			bool foundCurrentSelection = false;
			bool selectionUpdated = RecurseSelectNode (testTree.Nodes[0], nextNode,
				ref foundCurrentSelection);
			if (selectionUpdated)
			{
			}
			return selectionUpdated;
		}

		private bool RecurseSelectNode (TreeNode currentNode, bool nextNode,
			ref bool foundCurrentSelection)
		{
			// Check whether this is the current selection or a qualifying node
			if (currentNode == currentNode.TreeView.SelectedNode)
			{
				// Found the current selection
				foundCurrentSelection = true;
			}
			else if (foundCurrentSelection && currentNode.Tag is SymbologyTestItem)
			{
				// Found the next qualifying node after selection
				currentNode.TreeView.SelectedNode = currentNode;
				return true;
			}

			// Iterate child nodes of this node
			BiIterator<TreeNode, TreeNodeCollection> iter =
				new BiIterator<TreeNode, TreeNodeCollection> ();
			foreach (TreeNode childNode in iter.GetEnumerator (nextNode, currentNode.Nodes))
			{
				// Recurse children of node and stop if we change selection
				bool selectionChanged = RecurseSelectNode (childNode, nextNode,
					ref foundCurrentSelection);
				if (selectionChanged)
				{
					return selectionChanged;
				}
			}

			// No change to selection
			return false;
		}
		#endregion

		#region UI State Message Handlers
		private void testTree_AfterSelect (object sender, TreeViewEventArgs e)
		{
			// If the selected node is a leaf node...
			if (e.Node.Tag is SymbologyTestItem)
			{
				// Save any pending changes
				CommitChanges ();

				// Identify the test item and group
				SymbologyTestItem testItem = (SymbologyTestItem) e.Node.Tag;
				SymbologyTestGroup testGroup = (SymbologyTestGroup) e.Node.Parent.Tag;

				// Update the current item
				testSymbology.SelectedIndex = ((int) testGroup.Symbology) - 1;
				barcodeLabel.Text = testItem.Barcode;
				RefreshBarcode ();
				_testChangesPending = false;
				_symbologyChanged = false;
				_barcodeTextChanged = false;
			}
		}

		private void previousTestButton_Click (object sender, EventArgs e)
		{
			CommitChanges ();
			UpdateSelectedNode (false);
			scannerResult.Clear ();
			scannerResult.Focus ();
		}

		private void nextTestButton_Click (object sender, EventArgs e)
		{
			CommitChanges ();
			UpdateSelectedNode (true);
			scannerResult.Clear ();
			scannerResult.Focus ();
		}

		private void symbology_SelectedIndexChanged (object sender, EventArgs e)
		{
			_testChangesPending = true;
			_symbologyChanged = true;
		}

		private void barcodeLabel_TextChanged (object sender, EventArgs e)
		{
			_testChangesPending = true;
			_barcodeTextChanged = true;
		}

		private void symbology_Leave (object sender, EventArgs e)
		{
			if (_symbologyChanged)
			{
				RefreshBarcode ();
				_symbologyChanged = false;
			}
		}

		private void barcodeLabel_Leave (object sender, EventArgs e)
		{
			if (_barcodeTextChanged)
			{
				RefreshBarcode ();
				_barcodeTextChanged = false;
			}
		}

		private void scannerResult_KeyDown (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				// Check for match (allow prefix matches for symbologies 
				//	generating checksum characters)
				if (scannerResult.Text == barcodeLabel.Text ||
					scannerResult.Text.StartsWith (barcodeLabel.Text))
				{
					nextTestButton_Click (null, EventArgs.Empty);
				}
			}
		}
		#endregion

		#region Test Plan Persistence
		private void OnNewTestPlan (object sender, EventArgs e)
		{
			// Ensure pending changes are saved
			if (SaveModified ())
			{
				_testPlan = new SymbologyTestPlan ("default");
				_testPlanPathName = null;
				RefreshTree ();
				ClearTestPlanModified ();
				UpdateTitle ();
			}
		}

		/// <summary>
		/// Queries the user to save the current document if it has been
		/// modified.
		/// </summary>
		/// <returns>
		/// <c>true</c> if operation being performed by the caller can 
		/// continue; otherwise, <c>false</c>.
		/// </returns>
		private bool SaveModified ()
		{
			if (_testPlanDirty)
			{
				DialogResult result = MessageBox.Show (
					"You have unsaved changes!\nSave?", "Barcode Test Plan",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
				if (result == DialogResult.Yes)
				{
					// Delegate through save handler...
					OnSaveTestPlan (null, EventArgs.Empty);
				}
			}
			return true;
		}

		private void OnOpenTestPlan (object sender, EventArgs e)
		{
			if (SaveModified () &&
				openFileDialog.ShowDialog (this) == DialogResult.OK)
			{
				// Open XML file reader
				// TODO: Incorporate schema based validation
				XmlReaderSettings readerConfig = new XmlReaderSettings ();
				readerConfig.CloseInput = true;
				readerConfig.ConformanceLevel = ConformanceLevel.Document;
				readerConfig.IgnoreComments = true;

				using (XmlReader xmlReader = XmlReader.Create (
					new FileStream (openFileDialog.FileName, FileMode.Open,
					FileAccess.Read, FileShare.Read), readerConfig))
				{
					// Pass to XmlSerializer
					XmlSerializer xmlSerializer = new XmlSerializer (
						typeof (SymbologyTestPlan),
						new Type[] { typeof (SymbologyTestGroup),
							typeof (SymbologyTestItem) });
					_testPlan = (SymbologyTestPlan) xmlSerializer.Deserialize (xmlReader);

					// Close xml reader and cache name
					xmlReader.Close ();
					_testPlanPathName = openFileDialog.FileName;
				}

				// Refresh tree clear modified and update title
				RefreshTree ();
				ClearTestPlanModified ();
				UpdateTitle ();
			}
		}

		private void DoSave (string fileName)
		{
			XmlWriterSettings writerConfig = new XmlWriterSettings ();
			writerConfig.CloseOutput = true;
			writerConfig.ConformanceLevel = ConformanceLevel.Document;
			writerConfig.Encoding = Encoding.UTF8;
			writerConfig.Indent = true;
			writerConfig.IndentChars = "\t";

			using (XmlWriter xmlWriter = XmlWriter.Create (
				new FileStream (fileName, FileMode.Create, FileAccess.Write,
				FileShare.None), writerConfig))
			{
				// Pass to XmlSerializer
				XmlSerializer xmlSerializer = new XmlSerializer (
					typeof (SymbologyTestPlan),
					new Type[] { typeof (SymbologyTestGroup),
							typeof (SymbologyTestItem) });
				xmlSerializer.Serialize (xmlWriter, _testPlan);

				// Flush and close
				xmlWriter.Flush ();
				xmlWriter.Close ();
			}

			// Cache the filename and clear modified state
			_testPlanPathName = fileName;
			ClearTestPlanModified ();
		}

		private void OnSaveTestPlan (object sender, EventArgs e)
		{
			// If no test plan; nothing to do
			if (_testPlan == null)
			{
				return;
			}

			// Determine save file name...
			string fileName = _testPlanPathName;
			if (string.IsNullOrEmpty (fileName))
			{
				if (saveFileDialog.ShowDialog (this) == DialogResult.OK)
				{
					fileName = saveFileDialog.FileName;
				}
				else
				{
					fileName = null;
				}
			}

			// If we have a valid filename then pass to save
			if (!string.IsNullOrEmpty (fileName))
			{
				DoSave (fileName);
			}
		}

		private void OnSaveTestPlanAs (object sender, EventArgs e)
		{
			if (_testPlan == null)
			{
				return;
			}

			// Determine save file name...
			if (saveFileDialog.ShowDialog (this) == DialogResult.OK)
			{
				DoSave (saveFileDialog.FileName);
			}
		} 
		#endregion

		#region Image Export
		private void exportImageToolStripMenuItem_Click (object sender, EventArgs e)
		{
			ExportBarcodeImagesForm export = new ExportBarcodeImagesForm();
			if (export.ShowDialog () == DialogResult.OK)
			{
				ExportProgress.Start (
					_testPlan,
					export.RootFolder,
					export.OverwriteFiles,
					export.FlattenHierarchy);
			}
		}
		#endregion
		#endregion
	}
}