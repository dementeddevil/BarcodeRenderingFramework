//-----------------------------------------------------------------------
// <copyright file="BarcodeImageUriBuilder.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode.Web
{
	using System;
	using System.IO;
	using System.Text;
	using System.Web;

	/// <summary>
	/// Provides a custom constructor for barcode uniform resource identifiers
	/// (URIs) and modifies URIs for the <see cref="T:BarcodeImageUri"/> class.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <b>BarcodeImageUriBuilder</b> class provides a convenient way to 
	/// modify the contents of a <b>BarcodeImageUri</b> instance without 
	/// creating a new <b>BarcodeImageUri</b> instance for each modification.
	/// </para>
	/// <para>
	/// The <b>BarcodeImageUriBuilder</b> properties provide read/write access
	/// to the read-only <b>BarcodeImageUri</b> properties so that they can be
	/// modified.
	/// </para>
	/// </remarks>
	public class BarcodeImageUriBuilder
	{
		#region Private Fields
		private string _text;

		private bool _useExtensionlessUri;

		private BarcodeSymbology _encodingScheme;

		private int _scale = 1;
		private bool _isScaleSet;

		private int _barMinHeight = 30;
		private int _barMaxHeight = 30;
		private int _barMinWidth = 1;
		private int _barMaxWidth = 1;

		private QrEncodeMode _qrEncodingMode = QrEncodeMode.Byte;
		private QrErrorCorrection _qrErrorCorrect = QrErrorCorrection.M;
		private int _qrVersion = 5;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeImageUriBuilder"/> class.
		/// </summary>
		public BarcodeImageUriBuilder()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// The barcode string to be encoded
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use extensionless URI.
		/// </summary>
		/// <value>
		/// <c>true</c> to use extensionless URIs; otherwise, <c>false</c>.
		/// </value>
		public bool UseExtensionlessUri
		{
			get
			{
				return _useExtensionlessUri;
			}
			set
			{
				_useExtensionlessUri = value;
			}
		}

		/// <summary>
		/// Gets/sets the barcode encoding scheme
		/// </summary>
		/// <value>A value from the <see cref="T:BarcodeSymbology"/> 
		/// enumeration.</value>
		public BarcodeSymbology EncodingScheme
		{
			get
			{
				return _encodingScheme;
			}
			set
			{
				_encodingScheme = value;
				if (!_isScaleSet)
				{
					if (value == BarcodeSymbology.CodeQr)
					{
						_scale = 3;
					}
					else
					{
						_scale = 1;
					}
				}
			}
		}

		/// <summary>
		/// Gets/sets an <see cref="T:Int32"/> that determines the height of
		/// the rendered bar-code.
		/// </summary>
		/// <value>The height of the bar min.</value>
		public int BarMinHeight
		{
			get
			{
				return _barMinHeight;
			}
			set
			{
				_barMinHeight = value;
			}
		}

		/// <summary>
		/// Gets/sets an <see cref="T:Int32"/> that determines the height of
		/// the rendered bar-code.
		/// </summary>
		/// <value>The height of the bar max.</value>
		public int BarMaxHeight
		{
			get
			{
				return _barMaxHeight;
			}
			set
			{
				_barMaxHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the bar min.
		/// </summary>
		/// <value>The width of the bar min.</value>
		public int BarMinWidth
		{
			get
			{
				return _barMinWidth;
			}
			set
			{
				_barMinWidth = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of the bar max.
		/// </summary>
		/// <value>The width of the bar max.</value>
		public int BarMaxWidth
		{
			get
			{
				return _barMaxWidth;
			}
			set
			{
				_barMaxWidth = value;
			}
		}

		/// <summary>
		/// Gets or sets the barcode scale factor.
		/// </summary>
		/// <value>The barcode scale.</value>
		/// <remarks>
		/// The default for this property is;
		/// 1 for all 1D barcodes.
		/// 3 for QR barcode.
		/// </remarks>
		public int Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				_scale = value;
				_isScaleSet = true;
			}
		}

		/// <summary>
		/// Gets or sets the QR barcode version.
		/// </summary>
		/// <value>
		/// The QR barcode version.
		/// 0=auto-detect, 1-40=specific version
		/// </value>
		/// <remarks>
		/// Larger version numbers allow a larger quantity of data to be
		/// encoded.
		/// </remarks>
		/// <remarks>
		/// The default for this property is 5.
		/// </remarks>
		public int QrVersion
		{
			get
			{
				return _qrVersion;
			}
			set
			{
				_qrVersion = value;
			}
		}

		/// <summary>
		/// Gets or sets the QR barcode encoding mode.
		/// </summary>
		/// <value>
		/// A value from the <see cref="QrEncodeMode"/> enumeration.
		/// </value>
		public QrEncodeMode QrEncodingMode
		{
			get
			{
				return _qrEncodingMode;
			}
			set
			{
				_qrEncodingMode = value;
			}
		}

		/// <summary>
		/// Gets or sets the QR barcode error correction scheme.
		/// </summary>
		/// <value>
		/// A value from the <see cref="QrErrorCorrection"/> enumeration.
		/// </value>
		public QrErrorCorrection QrErrorCorrect
		{
			get
			{
				return _qrErrorCorrect;
			}
			set
			{
				_qrErrorCorrect = value;
			}
		}

		/// <summary>
		/// Gets the built <see cref="T:BarcodeImageUri"/> that represents
		/// the current state of the builder.
		/// </summary>
		public BarcodeImageUri Uri
		{
			get
			{
				return new BarcodeImageUri(ToString());
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Overridden. Gets a textual representation of the build URI.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (EncodingScheme == BarcodeSymbology.CodeQr)
			{
				return BarcodeImageUriBuilder.GetFileNameFromQrParams(
					Text, Scale, UseExtensionlessUri,
					QrEncodingMode, QrErrorCorrect, QrVersion);
			}
			else
			{
				return BarcodeImageUriBuilder.GetFileNameFromParams(
					Text, Scale, UseExtensionlessUri,
					EncodingScheme, BarMinHeight, BarMaxHeight, BarMinWidth, BarMaxWidth);
			}
		}
		#endregion

		#region Private Static Methods
		private static string GetFileNameFromParams(
			string text, int scale, bool useExtensionUri,
			BarcodeSymbology encodingScheme, int barMinHeight, int barMaxHeight,
			int barMinWidth, int barMaxWidth)
		{
			// Build key string
			string fileName = string.Format(
				"Barcode[{0},{1},{2},{3},{4},{5}]:{6}",
				(int)encodingScheme, barMinHeight, barMaxHeight,
				barMinWidth, barMaxWidth, scale, text);

			// Return encoded filename based on key
			return BuildEncodedFileName(fileName, useExtensionUri);
		}

		private static string GetFileNameFromQrParams(
			string text, int scale, bool useExtensionUri,
			QrEncodeMode encoding, QrErrorCorrection errorCorrect, int version)
		{
			// Build key string
			string fileName = string.Format(
				"QrBarcode[{0},{1},{2},{3}]:{4}",
				(int)encoding, (int)errorCorrect, version, scale, text);

			// Return encoded filename based on key
			return BuildEncodedFileName(fileName, useExtensionUri);
		}

		private static string BuildEncodedFileName(
			string fileName, bool useExtensionUri)
		{
			// Add filename hash for security
			int hash = fileName.GetHashCode();
			fileName += ":" + hash.ToString();

			// Create memory stream to capture encrypted and encoded content
			MemoryStream memStm = new MemoryStream();

			// Write to crypto-stream via stream writer in UTF8
			StreamWriter writer = new StreamWriter(memStm, Encoding.UTF8);
			writer.Write(fileName);
			writer.Flush();

			long streamLength = memStm.Position;
			memStm.SetLength(streamLength);

			writer.Close();

			// Memory stream buffer is always big which plays havoc with
			//	the base 64 conversion...
			byte[] buffer = new byte[streamLength];
			Array.Copy(memStm.GetBuffer(), buffer, (int)streamLength);

			// Base64 encode the memory block from the memory stream
			fileName = HttpServerUtility.UrlTokenEncode(buffer);

			if (useExtensionUri)
			{
				fileName = "~/Barcode/" + fileName;
			}
			else
			{
				// Check the filename isn't too long!
				fileName = "~/" + fileName + ".Barcode";
			}

			// Prefix with root application path
			// TODO: Return absolute URI...
			return fileName;
		}
		#endregion
	}
}
