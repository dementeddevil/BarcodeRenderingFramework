//-----------------------------------------------------------------------
// <copyright file="BarcodeImageReportItem.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.SSRS
{
	using System;
	using System.Drawing.Imaging;
	using System.IO;
	using Microsoft.ReportingServices.OnDemandReportRendering;

	public class BarcodeImageReportItem : ICustomReportItem
	{
		#region Private Fields
		private const int DPI = 96;
		private CustomReportItem _cri;
		#endregion

		#region ICustomReportItem Members
		public void GenerateReportItemDefinition(CustomReportItem cri)
		{
			// Create image definition that will be used to render the image
			cri.CreateCriImageDefinition();

			Image image = (Image)cri.GeneratedReportItem;
		}

		public void EvaluateReportItemInstance(CustomReportItem cri)
		{
			// Cache the CRI object
			_cri = cri;

			// Render the actual image now
			Image image = (Image)cri.GeneratedReportItem;
			image.ImageInstance.ImageData = DrawImage();
		}
		#endregion

		#region Private Methods
		private byte[] DrawImage()
		{
			// Determine barcode symbology
			BarcodeSymbology symbology = BarcodeSymbology.Unknown;
			string symbologyText = (string)GetCustomProperty("barcode:Symbology");
			symbology = (BarcodeSymbology)Enum.Parse(typeof(BarcodeSymbology), symbologyText);
			if (symbology != BarcodeSymbology.Unknown)
			{
				// Create draw object
				BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(symbology);

				// Get default metrics and override with values specified in CRI
				// TODO: Need more elegant method for doing this...
				BarcodeMetrics metrics = drawObject.GetDefaultMetrics(30);
				metrics.Scale =
					GetCustomPropertyInt32("barcode:Scale", metrics.Scale);

				BarcodeMetrics1d metrics1d = metrics as BarcodeMetrics1d;
				if (metrics1d != null)
				{
					metrics1d.MaxHeight =
						GetCustomPropertyInt32("barcode:MaximumBarHeight", metrics1d.MaxHeight);
					metrics1d.MinHeight =
						GetCustomPropertyInt32("barcode:MinimumBarHeight", metrics1d.MinHeight);
					metrics1d.MinWidth =
						GetCustomPropertyInt32("barcode:MinimumBarWidth", metrics1d.MinWidth);
					metrics1d.MaxWidth =
						GetCustomPropertyInt32("barcode:MaximumBarWidth", metrics1d.MaxWidth);
					int interGlyphSpacing =
						GetCustomPropertyInt32("barcode:InterGlyphSpacing", -1);
					if (interGlyphSpacing >= 0)
					{
						metrics1d.InterGlyphSpacing = interGlyphSpacing;
					}
					metrics1d.RenderVertically =
						GetCustomPropertyBool("barcode:RenderVertically", metrics1d.RenderVertically);
				}
				else if (symbology == BarcodeSymbology.CodeQr)
				{
					BarcodeMetricsQr qrMetrics = (BarcodeMetricsQr)metrics;
					qrMetrics.Version =
						GetCustomPropertyInt32("barcode:QrVersion", qrMetrics.Version);
					qrMetrics.EncodeMode = (QrEncodeMode)
						GetCustomPropertyInt32("barcode:QrEncodeMode", (int)qrMetrics.EncodeMode);
					qrMetrics.ErrorCorrection = (QrErrorCorrection)
						GetCustomPropertyInt32("barcode:QrErrorCorrection", (int)qrMetrics.ErrorCorrection);
				}

				// Get the text to render
				string textToRender = (string)GetCustomProperty("barcode:Text");

				// Determine available space for rendering
				int criWidth = (int)(_cri.Width.ToInches() * DPI);
				int criHeight = (int)(_cri.Height.ToInches() * DPI);

				// Create bitmap of the appropriate size
				System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
					criWidth, criHeight, PixelFormat.Format32bppArgb);
				using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
				{
					// Clear entire background
					g.Clear(System.Drawing.Color.White);

					// Get barcode image
					System.Drawing.Image barcodeImage =
						drawObject.Draw(textToRender, metrics);

					// Centre the image
					int x = (bmp.Width - barcodeImage.Width) / 2;
					int y = (bmp.Height - barcodeImage.Height) / 2;
					g.DrawImageUnscaled(barcodeImage, x, y);
				}

				// Create memory stream for new image
				using (MemoryStream stream = new MemoryStream())
				{
					// Save image and setup CRI image
					bmp.Save(stream, ImageFormat.Bmp);
					return stream.ToArray();
				}
			}
			return null;
		}

		private int GetCustomPropertyInt32(string propertyName)
		{
			return GetCustomPropertyInt32(propertyName, 0);
		}

		private int GetCustomPropertyInt32(string propertyName, int defaultValue)
		{
			int result;
			if (!Int32.TryParse((string)GetCustomProperty(propertyName), out result))
			{
				result = defaultValue;
			}
			return result;
		}

		private bool GetCustomPropertyBool(string propertyName)
		{
			return GetCustomPropertyBool(propertyName, false);
		}

		private bool GetCustomPropertyBool(string propertyName, bool defaultValue)
		{
			bool result;
			if (!Boolean.TryParse((string)GetCustomProperty(propertyName), out result))
			{
				result = defaultValue;
			}
			return result;
		}

		private object GetCustomProperty(string propertyName)
		{
			return GetCustomProperty(_cri.CustomProperties, propertyName, null);
		}

		private object GetCustomProperty(string propertyName, object defaultValue)
		{
			return GetCustomProperty(_cri.CustomProperties, propertyName, defaultValue);
		}

		private static object GetCustomProperty(CustomPropertyCollection properties, string propertyName, object defaultValue)
		{
			object result = defaultValue;
			if (properties != null && properties.Count > 0)
			{
				CustomProperty prop = properties[propertyName];
				if (prop != null)
				{
					if (prop.Value.IsExpression)
					{
						result = prop.Instance.Value;
					}
					else
					{
						result = prop.Value.Value;
					}
				}
			}
			return result;
		}
		#endregion
	}
}
