//-----------------------------------------------------------------------
// <copyright file="IVaryLengthGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// <c>IVaryLengthGlyph</c> identifies a glyph object as supporting a 
    /// custom bit encoding width
    /// </summary>
    public interface IVaryLengthGlyph : IBarGlyph
    {
        /// <summary>
        /// Gets the number of bits used for the bit encoding.
        /// </summary>
        /// <value>The width encoding.</value>
        short BitEncodingWidth
        {
            get;
        }
    }
}
