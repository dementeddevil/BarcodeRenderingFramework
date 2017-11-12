//-----------------------------------------------------------------------
// <copyright file="BarcodeDrawFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
	using System;
	using Zen.Barcode.Properties;

	/// <summary>
	/// <b>BarcodeDrawFactory</b> returns draw agents capable of generating 
	/// the appropriate bar-code image.
	/// </summary>
	public static class BarcodeDrawFactory
	{
		#region Private Fields
		private static Code39BarcodeDraw _code39WithoutChecksum;
		private static Code39BarcodeDraw _code39WithChecksum;
		private static Code93BarcodeDraw _code93WithChecksum;
		private static Code128BarcodeDraw _code128WithChecksum;
		private static Code11BarcodeDraw _code11WithoutChecksum;
		private static Code11BarcodeDraw _code11WithChecksum;
		private static CodeEan13BarcodeDraw _codeEan13WithChecksum;
		private static CodeEan8BarcodeDraw _codeEan8WithChecksum;
		private static Code25BarcodeDraw _code25StandardWithoutChecksum;
		private static Code25BarcodeDraw _code25StandardWithChecksum;
		private static Code25BarcodeDraw _code25InterleavedWithoutChecksum;
		private static Code25BarcodeDraw _code25InterleavedWithChecksum;
		private static CodePdf417BarcodeDraw _codePdf417;
		private static CodeQrBarcodeDraw _codeQr;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets an agent capable of rendering a Code39 barcode without
		/// adding a checksum glyph.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code39BarcodeDraw"/> object.</value>
		public static Code39BarcodeDraw Code39WithoutChecksum
		{
			get
			{
				if (_code39WithoutChecksum == null)
				{
					_code39WithoutChecksum = new Code39BarcodeDraw(
						Code39GlyphFactory.Instance);
				}
				return _code39WithoutChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code39 barcode with an
		/// added checksum glyph.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code39BarcodeDraw"/> object.</value>
		public static Code39BarcodeDraw Code39WithChecksum
		{
			get
			{
				if (_code39WithChecksum == null)
				{
					_code39WithChecksum = new Code39BarcodeDraw(
						Code39Checksum.Instance);
				}
				return _code39WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code93 barcode with added
		/// checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code93BarcodeDraw"/> object.</value>
		public static Code93BarcodeDraw Code93WithChecksum
		{
			get
			{
				if (_code93WithChecksum == null)
				{
					_code93WithChecksum = new Code93BarcodeDraw(
						Code93Checksum.Instance);
				}
				return _code93WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code128 barcode with added
		/// checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code128BarcodeDraw"/> object.</value>
		public static Code128BarcodeDraw Code128WithChecksum
		{
			get
			{
				if (_code128WithChecksum == null)
				{
					_code128WithChecksum = new Code128BarcodeDraw(
						Code128Checksum.Instance);
				}
				return _code128WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code11 barcode.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code11BarcodeDraw"/> object.</value>
		public static Code11BarcodeDraw Code11WithoutChecksum
		{
			get
			{
				if (_code11WithoutChecksum == null)
				{
					_code11WithoutChecksum = new Code11BarcodeDraw(
						Code11GlyphFactory.Instance);
				}
				return _code11WithoutChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code11 barcode with added
		/// checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code11BarcodeDraw"/> object.</value>
		public static Code11BarcodeDraw Code11WithChecksum
		{
			get
			{
				if (_code11WithChecksum == null)
				{
					_code11WithChecksum = new Code11BarcodeDraw(
						Code11Checksum.Instance);
				}
				return _code11WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code EAN-13 barcode with
		/// added checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.CodeEan13BarcodeDraw"/> object.</value>
		public static CodeEan13BarcodeDraw CodeEan13WithChecksum
		{
			get
			{
				if (_codeEan13WithChecksum == null)
				{
					_codeEan13WithChecksum = new CodeEan13BarcodeDraw(
						CodeEan13Checksum.Instance);
				}
				return _codeEan13WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code EAN-8 barcode with
		/// added checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.CodeEan8BarcodeDraw"/> object.</value>
		public static CodeEan8BarcodeDraw CodeEan8WithChecksum
		{
			get
			{
				if (_codeEan8WithChecksum == null)
				{
					_codeEan8WithChecksum = new CodeEan8BarcodeDraw(
						CodeEan8Checksum.Instance);
				}
				return _codeEan8WithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code 25 barcode without
		/// checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code25BarcodeDraw"/> object.</value>
		public static Code25BarcodeDraw Code25StandardWithoutChecksum
		{
			get
			{
				if (_code25StandardWithoutChecksum == null)
				{
					_code25StandardWithoutChecksum = new Code25BarcodeDraw(
						Code25GlyphFactory.StandardInstance);
				}
				return _code25StandardWithoutChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code 25 barcode with
		/// added checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code25BarcodeDraw"/> object.</value>
		public static Code25BarcodeDraw Code25StandardWithChecksum
		{
			get
			{
				if (_code25StandardWithChecksum == null)
				{
					_code25StandardWithChecksum = new Code25BarcodeDraw(
						Code25Checksum.StandardInstance);
				}
				return _code25StandardWithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code 25 barcode without
		/// checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code25BarcodeDraw"/> object.</value>
		public static Code25BarcodeDraw Code25InterleavedWithoutChecksum
		{
			get
			{
				if (_code25InterleavedWithoutChecksum == null)
				{
					_code25InterleavedWithoutChecksum = new Code25BarcodeDraw(
						Code25GlyphFactory.InterleavedInstance);
				}
				return _code25InterleavedWithoutChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code 25 barcode with
		/// added checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.Code25BarcodeDraw"/> object.</value>
		public static Code25BarcodeDraw Code25InterleavedWithChecksum
		{
			get
			{
				if (_code25InterleavedWithChecksum == null)
				{
					_code25InterleavedWithChecksum = new Code25BarcodeDraw(
						Code25Checksum.InterleavedInstance);
				}
				return _code25InterleavedWithChecksum;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code PDF417 barcode with
		/// added checksum glyphs.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.CodePdf417BarcodeDraw"/> object.</value>
		public static CodePdf417BarcodeDraw CodePdf417
		{
			get
			{
				if (_codePdf417 == null)
				{
					_codePdf417 = new CodePdf417BarcodeDraw();
				}
				return _codePdf417;
			}
		}

		/// <summary>
		/// Gets an agent capable of rendering a Code QR barcode.
		/// </summary>
		/// <value>A <see cref="T:Zen.Barcode.CodeQrBarcodeDraw"/> object.</value>
		public static CodeQrBarcodeDraw CodeQr
		{
			get
			{
				if (_codeQr == null)
				{
					_codeQr = new CodeQrBarcodeDraw();
				}
				return _codeQr;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the barcode drawing object for rendering the specified
		/// barcode symbology.
		/// </summary>
		/// <param name="symbology">
		/// A value from the <see cref="T:Zen.Barcode.BarcodeSymbology"/> enumeration.
		/// </param>
		/// <returns>
		/// A class derived from <see cref="T:Zen.Barcode.BarcodeDraw"/>.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		/// Thrown if the specified symbology is invalid or unknown.
		/// </exception>
		public static BarcodeDraw GetSymbology(BarcodeSymbology symbology)
		{
			switch (symbology)
			{
				case BarcodeSymbology.Code39NC:
					return Code39WithoutChecksum;
				case BarcodeSymbology.Code39C:
					return Code39WithChecksum;
				case BarcodeSymbology.Code93:
					return Code93WithChecksum;
				case BarcodeSymbology.Code128:
					return Code128WithChecksum;
				case BarcodeSymbology.Code11NC:
					return Code11WithoutChecksum;
				case BarcodeSymbology.Code11C:
					return Code11WithChecksum;
				case BarcodeSymbology.CodeEan13:
					return CodeEan13WithChecksum;
				case BarcodeSymbology.CodeEan8:
					return CodeEan8WithChecksum;
				case BarcodeSymbology.Code25StandardNC:
					return Code25StandardWithoutChecksum;
				case BarcodeSymbology.Code25StandardC:
					return Code25StandardWithChecksum;
				case BarcodeSymbology.Code25InterleavedNC:
					return Code25InterleavedWithoutChecksum;
				case BarcodeSymbology.Code25InterleavedC:
					return Code25InterleavedWithChecksum;
				case BarcodeSymbology.CodePdf417:
					return CodePdf417;
				case BarcodeSymbology.CodeQr:
					return CodeQr;
				default:
					throw new ArgumentException(
						Resources.BarcodeSymbologyInvalid, "symbology");
			}
		}
		#endregion
	}

	/// <summary>
	/// <c>BarcodeSymbology</c> defines the supported barcode symbologies.
	/// </summary>
	public enum BarcodeSymbology
	{
		/// <summary>
		/// Unknown symbology.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Code 39 (aka Code 3 of 9) without checksum
		/// </summary>
		Code39NC = 1,

		/// <summary>
		/// Code 39 (aka Code 3 of 9) with checksum
		/// </summary>
		Code39C = 2,

		/// <summary>
		/// Code 93 with checksum
		/// </summary>
		Code93 = 3,

		/// <summary>
		/// Code 128 with checksum
		/// </summary>
		Code128 = 4,

		/// <summary>
		/// Code 11 without checksum
		/// </summary>
		Code11NC = 5,

		/// <summary>
		/// Code 11 with checksum
		/// </summary>
		Code11C = 6,

		/// <summary>
		/// Code EAN-13 with checksum
		/// </summary>
		CodeEan13 = 7,

		/// <summary>
		/// Code EAN-8 with checksum
		/// </summary>
		CodeEan8 = 8,

		/// <summary>
		/// Code 25 standard without checksum
		/// </summary>
		Code25StandardNC = 9,

		/// <summary>
		/// Code 25 standard with checksum
		/// </summary>
		Code25StandardC = 10,

		/// <summary>
		/// Code 25 interleaved without checksum
		/// </summary>
		Code25InterleavedNC = 11,

		/// <summary>
		/// Code 25 interleaved with checksum
		/// </summary>
		Code25InterleavedC = 12,

		/// <summary>
		/// Code PDF 417 (2D symbology with variable error correction)
		/// </summary>
		CodePdf417 = 13,

		/// <summary>
		/// Code QR (2D symbology with error correction)
		/// </summary>
		CodeQr = 14,
	}
}
