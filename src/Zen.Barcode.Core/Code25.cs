//-----------------------------------------------------------------------
// <copyright file="Code25.cs" company="Zen Design Corp">
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
	/// <b>Code25GlyphFactory</b> base class for all code 25 
	/// <see cref="GlyphFactory"/> objects.
	/// </summary>
	public abstract class Code25GlyphFactory : GlyphFactory
	{
		#region Private Fields
		private static Code25StandardGlyphFactory _theStdFactory;
		private static Code25InterleavedGlyphFactory _theIntFactory;
		private static object _syncFactory = new object ();
		#endregion

		#region Protected Constructors
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="T:Code25GlyphFactory"/> class.
		/// </summary>
		protected Code25GlyphFactory ()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Code25StandardGlyphFactory StandardInstance
		{
			get
			{
				if (_theStdFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theStdFactory == null)
						{
							_theStdFactory = new Code25StandardGlyphFactory ();
						}
					}
				}
				return _theStdFactory;
			}
		}

		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Code25InterleavedGlyphFactory InterleavedInstance
		{
			get
			{
				if (_theIntFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theIntFactory == null)
						{
							_theIntFactory = new Code25InterleavedGlyphFactory ();
						}
					}
				}
				return _theIntFactory;
			}
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:CompositeGlyph"/>
		/// that represent the composite bar-code glyphs for the given 
		/// bar-code symbology.
		/// </summary>
		/// <returns></returns>
		protected override CompositeGlyph[] GetCompositeGlyphs ()
		{
			return new CompositeGlyph[0];
		}
		#endregion
	}

	/// <summary>
	/// <b>Code25StandardGlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code 25 bar-code glyph
	/// objects.
	/// </summary>
	public sealed class Code25StandardGlyphFactory : Code25GlyphFactory
	{
		#region Private Fields
		private BarGlyph[] _glyphs;
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="T:Code25StandardGlyphFactory"/> class.
		/// </summary>
		public Code25StandardGlyphFactory ()
		{
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
					new VaryLengthGlyph ('0', 0x15DD, 13),
					new VaryLengthGlyph ('1', 0x1D57, 13),
					new VaryLengthGlyph ('2', 0x1757, 13),
					new VaryLengthGlyph ('3', 0x1DD5, 13),
					new VaryLengthGlyph ('4', 0x15D7, 13),
					new VaryLengthGlyph ('5', 0x1D75, 13),
					new VaryLengthGlyph ('6', 0x1775, 13),
					new VaryLengthGlyph ('7', 0x1577, 13),
					new VaryLengthGlyph ('8', 0x1D5D, 13),
					new VaryLengthGlyph ('9', 0x175D, 13),
					new VaryLengthGlyph ('-', 0x006D, 7),
					new VaryLengthGlyph ('*', 0x006B, 7)
				};
			}
			return _glyphs;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code25InterleavedGlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code 25 bar-code glyph
	/// objects.
	/// </summary>
	public sealed class Code25InterleavedGlyphFactory: Code25GlyphFactory
	{
		#region Private Fields
		private BarGlyph[] _glyphs;
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="T:Code25InterleavedGlyphFactory"/> class.
		/// </summary>
		public Code25InterleavedGlyphFactory ()
		{
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
					new VaryLengthGlyph ('0', 6, 5),
					new VaryLengthGlyph ('1', 17, 5),
					new VaryLengthGlyph ('2', 9, 5),
					new VaryLengthGlyph ('3', 24, 5),
					new VaryLengthGlyph ('4', 5, 5),
					new VaryLengthGlyph ('5', 20, 5),
					new VaryLengthGlyph ('6', 12, 5),
					new VaryLengthGlyph ('7', 3, 5),
					new VaryLengthGlyph ('8', 18, 5),
					new VaryLengthGlyph ('9', 10, 5),
					new VaryLengthGlyph ('-', 0x0A, 4),
					new VaryLengthGlyph ('*', 0x0D, 4)
				};
			}
			return _glyphs;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code25Checksum</b> implements a Code25Standard checksum generator as a
	/// singleton class.
	/// </summary>
	/// <remarks>
	/// This class cannot be inherited from.
	/// </remarks>
	public sealed class Code25Checksum : FactoryChecksum<Code25GlyphFactory>
	{
		#region Private Fields
		private static Code25Checksum _theStdChecksum;
		private static Code25Checksum _theIntChecksum;
		private static object _syncChecksum = new object ();
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:Code25Checksum"/> class.
		/// </summary>
		private Code25Checksum (Code25GlyphFactory factory)
			: base (factory)
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the static <see cref="T:Code25Checksum"/> object instance.
		/// </summary>
		public static Code25Checksum StandardInstance
		{
			get
			{
				if (_theStdChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theStdChecksum == null)
						{
							_theStdChecksum = new Code25Checksum (
								Code25GlyphFactory.StandardInstance);
						}
					}
				}
				return _theStdChecksum;
			}
		}

		/// <summary>
		/// Gets the static <see cref="T:Code25Checksum"/> object instance.
		/// </summary>
		public static Code25Checksum InterleavedInstance
		{
			get
			{
				if (_theIntChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theIntChecksum == null)
						{
							_theIntChecksum = new Code25Checksum (
								Code25GlyphFactory.InterleavedInstance);
						}
					}
				}
				return _theIntChecksum;
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
		/// <returns></returns>
		public override Glyph[] GetChecksum (string text, bool allowComposite)
		{
			if (string.IsNullOrEmpty (text))
			{
				throw new ArgumentNullException ("text");
			}

			// Determine checksum
			bool even = true;
			int oddTotal = 0, evenTotal = 0;
			for (int index = text.Length - 1; index >= 0; --index, even = !even)
			{
				char ch = text[index];
				if (!char.IsDigit (ch))
				{
					throw new InvalidOperationException ("text contains invalid characters - numbers only.");
				}

				int digit = (ch - '0');
				if (even)
				{
					evenTotal += (digit * 3);
				}
				else
				{
					oddTotal += digit;
				}
			}

			int checkDigit = 10 - ((evenTotal + oddTotal) % 10);
			return new Glyph[] { Factory.GetRawGlyph (checkDigit) };
		}
		#endregion
	}

	/// <summary>
	/// <b>Code25StandardBarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
	/// that can render complete Code25Standard barcodes with or without checksum.
	/// </summary>
	public class Code25BarcodeDraw
		: BarcodeDrawBase<Code25GlyphFactory, Code25Checksum>
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Code25BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="factory">The factory.</param>
		public Code25BarcodeDraw (Code25GlyphFactory factory)
			: base (factory, 0)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Code25BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="checksum">The checksum.</param>
		public Code25BarcodeDraw (Code25Checksum checksum)
			: base (checksum.Factory, checksum, 0)
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
			return new BarcodeMetrics (1, 1, maxHeight);
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
                (100 * (24 + (barcodeCharLength * 11)));
            return new BarcodeMetrics(narrowBarWidth, maxHeight);
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

			// If text is odd length
			if (Factory is Code25InterleavedGlyphFactory)
			{
				bool isOddLength = false;
				if ((text.Length % 2) == 1)
				{
					isOddLength = true;
				}

				// Interleaved 2 of 5 can only encode even number of digits
				if (isOddLength)
				{
					text = "0" + text;
				}
			}

			result.AddRange (Factory.GetGlyphs (text));
			if (Checksum != null)
			{
				result.AddRange (Checksum.GetChecksum (text));
			}

			// Add start/stop glyphs
			result.Insert (0, Factory.GetRawGlyph ('-'));
			result.Add (Factory.GetRawGlyph ('*'));
			return result.ToArray ();
		}

		/// <summary>
		/// Overridden. Gets the default inter-glyph spacing in pixels.
		/// </summary>
		/// <param name="barMinWidth"></param>
		/// <param name="barMaxWidth"></param>
		/// <returns></returns>
		protected override int GetDefaultInterGlyphSpace (int barMinWidth, int barMaxWidth)
		{
			if (Factory is Code25StandardGlyphFactory)
			{
				return barMinWidth;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Gets the length in pixels needed to render the specified barcode.
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
		protected override int GetBarcodeLength(
			Glyph[] barcode, int interGlyphSpace, int barMinWidth, int barMaxWidth)
		{
			if (!(Factory is Code25InterleavedGlyphFactory))
			{
				return base.GetBarcodeLength(barcode, interGlyphSpace, barMinWidth, barMaxWidth);
			}

			return (((barcode.Length - 2) * 7) + 8) * barMinWidth;
		}

		/// <summary>
		/// Renders the barcode bars.
		/// </summary>
		/// <param name="barcode">A collection of <see cref="T:Zen.Barcode.Glyph"/> objects representing the
		/// barcode to be rendered.</param>
		/// <param name="dc">A <see cref="T:System.Drawing.Graphics"/> representing the draw context.</param>
		/// <param name="bounds">The bounding rectangle.</param>
		/// <param name="interGlyphSpace">The inter glyph space in pixels.</param>
		/// <param name="barMinHeight">Minimum bar height in pixels.</param>
		/// <param name="barMinWidth">Small bar width in pixels.</param>
		/// <param name="barMaxWidth">Large bar width in pixels.</param>
		/// <remarks>
		/// By default this method renders each glyph by calling the
		/// <see cref="M:RenderBar"/> method, applying the specified
		/// inter-glyph spacing as necessary.
		/// </remarks>
		protected override void RenderBars(Glyph[] barcode, Graphics dc, Rectangle bounds, int interGlyphSpace, int barMinHeight, int barMinWidth, int barMaxWidth)
		{
			// Standard Code 2 of 5 can be rendered via base class
			if (!(Factory is Code25InterleavedGlyphFactory))
			{
				base.RenderBars(barcode, dc, bounds, interGlyphSpace, barMinHeight, barMinWidth, barMaxWidth);
				return;
			}

			// Interleaved version needs custom rendering support

			int barOffset = 0;
			for (int index = 0; index < barcode.Length; )
			{
				BarGlyph glyph = (BarGlyph)barcode[index];
				int height = GetGlyphHeight(glyph, barMinHeight, bounds.Height);

				if (index == 0 || index == (barcode.Length - 1))
				{
					RenderBar(index, glyph, dc, bounds, ref barOffset, barMinHeight,
						barMinWidth, barMaxWidth);
					++index;
				}
				else
				{
					// We need to render two glyphs at the same time
					int encodingBitCount = 5;
					BarGlyph firstGlyph = (BarGlyph)barcode[index];
					BarGlyph secondGlyph = (BarGlyph)barcode[index +1];

					// Get the bit encoding for each glyph

					for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
					{
						int bitMask = (1 << bitIndex);
						if ((firstGlyph.BitEncoding & bitMask) != 0)
						{
							dc.FillRectangle(Brushes.Black, barOffset, bounds.Top,
								barMinWidth * 2, height);
							barOffset += barMinWidth * 2;
						}
						else
						{
							dc.FillRectangle(Brushes.Black, barOffset, bounds.Top,
								barMinWidth, height);
							barOffset += barMinWidth;
						}

						if ((secondGlyph.BitEncoding & bitMask) != 0)
						{
							barOffset += barMinWidth * 2;
						}
						else
						{
							barOffset += barMinWidth;
						}
					}

					// Advance to next pair
					index += 2;
				}

				// Account for inter glyph spacing
				barOffset += interGlyphSpace;
			}
			
		}
		#endregion
	}
}
