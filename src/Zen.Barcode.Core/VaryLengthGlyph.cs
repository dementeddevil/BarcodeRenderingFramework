//-----------------------------------------------------------------------
// <copyright file="VaryLengthGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// <c>VaryLengthGlyph</c> serves as a base class for variable length
    /// fixed-pitch bar-code glyphs.
    /// </summary>
    public class VaryLengthGlyph : BarGlyph, IVaryLengthGlyph
    {
        #region Private Fields
        private short _bitEncodingWidth;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Zen.Barcode.VaryLengthGlyph"/>
        /// class.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="bitEncoding">The bit encoding.</param>
        /// <param name="bitEncodingWidth">Width of the bit encoding.</param>
        public VaryLengthGlyph(char character, short bitEncoding,
            short bitEncodingWidth)
            : base(character, bitEncoding)
        {
            _bitEncodingWidth = bitEncodingWidth;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the number of bits used for the bit encoding.
        /// </summary>
        /// <value>The width encoding.</value>
        public short BitEncodingWidth
        {
            get
            {
                return _bitEncodingWidth;
            }
        }
        #endregion
    }
}
