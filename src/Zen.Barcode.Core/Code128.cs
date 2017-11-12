//-----------------------------------------------------------------------
// <copyright file="Code128.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Zen.Barcode
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	/// <summary>
	/// <b>Code128Glyph</b> extends <see cref="T:MultisetGlyph"/> by defining
	/// the three Code128 barcode sections.
	/// </summary>
	public class Code128Glyph : MultisetGlyph
	{
		#region Private Fields
		private Code128SpecialGlyph[] _special;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Code128Glyph"/> class.
		/// </summary>
		/// <param name="first">The first set character.</param>
		/// <param name="second">The second set character.</param>
		/// <param name="third">The third set character.</param>
		/// <param name="bitEncoding">The glyph bit encoding.</param>
		public Code128Glyph(char first, char second, char third, short bitEncoding)
			: base(new char[] { first, second, third }, bitEncoding)
		{
			_special = new Code128SpecialGlyph[] 
				{ 
					Code128SpecialGlyph.None,
					Code128SpecialGlyph.None,
					Code128SpecialGlyph.None
				};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Code128Glyph"/> class.
		/// </summary>
		/// <param name="first">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the first set object.</param>
		/// <param name="second">The second set character.</param>
		/// <param name="third">The third set character.</param>
		/// <param name="bitEncoding">The glyph bit encoding.</param>
		public Code128Glyph(Code128SpecialGlyph first, char second, char third, short bitEncoding)
			: base(new char[] { (char)0, second, third }, bitEncoding)
		{
			if (first == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("first cannot be None.");
			}
			_special = new Code128SpecialGlyph[] 
				{ 
					first,
					Code128SpecialGlyph.None,
					Code128SpecialGlyph.None
				};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Code128Glyph"/> class.
		/// </summary>
		/// <param name="first">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the first set object.</param>
		/// <param name="second">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the second set object.</param>
		/// <param name="third">The third set character.</param>
		/// <param name="bitEncoding">The glyph bit encoding.</param>
		public Code128Glyph(Code128SpecialGlyph first, Code128SpecialGlyph second, char third, short bitEncoding)
			: base(new char[] { (char)0, (char)0, third }, bitEncoding)
		{
			if (first == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("first cannot be None.");
			}
			if (second == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("second cannot be None.");
			}
			_special = new Code128SpecialGlyph[] 
				{ 
					first,
					second,
					Code128SpecialGlyph.None
				};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Code128Glyph"/> class.
		/// </summary>
		/// <param name="first">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the first set object.</param>
		/// <param name="second">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the second set object.</param>
		/// <param name="third">A <see cref="T:Code128SpecialGlyph"/> 
		/// representing the third set object.</param>
		/// <param name="bitEncoding">The glyph bit encoding.</param>
		public Code128Glyph(Code128SpecialGlyph first, Code128SpecialGlyph second, Code128SpecialGlyph third, short bitEncoding)
			: base(new char[] { (char)0, (char)0, (char)0 }, bitEncoding)
		{
			if (first == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("first cannot be None.");
			}
			if (second == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("second cannot be None.");
			}
			if (third == Code128SpecialGlyph.None)
			{
				throw new ArgumentException("third cannot be None.");
			}
			_special = new Code128SpecialGlyph[] 
				{ 
					first,
					second,
					third
				};
		}
		#endregion

		#region Public Properties

		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the <see cref="T:Code128SpecialGlyph"/> glyph by set.
		/// </summary>
		/// <param name="set">The set.</param>
		/// <returns></returns>
		public Code128SpecialGlyph GetSpecialBySet(int set)
		{
			if (set < 0 || set >= _special.Length)
			{
				throw new ArgumentOutOfRangeException("set", set,
					"set out of range.");
			}
			return _special[set];
		}
		#endregion
	}

	/// <summary>
	/// Defines special control codes which have no character representation.
	/// </summary>
	public enum Code128SpecialGlyph
	{
		/// <summary>
		/// Not special
		/// </summary>
		None,

		/// <summary>
		/// Start of character set A
		/// </summary>
		StartSetA,

		/// <summary>
		/// Start of character set B
		/// </summary>
		StartSetB,

		/// <summary>
		/// Start of character set C
		/// </summary>
		StartSetC,

		/// <summary>
		/// Switch to character set A
		/// </summary>
		SwitchToA,

		/// <summary>
		/// Switch to character set B
		/// </summary>
		SwitchToB,

		/// <summary>
		/// Switch to character set C
		/// </summary>
		SwitchToC,

		/// <summary>
		/// Function 1
		/// </summary>
		Func1,

		/// <summary>
		/// Function 2
		/// </summary>
		Func2,

		/// <summary>
		/// Function 3
		/// </summary>
		Func3,

		/// <summary>
		/// Function 4
		/// </summary>
		Func4,

		/// <summary>
		/// Shift
		/// </summary>
		Shift,

		/// <summary>
		/// Stop
		/// </summary>
		Stop,

		/// <summary>
		/// Terminal
		/// </summary>
		Terminal
	}

	/// <summary>
	/// <b>Code128GlyphFactory</b> concrete implementation of 
	/// <see cref="GlyphFactory"/> for providing Code 128 bar-code glyph
	/// objects.
	/// </summary>
	public class Code128GlyphFactory : GlyphFactory
	{
		#region Private Fields
		private static Code128GlyphFactory _theFactory;
		private static object _syncFactory = new object();

		private Code128Glyph[] _glyphs;
		#endregion

		#region Private Constructors
		private Code128GlyphFactory()
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the global instance.
		/// </summary>
		/// <value>The instance.</value>
		public static Code128GlyphFactory Instance
		{
			get
			{
				if (_theFactory == null)
				{
					lock (_syncFactory)
					{
						if (_theFactory == null)
						{
							_theFactory = new Code128GlyphFactory();
						}
					}
				}
				return _theFactory;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the array of <see cref="T:Glyph"/> objects that correspond
		/// to the specified string.
		/// </summary>
		/// <param name="text">A <see cref="T:String"/> containing text to be
		/// translated.</param>
		/// <param name="allowComposite">if set to <c>true</c> to allow
		/// composite glyphs to be returned.</param>
		/// <returns>
		/// A collection of <see cref="T:Glyph"/> objects.
		/// </returns>
		public override Glyph[] GetGlyphs(string text, bool allowComposite)
		{
			if (allowComposite)
			{
				throw new NotSupportedException(
					"Return of composite glyphs not supported.");
			}

			List<Glyph> list = new List<Glyph>();
			while (!string.IsNullOrEmpty(text))
			{
				// Get next sub-block in a character set.
				int set;
				string subBlock;
				text = GetSetBlock(text, out set, out subBlock);

				// Add start or change glyph
				if (list.Count == 0)
				{
					switch (set)
					{
						case 0:
							list.Add(_glyphs[103]);
							break;
						case 1:
							list.Add(_glyphs[104]);
							break;
						case 2:
							list.Add(_glyphs[105]);
							break;
					}
				}
				else
				{
					switch (set)
					{
						case 0:
							list.Add(_glyphs[101]);
							break;
						case 1:
							list.Add(_glyphs[100]);
							break;
						case 2:
							list.Add(_glyphs[99]);
							break;
					}
				}

				// Get glyphs according to char-set
				for (int charIndex = 0; charIndex < subBlock.Length; ++charIndex)
				{
					// Get some helpful bits and pieces
					bool lastChar = (charIndex == (subBlock.Length - 1));
					char character = subBlock[charIndex];

					// Detect when dealing with numeric sequence and attempt
					//	to encode digits using double density encoding scheme
					int glyphIndex = -1;
					if (set == 2 && char.IsDigit(character))
					{
						if (!lastChar && char.IsDigit(subBlock[charIndex + 1]))
						{
							glyphIndex = Int32.Parse(subBlock.Substring(charIndex, 2));
							++charIndex;
						}
						else
						{
							glyphIndex = Int32.Parse(subBlock.Substring(charIndex, 1));
						}
					}

					// Everything else using the standard char-to-glyph lookup
					else
					{
						glyphIndex = GetSetIndex(set, character);
					}
					System.Diagnostics.Debug.Assert(glyphIndex != -1);
					list.Add(_glyphs[glyphIndex]);
				}
			}
			return list.ToArray();
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:BarGlyph"/> that
		/// represent the raw bar-code glyphs for the given bar-code symbology.
		/// </summary>
		/// <returns>
		/// The full collection of <see cref="T:BarGlyph"/> objects
		/// associated with this symbology.
		/// </returns>
		protected override BarGlyph[] GetGlyphs()
		{
			if (_glyphs == null)
			{
				_glyphs = new Code128Glyph[]
				{
					new Code128Glyph (' ', ' ', (char)0, 0x6CC),
					new Code128Glyph ('!', '!', (char)1, 0x66C),
					new Code128Glyph ('\"', '\"', (char)2, 0x666),
					new Code128Glyph ('#', '#', (char)3, 0x498),
					new Code128Glyph ('$', '$', (char)4, 0x48C),
					new Code128Glyph ('%', '%', (char)5, 0x44C),
					new Code128Glyph ('&', '&', (char)6, 0x4C8),
					new Code128Glyph ('\'', '\'', (char)7, 0x4C4),
					new Code128Glyph ('(', '(', (char)8, 0x464),
					new Code128Glyph (')', ')', (char)9, 0x648),
					new Code128Glyph ('*', '*', (char)10, 0x644),
					new Code128Glyph ('+', '+', (char)11, 0x624),
					new Code128Glyph (',', ',', (char)12, 0x59C),
					new Code128Glyph ('-', '-', (char)13, 0x4DC),
					new Code128Glyph ('.', '.', (char)14, 0x4CE),
					new Code128Glyph ('/', '/', (char)15, 0x5CC),
					new Code128Glyph ('0', '0', (char)16, 0x4EC),
					new Code128Glyph ('1', '1', (char)17, 0x4E6),
					new Code128Glyph ('2', '2', (char)18, 0x672),
					new Code128Glyph ('3', '3', (char)19, 0x65C),
					new Code128Glyph ('4', '4', (char)20, 0x64E),
					new Code128Glyph ('5', '5', (char)21, 0x6E4),
					new Code128Glyph ('6', '6', (char)22, 0x674),
					new Code128Glyph ('7', '7', (char)23, 0x76E),
					new Code128Glyph ('8', '8', (char)24, 0x74C),
					new Code128Glyph ('9', '9', (char)25, 0x72C),
					new Code128Glyph (':', ':', (char)26, 0x726),
					new Code128Glyph (';', ';', (char)27, 0x764),
					new Code128Glyph ('<', '<', (char)28, 0x734),
					new Code128Glyph ('=', '=', (char)29, 0x732),
					new Code128Glyph ('>', '>', (char)30, 0x6D8),
					new Code128Glyph ('?', '?', (char)31, 0x6C6),
					new Code128Glyph ('@', '@', (char)32, 0x636),
					new Code128Glyph ('A', 'A', (char)33, 0x518),
					new Code128Glyph ('B', 'B', (char)34, 0x458),
					new Code128Glyph ('C', 'C', (char)35, 0x446),
					new Code128Glyph ('D', 'D', (char)36, 0x588),
					new Code128Glyph ('E', 'E', (char)37, 0x468),
					new Code128Glyph ('F', 'F', (char)38, 0x462),
					new Code128Glyph ('G', 'G', (char)39, 0x688),
					new Code128Glyph ('H', 'H', (char)40, 0x628),
					new Code128Glyph ('I', 'I', (char)41, 0x622),
					new Code128Glyph ('J', 'J', (char)42, 0x5B8),
					new Code128Glyph ('K', 'K', (char)43, 0x58E),
					new Code128Glyph ('L', 'L', (char)44, 0x46E),
					new Code128Glyph ('M', 'M', (char)45, 0x5D8),
					new Code128Glyph ('N', 'N', (char)46, 0x5C6),
					new Code128Glyph ('O', 'O', (char)47, 0x476),
					new Code128Glyph ('P', 'P', (char)48, 0x776),
					new Code128Glyph ('Q', 'Q', (char)49, 0x68E),
					new Code128Glyph ('R', 'R', (char)50, 0x62E),
					new Code128Glyph ('S', 'S', (char)51, 0x6E8),
					new Code128Glyph ('T', 'T', (char)52, 0x6E2),
					new Code128Glyph ('U', 'U', (char)53, 0x6EE),
					new Code128Glyph ('V', 'V', (char)54, 0x758),
					new Code128Glyph ('W', 'W', (char)55, 0x746),
					new Code128Glyph ('X', 'X', (char)56, 0x716),
					new Code128Glyph ('Y', 'Y', (char)57, 0x768),
					new Code128Glyph ('Z', 'Z', (char)58, 0x762),
					new Code128Glyph ('[', '[', (char)59, 0x71A),
					new Code128Glyph ('\\', '\\', (char)60, 0x77A),
					new Code128Glyph (']', ']', (char)61, 0x642),
					new Code128Glyph ('^', '^', (char)62, 0x78A),
					new Code128Glyph ('_', '_', (char)63, 0x530),
					new Code128Glyph ((char) 0, '`', (char)64, 0x50C),
					new Code128Glyph ((char) 1, 'a', (char)65, 0x4B0),
					new Code128Glyph ((char) 2, 'b', (char)66, 0x486),
					new Code128Glyph ((char) 3, 'c', (char)67, 0x42C),
					new Code128Glyph ((char) 4, 'd', (char)68, 0x426),
					new Code128Glyph ((char) 5, 'e', (char)69, 0x590),
					new Code128Glyph ((char) 6, 'f', (char)70, 0x584),
					new Code128Glyph ((char) 7, 'g', (char)71, 0x4D0),
					new Code128Glyph ((char) 8, 'h', (char)72, 0x4C2),
					new Code128Glyph ((char) 9, 'i', (char)73, 0x434),
					new Code128Glyph ((char) 10, 'j', (char)74, 0x432),
					new Code128Glyph ((char) 11, 'k', (char)75, 0x612),
					new Code128Glyph ((char) 12, 'l', (char)76, 0x650),
					new Code128Glyph ((char) 13, 'm', (char)77, 0x7BA),
					new Code128Glyph ((char) 14, 'n', (char)78, 0x614),
					new Code128Glyph ((char) 15, 'o', (char)79, 0x47A),
					new Code128Glyph ((char) 16, 'p', (char)80, 0x53C),
					new Code128Glyph ((char) 17, 'q', (char)81, 0x4BC),
					new Code128Glyph ((char) 18, 'r', (char)82, 0x49E),
					new Code128Glyph ((char) 19, 's', (char)83, 0x5E4),
					new Code128Glyph ((char) 20, 't', (char)84, 0x4F4),
					new Code128Glyph ((char) 21, 'u', (char)85, 0x4F2),
					new Code128Glyph ((char) 22, 'v', (char)86, 0x7A4),
					new Code128Glyph ((char) 23, 'w', (char)87, 0x794),
					new Code128Glyph ((char) 24, 'x', (char)88, 0x792),
					new Code128Glyph ((char) 25, 'y', (char)89, 0x6DE),
					new Code128Glyph ((char) 26, 'z', (char)90, 0x6F6),
					new Code128Glyph ((char) 27, '{', (char)91, 0x7B6),
					new Code128Glyph ((char) 28, '|', (char)92, 0x578),
					new Code128Glyph ((char) 29, '}', (char)93, 0x51E),
					new Code128Glyph ((char) 30, '~', (char)94, 0x45E),
					new Code128Glyph ((char) 31, (char) 127, (char)95, 0x5E8),
					new Code128Glyph (Code128SpecialGlyph.Func3, Code128SpecialGlyph.Func3, (char)96, 0x5E2),
					new Code128Glyph (Code128SpecialGlyph.Func2, Code128SpecialGlyph.Func2, (char)97, 0x7A8),
					new Code128Glyph (Code128SpecialGlyph.Shift, Code128SpecialGlyph.Shift, (char)98, 0x7A2),
					new Code128Glyph (Code128SpecialGlyph.SwitchToC, Code128SpecialGlyph.SwitchToC, (char)99, 0x5DE),
					new Code128Glyph (Code128SpecialGlyph.SwitchToB, Code128SpecialGlyph.Func4, Code128SpecialGlyph.SwitchToB, 0x5EE),
					new Code128Glyph (Code128SpecialGlyph.Func4, Code128SpecialGlyph.SwitchToA, Code128SpecialGlyph.SwitchToA, 0x75E),
					new Code128Glyph (Code128SpecialGlyph.Func1, Code128SpecialGlyph.Func1, Code128SpecialGlyph.Func1, 0x7AE),
					new Code128Glyph (Code128SpecialGlyph.StartSetA, Code128SpecialGlyph.StartSetA, Code128SpecialGlyph.StartSetA, 0x684),
					new Code128Glyph (Code128SpecialGlyph.StartSetB, Code128SpecialGlyph.StartSetB, Code128SpecialGlyph.StartSetB, 0x690),
					new Code128Glyph (Code128SpecialGlyph.StartSetC, Code128SpecialGlyph.StartSetC, Code128SpecialGlyph.StartSetC, 0x69C),
					new Code128Glyph (Code128SpecialGlyph.Stop, Code128SpecialGlyph.Stop, Code128SpecialGlyph.Stop, 0x63A),
					new Code128Glyph (Code128SpecialGlyph.Terminal, Code128SpecialGlyph.Terminal, Code128SpecialGlyph.Terminal, 0x600)
				};
			}
			return _glyphs;
		}

		/// <summary>
		/// Overridden. Gets the collection of <see cref="T:CompositeGlyph"/>
		/// that represent the composite bar-code glyphs for the given bar-code
		/// symbology.
		/// </summary>
		/// <returns>
		/// The full collection of <see cref="T:CompositeGlyph"/>
		/// objects associated with this symbology.
		/// </returns>
		protected override CompositeGlyph[] GetCompositeGlyphs()
		{
			return new CompositeGlyph[0];
		}
		#endregion

		#region Internal Methods
		internal BarGlyph[] GetGlyphArray()
		{
			return GetGlyphs();
		}
		#endregion

		#region Private Methods
		private enum BlockParserState
		{
			Free = 0,
			UseSetA = 1,
			UseUpgradableSetA = 2,
			UseSetB = 3,
			UseSetC = 4
		}

		private bool IsInSet(int set, char character)
		{
			return (GetSetIndex(set, character, Code128SpecialGlyph.None) != -1);
		}

		private bool IsInSet(int set, Code128SpecialGlyph special)
		{
			return (GetSetIndex(set, (char)0, special) != -1);
		}

		private int GetSetIndex(int set, char character)
		{
			return GetSetIndex(set, character, Code128SpecialGlyph.None);
		}

		private int GetSetIndex(int set, Code128SpecialGlyph special)
		{
			return GetSetIndex(set, (char)0, special);
		}

		private int GetSetIndex(int set, char character, Code128SpecialGlyph special)
		{
			GetGlyphs();
			for (int index = 0; index < _glyphs.Length; ++index)
			{
				if ((special != Code128SpecialGlyph.None &&
					_glyphs[index].GetSpecialBySet(set) == special) ||
					(special == Code128SpecialGlyph.None &&
					_glyphs[index].GetCharacterBySet(set) == character))
				{
					return index;
				}
			}
			return -1;
		}

		private string GetSetBlock(string text, out int set, out string subBlock)
		{
			// Needed to ensure glyphs allocated.
			GetGlyphs();

			BlockParserState state = BlockParserState.Free;
			int index;
			for (index = 0; index < text.Length; ++index)
			{
				// Check for Set C
				// NOTE: This check eliminates two digits per iteration
				if (char.IsDigit(text[index]) &&
					index < (text.Length - 1) &&
					char.IsDigit(text[index + 1]))
				{
					if (state == BlockParserState.Free)
					{
						state = BlockParserState.UseSetC;
						++index;
						continue;
					}
					else if (state == BlockParserState.UseSetC)
					{
						++index;
						continue;
					}
				}

				// Check for Set A
				else if (IsInSet(0, text[index]))
				{
					if (state == BlockParserState.Free)
					{
						if (IsInSet(1, text[index]))
						{
							state = BlockParserState.UseUpgradableSetA;
						}
						else
						{
							state = BlockParserState.UseSetA;
						}
						continue;
					}
					if (state == BlockParserState.UseSetA)
					{
						continue;
					}
					if (state == BlockParserState.UseUpgradableSetA)
					{
						if (!IsInSet(1, text[index]))
						{
							state = BlockParserState.UseSetA;
						}
						continue;
					}
				}

				// Check for Set B
				else if (IsInSet(1, text[index]))
				{
					if (state == BlockParserState.Free ||
						state == BlockParserState.UseUpgradableSetA)
					{
						state = BlockParserState.UseSetB;
						continue;
					}
					else if (state == BlockParserState.UseSetB)
					{
						continue;
					}
				}

				// If we reach this point we need to chop and return
				break;
			}

			// Must have something...
			System.Diagnostics.Debug.Assert(index > 0);
			subBlock = text.Substring(0, index);
			switch (state)
			{
				case BlockParserState.UseSetA:
				case BlockParserState.UseUpgradableSetA:
					set = 0;
					break;

				case BlockParserState.UseSetB:
					set = 1;
					break;

				case BlockParserState.UseSetC:
					set = 2;
					break;

				default:
					throw new InvalidOperationException("Invalid state.");
			}

			// Return remaining text
			return text.Substring(index);
		}
		#endregion
	}

	/// <summary>
	/// <b>Code128Checksum</b> implements a Code128 checksum generator as a
	/// singleton class.
	/// </summary>
	/// <remarks>
	/// This class cannot be inherited from.
	/// </remarks>
	public sealed class Code128Checksum : FactoryChecksum<Code128GlyphFactory>
	{
		#region Private Fields
		private static Code128Checksum _theChecksum;
		private static object _syncChecksum = new object();
		#endregion

		#region Private Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="T:Code128Checksum"/> class.
		/// </summary>
		private Code128Checksum()
			: base(Code128GlyphFactory.Instance)
		{
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the static <see cref="T:Code128Checksum"/> object instance.
		/// </summary>
		public static Code128Checksum Instance
		{
			get
			{
				if (_theChecksum == null)
				{
					lock (_syncChecksum)
					{
						if (_theChecksum == null)
						{
							_theChecksum = new Code128Checksum();
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
			Glyph[] fullGlyph = Factory.GetGlyphs(text, false);
			long checksum = Factory.GetRawGlyphIndex((BarGlyph)(fullGlyph[0]));
			for (int index = 1; index < fullGlyph.Length; ++index)
			{
				checksum += (index * Factory.GetRawGlyphIndex(
					((BarGlyph)(fullGlyph[index]))));
			}
			checksum = checksum % 103;
			return new Glyph[] { Factory.GetRawGlyph((int)checksum) };
		}
		#endregion

		#region Private Methods
		private char GetChecksumChar(string text, int weighting)
		{
			int weightedSum = 0;
			for (int index = 0; index < text.Length; ++index)
			{
				int checkValue = Factory.GetRawCharIndex(text[text.Length - index - 1]);
				int weight = (index % weighting) + 1;
				weightedSum += (weight * checkValue);
			}
			return Factory.GetRawGlyph(weightedSum % 47).Character;
		}
		#endregion
	}

	/// <summary>
	/// <b>Code128BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
	/// that can render complete Code128 barcodes with checksum.
	/// </summary>
	public class Code128BarcodeDraw
		: BarcodeDrawBase<Code128GlyphFactory, Code128Checksum>
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Code128BarcodeDraw"/>
		/// class.
		/// </summary>
		/// <param name="checksum">The checksum.</param>
		public Code128BarcodeDraw(Code128Checksum checksum)
			: base(checksum.Factory, checksum, 11)
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
			return new BarcodeMetrics1d(1, maxHeight);
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
			return new BarcodeMetrics1d(narrowBarWidth, maxHeight);
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
			List<Glyph> result = new List<Glyph>();
			result.AddRange(Factory.GetGlyphs(text));
			if (Checksum != null)
			{
				result.AddRange(Checksum.GetChecksum(text));
			}
			result.Add(Factory.GetRawGlyph(106));	// Stop
			result.Add(Factory.GetRawGlyph(107));	// Terminator
			return result.ToArray();
		}

		/// <summary>
		/// Overridden. Gets the length in pixels needed to render the 
		/// specified barcode.
		/// </summary>
		/// <param name="barcode">Barcode glyphs to be analysed.</param>
		/// <param name="interGlyphSpace">Amount of inter-glyph space.</param>
		/// <param name="barMinWidth">Minimum bar width.</param>
		/// <param name="barMaxWidth">Maximum bar width.</param>
		/// <returns></returns>
		protected override int GetBarcodeLength(Glyph[] barcode,
			int interGlyphSpace, int barMinWidth, int barMaxWidth)
		{
			return base.GetBarcodeLength(barcode, interGlyphSpace,
				barMinWidth, barMaxWidth) - (9 * barMinWidth);
		}
		#endregion
	}
}
