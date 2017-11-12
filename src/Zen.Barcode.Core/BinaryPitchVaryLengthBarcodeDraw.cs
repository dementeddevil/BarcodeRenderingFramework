//-----------------------------------------------------------------------
// <copyright file="BinaryPitchVaryLengthBarcodeDraw.cs" company="Zen Design Corp">
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
    /// <b>BinaryPitchVaryLengthBarcodeDraw</b> serves as a base class for
    /// all barcode symbologies using two pitch representation of bars with
    /// variable length encoding in each glyph.
    /// </summary>
    /// <typeparam name="TGlyphFactory">The glyph factory type derived from <see cref="T:GlyphFactory"/>.</typeparam>
    /// <typeparam name="TChecksum">The checksum type derived from <see cref="T:Checksum"/>.</typeparam>
    public abstract class BinaryPitchVaryLengthBarcodeDraw<TGlyphFactory, TChecksum>
        : BinaryPitchBarcodeDraw<TGlyphFactory, TChecksum>
        where TGlyphFactory : GlyphFactory
        where TChecksum : Checksum
    {
        #region Protected Constructors
        /// <summary>
        /// Initialises an instance of <see cref="T:BarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        protected BinaryPitchVaryLengthBarcodeDraw(TGlyphFactory factory, int encodingBitCount)
            : base(factory, encodingBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchVaryLengthBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        /// <param name="widthBitCount">Width of the width bit.</param>
        protected BinaryPitchVaryLengthBarcodeDraw(TGlyphFactory factory, int encodingBitCount,
            int widthBitCount)
            : base(factory, encodingBitCount, widthBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchVaryLengthBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        protected BinaryPitchVaryLengthBarcodeDraw(TGlyphFactory factory, TChecksum checksum,
            int encodingBitCount)
            : base(factory, checksum, encodingBitCount)
        {
        }

        /// <summary>
        /// Initialises an instance of <see cref="T:BinaryPitchVaryLengthBarcodeDraw"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="encodingBitCount">
        /// Number of bits in each encoded glyph.
        /// Set to <c>0</c> for variable length bit encoded glyphs.
        /// </param>
        /// <param name="widthBitCount">Width of the width bit.</param>
        protected BinaryPitchVaryLengthBarcodeDraw(TGlyphFactory factory, TChecksum checksum,
            int encodingBitCount, int widthBitCount)
            : base(factory, checksum, encodingBitCount, widthBitCount)
        {
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Overridden. Gets the glyph's encoding bit width.
        /// </summary>
        /// <param name="glyph">The <see cref="T:Glyph"/> to be queried.</param>
        /// <returns>Number of bits used to encode the glyph.</returns>
        /// <exception cref="T:InvalidCastException">
        /// If the passed glyph is not derived from <see cref="T:BinaryPitchVaryLengthGlyph"/>.
        /// </exception>
        protected override int GetGlyphEncodingBitCount(Glyph glyph)
        {
            return ((BinaryPitchVaryLengthGlyph) glyph).BitEncodingWidth;
        }
        #endregion
    }
}
