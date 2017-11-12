//-----------------------------------------------------------------------
// <copyright file="XsltBarcodeExtension.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
				
namespace Zen.Barcode.Web.Xsl
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
	/// <b>XsltBarcodeExtension</b> provides a handful of custom functions 
	/// that are callable from XSLT style-sheets and allow barcode URIs to
	/// be encoded in an XSLT style-sheet output.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The use of this class enables XSLT style-sheet authors to substitute
	/// barcode text for the associated image URI directly in the style-sheet.
	/// </para>
	/// <para>
	/// To use this extension it is necessary to ensure that the object is
	/// registered via an XsltArgumentList and passed to the XslTransform
	/// class.
	/// </para>
	/// <para>
	/// Extension namespace: http://schemas.siamzen.com/barcodes
	/// </para>
	/// <example>
	/// XsltArgumentList list = new XsltArgumentList ();
	/// list.AddExtensionObject ("http://schemas.siamzen.com/barcodes",
	///		new XsltBarcodeExtension ());
	/// </example>
	/// </remarks>
	public class XsltBarcodeExtension
	{
		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 11 barcode symbology without checksum.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode11NC (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code11NC;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = 1;
			uri.BarMaxWidth = 3;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 11 barcode symbology with checksum.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode11C (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code11C;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = 1;
			uri.BarMaxWidth = 3;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 39 barcode symbology without checksum.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode39NC (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code39NC;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = 1;
			uri.BarMaxWidth = 2;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 39 barcode symbology with checksum.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode39C (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code39C;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = 1;
			uri.BarMaxWidth = 2;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 93 barcode symbology.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode93 (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code93;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = 1;
			uri.BarMaxWidth = 2;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code 128 barcode symbology.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcode128 (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.Code93;
			uri.BarMinHeight = uri.BarMaxHeight = 30;
			uri.BarMinWidth = uri.BarMaxWidth = 1;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code EAN-13 barcode symbology.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcodeEan13 (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.CodeEan13;
			uri.BarMinHeight = 25;
			uri.BarMaxHeight = 30;
			uri.BarMinWidth = uri.BarMaxWidth = 1;
			return uri.ToString ();
		}

		/// <summary>
		/// Gets the barcode image URI for the specified text using the
		/// Code EAN-8 barcode symbology.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string GetBarcodeEan8 (string text)
		{
			BarcodeImageUriBuilder uri = new BarcodeImageUriBuilder ();
			uri.Text = text;
			uri.EncodingScheme = BarcodeSymbology.CodeEan8;
			uri.BarMinHeight = 25;
			uri.BarMaxHeight = 30;
			uri.BarMinWidth = uri.BarMaxWidth = 1;
			return uri.ToString ();
		}
	}
}
