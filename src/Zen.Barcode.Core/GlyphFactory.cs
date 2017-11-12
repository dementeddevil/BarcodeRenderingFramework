//-----------------------------------------------------------------------
// <copyright file="GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Abstract class factory for retrieval of barcode glyph objects.
    /// </summary>
    /// <remarks>
    /// The <see cref="M:GetGlyphs"/> and <see cref="M:GetCompositeGlyphs"/>
    /// methods must be implemented in derived classes.
    /// </remarks>
    public abstract class GlyphFactory
    {
        #region Private Fields
        private Dictionary<char, BarGlyph> _rawLookup;
        private Dictionary<char, Glyph> _lookup;
        #endregion

        #region Protected Constructors
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:Zen.Barcode.GlyphFactory"/> class.
        /// </summary>
        protected GlyphFactory()
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the ordinal position for the specified character.
        /// </summary>
        /// <param name="character">The character to be located.</param>
        /// <returns>An <see cref="T:System.Int32"/> representing the character 
        /// ordinal index.</returns>
        public virtual int GetRawCharIndex(char character)
        {
            int index = 0;
            foreach (BarGlyph glyph in GetGlyphs())
            {
                if (glyph.Character == character)
                {
                    return index;
                }
                ++index;
            }
            throw new ArgumentException("Invalid character.");
        }

        /// <summary>
        /// Gets the <see cref="T:Zen.Barcode.BarGlyph"/> at the specified
        /// ordinal index.
        /// </summary>
        /// <param name="index">The glyph ordinal index.</param>
        /// <returns>
        /// A <see cref="T:Zen.Barcode.BarGlyph"/> glyph object.
        /// </returns>
        public virtual BarGlyph GetRawGlyph(int index)
        {
            return GetGlyphs()[index];
        }

        /// <summary>
        /// Gets the <see cref="T:Zen.Barcode.BarGlyph"/> that corresponds
        /// to the specified character.
        /// </summary>
        /// <param name="character">The character to be located.</param>
        /// <returns>
        /// A <see cref="T:Zen.Barcode.BarGlyph"/> glyph object.
        /// </returns>
        public virtual BarGlyph GetRawGlyph(char character)
        {
            EnsureRawGlyphLookup();
            return _rawLookup[character];
        }

        /// <summary>
        /// Gets the index of this glyph.
        /// </summary>
        /// <param name="glyph">
        /// The <see cref="T:Zen.Barcode.BarGlyph"/> to be located.
        /// </param>
        /// <returns>
        /// The ordinal index of the glyph or -1 if not found.
        /// </returns>
        public virtual int GetRawGlyphIndex(BarGlyph glyph)
        {
            int foundIndex = -1;
            BarGlyph[] glyphs = GetGlyphs();
            for (int index = 0; index < glyphs.Length; ++index)
            {
                if (glyphs[index] == glyph)
                {
                    foundIndex = index;
                    break;
                }
            }
            return foundIndex;
        }

        /// <summary>
        /// Gets the array of <see cref="T:Zen.Barcode.Glyph"/> objects that
        /// correspond to the specified character.
        /// </summary>
        /// <param name="character">The character to be translated.</param>
        /// <returns>
        /// A collection of <see cref="T:Zen.Barcode.Glyph"/> objects.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method may return more than one glyph depending on the
        /// underlying encoding mechanism being used.
        /// </para>
        /// <para>
        /// This method will never return composite glyphs.
        /// </para>
        /// </remarks>
        public virtual Glyph[] GetGlyphs(char character)
        {
            return GetGlyphs(character, false);
        }

        /// <summary>
        /// Gets the array of <see cref="T:Zen.Barcode.Glyph"/> objects that
        /// correspond to the specified character.
        /// </summary>
        /// <param name="character">The character to be translated.</param>
        /// <param name="allowComposite">if set to <c>true</c> to allow 
        /// composite glyphs to be returned.</param>
        /// <returns>
        /// A collection of <see cref="T:Zen.Barcode.Glyph"/> objects.
        /// </returns>
        /// <remarks>
        /// This method may return more than one glyph depending on the
        /// underlying encoding mechanism being used.
        /// </remarks>
        public virtual Glyph[] GetGlyphs(char character, bool allowComposite)
        {
            EnsureFullLookup();
            Glyph glyph = _lookup[character];
            CompositeGlyph compGlyph = glyph as CompositeGlyph;
            if (compGlyph != null && !allowComposite)
            {
                return new Glyph[] { compGlyph.First, compGlyph.Second };
            }
            else
            {
                return new Glyph[] { glyph };
            }
        }

        /// <summary>
        /// Gets the array of <see cref="T:Zen.Barcode.Glyph"/> objects that
        /// correspond to the specified string.
        /// </summary>
        /// <param name="text">
        /// A <see cref="T:System.String"/> containing text to be translated.
        /// </param>
        /// <returns>
        /// A collection of <see cref="T:Zen.Barcode.Glyph"/> objects.
        /// </returns>
        public virtual Glyph[] GetGlyphs(string text)
        {
            return GetGlyphs(text, false);
        }

        /// <summary>
        /// Gets the array of <see cref="T:Zen.Barcode.Glyph"/> objects that 
        /// correspond to the specified string.
        /// </summary>
        /// <param name="text">
        /// A <see cref="T:System.String"/> containing text to be translated.
        /// </param>
        /// <param name="allowComposite">if set to <c>true</c> to allow 
        /// composite glyphs to be returned.</param>
        /// <returns>
        /// A collection of <see cref="T:Zen.Barcode.Glyph"/> objects.
        /// </returns>
        public virtual Glyph[] GetGlyphs(string text, bool allowComposite)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Glyph[0];
            }

            List<Glyph> glyphs = new List<Glyph>();
            foreach (char character in text)
            {
                glyphs.AddRange(GetGlyphs(character, allowComposite));
            }
            return glyphs.ToArray();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Gets the collection of <see cref="T:Zen.Barcode.BarGlyph"/> that 
        /// represent the raw bar-code glyphs for the given bar-code symbology.
        /// </summary>
        /// <returns>The full collection of <see cref="T:BarGlyph"/> objects
        /// associated with this symbology.</returns>
        protected abstract BarGlyph[] GetGlyphs();

        /// <summary>
        /// Gets the collection of <see cref="T:Zen.Barcode.CompositeGlyph"/>
        /// that represent the composite bar-code glyphs for the given bar-code
        /// symbology.
        /// </summary>
        /// <returns>
        /// The full collection of <see cref="T:Zen.Barcode.CompositeGlyph"/>
        /// objects associated with this symbology.
        /// </returns>
        protected abstract CompositeGlyph[] GetCompositeGlyphs();
        #endregion

        #region Private Methods
        private void EnsureRawGlyphLookup()
        {
            // Raw lookup table is simple.
            if (_rawLookup == null)
            {
                _rawLookup = new Dictionary<char, BarGlyph>();
                BarGlyph[] barGlyphs = GetGlyphs();
                foreach (BarGlyph glyph in barGlyphs)
                {
                    _rawLookup.Add(glyph.Character, glyph);
                }
            }
        }

        private void EnsureFullLookup()
        {
            if (_lookup == null)
            {
                _lookup = new Dictionary<char, Glyph>();
                CompositeGlyph[] glyphs = GetCompositeGlyphs();
                foreach (CompositeGlyph glyph in glyphs)
                {
                    _lookup.Add(glyph.Character, glyph);
                }
                foreach (BarGlyph glyph in GetGlyphs())
                {
                    if (!_lookup.ContainsKey(glyph.Character))
                    {
                        _lookup.Add(glyph.Character, glyph);
                    }
                }
            }
        }
        #endregion
    }
}
