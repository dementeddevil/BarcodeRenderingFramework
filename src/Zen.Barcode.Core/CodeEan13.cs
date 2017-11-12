//-----------------------------------------------------------------------
// <copyright file="CodeEan13.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;
	using System.Text.RegularExpressions;

	/// <summary>
	/// <b>CodeEan13GlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code EAN 13 bar-code glyph
	/// objects.
	/// </summary>
	public sealed class CodeEan13GlyphFactory : GlyphFactory
	{
		#region Private Fields
		private static CodeEan13GlyphFactory _theFactory;
		private static object _syncFactory = new object();

		private BarGlyph[] _glyphs;
		private CompositeGlyph[] _compositeGlyphs;
		#endregion

		#region Private Constructors
		private CodeEan13GlyphFactory()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static CodeEan13GlyphFactory Instance
		{
			get
			{
				if (_theFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theFactory == null)
						{
							_theFactory = new CodeEan13GlyphFactory();
						}
					}
				}
				return _theFactory;
			}
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Gets the array of <see cref="T:Glyph"/> objects that correspond
		/// to the specified string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="allowComposite">if set to <c>true</c> [allow composite].</param>
		/// <returns></returns>
		/// <remarks>
		/// For this method to work as expected the barcode text should not
		/// include the first EAN13 digit (this is used to derive parity and
		/// is not rendered) but should include the checksum digit.
		/// </remarks>
		public override Glyph[] GetGlyphs(string text, bool allowComposite)
		{
			List<Glyph> result = new List<Glyph>();
			for (int index = 0; index < text.Length; ++index)
			{
				int digit = text[index] - '0';
				if (digit < 0 || digit > 9)
				{
					throw new ArgumentException("EAN13 barcode invalid.");
				}

				if (index < 6)
				{
					result.AddRange(GetGlyphs(text[index], allowComposite));
				}
				else
				{
					result.Add(GetRawGlyph((char)('k' + digit)));
				}
			}
			return result.ToArray();
		}

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
					// Start/stop glyphs
					new VaryLengthGlyph ('*', 0x05, 3),	// 0000101

					// Seperator glyph
					new VaryLengthGlyph ('|', 0x0A, 5),	// 0001010

					// Glyphs when LHS, ODD parity
					new BarGlyph ('A', 0x0D),	// 0001101
					new BarGlyph ('B', 0x19),	// 0011001
					new BarGlyph ('C', 0x13),	// 0010011
					new BarGlyph ('D', 0x3D),	// 0111101
					new BarGlyph ('E', 0x23),	// 0100011
					new BarGlyph ('F', 0x31),	// 0110001
					new BarGlyph ('G', 0x2F),	// 0101111
					new BarGlyph ('H', 0x3B),	// 0111011
					new BarGlyph ('I', 0x37),	// 0110111
					new BarGlyph ('J', 0x0B),	// 0001011

					// Glyphs when LHS, EVEN parity
					new BarGlyph ('a', 0x27),	// 0100111
					new BarGlyph ('b', 0x33),	// 0110011
					new BarGlyph ('c', 0x1B),	// 0011011
					new BarGlyph ('d', 0x21),	// 0100001
					new BarGlyph ('e', 0x1D),	// 0011101
					new BarGlyph ('f', 0x39),	// 0111001
					new BarGlyph ('g', 0x05),	// 0000101
					new BarGlyph ('h', 0x11),	// 0010001
					new BarGlyph ('i', 0x09),	// 0001001
					new BarGlyph ('j', 0x17),	// 0010111

					// Glyphs when RHS 
					new BarGlyph ('k', 0x72),	// 1110010
					new BarGlyph ('l', 0x66),	// 1100110
					new BarGlyph ('m', 0x6C),	// 1101100
					new BarGlyph ('n', 0x42),	// 1000010
					new BarGlyph ('o', 0x5C),	// 1011100
					new BarGlyph ('p', 0x4E),	// 1001110
					new BarGlyph ('q', 0x50),	// 1010000
					new BarGlyph ('r', 0x44),	// 1000100
					new BarGlyph ('s', 0x48),	// 1001000
					new BarGlyph ('t', 0x74),	// 1110100
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
			if (_compositeGlyphs == null)
			{
				_compositeGlyphs = new CompositeGlyph[]
				{
					new CompositeGlyph ('0', GetRawGlyph('A'), GetRawGlyph ('a')),
					new CompositeGlyph ('1', GetRawGlyph('B'), GetRawGlyph ('b')),
					new CompositeGlyph ('2', GetRawGlyph('C'), GetRawGlyph ('c')),
					new CompositeGlyph ('3', GetRawGlyph('D'), GetRawGlyph ('d')),
					new CompositeGlyph ('4', GetRawGlyph('E'), GetRawGlyph ('e')),
					new CompositeGlyph ('5', GetRawGlyph('F'), GetRawGlyph ('f')),
					new CompositeGlyph ('6', GetRawGlyph('G'), GetRawGlyph ('g')),
					new CompositeGlyph ('7', GetRawGlyph('H'), GetRawGlyph ('h')),
					new CompositeGlyph ('8', GetRawGlyph('I'), GetRawGlyph ('i')),
					new CompositeGlyph ('9', GetRawGlyph('J'), GetRawGlyph ('j')),
				};
			}
			return _compositeGlyphs;
		}
		#endregion
	}

	/// <summary>
	/// <b>CodeEan13Checksum</b> implements a Code EAN-13 checksum generator
	/// as a singleton class.
	/// </summary>
	/// <remarks>
	/// This class cannot be inherited from.
	/// </remarks>
	public sealed class CodeEan13Checksum : FactoryChecksum<CodeEan13GlyphFactory>
	{
		#region Private Fields
		private static CodeEan13Checksum _theChecksum;
		private static object _syncChecksum = new object();
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:CodeEan13Checksum"/> class.
		/// </summary>
		private CodeEan13Checksum()
			: base(CodeEan13GlyphFactory.Instance)
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the static <see cref="T:CodeEan13Checksum"/> object instance.
		/// </summary>
		public static CodeEan13Checksum Instance
		{
			get
			{
				if (_theChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theChecksum == null)
						{
							_theChecksum = new CodeEan13Checksum();
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
		/// <param name="text">Text to be processed.</param>
		/// <param name="allowComposite">if set to <c>true</c> [allow composite].</param>
		/// <returns>
		/// A collection of <see cref="T:Glyph"/> objects representing
		/// the checksum information.
		/// </returns>
		public override Glyph[] GetChecksum(string text, bool allowComposite)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}

			// Determine checksum
			char check = (char)(GetChecksumChar(text) - '0' + 'k');
			return Factory.GetGlyphs(check, allowComposite);
		}

		/// <summary>
		/// Gets the checksum char.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public char GetChecksumChar(string text)
		{
			int sum = 0;
			int textLength = text.Length - 1;
			for (int index = 0; index < text.Length; ++index)
			{
				int digitValue = text[textLength - index] - '0';
				if ((index % 2) == 0)
				{
					sum += digitValue * 3;
				}
				else
				{
					sum += digitValue;
				}
			}
			sum = sum % 10;
			if (sum > 0)
			{
				sum = 10 - sum;
			}
			return (char)('0' + sum);
		}
		#endregion
	}

	/// <summary>
	/// <b>CodeEan13BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
	/// that can render complete Code EAN 13 barcodes with checksum.
	/// </summary>
	public class CodeEan13BarcodeDraw
		: BarcodeDrawBase<CodeEan13GlyphFactory, CodeEan13Checksum>
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CodeEan13BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="checksum">The checksum.</param>
		public CodeEan13BarcodeDraw(CodeEan13Checksum checksum)
			: base(checksum.Factory, checksum, 7)
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
			// TODO: Min bar height should be percentage of max height
			return new BarcodeMetrics1d(1, 1, maxHeight - 5, maxHeight);
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
			int minHeight = (maxHeight * 85) / 100;
			int narrowBarWidth = (printResolution.Width * desiredBarcodeDimensions.Width) /
				(100 * (24 + (barcodeCharLength * 11)));
			return new BarcodeMetrics1d(narrowBarWidth, narrowBarWidth, minHeight, maxHeight);
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
			// Run proposed barcode through validator
			Match m = Regex.Match(text, @"^\s*(?<barcode>[0-9]{12})\s*$");
			if (!m.Success)
			{
				throw new ArgumentException("Invalid barcode.");
			}
			string barcodeText = m.Groups["barcode"].Value;

			// Determine parity to use
			byte[] parityTable = new byte[]
			{
				0x00, // OOOOOO
				0x0B, // OOEOEE
				0x0D, // OOEEOE
				0x0E, // OOEEEO
				0x13, // OEOOEE
				0x19, // OEEOOE
				0x1D, // OEEEOE
				0x15, // OEOEOE
				0x16, // OEOEEO
				0x1A, // OEEOEO
			};

			// Determine parity value for remaining encoding then strip
			//	after calculating the checksum
			char parityDigit = barcodeText[0];
			byte parity = parityTable[parityDigit - '0'];
			if (Checksum != null)
			{
				barcodeText += Checksum.GetChecksumChar(barcodeText);
			}
			barcodeText = barcodeText.Substring(1);

			// Build result with composite glyphs
			List<Glyph> result = new List<Glyph>();
			result.AddRange(Factory.GetGlyphs(barcodeText, true));

			// Now translate each composite glyph using the parity
			//	in the second digit.
			// The first two digits are encoded without parity.
			int parityIndex = 32;
			for (int index = 0; index < result.Count; ++index)
			{
				// Fetch next glyph
				if (result[index] is CompositeGlyph)
				{
					// Determine effective parity arrangement
					byte effectiveParity = (index < 6) ? parity : (byte)0;

					CompositeGlyph composite = (CompositeGlyph)result[index];
					if ((parityIndex & effectiveParity) == 0)
					{
						result[index] = composite.First;
					}
					else
					{
						result[index] = composite.Second;
					}
					if (parityIndex > 1)
					{
						parityIndex /= 2;
					}
				}
			}

			// Add start/stop and seperator glyphs
			result.Insert(6, Factory.GetRawGlyph('|'));
			result.Insert(0, Factory.GetRawGlyph('*'));
			result.Add(Factory.GetRawGlyph('*'));
			return result.ToArray();
		}

		/// <summary>
		/// Gets the height of the glyph.
		/// </summary>
		/// <param name="glyph">The glyph.</param>
		/// <param name="barMinHeight">Height of the bar min.</param>
		/// <param name="barMaxHeight">Height of the bar max.</param>
		/// <returns></returns>
		/// <remarks>
		/// For EAN 13 the only full height glyphs are start/stop/seperator
		/// glyphs.
		/// </remarks>
		protected override int GetGlyphHeight(Glyph glyph, int barMinHeight, int barMaxHeight)
		{
			if (glyph.Character == '*' || glyph.Character == '|')
			{
				return barMaxHeight;
			}
			return barMinHeight;
		}

		/// <summary>
		/// Overridden. Gets the default inter-glyph spacing in pixels.
		/// </summary>
		/// <param name="barMinWidth"></param>
		/// <param name="barMaxWidth"></param>
		/// <returns></returns>
		protected override int GetDefaultInterGlyphSpace(int barMinWidth, int barMaxWidth)
		{
			return 0;
		}
		#endregion
	}
}
