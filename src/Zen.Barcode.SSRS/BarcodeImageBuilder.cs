// -----------------------------------------------------------------------
// <copyright file="BarcodeImageBuilder.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Zen.Barcode.SSRS
{
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;

	/// <summary>
	/// <c>BarcodeImageBuilder</c> class is used to render barcode images in
	/// a report header or footer.
	/// </summary>
	public class BarcodeImageBuilder
	{
		#region Private Fields
		private BarcodeSymbology _symbology = BarcodeSymbology.Unknown;
		private BarcodeMetrics _metrics;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeImageBuilder"/> class.
		/// </summary>
		public BarcodeImageBuilder()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the symbology.
		/// </summary>
		/// <value>The symbology.</value>
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

					BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(Symbology);
					_metrics = drawObject.GetDefaultMetrics(30);
				}
			}
		}

		/// <summary>
		/// Gets the metrics used to render 1D barcode symbologies.
		/// </summary>
		/// <value>The metrics.</value>
		/// <remarks>
		/// Only valid when symbology is set to 1D barcode.
		/// </remarks>
		public BarcodeMetrics1d Metrics1d
		{
			get
			{
				return _metrics as BarcodeMetrics1d;
			}
		}

		/// <summary>
		/// Gets the metrics used to render 2D barcode symbologies.
		/// </summary>
		/// <value>The metrics.</value>
		/// <remarks>
		/// Only valid when symbology is set to 2D barcode.
		/// </remarks>
		public BarcodeMetrics2d Metrics2d
		{
			get
			{
				return _metrics as BarcodeMetrics2d;
			}
		}

		/// <summary>
		/// Gets the metrics used to render 2D QR barcode symbology.
		/// </summary>
		/// <value>The metrics.</value>
		/// <remarks>
		/// Only valid when symbology is set to CodeQr.
		/// </remarks>
		public BarcodeMetricsQr MetricsQr
		{
			get
			{
				return _metrics as BarcodeMetricsQr;
			}
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get;
			set;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the barcode image based on the current barcode builder
		/// properties.
		/// </summary>
		/// <returns>
		/// Returns a raw byte array of the barcode in standard BMP format.
		/// </returns>
		public byte[] GetBarcodeImage()
		{
			// Sanity check
			if (Symbology == BarcodeSymbology.Unknown)
			{
				return null;
			}

			// Draw image
			BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(Symbology);
			using (Image image = drawObject.Draw(Text, _metrics))
			{
				// Create memory stream for new image
				using (MemoryStream stream = new MemoryStream())
				{
					// Save image and return raw byte array
					image.Save(stream, ImageFormat.Bmp);
					return stream.ToArray();
				}
			}
		}
		#endregion
	}
}
