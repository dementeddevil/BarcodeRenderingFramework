//-----------------------------------------------------------------------
// <copyright file="BinaryPitchBarcodeDraw.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Text;

    /// <summary>
    /// <b>BinaryPitchBarcodeDraw</b> is an abstract class that serves as a
    /// base class for rendering all dual-pitch barcode symbologies.
    /// </summary>
    /// <typeparam name="TGlyphFactory">The glyph factory type derived from <see cref="T:GlyphFactory"/>.</typeparam>
    /// <typeparam name="TChecksum">The checksum type derived from <see cref="T:Checksum"/>.</typeparam>
    public abstract class BinaryPitchBarcodeDraw<TGlyphFactory, TChecksum>
        : BarcodeDrawBase<TGlyphFactory, TChecksum>
        where TGlyphFactory : GlyphFactory
        where TChecksum : Checksum
    {
        #region Protected Constructors
        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        protected BinaryPitchBarcodeDraw(TGlyphFactory factory, int encodingBitCount)
            : base(factory, encodingBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        /// <param name="widthBitCount">Width of the width bit.</param>
        protected BinaryPitchBarcodeDraw(TGlyphFactory factory, int encodingBitCount,
            int widthBitCount)
            : base(factory, encodingBitCount, widthBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        protected BinaryPitchBarcodeDraw(TGlyphFactory factory, TChecksum checksum,
            int encodingBitCount)
            : base(factory, checksum, encodingBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        /// <param name="widthBitCount">Width of the width bit.</param>
        protected BinaryPitchBarcodeDraw(TGlyphFactory factory, TChecksum checksum,
            int encodingBitCount, int widthBitCount)
            : base(factory, checksum, encodingBitCount, widthBitCount)
        {
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Overridden. Gets the barcode length.
        /// </summary>
        /// <param name="barcode">Barcode glyphs to be analysed.</param>
        /// <param name="interGlyphSpace">Amount of inter-glyph space.</param>
        /// <param name="barMinWidth">Minimum barcode width.</param>
        /// <param name="barMaxWidth">Maximum barcode width.</param>
        /// <returns></returns>
        protected override int GetBarcodeLength(Glyph[] barcode,
            int interGlyphSpace, int barMinWidth, int barMaxWidth)
        {
            // Sanity check
            if (barMinWidth == barMaxWidth)
            {
                throw new InvalidOperationException("Only variable pitch drawing supported.");
            }

            // Determine bar code length in pixels
            int totalImageWidth = GetBarcodeInterGlyphLength(barcode, interGlyphSpace);
            foreach (BinaryPitchGlyph glyph in barcode)
            {
                // Determine encoding bit-width for this character
                int encodingBitCount = GetGlyphEncodingBitCount(glyph);
                int widthIndex = WidthBitCount - 1;
                bool lastBitState = false;
                for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
                {
                    // Determine whether the bit state is changing
                    int bitmask = (1 << bitIndex);
                    bool currentBitState = false;
                    if ((bitmask & glyph.BitEncoding) != 0)
                    {
                        currentBitState = true;
                    }

                    // Adjust the width bit checker
                    if (bitIndex < (encodingBitCount - 1) &&
                        lastBitState != currentBitState)
                    {
                        --widthIndex;
                    }
                    lastBitState = currentBitState;

                    // Determine width encoding bit mask
                    bitmask = (1 << widthIndex);
                    if ((bitmask & glyph.WidthEncoding) != 0)
                    {
                        totalImageWidth += barMaxWidth;
                    }
                    else
                    {
                        totalImageWidth += barMinWidth;
                    }
                }
            }
            return totalImageWidth;
        }

        /// <summary>
        /// Gets the glyph's encoding bit width.
        /// </summary>
        /// <param name="glyph">The <see cref="T:Glyph"/> to be queried.</param>
        /// <returns>Number of bits used to encode the glyph.</returns>
        /// <remarks>
        /// By default this method returns the underlying encoding bit width.
        /// </remarks>
        protected virtual int GetGlyphEncodingBitCount(Glyph glyph)
        {
            return EncodingBitCount;
        }

		/// <summary>
		/// Overridden. Renders the specified glyph.
		/// </summary>
		/// <param name="glyphIndex">Index of the glyph.</param>
		/// <param name="glyph">A <see cref="T:Glyph"/> to be rendered.</param>
		/// <param name="dc">A <see cref="T:Graphics"/> representing the device context.</param>
		/// <param name="bounds">The bounding rectangle.</param>
		/// <param name="barOffset">The bar offset in pixels.</param>
		/// <param name="barMinHeight">Minimum bar height in pixels.</param>
		/// <param name="barMinWidth">Minimum bar width.</param>
		/// <param name="barMaxWidth">Maximum bar width.</param>
		/// <exception cref="T:InvalidOperationException">
		/// Thrown if the encoding bit count is zero or variable-pitch
		/// bar rendering is attempted.
		/// </exception>
        protected override void RenderBar(int glyphIndex, BarGlyph glyph, Graphics dc,
            Rectangle bounds, ref int barOffset, int barMinHeight,
            int barMinWidth, int barMaxWidth)
        {
            // Sanity check
            BinaryPitchGlyph binGlyph = glyph as BinaryPitchGlyph;
            if (binGlyph == null)
            {
                throw new InvalidOperationException("Glyph must be derived from BinaryPitchGlyph.");
            }
            if (barMinWidth == barMaxWidth)
            {
                throw new InvalidOperationException("Only variable-pitch drawing supported.");
            }
            if (WidthBitCount == 0)
            {
                throw new InvalidOperationException("Must have width bit information.");
            }

            // Get glyph encoding width
            int encodingBitCount = GetGlyphEncodingBitCount(glyph);

            // Render glyph
            int widthIndex = WidthBitCount - 1;
            bool lastBitState = false;
            for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
            {
                int bitMask = 1 << bitIndex;
                int barWidth = barMinWidth;

                bool currentBitState = false;
                if ((bitMask & binGlyph.BitEncoding) != 0)
                {
                    currentBitState = true;
                }

                // Adjust the width bit checker
                if (bitIndex < (encodingBitCount - 1) &&
                    lastBitState != currentBitState)
                {
                    --widthIndex;
                }
                lastBitState = currentBitState;

                // Determine width encoding bit mask
                int widthMask = (1 << widthIndex);
                if ((widthMask & binGlyph.WidthEncoding) != 0)
                {
                    barWidth = barMaxWidth;
                }

                if ((binGlyph.BitEncoding & bitMask) != 0)
                {
                    dc.FillRectangle(Brushes.Black, barOffset, bounds.Top,
                        barWidth, bounds.Height);
                }

                // Update offset
                barOffset += barWidth;
            }
        }
        #endregion
    }
}
