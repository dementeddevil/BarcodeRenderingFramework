using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Zen.Barcode;

namespace BarcodeRender
{
	/// <summary>
	/// <c>BarcodePanel</c> encapsulates a Windows Forms barcode control.
	/// </summary>
	public partial class BarcodePanel : Panel
	{
		#region Private Fields
		private BarcodeSymbology _symbology;
		private int _maxBarHeight = 30;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initialises an instance of <see cref="T:BarcodePanel" />.
		/// </summary>
		public BarcodePanel ()
		{
			InitializeComponent ();
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the barcode symbology.
		/// </summary>
		/// <value>The symbology.</value>
		[Category ("Appearance")]
		[DefaultValue ((int) BarcodeSymbology.Unknown)]
		[TypeConverter (typeof (EnumConverter))]
		[Description ("Gets/sets the barcode symbology used by this control.")]
		public BarcodeSymbology Symbology
		{
			get
			{
				return _symbology;
			}
			set
			{
				if (_symbology != value)
				{
					_symbology = value;
					RefreshBarcodeImage ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		/// <value></value>
		/// <returns>The text associated with this control.</returns>
		[Browsable (true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (base.Text != value)
				{
					base.Text = value;
					RefreshBarcodeImage ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum height of the rendered barcode.
		/// </summary>
		/// <value>The maximum height of a barcode bar in pixels.</value>
		[Category ("Appearance")]
		[DefaultValue (30)]
		[Description ("Gets/sets the maximum height of the rendered barcode bars.")]
		public int MaxBarHeight
		{
			get
			{
				return _maxBarHeight;
			}
			set
			{
				if (_maxBarHeight != value)
				{
					_maxBarHeight = value;
					RefreshBarcodeImage ();
				}
			}
		}
		#endregion

		#region Private Methods
		private void RefreshBarcodeImage ()
		{
			// Allocate new barcode image as needed
			if (_symbology != BarcodeSymbology.Unknown && !string.IsNullOrEmpty (Text))
			{
				try
				{
					var drawObject = BarcodeDrawFactory.GetSymbology (_symbology);
					var metrics = drawObject.GetDefaultMetrics(_maxBarHeight);
					metrics.Scale = 2;
					BackgroundImage = drawObject.Draw (Text, metrics);
				}
				catch
				{
					BackgroundImage = null;
				}
			}
			else
			{
				BackgroundImage = null;
			}

			// Setup the auto-scroll minimum size
			if (BackgroundImage == null)
			{
				AutoScrollMinSize = new Size (0, 0);
			}
			else
			{
				AutoScrollMinSize = BackgroundImage.Size;
			}
		}
		#endregion
	}
}
