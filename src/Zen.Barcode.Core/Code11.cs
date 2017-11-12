//-----------------------------------------------------------------------
// <copyright file="Code11.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// <b>Code11GlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code 11 bar-code glyph
	/// objects.
	/// </summary>
	public sealed class Code11GlyphFactory : GlyphFactory
	{
		#region Private Fields
		private static Code11GlyphFactory _theFactory;
		private static object _syncFactory = new object();

		private BarGlyph[] _glyphs;
		#endregion

		#region Private Constructors
		private Code11GlyphFactory()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Code11GlyphFactory Instance
		{
			get
			{
				if (_theFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theFactory == null)
						{
							_theFactory = new Code11GlyphFactory();
						}
					}
				}
				return _theFactory;
			}
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:BarGlyph"/> that
		/// represent the raw bar-code glyphs for the given bar-code symbology.
		/// </summary>
		/// <returns></returns>
		protected override BarGlyph[] GetGlyphs()
		{
			if (_glyphs == null)
			{
				_glyphs = new BarGlyph[]
				{
					new BinaryPitchVaryLengthGlyph ('0', 0x2B, 0x01, 6),
					new BinaryPitchVaryLengthGlyph ('1', 0x6B, 0x11, 7),
					new BinaryPitchVaryLengthGlyph ('2', 0x4B, 0x09, 7),
					new BinaryPitchVaryLengthGlyph ('3', 0x65, 0x18, 7),
					new BinaryPitchVaryLengthGlyph ('4', 0x5B, 0x05, 7),
					new BinaryPitchVaryLengthGlyph ('5', 0x6D, 0x14, 7),
					new BinaryPitchVaryLengthGlyph ('6', 0x4D, 0x0C, 7),
					new BinaryPitchVaryLengthGlyph ('7', 0x53, 0x03, 7),
					new BinaryPitchVaryLengthGlyph ('8', 0x69, 0x12, 7),
					new BinaryPitchVaryLengthGlyph ('9', 0x35, 0x10, 6),
					new BinaryPitchVaryLengthGlyph ('-', 0x2D, 0x04, 6),
					new BinaryPitchVaryLengthGlyph ('*', 0x59, 0x06, 7)
				};
			}
			return _glyphs;
		}

		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:CompositeGlyph"/>
		/// that represent the composite bar-code glyphs for the given 
		/// bar-code symbology.
		/// </summary>
		/// <returns></returns>
		protected override CompositeGlyph[] GetCompositeGlyphs()
		{
			return new CompositeGlyph[0];
		}
		#endregion
	}

	/// <summary>
	/// <b>Code11Checksum</b> implements a Code11 checksum generator as a
	/// singleton class.
	/// </summary>
	/// <remarks>
	/// This class cannot be inherited from.
	/// </remarks>
	public sealed class Code11Checksum : FactoryChecksum<Code11GlyphFactory>
	{
		#region Private Fields
		private static Code11Checksum _theChecksum;
		private static object _syncChecksum = new object();
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:Code11Checksum"/> class.
		/// </summary>
		private Code11Checksum()
			: base(Code11GlyphFactory.Instance)
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the static <see cref="T:Code11Checksum"/> object instance.
		/// </summary>
		public static Code11Checksum Instance
		{
			get
			{
				if (_theChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theChecksum == null)
						{
							_theChecksum = new Code11Checksum();
						}
					}
				}
				return _theChecksum;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Overridden. Gets an array of <see cref="T:Glyph"/> objects that
		/// represent the checksum for the specified text string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="allowComposite">if set to <c>true</c> [allow composite].</param>
		/// <returns>
		/// A collection of <see cref="T:Glyph"/> objects that
		/// represent the checksum.
		/// </returns>
		public override Glyph[] GetChecksum(string text, bool allowComposite)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}

			// Determine checksum
			if (text.Length > 10)
			{
				char checkC = GetChecksumChar(text, 11);
				text += checkC;
				char checkK = GetChecksumChar(text, 9);
				string checkCK = string.Format("{0}{1}", checkC, checkK);
				return Factory.GetGlyphs(checkCK, allowComposite);
			}
			else
			{
				char checkC = GetChecksumChar(text, 11);
				return Factory.GetGlyphs(checkC, allowComposite);
			}
		}
		#endregion

		#region Private Methods
		private char GetChecksumChar(string text, int weight)
		{
			int sum = 0;
			for (int index = 0; index < text.Length; ++index)
			{
				int checkValue = Factory.GetRawCharIndex(text[text.Length - index - 1]);
				sum += (checkValue * ((index % 10) + 1));
			}
			return Factory.GetRawGlyph((int)(sum % weight)).Character;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code11BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
	/// that can render complete Code11 barcodes with or without checksum.
	/// </summary>
	public class Code11BarcodeDraw
		: BinaryPitchVaryLengthBarcodeDraw<Code11GlyphFactory, Code11Checksum>
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Code11BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="factory">The factory.</param>
		public Code11BarcodeDraw(Code11GlyphFactory factory)
			: base(factory, 0, 5)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Code11BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="checksum">The checksum.</param>
		public Code11BarcodeDraw(Code11Checksum checksum)
			: base(checksum.Factory, checksum, 0, 5)
		{
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Overridden. Gets a <see cref="T:BarcodeMetrics"/> object 
		/// containing default settings for the specified maximum bar height.
		/// </summary>
		/// <param name="maxHeight">The maximum barcode height.</param>
		/// <returns>
		/// A <see cref="T:BarcodeMetrics"/> object.
		/// </returns>
		public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
		{
			return new BarcodeMetrics1d(1, 3, maxHeight);
		}

		/// <summary>
		/// Overridden. Gets a <see cref="T:BarcodeMetrics"/> object containing the print
		/// metrics needed for printing a barcode of the specified physical
		/// size on a device operating at the specified resolution.
		/// </summary>
		/// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
		/// <param name="printResolution">The print resolution in pixels per inch.</param>
		/// <param name="barcodeCharLength">Length of the barcode in characters.</param>
		/// <returns>
		/// A <see cref="T:BarcodeMetrics"/> object.
		/// </returns>
		public override BarcodeMetrics GetPrintMetrics(
			Size desiredBarcodeDimensions, Size printResolution,
			int barcodeCharLength)
		{
			int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
			int narrowBarWidth = (printResolution.Width * desiredBarcodeDimensions.Width) /
				(100 * (24 + (barcodeCharLength * 12)));
			return new BarcodeMetrics1d(narrowBarWidth, narrowBarWidth * 2, maxHeight);
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Gets the glyphs needed to render a full barcode.
		/// </summary>
		/// <param name="text">Text to convert into bar-code.</param>
		/// <returns>A collection of <see cref="T:Glyph"/> objects.</returns>
		protected override Glyph[] GetFullBarcode(string text)
		{
			List<Glyph> result = new List<Glyph>();
			result.AddRange(Factory.GetGlyphs(text));
			if (Checksum != null)
			{
				result.AddRange(Checksum.GetChecksum(text));
			}
			result.Insert(0, Factory.GetRawGlyph('*'));
			result.Add(Factory.GetRawGlyph('*'));
			return result.ToArray();
		}

		/// <summary>
		/// Overridden. Gets the default inter-glyph spacing in pixels.
		/// </summary>
		/// <param name="barMinWidth"></param>
		/// <param name="barMaxWidth"></param>
		/// <returns></returns>
		protected override int GetDefaultInterGlyphSpace(int barMinWidth, int barMaxWidth)
		{
			return barMinWidth;
		}
		#endregion
	}
}
