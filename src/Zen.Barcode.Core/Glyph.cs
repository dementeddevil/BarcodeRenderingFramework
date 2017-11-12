//-----------------------------------------------------------------------
// <copyright file="Glyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// <b>Glyph</b> represents the base class for describing a barcode
    /// character.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In almost all cases barcode symbologies should not be derived directly
    /// from this class but instead from the fixed pitch or variable pitch
    /// derived classes which contain functionality to control bar width.
    /// </para>
    /// <para>
    /// The glyph classes are typically flyweight objects containing immutable
    /// state making them suitable for distribution from static class factories
    /// - aka symbology factories.
    /// </para>
    /// </remarks>
    public class Glyph : IGlyph
    {
        private char _character;

        /// <summary>
        /// Initialises a new instance of the <see cref="T:Zen.Barcode.Glyph"/>
        /// class with the specified bit encoding.
        /// </summary>
        /// <param name="character">Character represented by glyph.</param>
        public Glyph(char character)
        {
            _character = character;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Char"/> character associated with this glyph.
        /// </summary>
        /// <value>The character.</value>
        public char Character
        {
            get
            {
                return _character;
            }
        }
    }
}
