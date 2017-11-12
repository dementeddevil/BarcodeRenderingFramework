//-----------------------------------------------------------------------
// <copyright file="MultisetGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// <b>MultisetGlyph</b> serves as a base class for barcode symbologies
    /// where a given encoding is reused by multiple characters.
    /// </summary>
    public class MultisetGlyph : BarGlyph
    {
        #region Private Fields
        private char[] _characters;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="T:Zen.Barcode.MultisetGlyph"/>
        /// class.
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="bitEncoding"></param>
        public MultisetGlyph(char[] characters, short bitEncoding)
            : base(characters[0], bitEncoding)
        {
            _characters = characters;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets an <see cref="T:System.Int32"/> that represents the number of
        /// character sets encoded in this glyph.
        /// </summary>
        public int Sets
        {
            get
            {
                return _characters.Length;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the character representation for the specified character set.
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public char GetCharacterBySet(int set)
        {
            if (set < 0 || set >= _characters.Length)
            {
                throw new ArgumentOutOfRangeException("set", set,
                    "set out of range.");
            }
            return _characters[set];
        }
        #endregion
    }
}
