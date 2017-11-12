//-----------------------------------------------------------------------
// <copyright file="Code93.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
				
namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
	/// <b>Code93GlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code 93 bar-code glyph
	/// objects.
	/// </summary>
	public class Code93GlyphFactory : GlyphFactory
	{
		#region Private Fields
		private static Code93GlyphFactory _theFactory;
		private static object _syncFactory = new object ();

		private BarGlyph[] _glyphs;
		private CompositeGlyph[] _compositeGlyphs;
		#endregion

		#region Private Constructors
		private Code93GlyphFactory ()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Code93GlyphFactory Instance
		{
			get
			{
				if (_theFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theFactory == null)
						{
							_theFactory = new Code93GlyphFactory ();
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
		protected override BarGlyph[] GetGlyphs ()
		{
			if (_glyphs == null)
			{
				_glyphs = new BarGlyph[]
				{
					new BarGlyph ('0', 0x114),
					new BarGlyph ('1', 0x148),
					new BarGlyph ('2', 0x144),
					new BarGlyph ('3', 0x142),
					new BarGlyph ('4', 0x128),
					new BarGlyph ('5', 0x124),
					new BarGlyph ('6', 0x122),
					new BarGlyph ('7', 0x150),
					new BarGlyph ('8', 0x112),
					new BarGlyph ('9', 0x10A),
					new BarGlyph ('A', 0x1A8),
					new BarGlyph ('B', 0x1A4),
					new BarGlyph ('C', 0x1A2),
					new BarGlyph ('D', 0x194),
					new BarGlyph ('E', 0x192),
					new BarGlyph ('F', 0x18A),
					new BarGlyph ('G', 0x168),
					new BarGlyph ('H', 0x164),
					new BarGlyph ('I', 0x162),
					new BarGlyph ('J', 0x134),
					new BarGlyph ('K', 0x11A),
					new BarGlyph ('L', 0x158),
					new BarGlyph ('M', 0x14C),
					new BarGlyph ('N', 0x146),
					new BarGlyph ('O', 0x12C),
					new BarGlyph ('P', 0x116),
					new BarGlyph ('Q', 0x1B4),
					new BarGlyph ('R', 0x1B2),
					new BarGlyph ('S', 0x1AC),
					new BarGlyph ('T', 0x1A6),
					new BarGlyph ('U', 0x196),
					new BarGlyph ('V', 0x19A),
					new BarGlyph ('W', 0x16C),
					new BarGlyph ('X', 0x166),
					new BarGlyph ('Y', 0x136),
					new BarGlyph ('Z', 0x13A),
					new BarGlyph ('-', 0x12E),
					new BarGlyph ('.', 0x1D4),
					new BarGlyph (' ', 0x1D2),
					new BarGlyph ('$', 0x1CA),
					new BarGlyph ('/', 0x16E),
					new BarGlyph ('+', 0x176),
					new BarGlyph ('%', 0x1AE),
					new BarGlyph ('<', 0x126), // ($)
					new BarGlyph ('=', 0x1DA), // (%)
					new BarGlyph ('>', 0x1D6), // (/)
					new BarGlyph ('?', 0x132), // (+)
					new BarGlyph ('*', 0x15E),
					new BarGlyph ('|', 0x100),
				};
			}
			return _glyphs;
		}

		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:CompositeGlyph"/>
		/// that represent the composite bar-code glyphs for the given bar-code
		/// symbology.
		/// </summary>
		/// <returns></returns>
		protected override CompositeGlyph[] GetCompositeGlyphs ()
		{
			if (_compositeGlyphs == null)
			{
				_compositeGlyphs = new CompositeGlyph[]
				{
					new CompositeGlyph ((char) 0, GetRawGlyph('='), GetRawGlyph ('U')),
					new CompositeGlyph ((char) 1, GetRawGlyph('<'), GetRawGlyph ('A')),
					new CompositeGlyph ((char) 2, GetRawGlyph('<'), GetRawGlyph ('B')),
					new CompositeGlyph ((char) 3, GetRawGlyph('<'), GetRawGlyph ('C')),
					new CompositeGlyph ((char) 4, GetRawGlyph('<'), GetRawGlyph ('D')),
					new CompositeGlyph ((char) 5, GetRawGlyph('<'), GetRawGlyph ('E')),
					new CompositeGlyph ((char) 6, GetRawGlyph('<'), GetRawGlyph ('F')),
					new CompositeGlyph ((char) 7, GetRawGlyph('<'), GetRawGlyph ('G')),
					new CompositeGlyph ((char) 8, GetRawGlyph('<'), GetRawGlyph ('H')),
					new CompositeGlyph ((char) 9, GetRawGlyph('<'), GetRawGlyph ('I')),
					new CompositeGlyph ((char) 10, GetRawGlyph('<'), GetRawGlyph ('J')),
					new CompositeGlyph ((char) 11, GetRawGlyph('<'), GetRawGlyph ('K')),
					new CompositeGlyph ((char) 12, GetRawGlyph('<'), GetRawGlyph ('L')),
					new CompositeGlyph ((char) 13, GetRawGlyph('<'), GetRawGlyph ('M')),
					new CompositeGlyph ((char) 14, GetRawGlyph('<'), GetRawGlyph ('N')),
					new CompositeGlyph ((char) 15, GetRawGlyph('<'), GetRawGlyph ('O')),
					new CompositeGlyph ((char) 16, GetRawGlyph('<'), GetRawGlyph ('P')),
					new CompositeGlyph ((char) 17, GetRawGlyph('<'), GetRawGlyph ('Q')),
					new CompositeGlyph ((char) 18, GetRawGlyph('<'), GetRawGlyph ('R')),
					new CompositeGlyph ((char) 19, GetRawGlyph('<'), GetRawGlyph ('S')),
					new CompositeGlyph ((char) 20, GetRawGlyph('<'), GetRawGlyph ('T')),
					new CompositeGlyph ((char) 21, GetRawGlyph('<'), GetRawGlyph ('U')),
					new CompositeGlyph ((char) 22, GetRawGlyph('<'), GetRawGlyph ('V')),
					new CompositeGlyph ((char) 23, GetRawGlyph('<'), GetRawGlyph ('W')),
					new CompositeGlyph ((char) 24, GetRawGlyph('<'), GetRawGlyph ('X')),
					new CompositeGlyph ((char) 25, GetRawGlyph('<'), GetRawGlyph ('Y')),
					new CompositeGlyph ((char) 26, GetRawGlyph('<'), GetRawGlyph ('Z')),
					new CompositeGlyph ((char) 27, GetRawGlyph('='), GetRawGlyph ('A')),
					new CompositeGlyph ((char) 28, GetRawGlyph('='), GetRawGlyph ('B')),
					new CompositeGlyph ((char) 29, GetRawGlyph('='), GetRawGlyph ('C')),
					new CompositeGlyph ((char) 30, GetRawGlyph('='), GetRawGlyph ('D')),
					new CompositeGlyph ((char) 31, GetRawGlyph('='), GetRawGlyph ('E')),
					new CompositeGlyph (';', GetRawGlyph('='), GetRawGlyph ('F')),
					new CompositeGlyph ('<', GetRawGlyph('='), GetRawGlyph ('G')),
					new CompositeGlyph ('=', GetRawGlyph('='), GetRawGlyph ('H')),
					new CompositeGlyph ('>', GetRawGlyph('='), GetRawGlyph ('I')),
					new CompositeGlyph ('?', GetRawGlyph('='), GetRawGlyph ('J')),
					new CompositeGlyph ('[', GetRawGlyph('='), GetRawGlyph ('K')),
					new CompositeGlyph ('\\', GetRawGlyph('='), GetRawGlyph ('L')),
					new CompositeGlyph (']', GetRawGlyph('='), GetRawGlyph ('M')),
					new CompositeGlyph ('^', GetRawGlyph('='), GetRawGlyph ('N')),
					new CompositeGlyph ('_', GetRawGlyph('='), GetRawGlyph ('O')),
					new CompositeGlyph ('{', GetRawGlyph('='), GetRawGlyph ('P')),
					new CompositeGlyph ('|', GetRawGlyph('='), GetRawGlyph ('Q')),
					new CompositeGlyph ('}', GetRawGlyph('='), GetRawGlyph ('R')),
					new CompositeGlyph ('~', GetRawGlyph('='), GetRawGlyph ('S')),
					new CompositeGlyph ('@', GetRawGlyph('='), GetRawGlyph ('V')),
					new CompositeGlyph ('`', GetRawGlyph('='), GetRawGlyph ('W')),
					new CompositeGlyph ((char) 127, GetRawGlyph('='), GetRawGlyph ('Z')),
					new CompositeGlyph ('!', GetRawGlyph('>'), GetRawGlyph ('A')),
					new CompositeGlyph ('\"', GetRawGlyph('>'), GetRawGlyph ('B')),
					new CompositeGlyph ('#', GetRawGlyph('>'), GetRawGlyph ('C')),
					new CompositeGlyph ('$', GetRawGlyph('>'), GetRawGlyph ('D')),
					new CompositeGlyph ('%', GetRawGlyph('>'), GetRawGlyph ('E')),
					new CompositeGlyph ('&', GetRawGlyph('>'), GetRawGlyph ('F')),
					new CompositeGlyph ('\'', GetRawGlyph('>'), GetRawGlyph ('G')),
					new CompositeGlyph ('(', GetRawGlyph('>'), GetRawGlyph ('H')),
					new CompositeGlyph (')', GetRawGlyph('>'), GetRawGlyph ('I')),
					new CompositeGlyph ('*', GetRawGlyph('>'), GetRawGlyph ('J')),
					new CompositeGlyph ('+', GetRawGlyph('>'), GetRawGlyph ('K')),
					new CompositeGlyph (',', GetRawGlyph('>'), GetRawGlyph ('L')),
					new CompositeGlyph ('a', GetRawGlyph('?'), GetRawGlyph ('A')),
					new CompositeGlyph ('b', GetRawGlyph('?'), GetRawGlyph ('B')),
					new CompositeGlyph ('c', GetRawGlyph('?'), GetRawGlyph ('C')),
					new CompositeGlyph ('d', GetRawGlyph('?'), GetRawGlyph ('D')),
					new CompositeGlyph ('e', GetRawGlyph('?'), GetRawGlyph ('E')),
					new CompositeGlyph ('f', GetRawGlyph('?'), GetRawGlyph ('F')),
					new CompositeGlyph ('g', GetRawGlyph('?'), GetRawGlyph ('G')),
					new CompositeGlyph ('h', GetRawGlyph('?'), GetRawGlyph ('H')),
					new CompositeGlyph ('i', GetRawGlyph('?'), GetRawGlyph ('I')),
					new CompositeGlyph ('j', GetRawGlyph('?'), GetRawGlyph ('J')),
					new CompositeGlyph ('k', GetRawGlyph('?'), GetRawGlyph ('K')),
					new CompositeGlyph ('l', GetRawGlyph('?'), GetRawGlyph ('L')),
					new CompositeGlyph ('m', GetRawGlyph('?'), GetRawGlyph ('M')),
					new CompositeGlyph ('n', GetRawGlyph('?'), GetRawGlyph ('N')),
					new CompositeGlyph ('o', GetRawGlyph('?'), GetRawGlyph ('O')),
					new CompositeGlyph ('p', GetRawGlyph('?'), GetRawGlyph ('P')),
					new CompositeGlyph ('q', GetRawGlyph('?'), GetRawGlyph ('Q')),
					new CompositeGlyph ('r', GetRawGlyph('?'), GetRawGlyph ('R')),
					new CompositeGlyph ('s', GetRawGlyph('?'), GetRawGlyph ('S')),
					new CompositeGlyph ('t', GetRawGlyph('?'), GetRawGlyph ('T')),
					new CompositeGlyph ('u', GetRawGlyph('?'), GetRawGlyph ('U')),
					new CompositeGlyph ('v', GetRawGlyph('?'), GetRawGlyph ('V')),
					new CompositeGlyph ('w', GetRawGlyph('?'), GetRawGlyph ('W')),
					new CompositeGlyph ('x', GetRawGlyph('?'), GetRawGlyph ('X')),
					new CompositeGlyph ('y', GetRawGlyph('?'), GetRawGlyph ('Y')),
					new CompositeGlyph ('z', GetRawGlyph('?'), GetRawGlyph ('Z'))
				};
			}
			return _compositeGlyphs;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code93Checksum</b> implements a Code93 checksum generator as a
	/// singleton class.
	/// </summary>
	/// <remarks>
	/// This class cannot be inherited from.
	/// </remarks>
	public sealed class Code93Checksum : FactoryChecksum<Code93GlyphFactory>
	{
		#region Private Fields
		private static Code93Checksum _theChecksum;
		private static object _syncChecksum = new object ();
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:Code93Checksum"/> class.
		/// </summary>
		private Code93Checksum ()
			: base (Code93GlyphFactory.Instance)
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the static <see cref="T:Code93Checksum"/> object instance.
		/// </summary>
		public static Code93Checksum Instance
		{
			get
			{
				if (_theChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theChecksum == null)
						{
							_theChecksum = new Code93Checksum ();
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
		/// <param name="text"></param>
		/// <returns></returns>
		public override Glyph[] GetChecksum (string text)
		{
			if (string.IsNullOrEmpty (text))
			{
				throw new ArgumentNullException ("text");
			}

			// Determine checksum
			char firstCheck = GetChecksumChar (text, 20);
			text += firstCheck;
			char secondCheck = GetChecksumChar (text, 15);

			List<Glyph> result = new List<Glyph> ();
			result.Add (Factory.GetRawGlyph (firstCheck));
			result.Add (Factory.GetRawGlyph (secondCheck));
			return result.ToArray ();
		}
		#endregion

		#region Private Methods
		private char GetChecksumChar (string text, int weighting)
		{
			int weightedSum = 0;
			for (int index = 0; index < text.Length; ++index)
			{
				int checkValue = Factory.GetRawCharIndex (text[text.Length - index - 1]);
				int weight = (index % weighting) + 1;
				weightedSum += (weight * checkValue);
			}
			return Factory.GetRawGlyph (weightedSum % 47).Character;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code93BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
	/// that can render complete Code93 barcodes with checksum.
	/// </summary>
	public class Code93BarcodeDraw
		: BarcodeDrawBase<Code93GlyphFactory, Code93Checksum>
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Code93BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="checksum">The checksum.</param>
		public Code93BarcodeDraw (Code93Checksum checksum)
			: base (checksum.Factory, checksum, 9)
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
			return new BarcodeMetrics (1, 2, maxHeight);
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
            return new BarcodeMetrics(narrowBarWidth, narrowBarWidth * 2, maxHeight);
        }
        #endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Gets the glyphs needed to render a full barcode.
		/// </summary>
		/// <param name="text">Text to convert into bar-code.</param>
		/// <returns>A collection of <see cref="T:Glyph"/> objects.</returns>
		protected override Glyph[] GetFullBarcode (string text)
		{
			List<Glyph> result = new List<Glyph> ();
			result.AddRange (Factory.GetGlyphs (text));
			if (Checksum != null)
			{
				result.AddRange (Checksum.GetChecksum (text));
			}
			result.Insert (0, Factory.GetRawGlyph ('*'));
			result.Add (Factory.GetRawGlyph ('*'));
			result.Add (Factory.GetRawGlyph ('|'));
			return result.ToArray ();
		}

		/// <summary>
		/// Overridden. Gets the length in pixels needed to render the specified barcode.
		/// </summary>
		/// <param name="barcode">Barcode glyphs to be analysed.</param>
		/// <param name="interGlyphSpace">Amount of inter-glyph space.</param>
		/// <param name="barMinWidth">Minimum barcode width.</param>
		/// <param name="barMaxWidth">Maximum barcode width.</param>
		/// <returns>The barcode width in pixels.</returns>
		/// <remarks>
		/// Currently this method does not account for any "quiet space"
		/// around the barcode as dictated by each symbology standard.
		/// </remarks>
		protected override int GetBarcodeLength (Glyph[] barcode, 
			int interGlyphSpace, int barMinWidth, int barMaxWidth)
		{
			return base.GetBarcodeLength (barcode, interGlyphSpace,
				barMinWidth, barMaxWidth) - (8 * barMinWidth);
		}
		#endregion
	}
}
